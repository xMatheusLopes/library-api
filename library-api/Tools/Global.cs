using System;
namespace library_api.Tools
{
    public class Global
    {
        public string Environment { get; set; }
        public string BaseUrl { get; set; }

        public void SetBaseUrl()
        {
            if (Environment == "production")
            {
                BaseUrl = "";
            } else
            {
                BaseUrl = "http://localhost:5000/";
            }
        }
    }
}
