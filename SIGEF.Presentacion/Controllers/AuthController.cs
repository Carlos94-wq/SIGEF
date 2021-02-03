using Microsoft.Ajax.Utilities;
using SIGEF.Entidades;
using SIGEF.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGEF.Presentacion.Controllers
{
    public class AuthController : Controller
    {

        private readonly UsuarioService _Service;

        public AuthController(UsuarioService service)
        {
            this._Service = service;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(Usuario usuario)
        {
            var response = this._Service.Login(usuario);

            if (response == null)//En caso de que no recupere todo, lo manda a qui para que no cree una excepcion
            {
                var data = new
                {
                    NoFound = "No user Found"
                };

                return Json(data);
            }
            else
            {
                var url = "";

                //Filtro de perfiles
                //setea el valor de la url segun el rol de usuairio para ssaber a que menu mandarlo
                if (response.Estatus.IdEstatus == 2)
                {
                    url = Url.Action("CompleteAccount");
                }
                else
                {
                    if (response._Rol.IdRol == 4)
                    {
                        url = Url.Action("ParticipantsPagedList", "Main");
                    }

                    if (response._Rol.IdRol == 3 || response._Rol.IdRol == 1)
                    {
                        url = Url.Action("MainComiteView", "Main");
                    }
                }

                //Crea un nuevo objeto
                var data = new
                {
                    idUsuario = response.IdUsuario,
                    name = response.Nombre,
                    apellidos = response.Apellidos,
                    email = response.Email,
                    status = response.Estatus.IdEstatus,
                    rol = response._Rol.IdRol,
                    url = url
                };

                return Json(data);
            }
        }

        [HttpGet]//Vista parcial que funciona como ventana modal
        public ActionResult CreatAccount()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreatAccount( Usuario usuario )
        {
            var IsAdded = this._Service.Add(usuario);
           
            return Json( new { 
                data = IsAdded
            });
        }

        [HttpGet]
        public ActionResult CompleteAccount()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CompleteAccount( Usuario usuario )
        {
            var model = this._Service.Login(usuario);
            var Resp = this._Service.Complete(usuario.Codigo , model.IdUsuario );

            if (Resp == "1")
            {


                var url = "";

                if (model._Rol.IdRol == 4)//Verifica el rol del usuario
                {
                    url = Url.Action("ParticipantsPagedList", "Main");
                }

                return Json(new
                {
                    isComplete = true,
                    idUsuario = model.IdUsuario,
                    name = model.Nombre,
                    apellidos = model.Apellidos,
                    email = model.Email,
                    status = model.Estatus.IdEstatus,
                    rol = model._Rol.IdRol,
                    url = url
                });
            }
            else 
            {
                return Json(new {
                    isComplete = false,
                });
            }
        }

        [HttpGet]
        public ActionResult RestorePassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendCode(string email, string contra )
        {
            Usuario usuario = new Usuario();
            usuario.Email = email;
            usuario.Contrasenia = contra;

            var idUsuario = this._Service.Login(usuario);

            bool IsSendit = this._Service.SenNewCode( email, idUsuario.IdUsuario);

            return Json(new { 
                isSendit = IsSendit
            });
        }

        [HttpPost]
        public JsonResult RestorePassword(string email)
        {
            bool sendit = this._Service.RestorePassword(email);
            return Json(sendit);
        }
    }
}