using FlagX0.Web.DTOs;

namespace FlagX0.Web.Models
{
    public class FlagIndexViewModel
    {
        public PaginationDto<FlagDto> Pagination { get; set; }
        public List<int> SelectOptions { get; set; } = [5, 10, 15];
    }
}
