using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var old = GetAccountModel(login);
            old.FullName = model.FullName;
            old.FaceBook = model.FaceBook;
            old.Telegram = model.Telegram;
            old.Monobank = model.Monobank;
            old.Instagram = model.Instagram;
            old.Phone = model.Phone;
            if(model.Picture != null)
            {
                SaveImage(model.Picture, login);
            }
            old.UserPicture = Environment.CurrentDirectory + "/img/" + HashModel.Sha256Hash(old.Login) + ".jpg";
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
        public static void SaveImage(IFormFile postedFiles, string login)
        {

            string path = "img/" + HashModel.Sha256Hash(login) + ".jpg";
            FileStream stream = new FileStream(path, FileMode.Create);
            postedFiles.CopyTo(stream);
        }

    }
}
