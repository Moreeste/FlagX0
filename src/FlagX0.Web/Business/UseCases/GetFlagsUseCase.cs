using FlagX0.Web.Data;
using FlagX0.Web.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlagX0.Web.Business.UseCases
{
    public class GetFlagsUseCase(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
    {
        public async Task<List<FlagDto>> Execute()
        {
            string userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var response = await applicationDbContext.Flags.Where(x => x.UserId == userId)
                .AsNoTracking().ToListAsync();

            return response.Select(x => new FlagDto()
            {
                IsEnabled = x.Value,
                Name = x.Name
            }).ToList();
        }
    }
}
