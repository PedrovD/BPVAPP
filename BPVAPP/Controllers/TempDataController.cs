using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BPVAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace BPVAPP.Controllers
{
    [Route("api/[controller]")]
    public class TempDataController : Controller
    {

        [HttpGet]
        [Route("moeder")]
        public string GetName()
        {
            return "Je moeder";
        }

        [HttpPost]
        [Route("addcompany")]
        public string Add(Company model)
        {
            return model.ToString();
        }

    }
}