using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace NotebookStore.Business.Context
{
    public class HttpUserContext : IUserContext
    {
        private readonly IHttpContextAccessor context;

        public HttpUserContext(IHttpContextAccessor context)
        {
            this.context = context;
        }

        ClaimsPrincipal? IUserContext.GetCurrentUser()
        {
            return context?.HttpContext?.User;
        }
    }
}
