using System.Security.Claims;

namespace NotebookStore.Business.Context
{
    public interface IUserContext
    {
        /// <summary>
        /// Get the current user
        /// </summary>
        /// <returns>ClaimsPrincipal</returns>
        public ClaimsPrincipal? GetCurrentUser();
    }
}