using FlagX0.Web.Business.UseCases;
using FlagX0.Web.DTOs;
using FlagX0.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROP;

namespace FlagX0.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class FlagsController(
        AddFlagUseCase addFlagUseCase, 
        GetFlagsUseCase getFlagsUseCase, 
        GetSingleFlagUseCase getSingleFlagUseCase, 
        UpdateFlagUseCase updateFlagUseCase,
        DeleteFlagUseCase deleteFlagUseCase
        ) : Controller
    {
        [HttpGet("")]
        [HttpGet("{page:int}")]
        public async Task<IActionResult> Index(string? search, int page = 1, int size = 5)
        {
            var listFlags = (await getFlagsUseCase.Execute(search, page, size)).Throw();

            return View(new FlagIndexViewModel()
            {
                Pagination = listFlags
            });
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new FlagViewModel());
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddFlagToDatabase(FlagViewModel model)
        {
            Result<bool> isCreated = await addFlagUseCase.Execute(model.Name, model.IsEnabled);

            if (isCreated.Success)
            {
                return RedirectToAction("Index");
            }

            return View("Create", new FlagViewModel()
            {
                Error = isCreated.Errors.First().Message,
                IsEnabled = model.IsEnabled,
                Name = model.Name
            });
        }

        [HttpGet("{flagName}")]
        public async Task<IActionResult> GetSingle(string flagName, string? message)
        {
            var singleFlag = await getSingleFlagUseCase.Execute(flagName);

            return View("SingleFlag", new SingleFlagViewModel()
            {
                Flag = singleFlag.Value,
                Message = message
            });
        }

        [HttpPost("{flagName}")]
        public async Task<IActionResult> Update(FlagDto flag)
        {
            var singleFlag = await updateFlagUseCase.Execute(flag);

            return View("SingleFlag", new SingleFlagViewModel()
            {
                Flag = singleFlag.Value,
                Message = singleFlag.Success ? "Updated correctly" : singleFlag.Errors.First().Message
            });
        }

        [HttpGet("delete/{flagName}")]
        public async Task<IActionResult> Delete(string flagName)
        {
            var isDeleted = await deleteFlagUseCase.Execute(flagName);

            if (isDeleted.Success)
            {
                return RedirectToAction("");
            }

            return RedirectToAction("GetSingle", new
            {
                flagName,
                message = "Update correctly"
            });
        }
    }
}
