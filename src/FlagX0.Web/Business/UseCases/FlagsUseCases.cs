using FlagX0.Web.Business.UseCases.Flags;

namespace FlagX0.Web.Business.UseCases
{
    public record class FlagsUseCases(AddFlagUseCase Add, GetFlagsUseCase GetAll, GetSingleFlagUseCase Get,
        UpdateFlagUseCase Update, DeleteFlagUseCase Delete)
    {
    }
}
