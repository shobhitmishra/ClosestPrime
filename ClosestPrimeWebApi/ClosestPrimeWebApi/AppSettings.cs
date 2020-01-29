using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClosestPrimeWebApi
{
    using Microsoft.Extensions.Configuration;
    public class AppSettings
    {
        public string EndPointUri { get; set; }
        public string PrimaryKey { get; set; }
        public string StorageConnectionString { get; set; }
        public static AppSettings LoadAppSettings()
        {
            IConfigurationRoot configRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            AppSettings appSettings = configRoot.Get<AppSettings>();
            return appSettings;
        }
    }
}
