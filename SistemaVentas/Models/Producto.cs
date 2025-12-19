using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }

        public Producto() { }

        public Producto(int id, string nombre, string categoria)
        {
            IdProducto = id;
            Nombre = nombre;
            Categoria = categoria;
        }
    }
}
