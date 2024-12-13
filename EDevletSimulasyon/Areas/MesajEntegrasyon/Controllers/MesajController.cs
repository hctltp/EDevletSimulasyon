using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDevletSimulasyon.Areas.MesajEntegrasyon.Controllers
{
    
    public class MesajController : Controller
    {
        // GET: MesajEntegrasyon/Mesaj
        public ActionResult Index()
        {
            return View("~/Areas/MesajEntegrasyon/Views/Mesaj/Index.cshtml");
        }
    }
}