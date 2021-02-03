using Newtonsoft.Json.Schema;
using SIGEF.Entidades;
using SIGEF.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGEF.Presentacion.Controllers
{
    public class DdlController : Controller
    {

        private readonly CatalogoService _Service;

        public DdlController(CatalogoService service)
        {
            this._Service = service;
        }

        [HttpGet]
        public JsonResult DdlLiena()
        {
            List<Linea> lstLinea = this._Service.DdlLinea();

            return Json(lstLinea,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DdlTipoColaborador()
        {
            List<TipoExpositor> expositors = this._Service.DdlTipoExpositor();
            return Json(expositors,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DdlTipoArchivo()
        {
            List<TipoExpositor> expositors = this._Service.DdlTipoArchivo();
            return Json(expositors, JsonRequestBehavior.AllowGet);
        }
    }
}