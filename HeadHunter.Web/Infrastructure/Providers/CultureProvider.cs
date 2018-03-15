using System.Globalization;

namespace HeadHunter.Web.Infrastructure.Providers
{
    public class CultureProvider :ICultureProvider
    {
        public CultureInfo GetCultureInfo()
        {
            return CultureInfo.DefaultThreadCurrentCulture;
        }

        public string GetIsoCode()
        {
            return CultureInfo.DefaultThreadCurrentCulture.Parent.ToString();
        }
    }
}
