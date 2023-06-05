using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MoneyBackUp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoneyController : ControllerBase
    {

        private readonly IWebHostEnvironment host;

        public MoneyController(IWebHostEnvironment host)
        {
            this.host = host;
        }



        [HttpGet("ValidateMoney")]
        public ActionResult ValidateMoney()
        {
            return Ok(CreateCustomFile());
        }

        private bool CreateCustomFile()
        {
            string path = host.WebRootPath + "Value";

            if (System.IO.File.Exists(path))
            {
                var json = System.IO.File.ReadAllText(path);
                bool result = JsonConvert.DeserializeObject<bool>(json);
                return result;
            }
            else
            {
                bool result = true;
                var json = JsonConvert.SerializeObject(result);
                System.IO.File.WriteAllText(path, json);
                return CreateCustomFile();
            }

        }

    }
}