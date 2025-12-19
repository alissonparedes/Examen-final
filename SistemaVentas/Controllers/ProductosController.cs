using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaVentas.Models;
using SistemaVentas.Dbset;

namespace SistemaVentas.Controllers
{
    public class ProductosController
    {
        
        public static bool Insertar(Producto producto)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "INSERT INTO productos (id_productos, nombre, categoria) VALUES (@Id, @Nombre, @Categoria)";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Id", producto.IdProducto);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Categoria", producto.Categoria);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar producto: " + ex.Message);
            }
        }

       
        public static List<Producto> Seleccionar()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "SELECT id_productos, nombre, categoria FROM productos ORDER BY id_productos";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto producto = new Producto
                        {
                            IdProducto = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Categoria = reader.GetString(2)
                        };
                        lista.Add(producto);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al seleccionar productos: " + ex.Message);
            }
        }

        
        public static bool Actualizar(Producto producto)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "UPDATE productos SET nombre = @Nombre, categoria = @Categoria WHERE id_productos = @Id";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Id", producto.IdProducto);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Categoria", producto.Categoria);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar producto: " + ex.Message);
            }
        }

        
        public static bool Eliminar(int id)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "DELETE FROM productos WHERE id_productos = @Id";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Id", id);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar producto: " + ex.Message);
            }
        }

        
        public static Producto BuscarPorId(int id)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "SELECT id_productos, nombre, categoria FROM productos WHERE id_productos = @Id";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new Producto
                        {
                            IdProducto = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Categoria = reader.GetString(2)
                        };
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar producto: " + ex.Message);
            }
        }
    }
}