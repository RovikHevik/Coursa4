using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using VizitkaOnline.AppData;
using VizitkaOnline.Models;

namespace VizitkaOnline.Logic
{
    public static class DataLogic
    {

        public static bool IsExistsUser(UserModel model)
        {
            using ApplicationContext db = new();
            return db.UserModel.Any(user => user.Login == model.Login);
        }

        public static string CheckNotNull(UserModel model)
        {
            string result = "";
            using ApplicationContext db = new();
            if (model.Login == null || model.PasswordHash == null || model.Email == null || model.FirstName == null || model.LastName == null)
            {
                result = "Ошибка ввода данных.Пустые поля";
            } 
            else if(IsExistsUser(model))
            {
                result = "Данное имя занято, попробуйте другое";
            }

            return result;
        }

        public static string LoginUser(UserModel model)
        {
            string result = "";
            using ApplicationContext db = new();
            if (!IsExistsUser(model))
            {
                result = "Пользователя не существует.";
            }
            else if (GetUserModel(model.Login).PasswordHash.Contains(model.PasswordHash))
            {
                result = GetUserModel(model.Login).PasswordHash;
            }
            return result;
        }

        public static AccountModel GetAccountModel(string login)
        {
            using ApplicationContext db = new();
            return db.AccountModel.Where(ac => ac.Login == login).FirstOrDefault();
        }

        public static UserModel GetUserModel(string login)
        {
            using ApplicationContext db = new();
            return db.UserModel.Where(ac => ac.Login == login).FirstOrDefault();
        }

        public async static void UpdateDB(string login, AccountModel model)
        {
            using ApplicationContext db = new();
            AccountModel old = db.AccountModel.Where(ac => ac.Login == login).FirstOrDefault();
            old.FullName = model.FullName;
            old.FaceBook = model.FaceBook;
            old.Telegram = model.Telegram;
            old.Monobank = model.Monobank;
            old.Instagram = model.Instagram;
            old.Phone = model.Phone;
            old.UserPicture = "/img/" + old.Login + ".jpg";
            await db.SaveChangesAsync();
        }

        public async static void WriteToDb(UserModel model)
        {
            using ApplicationContext db = new();
            AccountModel accountModel = new()
            {
                id = model.id,
                Login = model.Login,
                FullName = model.FirstName + " " + model.LastName,
                UserPicture = "/img/default_av.jpg"
            };
            db.UserModel.Add(model);
            db.AccountModel.Add(accountModel);
            await db.SaveChangesAsync();
        }

        public async static void WriteToDb(AccountModel model)
        {
            using ApplicationContext db = new();
            db.AccountModel.Add(model);
            await db.SaveChangesAsync();
        }
        public static void SaveImage(IFormFile postedFiles, string login, IWebHostEnvironment _appEnvironment)
        {
            if (postedFiles != null)
            {
                string path = _appEnvironment.WebRootPath + "/img/" + login + ".jpg";
                using (var fileStream = File.Create(path))
                {
                   postedFiles.CopyTo(fileStream);
                }
                UpdateDB(login, GetAccountModel(login));
            }
        }
        public static string Sha256Hash(string inputWord)
        {
            byte[] tempHash;
            StringBuilder stringBuilder = new();
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                tempHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputWord));
            }
            foreach (var tempByte in tempHash)
            {
                stringBuilder.Append(tempByte.ToString("X2"));
            }
            return stringBuilder.ToString();
        }
    }
}
