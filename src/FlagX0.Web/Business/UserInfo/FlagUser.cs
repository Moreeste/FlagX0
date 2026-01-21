using Microsoft.AspNetCore.Identity;

namespace FlagX0.Web.Business.UserInfo
{
    public interface IFlagUserDetails
    {
        string UserId { get; }
    }

    public class FlagUserDetails(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager) : IFlagUserDetails
    {
        public string UserId
        {
            get
            {
                return userManager.GetUserId(httpContextAccessor.HttpContext!.User) ?? throw new InvalidOperationException("User is not authenticated.");
            }
        }
    }
}
