using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Views
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            this.Text = "Sistema de Ventas - Menú Principal";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes form = new frmClientes();
            form.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            frmProductos form = new frmProductos();
            form.ShowDialog();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            frmVentas form = new frmVentas();
            form.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir del sistema?", "Confirmar",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            if (!Dbset.ConexionDB.ProbarConexion())
            {
                MessageBox.Show("No se pudo conectar con la base de datos.\nVerifique la cadena de conexión.",
                    "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
