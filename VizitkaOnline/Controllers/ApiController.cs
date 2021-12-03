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
        //Api получение информации
        [HttpGet("/api/Delete/{id}/{password}")]
        public string DeleteUser(int id, string password)
        {
            if (DataLogic.DeleteFromDB(id, password).Result == true)
            {
                return "ok";
            }
            else
            {
                return "error";
            }
        }

        [HttpGet("/api/getUserFull/{login}/{password}")]
        public string FullApiUser(string login, string password)
        {
            try
            {
                return JsonSerializer.Serialize(DataLogic.GetFullData(login, DataLogic.Sha256Hash(password)));
            }        
            catch (Exception)
            {
                return JsonSerializer.Serialize("Not found user");
            }
        }

    }
}
