using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaVentas.Models;
using SistemaVentas.Dbset;

namespace SistemaVentas.Controllers
{
    public class VentasController
    {
        
        public static bool Insertar(Venta venta)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "INSERT INTO ventas (id_ventas, fecha, id_clientes, id_productos, cantidad, total) " +
                                   "VALUES (@Id, @Fecha, @IdCliente, @IdProducto, @Cantidad, @Total)";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Id", venta.IdVenta);
                    cmd.Parameters.AddWithValue("@Fecha", venta.Fecha);
                    cmd.Parameters.AddWithValue("@IdCliente", venta.IdCliente);
                    cmd.Parameters.AddWithValue("@IdProducto", venta.IdProducto);
                    cmd.Parameters.AddWithValue("@Cantidad", venta.Cantidad);
                    cmd.Parameters.AddWithValue("@Total", venta.Total);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar venta: " + ex.Message);
            }
        }

       
        public static List<Venta> Seleccionar()
        {
            List<Venta> lista = new List<Venta>();
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = @"SELECT v.id_ventas, v.fecha, v.id_clientes, v.id_productos, 
                                    v.cantidad, v.total, c.nombre, p.nombre 
                                    FROM ventas v
                                    INNER JOIN clientes c ON v.id_clientes = c.id_clientes
                                    INNER JOIN productos p ON v.id_productos = p.id_productos
                                    ORDER BY v.fecha DESC, v.id_ventas DESC";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Venta venta = new Venta
                        {
                            IdVenta = reader.GetInt32(0),
                            Fecha = reader.GetDateTime(1),
                            IdCliente = reader.GetInt32(2),
                            IdProducto = reader.GetInt32(3),
                            Cantidad = reader.GetInt32(4),
                            Total = reader.GetDecimal(5),
                            NombreCliente = reader.GetString(6),
                            NombreProducto = reader.GetString(7)
                        };
                        lista.Add(venta);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al seleccionar ventas: " + ex.Message);
            }
        }

       
        public static bool Actualizar(Venta venta)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "UPDATE ventas SET fecha = @Fecha, id_clientes = @IdCliente, id_productos = @IdProducto, " +
                                   "cantidad = @Cantidad, total = @Total WHERE id_ventas = @Id";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Id", venta.IdVenta);
                    cmd.Parameters.AddWithValue("@Fecha", venta.Fecha);
                    cmd.Parameters.AddWithValue("@IdCliente", venta.IdCliente);
                    cmd.Parameters.AddWithValue("@IdProducto", venta.IdProducto);
                    cmd.Parameters.AddWithValue("@Cantidad", venta.Cantidad);
                    cmd.Parameters.AddWithValue("@Total", venta.Total);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar venta: " + ex.Message);
            }
        }

      
        public static bool Eliminar(int id)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "DELETE FROM ventas WHERE id_ventas = @Id";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Id", id);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar venta: " + ex.Message);
            }
        }

        public static decimal ObtenerTotalVentas()
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "SELECT ISNULL(SUM(total), 0) FROM ventas";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener total de ventas: " + ex.Message);
            }
        }
    }
}