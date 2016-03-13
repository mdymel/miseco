using Microsoft.AspNet.Mvc;

namespace MiSeCo
{
    public class ApiController : Controller
    {
        [HttpGet]
        [Route("sitestatus")]
        public string SiteStatus()
        {
            return "OK";
        }
    }
}