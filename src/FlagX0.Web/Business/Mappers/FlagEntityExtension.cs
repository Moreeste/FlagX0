using FlagX0.Web.Data.Entities;
using FlagX0.Web.DTOs;

namespace FlagX0.Web.Business.Mappers
{
    public static class FlagEntityExtension
    {
        public static FlagDto ToDto(this FlagEntity entity)
        {
            return new FlagDto
            {
                IsEnabled = entity.Value,
                Name = entity.Name
            };
        }

        public static List<FlagDto> ToDto(this List<FlagEntity> entities)
        {
            return entities.Select(e => e.ToDto()).ToList();
        }
    }
}
