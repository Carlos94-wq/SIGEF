using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEF.Negocio.CustomEntites
{
    public class PonenciaListModel
    {
        public int IdPonencia { get; set; }
        public string Tema { get; set; }
        public string Descripcion { get; set; }
        public string FechaRegistro { get; set; }
        public string Linea { get; set; }
        public string Estatus { get; set; }
    }
}
