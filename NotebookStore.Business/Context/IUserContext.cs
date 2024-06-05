using System.Security.Claims;

namespace NotebookStore.Business.Context
{
    public interface IUserContext
    {
        public ClaimsPrincipal? GetCurrentUser();
    }
}