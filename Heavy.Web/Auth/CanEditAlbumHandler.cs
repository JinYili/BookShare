using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BookShare.Web.Auth
{
    public class CanEditBookHandler: AuthorizationHandler<QualifiedUserRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            QualifiedUserRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "Edit Book"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
