using System;
using System.Net.Http;
using VizitkaOnline.Models;

namespace VizitkaOnline.Logic
{
    public class SendLogIntoTelegram
    {
        readonly static string Token = "2036370209:AAG5G7E5YWyUO6aB4HL6HNxkZy3-yFkpoI8";
        readonly static string ChatId = "-1001690622795";
        private static readonly HttpClient client = new HttpClient();
        readonly static string Url = $"https://api.telegram.org/bot{Token}/sendMessage?chat_id={ChatId}&text=";

        public async void SendLog(ErrorTelegramModel model)
        {
            string stringError = $"Произошла ошибка  \nВремя: {model.DateTimeOfError}  \nу пользователя с юзер агентом: {model.UserAgent}  \nЗапросил страницу:{model.PageUrl}  \nАккаунт: {model.Login}";
            await client.GetAsync(Url + stringError);
        }
    }
}
