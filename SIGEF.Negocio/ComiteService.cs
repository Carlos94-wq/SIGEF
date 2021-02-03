using SIGEF.Dta;
using SIGEF.Entidades;
using SIGEF.Negocio.CustomEntites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEF.Negocio
{
    public class ComiteService
    {
        private readonly DBSIGFContext _Context;

        public ComiteService(DBSIGFContext Context)
        {
            this._Context = Context;
        }

        //Asignar ponencia a un evaluador
        public bool AsignarPonenia( EvaluadorModel model)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdUsuario", model.IdEvaluador),
                new SqlParameter("@IdPonencia", model.IdPonencia)
            };

            var Updated = this._Context.ExecuteNonQuery("spComite", parameters);

            return Updated > 0;
        }

        public List<PonenciaListModel> PonenciaByUsuario(int idUsuario)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Accion",2),
                new SqlParameter("@IdUsuario", idUsuario)
            };

            DataTable Dta = _Context.ExecuteReader("spComite", parameters);
            List<PonenciaListModel> Ponencias = (from DataRow dr in Dta.Rows
                                        select new PonenciaListModel()
                                        {
                                            IdPonencia = (int)dr["IdPonencia"],
                                            Tema = dr["Tema"].ToString(),
                                            Descripcion = dr["Descripcion"].ToString(),
                                            FechaRegistro = dr["FechaRegistro"].ToString(),
                                            Linea = dr["Linea"].ToString(),
                                            Estatus = dr["Estatus"].ToString()

                                        }).ToList();

            return Ponencias;
        }

        public List<Criterio> Sheet()
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Accion",5)
            };

            SqlParameter[] parameters2 = {
                new SqlParameter("@Accion",4)
            };

            DataTable Dta = _Context.ExecuteReader("spCatalogos", parameters);
            DataTable Ev = _Context.ExecuteReader("spCatalogos", parameters2);

            List<Criterio> Ponencias = (from DataRow dr in Dta.Rows
                                        select new Criterio()
                                        {
                                            IdCriterio = (int)dr["IdCriterio"],
                                            Descripcion = dr["Descripcion"].ToString(),
                                            Evaluacion = (from DataRow dr2 in Ev.Rows select new EstatusEvaluacion() {

                                                IdEstatus = (int)dr2["IdEstatusEvaluacion"],
                                                Descripcion = dr2["Descrip"].ToString()

                                            }).ToList()

                                        }).ToList();

            return Ponencias;
        }
    }
}
