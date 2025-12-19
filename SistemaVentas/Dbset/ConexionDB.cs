using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SistemaVentas.Dbset
{
    public class ConexionDB
    {
        
        private static string cadenaConexion = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar con la base de datos: " + ex.Message);
            }
        }

        public static bool ProbarConexion()
        {
            try
            {
                using (SqlConnection conexion = ObtenerConexion())
                {
                    return conexion.State == System.Data.ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
