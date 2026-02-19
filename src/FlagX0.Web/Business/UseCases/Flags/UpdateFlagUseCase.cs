using FlagX0.Web.Business.Mappers;
using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;
using FlagX0.Web.DTOs;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace FlagX0.Web.Business.UseCases.Flags
{
    public class UpdateFlagUseCase(ApplicationDbContext applicationDbContext)
    {
        public async Task<Result<FlagDto>> Execute(FlagDto dto)
            => await VerifyIsTheOnlyOneWithThatName(dto)
            .Bind(x => GetFromDb(x.Id))
            .Bind(x => Update(x, dto))
            .Map(x => x.ToDto());

        private async Task<Result<FlagDto>> VerifyIsTheOnlyOneWithThatName(FlagDto dto)
        {
            bool alreadyExists = await applicationDbContext.Flags.AnyAsync(f => f.Name.Equals(dto.Name) && f.Id != dto.Id);

            if (alreadyExists)
            {
                return Result.Failure<FlagDto>("Flag with the same name already exist.");
            }

            return dto;
        }

        private async Task<Result<FlagEntity>> GetFromDb(int id)
            => await applicationDbContext.Flags.Where(f => f.Id == id).SingleAsync();

        private async Task<Result<FlagEntity>> Update(FlagEntity entity, FlagDto dto)
        {
            entity.Value = dto.IsEnabled;
            entity.Name = dto.Name;
            await applicationDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
