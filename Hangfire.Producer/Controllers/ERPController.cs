using ERPPlugin.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire.Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ERPController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IConfiguration _configuration;
        private readonly PluginManager _pluginManager;

        public ERPController(IBackgroundJobClient backgroundJobClient, IConfiguration configuration,PluginManager pluginManager
            )
        {
            _backgroundJobClient = backgroundJobClient;
            _configuration = configuration;
            _pluginManager = pluginManager;
        }

        [NonAction]
        public IActionResult ScheduleUpdateTitle()
        {
            // Günde bir kez çalışacak şekilde GetTitle'ı schedule et
            RecurringJob.AddOrUpdate(() => UpdateTitle(), Cron.MinuteInterval(1));

            return Ok("Update title job scheduled successfully");
        }

        [NonAction]
        [Route(template: "ScheduleUpdateUser")]
        public IActionResult ScheduleUpdateUser()
        {
            // Günde iki kez çalışacak şekilde GetUser'ı schedule et
            RecurringJob.AddOrUpdate(() => UpdateUser(), "0 0,12 * * *");

            return Ok("GetUser job scheduled successfully");
        }

        [HttpGet]
        [Route(template: "UpdateTitle")]
        public async Task<List<Title>> UpdateTitle()
        {
            var result = await _pluginManager.GetTitlesFromPlugins();
            return result;
            //Todo gelen listeyi döngü içerisinde authentication.api de titles altında var mı kontrol et yoksa ekle varsa güncelle metodlarını
             ScheduleUpdateTitle();
        }

        [HttpGet]
        [Route(template: "UpdateUser")]
        public async Task<List<User>> UpdateUser()
        {
            //List<User> userList = null;
            //using (var proxy = new ERPDynamicLibraryProxy(_configuration))
            //{
            //    var result = await proxy.GetUser();

            //    // GetUser işlemi sonucunu işleyebilirsiniz, örneğin bir DTO'ya atayabilir ve başka bir işlem yapabilirsiniz.
            //    // Örneğin: var dto = new YourDTOType { Property1 = result.Property1, ... };
            //    // Ardından, DTO örneği ile başka bir işlem yapabilirsiniz.
            //    userList = result;
                return null;
            //}
        }
    }
}
