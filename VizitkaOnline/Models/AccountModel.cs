using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace VizitkaOnline.Models
{
    public class AccountModel : BaseAccountModel
    {   
        public string FullName { get; set; }
        public string FaceBook { get; set; }
        public string Instagram { get; set; }
        public string Telegram { get; set; }
        public string Monobank { get; set; }
        public string Phone { get; set; }
        public string UserPicture { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
    }
}
