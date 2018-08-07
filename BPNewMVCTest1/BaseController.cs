using BPNewMVCTest1Service.TokenDTService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPNewMVCTest1
{
    public class BaseController : Controller
    {
        private ITokenDTService _tokenDTService;
        public BaseController(ITokenDTService tokenDTService, IConfiguration configuration)
        {
            _tokenDTService = tokenDTService;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //string token = context.HttpContext.Request.Headers["Authorization"]; //Request.Headers["Authorization"];
            //_tokenDTService.SetToken(token);
            _tokenDTService.SetURL(Configuration.GetSection("API").GetSection("URL").Value);
            base.OnActionExecuting(context);
        }
    }
}

