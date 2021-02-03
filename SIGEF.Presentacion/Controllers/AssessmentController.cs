using SIGEF.Entidades;
using SIGEF.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGEF.Presentacion.Controllers
{
    public class AssessmentController : Controller
    {
        private readonly ComiteService _ComiteService;
        private readonly EvaluacionService evaluacionService;

        public AssessmentController(ComiteService ComiteService, EvaluacionService _EvaluacionService)
        {
            this._ComiteService = ComiteService;
            evaluacionService = _EvaluacionService;
        }

        // GET: Assessment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult AssessmentShet()
        {
            var Criterios = this._ComiteService.Sheet();
            return Json(Criterios, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateSheet(Evaluacion evaluacion)
        {
            var idEv = this.evaluacionService.CrearEvaluacion(evaluacion);
            return Json(idEv);
        }
    }
}