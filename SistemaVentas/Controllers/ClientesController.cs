using SistemaVentas.Dbset;
using SistemaVentas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SistemaVentas.Controllers
{
    public class ClientesController
    {
        
        public static bool Insertar(Cliente cliente)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "INSERT INTO clientes (id_clientes, nombre, ciudad) VALUES (@Id, @Nombre, @Ciudad)";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Id", cliente.IdCliente);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Ciudad", cliente.Ciudad);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar cliente: " + ex.Message);
            }
        }

       
        public static List<Cliente> Seleccionar()
        {
            List<Cliente> lista = new List<Cliente>();
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "SELECT id_clientes, nombre, ciudad FROM clientes ORDER BY id_clientes";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            IdCliente = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Ciudad = reader.GetString(2)
                        };
                        lista.Add(cliente);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al seleccionar clientes: " + ex.Message);
            }
        }

        
        public static bool Actualizar(Cliente cliente)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "UPDATE clientes SET nombre = @Nombre, ciudad = @Ciudad WHERE id_clientes = @Id";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Id", cliente.IdCliente);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Ciudad", cliente.Ciudad);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar cliente: " + ex.Message);
            }
        }

        
        public static bool Eliminar(int id)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "DELETE FROM clientes WHERE id_clientes = @Id";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Id", id);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cliente: " + ex.Message);
            }
        }

      
        public static Cliente BuscarPorId(int id)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    string query = "SELECT id_clientes, nombre, ciudad FROM clientes WHERE id_clientes = @Id";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new Cliente
                        {
                            IdCliente = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Ciudad = reader.GetString(2)
                        };
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cliente: " + ex.Message);
            }
        }
    }
}