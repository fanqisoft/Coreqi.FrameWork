using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Annotation;
using Coreqi.Services.Common.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Coreqi.WebApplication.Controllers
{
    [Bean]
    public class TestController : Controller
    {
        [Autowired]
        public IUserService userService { get; set; }
        public IActionResult Index()
        {
            return View();
        }
    }
}