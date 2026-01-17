using FlagX0.Web.Business.UseCases;
using FlagX0.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlagX0.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class FlagsController(AddFlagUseCase addFlagUseCase) : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new FlagViewModel());
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddFlagToDatabase(FlagViewModel model)
        {
            bool isCreated = await addFlagUseCase.Execute(model.Name, model.IsEnabled);

            return RedirectToAction("Index");
        }
    }
}
