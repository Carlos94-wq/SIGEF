using SIGEF.Dta;
using SIGEF.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SIGEF.Negocio
{
    public class UsuarioService
    {

        private readonly DBSIGFContext _Context;

        public UsuarioService(DBSIGFContext Context)
        {
            this._Context = Context;
        }

        //Verifica si ya el correo ya esta vinculado
        public int IsAlreadyExists(string Email)
        {
            SqlParameter[] parameter = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@Email", Email)
            };

            DataTable data = _Context.ExecuteReader("spUsuario", parameter);
            int Respuesta = (int)data.Rows[0]["Respuesta"];

            return Respuesta;
        }

        //Inicio de sesion
        public Usuario Login(Usuario Model)
        {

            SqlParameter[] parameters = {
                new SqlParameter("@Accion",3),
                new SqlParameter("@Email", Model.Email),
                new SqlParameter("@Contra", Model.Contrasenia)
            };

            DataTable Dta = _Context.ExecuteReader("spUsuario",parameters);
            Usuario Usuario = (from DataRow dr in Dta.Rows
                               select new Usuario()
                               {
                                   IdUsuario = (int)dr["IdUsuario"],
                                   Nombre = dr["Nombre"].ToString(),
                                   Apellidos = dr["Apellidos"].ToString(),
                                   Email = dr["Email"].ToString(),
                                   Estatus = new Estatus() { IdEstatus = (int)dr["IdEstatus"] },
                                   _Rol = new Rol() { IdRol = (int)dr["IdRol"] }

                               }).FirstOrDefault();

            return Usuario;
        }

        //Pre registro
        //los usuario se crean con estatus 2 (inactvo )
        public bool Add(Usuario Model)
        {
            var isExists = this.IsAlreadyExists(Model.Email);

            if (isExists == 1)
            {
                return false;
            }
            else 
            {

                SqlParameter[] parameter = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@Nombre", Model.Nombre.Trim()),
                    new SqlParameter("@Apellidos", Model.Apellidos.Trim()),
                    new SqlParameter("@Email",Model.Email.Trim()),
                    new SqlParameter("@Contra", Model.Contrasenia.Trim()),
                    new SqlParameter("@Institucion", Model.Institucion.Trim()),
                    new SqlParameter("@Pais", Model.Pais.Trim()),
                    new SqlParameter("@Ciudad", Model.Cuidad.Trim()),
                    new SqlParameter("@Prefijo", Model.Prefijo.Trim())
                };

                int Row = this._Context.ExecuteNonQuery("spUsuario", parameter);

                return Row > 0;
            }          
        }

        //Terminar Registros
        //los usuario se crean con estatus 1 ( inactvo )
        public string Complete(string codigo, int idUsuario)
        {
            SqlParameter[] parameter = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdUsuario", idUsuario ),
                new SqlParameter("@Codigo", codigo)
            };

            DataTable data = _Context.ExecuteReader("spCodigoVerificacion", parameter);
            string Respuesta = data.Rows[0]["MENSAJE"].ToString();

            return Respuesta;
        }

        //Madan nuevos codidos
        public bool SenNewCode(string email, int idUsuario)
        {
            SqlParameter[] parameter = {
                new SqlParameter("@Accion", 1),
                new SqlParameter("@IdUsuario", idUsuario)
            };
           
            int Respuesta = _Context.ExecuteNonQuery("spCodigoVerificacion", parameter);

            return Respuesta > 0;
        }

        public bool RestorePassword( string email )
        {
            var isExist = this.IsAlreadyExists(email);

            if (isExist == 0)
            {
                return false;
            }
            else
            {
                SqlParameter[] parameter = {
                new SqlParameter("@Email", email)
            };

                _Context.ExecuteNonQuery("spRecuperarContra", parameter);
                return true;
            }
        }
    }
}
