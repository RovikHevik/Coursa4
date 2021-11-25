using System;

namespace VizitkaOnline.Models
{
    public class ErrorTelegramModel
    {
        public string UserAgent { get; set; }
        public string Login { get; set; }
        public string PageUrl { get; set; }
        public string DateTimeOfError { get; set; }
    }
}
