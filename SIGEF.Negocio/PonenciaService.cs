using SIGEF.Dta;
using SIGEF.Entidades;
using SIGEF.Negocio.CustomEntites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEF.Negocio
{
    public class PonenciaService
    {
        private readonly DBSIGFContext _Context;

        public PonenciaService(DBSIGFContext Context)
        {
            this._Context = Context;
        }

        public int LastLecture(int idUsuario)
        {

            SqlParameter[] parameters = {
                new SqlParameter("@Accion", 4),
                new SqlParameter("@IdUsuario", idUsuario)
            };

            DataTable dt = this._Context.ExecuteReader("spPonencia", parameters);
            return (int)dt.Rows[0]["IdPonencia"];
        }

        //Al registrara una ponencia se retorna el id creado
        public bool AddPonencia( Ponencia ponencia ) 
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Accion", 1),
                new SqlParameter("@IdLinea", ponencia.IdLinea),
                new SqlParameter("@IdUsuario",ponencia.IdUsuario),
                new SqlParameter("@Tema", ponencia.Tema),
                new SqlParameter("@Descripcion", ponencia.Descripcion),
                new SqlParameter("@Consentimiento",ponencia.Concentimiento)
            };

            DataTable dt = this._Context.ExecuteReader("spPonencia", parameters);
            int idPonencia = (int)dt.Rows[0]["Ultima_Ponencia"]; //Obtiene el id de la ponencia

            if (idPonencia != 0) // si no se obtuvo id queire decir hubo registro 
            {
                foreach (var item in ponencia.Colaboradores) //Recorre la lista de colaboradores
                {

                    if (item.TipoColaboracion == "Corresponding Author")
                    {
                        item.IdTipoColaborador = 1;
                    }
                    else 
                    {
                        item.IdTipoColaborador = 2;
                    }

                    //Hace los insertes correspondientes
                    this.AddCollaborator(idPonencia, item.IdParticipante, item.IdTipoColaborador);
                }

                return true;
            }
            else 
            {
                return false;
            }
        }

        public bool AddCollaborator(int IdPonencia, int idUsuario, int TipoColaborador)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdPonencia", IdPonencia),
                new SqlParameter("@IdUsuario",idUsuario),
                new SqlParameter("@IdTipoColaborador", TipoColaborador)
            };

            int IsAdded = this._Context.ExecuteNonQuery("spPonencia", parameters);

            return IsAdded > 0;
        }

        public bool AddUploads(
                int IdPonencia,
                string Cover,
                string File)
        {

            SqlParameter[] CoverOps = {
                new SqlParameter("@Accion", 1),
                new SqlParameter("@IdPonencia", IdPonencia),
                new SqlParameter("@IdTipoArchivo",1),
                new SqlParameter("@Archivo", Cover)
            };
            int IsCoverAdd = this._Context.ExecuteNonQuery("spArchivos", CoverOps);

            SqlParameter[] FileOps = {
                new SqlParameter("@Accion", 1),
                new SqlParameter("@IdPonencia", IdPonencia),
                new SqlParameter("@IdTipoArchivo",2),
                new SqlParameter("@Archivo", File)
            };
            int IsFileAdd = this._Context.ExecuteNonQuery("spArchivos", FileOps);

            if (IsCoverAdd > 0 && IsFileAdd > 0) //para ambos archivos
            {
                return true;

            } else if (IsFileAdd > 0) //para solo ponencias
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Usuario> colaborator( string SearchValue ) 
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@Colaborador", SearchValue)
            };

            DataTable dt = this._Context.ExecuteReader("spPonencia", parameters);
            List<Usuario> collaborators = (from DataRow dr in dt.Rows
                                           select new Usuario()
                                           {
                                               IdUsuario = (int)dr["IdUsuario"],
                                               Nombre = (string)dr["Nombre"],
                                               Apellidos = (string)dr["Apellidos"],
                                               Email = (string)dr["Email"]

                                           }).ToList();


            return collaborators;
        }

        public List<Ponencia> PonenciaByUsuario(int idUsuario) 
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Accion",5),
                new SqlParameter("@IdUsuario", idUsuario)
            };

            DataTable Dta = _Context.ExecuteReader("spPonencia", parameters);
            List<Ponencia> Ponencias = (from DataRow dr in Dta.Rows
                      select new Ponencia()
                      {
                          IdPonencia = (int)dr["IdPonencia"],
                          IdLinea = (int)dr["IdLinea"],
                          Tema = dr["Tema"].ToString(),
                          Descripcion = dr["Descripcion"].ToString(),
                          IdEstatus = (int)dr["IdEstatus"]

                      }).ToList();

            return Ponencias;
        }

        public object PonenciaDetalle(int idPonencia)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Accion",1),
                new SqlParameter("@IdPonencia", idPonencia)
            };

            DataTable dt = this._Context.ExecuteReader("spDetallePonencia", parameters);
            Ponencia detalle = (from DataRow dr in dt.Rows
                                select new Ponencia()
                                {
                                    IdPonencia = (int)dr["IdPonencia"],
                                    Linea = (string)dr["Linea"],
                                    Tema = dr["Tema"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Estatus = (string)dr["Estatus"],
                                    FechaRegistro = dr["FechaRegistro"].ToString(),
                                    Concentimiento = (bool)dr["Consentimiento"],
                                    Observaciones = (string)dr["Observaciones"]

                                }).FirstOrDefault();

            SqlParameter[] Colaboradores = {
                new SqlParameter("@Accion",2),
                new SqlParameter("@IdPonencia", idPonencia)
            };

            DataTable dtColaboradores = this._Context.ExecuteReader("spDetallePonencia", Colaboradores);
            List<PonenciaColaborador> colaboradores = (from DataRow dr in dtColaboradores.Rows
                                select new PonenciaColaborador()
                                {
                                    IdPonencia = (int)dr["IdPonencia"],
                                    Nombre = dr["Nombre"].ToString(),
                                    TipoColaboracion = dr["TipoColaborador"].ToString(),

                                }).ToList();

            SqlParameter[] ArchivoParameters = {
                new SqlParameter("@Accion",3),
                new SqlParameter("@IdPonencia", idPonencia)
            };

            DataTable dtArchivos = this._Context.ExecuteReader("spDetallePonencia", ArchivoParameters);
            List<PonenciaArchivo> ponenciaArchivos = (from DataRow dr in dtArchivos.Rows
                                                       select new PonenciaArchivo()
                                                       {
                                                           IdPonencia = (int)dr["IdPonencia"],
                                                           Archivo = dr["Archivo"].ToString(),
                                                           IdTipoArchivo = (int)dr["IdTipoArchivo"],
                                                           TipoArchivo = dr["Descripcion"].ToString(),
                                                           FechaRegistro = dr["FechaRegistro"].ToString()

                                                       }).ToList();

            return new  {
                Ponencia = detalle,
                Colaboradores = colaboradores,
                Archivos = ponenciaArchivos
            };
        }

        public List<PonenciaListModel> PonenciaPegedList(int idUsuario)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Accion",1),
                new SqlParameter("@IdUsuario", idUsuario)
            };

            DataTable dt = this._Context.ExecuteReader("spComite", parameters);
            List<PonenciaListModel> lst = (from DataRow dr in dt.Rows
                                           select new PonenciaListModel
                                           {
                                               IdPonencia = (int)dr["IdPonencia"],
                                               Tema = dr["Tema"].ToString(),
                                               FechaRegistro = dr["FechaRegistro"].ToString(),
                                               Descripcion = dr["Descripcion"].ToString(),
                                               Linea = dr["Linea"].ToString()

                                           }).ToList();
            return lst;
        }
    }
}
