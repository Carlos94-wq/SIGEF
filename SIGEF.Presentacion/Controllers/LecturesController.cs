using SIGEF.Entidades;
using SIGEF.Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace SIGEF.Presentacion.Controllers
{
    public class LecturesController : Controller
    {
        private readonly PonenciaService _Service;

        public LecturesController(PonenciaService service)
        {
            this._Service = service;
        }

        [HttpGet]
        public ActionResult AddLectrue()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddLectrue(Ponencia ponencia)
        {
            bool isAdded = this._Service.AddPonencia(ponencia);
            return Json(isAdded);
        }

        [HttpPost]
        public JsonResult UploadFiles( 
                HttpPostedFileBase Cover, 
                HttpPostedFileBase Lecture, int idUsuario, int idLinea)
        {

            string lectureExt = Path.GetExtension(Lecture.FileName);
            string CoverExt = Path.GetExtension(Cover.FileName);

            if (lectureExt == ".docx" || lectureExt == ".doc" && CoverExt == ".docx" || CoverExt == ".doc")
            {
                string Siglas = "";
                switch (idLinea)
                {
                    case 1:
                        Siglas = "FAE";
                        break;
                    case 2:
                        Siglas = "MI";
                        break;
                    case 3:
                        Siglas = "MAA";
                        break;
                    case 4:
                        Siglas = "T";
                        break;
                }

                //Nomenclaura
                string CoverName = $"{idUsuario}-{Siglas}-C-V1-{Cover.FileName}";
                string LectureName = $"{idUsuario}-{Siglas}-L-V1-{Lecture.FileName}";

                int lastLecture = this._Service.LastLecture(idUsuario); //Obtiene laultima ponencia
                bool added = this._Service.AddUploads(lastLecture, CoverName, LectureName);

                //Escritura de archivos
                string CoverPath = Server.MapPath($"~/ARCHIVOS/" + CoverName);
                string LecturePath = Server.MapPath("~/ARCHIVOS/" + LectureName);

                //Carga de archivos
                Cover.SaveAs(CoverPath);
                Lecture.SaveAs(LecturePath);

                return Json(added);
            }
            else 
            {
                return Json(false);   
            }
        }

        [HttpPost]
        public JsonResult SearchCollaborator( Usuario usuario )
        {
            var lstusuario = this._Service.colaborator( usuario.SearchValue );

            if (lstusuario.Count == 0)
            {
                return (null);
            }

            return Json(lstusuario);
        }

        [HttpPost]
        public JsonResult UpdateFiles(int idPonencia, int idUsuario, string version, string idLinea, 
             HttpPostedFileBase Lecture, HttpPostedFileBase Cover) 
        {     

            if (Cover == null)
            {
                string lectureExt = Path.GetExtension(Lecture.FileName);

                if (lectureExt == ".docx" || lectureExt == ".doc")
                {
                    string Siglas = "";
                    switch (idLinea)
                    {
                        case "FAE - Finance and Economy":
                            Siglas = "FAE";
                            break;
                        case "MI - Methodological Issues":
                            Siglas = "MI";
                            break;
                        case "MAA - Management and Accounting":
                            Siglas = "MAA";
                            break;
                        case "T - Technology":
                            Siglas = "T";
                            break;
                    }

                    //Nomenclaura
                    string CoverName = $"";
                    string LectureName = $"{idUsuario}-{Siglas}-L-V{version}-{Lecture.FileName}";

                    bool added = this._Service.AddUploads(idPonencia, CoverName, LectureName);

                    //Escritura de archivos
                    string LecturePath = Server.MapPath("~/ARCHIVOS/" + LectureName);

                    //Carga de archivos
                    Lecture.SaveAs(LecturePath);

                    return Json(added);
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                string lectureExt = Path.GetExtension(Lecture.FileName);
                string CoverExt = Path.GetExtension(Cover.FileName);

                if ((lectureExt == ".docx" || lectureExt == ".doc") && (CoverExt == ".docx" || CoverExt == ".doc"))
                {
                    string Siglas = "";
                    switch (idLinea)
                    {
                        case "FAE - Finance and Economy":
                            Siglas = "FAE";
                            break;
                        case "MI - Methodological Issues":
                            Siglas = "MI";
                            break;
                        case "MAA - Management and Accounting":
                            Siglas = "MAA";
                            break;
                        case "T - Technology":
                            Siglas = "T";
                            break;
                    }

                    //Nomenclaura
                    string CoverName = $"{idUsuario}-{Siglas}-C-V{version}-{Cover.FileName}";
                    string LectureName = $"{idUsuario}-{Siglas}-L-V{version}-{Lecture.FileName}";

                    bool added = this._Service.AddUploads(idPonencia, CoverName, LectureName);

                    //Escritura de archivos
                    string CoverPath = Server.MapPath($"~/ARCHIVOS/" + CoverName);
                    string LecturePath = Server.MapPath("~/ARCHIVOS/" + LectureName);

                    //Carga de archivos
                    Cover.SaveAs(CoverPath);
                    Lecture.SaveAs(LecturePath);

                    return Json(added);
                }
                else
                {
                    return Json(false);
                }
            }        
        }
    }
}