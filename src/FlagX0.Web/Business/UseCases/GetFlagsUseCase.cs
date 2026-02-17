using FlagX0.Web.Business.Mappers;
using FlagX0.Web.Business.UserInfo;
using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;
using FlagX0.Web.DTOs;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace FlagX0.Web.Business.UseCases
{
    public class GetFlagsUseCase(ApplicationDbContext applicationDbContext, IFlagUserDetails userDetails)
    {
        public async Task<Result<PaginationDto<FlagDto>>> Execute(string? search, int page, int size)
            => await GetFromDb(search, page, size)
            .Map(x => x.ToDto())
            .Combine(x => TotalElements(search))
            .Map(x => new PaginationDto<FlagDto>(x.Item1, x.Item2, size, page, search));

        private async Task<Result<List<FlagEntity>>> GetFromDb(string? search, int page, int size)
        {
            var query = applicationDbContext.Flags
                .Where(f => f.UserId == userDetails.UserId)
                .Skip(size * (page - 1))
                .Take(size);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(f => f.Name.Contains(search));
            }

            return await query.ToListAsync();
        }

        private async Task<Result<int>> TotalElements(string? search)
        {
            var query = applicationDbContext.Flags.Where(f => f.UserId == userDetails.UserId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(f => f.Name.Contains(search));
            }

            return await query.CountAsync();
        }
    }
}
