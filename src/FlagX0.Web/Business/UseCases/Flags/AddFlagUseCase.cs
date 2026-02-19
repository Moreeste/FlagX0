using FlagX0.Web.Business.UserInfo;
using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace FlagX0.Web.Business.UseCases.Flags
{
    public class AddFlagUseCase(ApplicationDbContext applicationDbContext, IFlagUserDetails userDetails)
    {
        public async Task<Result<bool>> Execute(string flagName, bool isActive)
            => await ValidateFlag(flagName).Bind(x => AddFlagToDatabase(x, isActive));

        private async Task<Result<string>> ValidateFlag(string flagName)
        {
            bool flagExists = await applicationDbContext.Flags.Where(f => f.Name.Equals(flagName)).AnyAsync();

            if (flagExists)
            {
                return Result.Failure<string>("Flag name already exists.");
            }

            return flagName;
        }

        private async Task<Result<bool>> AddFlagToDatabase(string flagName, bool isActive)
        {
            FlagEntity entity = new()
            {
                Name = flagName,
                UserId = userDetails.UserId,
                Value = isActive
            };

            _ = await applicationDbContext.Flags.AddAsync(entity);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }
    }
}
