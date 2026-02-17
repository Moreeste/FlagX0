namespace FlagX0.Web.DTOs
{
    public record PaginationDto<T>(List<T> Items, int TotalItems, int PageSize, int CurrentPage, string? Search);
}
