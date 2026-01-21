using FlagX0.Web.Business.Mappers;
using FlagX0.Web.Business.UserInfo;
using FlagX0.Web.Data;
using FlagX0.Web.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FlagX0.Web.Business.UseCases
{
    public class GetFlagsUseCase(ApplicationDbContext applicationDbContext, IFlagUserDetails userDetails)
    {
        public async Task<List<FlagDto>> Execute()
        {
            var response = await applicationDbContext.Flags.Where(x => x.UserId == userDetails.UserId)
                .AsNoTracking().ToListAsync();

            return response.ToDto();
        }
    }
}
