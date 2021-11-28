using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("/api/getUserFull/{login}")]
        public string FullApiUser(string login)
        {
            try
            {
                return JsonSerializer.Serialize(DataLogic.GetFullDataAsync(login).Result);
            }        
            catch (Exception)
            {
                return JsonSerializer.Serialize("Not found user");
            }
        }
    }
}
