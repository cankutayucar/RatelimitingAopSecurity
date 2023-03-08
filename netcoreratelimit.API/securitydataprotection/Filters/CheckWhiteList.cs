using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using securitydataprotection.Middlewares;
using System.Net;

namespace securitydataprotection.Filters
{
    public class CheckWhiteList : ActionFilterAttribute
    {
        private readonly IpList _ipList;
        public CheckWhiteList(IOptions<IpList> options)
        {
            _ipList = options.Value;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var requestpIp = context.HttpContext.Connection.RemoteIpAddress;
            var isWhileList = this._ipList.WhiteList.Where(x => IPAddress.Parse(x).Equals(requestpIp)).Any();
            if (!isWhileList)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
