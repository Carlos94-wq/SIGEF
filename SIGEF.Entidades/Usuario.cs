using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEF.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public string Codigo { get; set; }
        public string Institucion { get; set; }
        public string Pais { get; set; }
        public string Cuidad { get; set; }
        public string Prefijo { get; set; }
        public string SearchValue { get; set; }

        public Estatus Estatus { get; set; }
        public Rol _Rol { get; set; }

        public Usuario()
        {
            this.Estatus = new Estatus();
            this._Rol = new Rol();
        }
    }
}
