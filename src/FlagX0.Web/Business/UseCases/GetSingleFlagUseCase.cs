using FlagX0.Web.Business.Mappers;
using FlagX0.Web.Business.UserInfo;
using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;
using FlagX0.Web.DTOs;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace FlagX0.Web.Business.UseCases
{
    public class GetSingleFlagUseCase(ApplicationDbContext applicationDbContext, IFlagUserDetails userDetails)
    {
        public async Task<Result<FlagDto>> Execute(string flagName)
            => await GetFromDb(flagName).Map(x => x.ToDto());

        private async Task<Result<FlagEntity>> GetFromDb(string flagName)
            => await applicationDbContext.Flags
            .Where(f => f.UserId == userDetails.UserId && f.Name.Equals(flagName))
            .AsNoTracking().SingleAsync();
    }
}
