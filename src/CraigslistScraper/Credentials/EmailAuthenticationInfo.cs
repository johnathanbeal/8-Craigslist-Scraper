using System;
using System.Collections.Generic;
using System.Text;

namespace CraigslistScraper.Credentials
{
    public class EmailAuthenticationInfo
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public EmailAuthenticationInfo()
        {
            Username = "jbeal.i360@gmail.com";

            Password = "";
        }
    }
}
