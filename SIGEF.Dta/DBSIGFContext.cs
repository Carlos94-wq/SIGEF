using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SIGEF.Dta
{
    public class DBSIGFContext
    {
        private readonly String ConnectionStrings;
        public DBSIGFContext()
        {
            this.ConnectionStrings = ConfigurationManager.ConnectionStrings["Pruebas"].ToString();
        }

        public DataTable ExecuteReader(string sentencia, SqlParameter[] Parametro = null)
        {
            //Objetos requeridos 
            SqlCommand comando = new SqlCommand(); //Ejecuta los comnados de sql
            SqlConnection Con = new SqlConnection(); //Establece Conexione

            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable Datos = new DataTable();

            try //Clausupa tray para detener cualquier error
            {
                Con.ConnectionString = ConnectionStrings;
                Con.Open();

                comando.Connection = Con;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = sentencia;

                if (Parametro != null)
                {
                    comando.Parameters.AddRange(Parametro.ToArray());
                }
                Da.SelectCommand = comando;
                Da.Fill(Datos);

                return Datos;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Con.Close();
            }
        }//Lectura

        public int ExecuteNonQuery(string sentencia, SqlParameter[] parametros = null)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            int respuesta = 0;

            try
            {
                con.ConnectionString = ConnectionStrings;
                con.Open();

                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sentencia;

                if (parametros != null)
                {
                    cmd.Parameters.AddRange(parametros.ToArray());

                    respuesta = cmd.ExecuteNonQuery();
                }

                return respuesta;
            }
            catch (SqlException e)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }//Manipulacion de datos
    }
}
