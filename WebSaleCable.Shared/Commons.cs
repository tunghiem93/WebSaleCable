using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSaleCable.Shared
{
    public class Commons
    {
        public const string Image200_100 = "http://placehold.it/200x100";
        public const string Image350_200 = "http://placehold.it/350x200";

        public static string HostImage = ConfigurationManager.AppSettings["HostImage"];
        public static string _PublicImages = string.IsNullOrEmpty(ConfigurationManager.AppSettings["PublicImages"]) ? "" : ConfigurationManager.AppSettings["PublicImages"];
    }
}
