using EPiServer.Personalization.VisitorGroups;
using EPiServer.Security;
using WebEssentials.AspNetCore.OutputCaching;

namespace ElucidLabs.Business
{
    public class OuputCacheVaryByCustomService : IOutputCacheVaryByCustomService
    {
        private readonly IPrincipalAccessor _principalAccessor;
        private readonly IVisitorGroupRepository _visitorGroupRepository;
        private readonly IVisitorGroupRoleRepository _visitorGroupRoleRepository;

        public OuputCacheVaryByCustomService(IPrincipalAccessor principalAccessor,
            IVisitorGroupRepository visitorGroupRepository,
            IVisitorGroupRoleRepository visitorGroupRoleRepository)
        {
            _principalAccessor = principalAccessor;
            _visitorGroupRepository = visitorGroupRepository;
            _visitorGroupRoleRepository = visitorGroupRoleRepository;
        }

        public string GetVaryByCustomString(HttpContext context, string arg)
        {
            if (arg == "visitorgroups")
            {
                return string.Join('|', GetActiveRolesForContext(context));
            }

            return string.Empty;
        }

        private IEnumerable<string> GetActiveRolesForContext(HttpContext context)
        {
            var roleNames = _visitorGroupRepository.List().Select(x => x.Name);

            foreach (var roleName in roleNames)
            {
                if (_visitorGroupRoleRepository.TryGetRole(roleName, out var role))
                {
                    if (role.IsMatch(_principalAccessor.Principal, context))
                    {
                        yield return roleName;
                    }
                }
            }
        }
    }
}
