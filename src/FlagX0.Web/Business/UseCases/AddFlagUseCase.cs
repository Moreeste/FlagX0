using FlagX0.Web.Business.UserInfo;
using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;

namespace FlagX0.Web.Business.UseCases
{
    public class AddFlagUseCase(ApplicationDbContext applicationDbContext, IFlagUserDetails userDetails)
    {
        public async Task<bool> Execute(string flagName, bool isActive)
        {
            FlagEntity entity = new()
            {
                Name = flagName,
                UserId = userDetails.UserId,
                Value = isActive
            };

            var response = applicationDbContext.Flags.AddAsync(entity);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }
    }
}
