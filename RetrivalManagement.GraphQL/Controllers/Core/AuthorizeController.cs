using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RetrivalManagement.Infrastructure.Security;
using RetrivalManagement.Infrastructure.Singleton;
using RetrivalManagement.Models.DbEntities.Main;
using RetrivalManagement.Models.Models;
using RetrivalManagement.UnitOfWork.Login;
using RxWeb.Core.Security;

namespace RetrivalManagement.GraphQL.Controllers.Core
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private ILoginUow LoginUow { get; set; }

        private IUserClaim UserClaim { get; set; }
        private UserAccessConfigInfo UserAccessConfig { get; set; }

        private IApplicationTokenProvider ApplicationTokenProvider { get; set; }

        public AuthorizeController(ILoginUow loginUow, UserAccessConfigInfo userAccessConfig, IUserClaim userClaim, IApplicationTokenProvider applicationTokenProvider)
        {
            this.LoginUow = loginUow;
            UserAccessConfig = userAccessConfig;
            this.UserClaim = userClaim;
            this.ApplicationTokenProvider = applicationTokenProvider;
        }

        [HttpGet(ACCESS)]
        public async Task<IActionResult> Get()
        {
            var accessModules = await UserAccessConfig.GetFullInfoAsync(UserClaim.UserId, LoginUow);
            return Ok(JsonConvert.SerializeObject(accessModules));
        }

        [HttpPost(LOGOUT)]
        public async Task<IActionResult> LogOut(UserConfig userConfig)
        {
            await ApplicationTokenProvider.RemoveTokenAsync(userConfig);
            return Ok();
        }

        [HttpPost(REFRESH)]
        public async Task<IActionResult> Refresh(UserConfig userConfig)
        {
            var user = await this.LoginUow.Repository<User>().SingleAsync(t => t.UserId == UserClaim.UserId);
            var token = await ApplicationTokenProvider.RefereshTokenAsync(user, userConfig);
            return Ok(token);
        }

        const string LOGOUT = "logout";

        const string ACCESS = "access";

        const string REFRESH = "refresh";
    }
}
