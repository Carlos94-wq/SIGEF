using SIGEF.Negocio.CustomEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGEF.Presentacion.Controllers
{
    public class MenuController : Controller
    {
        [HttpGet] /* /Menu/ParticipantMenu?rol= */
        public JsonResult ParticipantMenu(int rol) 
        {
            //Instancias
            ParticipantMenu Home = new ParticipantMenu();
            Home.Ruta = Url.Action("ParticipantsPagedList", "Main");
            Home.Icono = "fa fa-home";
            Home.Titulo = "Home";
           
            ParticipantMenu Lecture = new ParticipantMenu();
            Lecture.Ruta = Url.Action("AddLectrue", "Lectures");
            Lecture.Icono = "fa fa-file-archive-o";
            Lecture.Titulo = "Submit your Paper";  

            //Coleciones
            List<ParticipantMenu> list = new List<ParticipantMenu>();
            list.Add(Home);
            list.Add(Lecture);
          
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ComiteUser(int rol)
        {
            //Instancias
            ParticipantMenu Home = new ParticipantMenu();
            Home.Ruta = Url.Action("MainComiteView", "Main");
            Home.Icono = "fa fa-home";
            Home.Titulo = "Home";

            ParticipantMenu Lecture = new ParticipantMenu();
            Lecture.Ruta = Url.Action("InReviewComite", "Main");
            Lecture.Icono = "fa fa-eye";
            Lecture.Titulo = "Paper Review";

            //Coleciones
            List<ParticipantMenu> list = new List<ParticipantMenu>();
            list.Add(Home);
            list.Add(Lecture);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}