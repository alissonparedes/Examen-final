using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public string NombreCliente { get; set; }
        public string NombreProducto { get; set; }

        public Venta() { }

        public Venta(int id, DateTime fecha, int idCliente, int idProducto, int cantidad, decimal total)
        {
            IdVenta = id;
            Fecha = fecha;
            IdCliente = idCliente;
            IdProducto = idProducto;
            Cantidad = cantidad;
            Total = total;
        }
    }
}
