namespace VizitkaOnline.Models
{
    public class UserModel : BaseAccountModel
    {
        private string passwordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName {  get; set; }
        public string Email {  get; set; }
        public string PasswordHash
        {
            get
            {
                return passwordHash;
            }

            set
            {
                passwordHash = new PasswordHashModel().HashPassword(value);
            }
        }
    }
}
