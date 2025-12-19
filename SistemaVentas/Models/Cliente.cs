using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }

        public Cliente() { }

        public Cliente(int id, string nombre, string ciudad)
        {
            IdCliente = id;
            Nombre = nombre;
            Ciudad = ciudad;
        }
    }
}
