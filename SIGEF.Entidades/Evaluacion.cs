using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEF.Entidades
{
    public class Evaluacion
    {
        public int IdEvaluacion { get; set; }
        public int IdPonencia { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstatus { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaEvaluacion { get; set; }
        public List<EvaluacionDetalle> Detalles { get; set; }

        public Evaluacion()
        {
            this.Detalles = new List<EvaluacionDetalle>();
        }
    }
}
