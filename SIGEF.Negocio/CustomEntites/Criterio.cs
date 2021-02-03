using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEF.Negocio.CustomEntites
{
    public class Criterio
    {
        public int IdCriterio { get; set; }
        public string Descripcion { get; set; }
        public List<EstatusEvaluacion> Evaluacion{ get; set; }

        public Criterio()
        {
            this.Evaluacion = new List<EstatusEvaluacion>();
        }
    }
}
