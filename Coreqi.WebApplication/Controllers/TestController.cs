using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Annotation;
using Coreqi.Infrastructure.Logger;
using Coreqi.Services.Common.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coreqi.WebApplication.Controllers
{
    [Bean]
    public class TestController : Controller
    {
        [Autowired]
        public IUserService userService { get; set; }

        [Autowired]
        public ILogger<TestController> logger { get; set; }


        public IActionResult Index()
        {
            logger.LogInformation("哈哈");
            LogHelper.Info("1234", "4567");
            return View();
        }
    }
}