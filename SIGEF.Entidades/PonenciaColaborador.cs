using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEF.Entidades
{
    public class PonenciaColaborador
    {
        public int IdPonenciaColaborador { get; set; }
        public int IdPonencia { get; set; }
        public int IdParticipante { get; set; }
        public string TipoColaboracion { get; set; }
        public int IdTipoColaborador { get; set; }
        public string Colaboracion { get; set; }

        /*Campos para detalle*/
        public string Nombre { get; set; }
    }
}
