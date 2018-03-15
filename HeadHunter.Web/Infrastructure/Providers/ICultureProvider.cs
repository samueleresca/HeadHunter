using System.Globalization;

namespace HeadHunter.Web.Infrastructure.Providers
{
    public interface ICultureProvider
    {
        CultureInfo GetCultureInfo();
        string GetIsoCode();
    }
}