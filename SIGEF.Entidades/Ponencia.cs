using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SIGEF.Entidades
{
    public class Ponencia
    {
        public int IdPonencia { get; set; }
        public int IdLinea { get; set; }
        public int IdEstatus { get; set; }
        public int IdUsuario { get; set; }
        public string Tema { get; set; }
        public string Descripcion { get; set; }
        public bool? Concentimiento { get; set; }
        public string FechaRegistro { get; set; }
        public string Observaciones { get; set; }

        /*Campos para detalle*/
        public string Linea { get; set; }
        public string Estatus { get; set; }
        /*-------------------*/
        public List<PonenciaColaborador> Colaboradores { get; set; }
        public List<PonenciaArchivo> Archivos { get; set; }

        public Ponencia()
        {
            this.Colaboradores = new List<PonenciaColaborador>();
            this.Archivos = new List<PonenciaArchivo>();
        }
    }
}
