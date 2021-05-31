using System;
using System.Configuration;

namespace Pics
{
    class Config
    {
        public readonly int width;
        public readonly int height;
        public Config()
        {
            try
            {
                var setting = ConfigurationManager.AppSettings;
                width = get_number(setting, "width", "10");
                height = get_number(setting, "height", "10");
            }
            catch (System.Exception)
            {
                Console.WriteLine("config file error");
                throw;
            }

        }
        private int get_number(System.Collections.Specialized.NameValueCollection settings, string key, string default_value) {
            var value = settings[key] ?? default_value;
            return int.Parse(value);
        }
    }
}
