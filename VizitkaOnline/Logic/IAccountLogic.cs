using VizitkaOnline.Models;

namespace VizitkaOnline.Logic
{
    public interface IAccountLogic
    {
        public UserModel GetUserModel(int id);
        public void UpdateDB(int id, UserModel userModel);
        public void WriteToDb(UserModel userModel);

    }
}
