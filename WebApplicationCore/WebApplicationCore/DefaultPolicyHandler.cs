using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCore
{
    public enum HttpAction : uint
    {
        View = 0,
        Add = 1,
        Edit = 2,
        Delete = 3,
        All = 4
    };
    public class DefaultPolicyRequirement : IAuthorizationRequirement
    {
        public int _actionNum { get; set; }
        public DefaultPolicyRequirement(int actionNum)
        {
            _actionNum = actionNum;
        }
    }
    public class DefaultPolicyHandler : AuthorizationHandler<DefaultPolicyRequirement>
    {
        // private readonly IUserPermissionFactory _userPermissionFactory;

        public DefaultPolicyHandler(/*IUserPermissionFactory userPermissionFactory*/)
        {
            // _userPermissionFactory = userPermissionFactory;
        }



        protected override Task HandleRequirementAsync(AuthorizationHandlerContext authContext, DefaultPolicyRequirement requirement)
        {
            var token = "";
            var isUser = false;
            var authorized = false;
            var appAuthentication = false;
            var authentication = false;
            // Context:
            var authFilterCtx = (Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext)authContext.Resource;
            HttpContext context = authFilterCtx.HttpContext; // Access context here



            return Task.CompletedTask;
        }

    }
}
