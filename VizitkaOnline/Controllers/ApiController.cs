using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VizitkaOnline.Logic;

namespace VizitkaOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        //Api получение информации
        [HttpGet("/api/{login}")]
        public string ApiUser(string login)
        {
            return JsonSerializer.Serialize(DataLogic.GetUserModel(login));
        }        
    }
}
