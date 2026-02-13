using FlagX0.Web.Business.UserInfo;
using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace FlagX0.Web.Business.UseCases
{
    public class DeleteFlagUseCase(ApplicationDbContext applicationDbContext, IFlagUserDetails userDetails)
    {
        public async Task<Result<bool>> Execute(string flagName)
            => await GetEntity(flagName).Bind(DeleteEntity);

        private async Task<Result<FlagEntity>> GetEntity(string flagName)
            => await applicationDbContext.Flags
            .Where(f => f.Name.Equals(flagName) && f.UserId == userDetails.UserId).SingleAsync();

        private async Task<Result<bool>> DeleteEntity(FlagEntity entity)
        {
            applicationDbContext.Flags.Remove(entity);
            await applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
