﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSaleCable.Shared
{
    public class Commons
    {
        #region Enum
        
        #endregion
        public const string Image200_100 = "http://placehold.it/200x100";
        public const string Image400_250 = "http://placehold.it/400x250";
        public const string Image600_450 = "http://placehold.it/600x450";

        public static string HostImage = ConfigurationManager.AppSettings["HostImage"];
        public static string _PublicImages = string.IsNullOrEmpty(ConfigurationManager.AppSettings["PublicImages"]) ? "" : ConfigurationManager.AppSettings["PublicImages"];
    }
}
