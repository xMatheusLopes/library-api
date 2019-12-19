using System;
namespace library_api.Models
{
    public class EmailCofirmation
    {
        public string Username { get; set; }
        public string BaseUrl { get; set; }
        public string ConfirmationUrl { get; set; }
    }
}
