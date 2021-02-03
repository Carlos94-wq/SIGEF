using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SIGEF.Entidades
{
    public class PonenciaArchivo
    {
        public int IdPonenciaArchivo { get; set; }
        public int IdPonencia { get; set; }
        public int IdTipoArchivo { get; set; }
        public int IdEstatus { get; set; }
        public string Archivo { get; set; }
        public string Observaciones { get; set; }

        public string TipoArchivo { get; set; }
        public string FechaRegistro { get; set; }
    }
}
