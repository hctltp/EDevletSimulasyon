using System.Web.Mvc;

namespace EDevletSimulasyon.Areas.MesajEntegrasyon
{
    public class MesajEntegrasyonAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MesajEntegrasyon";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MesajEntegrasyon_default",
                "MesajEntegrasyon/{controller}/{action}/{id}",
                new { controller = "Mesaj", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}