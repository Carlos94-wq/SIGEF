using SIGEF.Entidades;
using SIGEF.Negocio;
using SIGEF.Negocio.CustomEntites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGEF.Presentacion.Controllers
{
    public class MainController : Controller
    {
        private readonly PonenciaService _Service;
        private readonly ComiteService comiteService;

        public MainController(PonenciaService service, ComiteService comiteService)
        {
            this._Service = service;
            this.comiteService = comiteService;
        }

        [HttpGet]//Participantes vista
        public ActionResult ParticipantsPagedList()
        {
            return View();
        }

        [HttpGet]//Ventana modal
        public ActionResult _ParticipantsFilesModal()
        {
            return View();
        }

        //Lista de acrchivos
        [HttpGet] //Participantes
        public JsonResult PagedLecturesList(int idParticipante)
        {
            var list = this._Service.PonenciaByUsuario(idParticipante);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LectureDetail(int id)
        {
            var detalle = this._Service.PonenciaDetalle(id);
            return Json(detalle, JsonRequestBehavior.AllowGet);
        }

        //Commite
        [HttpGet]
        public ActionResult MainComiteView()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PagedList(int id )
        {
            //Parametros de jquery
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            List<PonenciaListModel> ponencias = this._Service.PonenciaPegedList(id);

            recordsTotal = ponencias.Count();
            ponencias = ponencias.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = ponencias
            });
        }

        [HttpPost]
        public JsonResult GetLecture( EvaluadorModel model )
        {
            var Updated = this.comiteService.AsignarPonenia(model);
            return Json(Updated, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult InReviewComite()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ComiteLecture(int idusuario)
        {

            //Parametros de jquery
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            List<PonenciaListModel> list = this.comiteService.PonenciaByUsuario(idusuario);

            recordsTotal = list.Count();
            list = list.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = list
            });
        }

        //Cambiar esto
        [HttpGet]
        public FileResult DownLoadManul(string fileName)
        {
            var FileVirtaulPath = "~/ARCHIVOS/" + fileName;
            return File(FileVirtaulPath, "application/forece- download", Path.GetFileName(FileVirtaulPath));
        }
    }
}