using Microsoft.Extensions.Configuration;
using MyEdu.Service.Helpers;

namespace MyEdu.Service.Extensions
{
    public static class WebUrlExtensions
    {
        public static string GetFullWebUrl(this string fileName, IConfiguration config)
        {
            string hostUrl = HttpContextHelper.Context?.Request?.Scheme +
                "://" + HttpContextHelper.Context?.Request?.Host.Value;

            string storagePath = config.GetSection("Storage:ImageUrl").Value;

            var url = $@"{hostUrl}/{storagePath}/{fileName}";

            return url;
        }
    }
}
