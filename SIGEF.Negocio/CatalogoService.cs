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
    public class CatalogoService
    {
        private readonly DBSIGFContext _Context;

        public CatalogoService( DBSIGFContext context )
        {
            this._Context = context;
        }

        public List<Linea> DdlLinea() 
        {
            List<Linea> lineas = new List<Linea>();

            SqlParameter[] parameters = {
                new SqlParameter("@Accion",3)
            };

            DataTable Dta = _Context.ExecuteReader("spCatalogos", parameters);
            lineas = (from DataRow dr in Dta.Rows
                      select new Linea()
                      {
                          IdLinea = (int)dr["IdLinea"],
                          Siglas = (string)dr["CodLinea"],
                          Descripcion = dr["Descripcion"].ToString()

                      }).ToList();

            return lineas;
        }

        public List<TipoExpositor> DdlTipoExpositor()
        {
            List<TipoExpositor> expositors = new List<TipoExpositor>();

            SqlParameter[] parameters = {
                new SqlParameter("@Accion",1)
            };

            DataTable Dta = _Context.ExecuteReader("spCatalogos", parameters);
            expositors = (from DataRow dr in Dta.Rows
                      select new TipoExpositor()
                      {
                          IdTipoExpositor = (int)dr["IdTipoColaborador"],
                          Descripcion = dr["Descripcion"].ToString()

                      }).ToList();

            return expositors;
        }

        public List<TipoExpositor> DdlTipoArchivo()
        {

            SqlParameter[] parameters = {
                new SqlParameter("@Accion",2)
            };

            DataTable Dta = _Context.ExecuteReader("spCatalogos", parameters);
            var expositors = (from DataRow dr in Dta.Rows
                          select new TipoExpositor()
                          {
                              IdTipoExpositor = (int)dr["IdTipoArchivo"],
                              Descripcion = dr["Descripcion"].ToString()

                          }).ToList();

            return expositors;
        }
    }
}
