using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PersonalFinanceTracker.Helpers;

namespace PersonalFinanceTracker.Controllers
{
    public class BaseController : Controller
    {
        private readonly JwtHelper _jwtHelper;

        public BaseController(JwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = _jwtHelper.GetUserIdFromCookie();
            ViewBag.IsUserLoggedIn = !string.IsNullOrEmpty(userId);
            ViewBag.UserId = userId;
            base.OnActionExecuting(context);
        }
    }

}
