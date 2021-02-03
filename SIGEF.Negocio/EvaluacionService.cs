using SIGEF.Dta;
using SIGEF.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEF.Negocio
{
    public class EvaluacionService
    {
        private readonly DBSIGFContext context;

        public EvaluacionService(DBSIGFContext context)
        {
            this.context = context;
        }

        public bool CrearEvaluacion(Evaluacion evaluacion)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@Accion", 1),
                new SqlParameter("@IdPonencia", evaluacion.IdPonencia),
                new SqlParameter("@IdEstatus",evaluacion.IdEstatus),
                new SqlParameter("@UserRevision",evaluacion.IdUsuario),
                new SqlParameter("@Observaciones",evaluacion.Observaciones),
            };

            DataTable dt = this.context.ExecuteReader("spEvaluacion", parameter);
            int id = Convert.ToInt32(dt.Rows[0]["IdEvaluacion"]);

            var rows = this.AregarDetalles(id, evaluacion.Detalles);

            if (id > 0 && rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            
        }

        public int AregarDetalles(int id, List<EvaluacionDetalle> detalles)
        {
            var rows = 0;

            foreach (var item in detalles)
            {
               SqlParameter[] parameter =
               {
                    new SqlParameter("@Accion", 2),
                    new SqlParameter("@IdEvaluacion", id),
                    new SqlParameter("@IdCriterio", item.IdCriterio),
                    new SqlParameter("@IdEstatusEvaluacion", item.IdEstatusEvaluacion),
               };

               rows = this.context.ExecuteNonQuery("spEvaluacion", parameter);
            }

            return rows;
        }
    }
}
