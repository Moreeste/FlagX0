using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;
using System.Security.Claims;

namespace FlagX0.Web.Business.UseCases
{
    public class AddFlagUseCase(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
    {
        public async Task<bool> Execute(string flagName, bool isActive)
        {
            string userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            FlagEntity entity = new()
            {
                Name = flagName,
                UserId = userId,
                Value = isActive
            };

            var response = applicationDbContext.Flags.AddAsync(entity);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }
    }
}
