using SistemaVentas.Controllers;
using SistemaVentas.Models;
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
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
            this.Text = "Gestión de Clientes";
            this.StartPosition = FormStartPosition.CenterScreen;
            ConfigurarDataGridView();
            CargarDatos();
        }

        private void ConfigurarDataGridView()
        {
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;
            dgvClientes.ReadOnly = true;
            dgvClientes.AllowUserToAddRows = false;
        }

        private void CargarDatos()
        {
            try
            {
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = ClientesController.Seleccionar();

                if (dgvClientes.Columns.Count > 0)
                {
                    dgvClientes.Columns["IdCliente"].HeaderText = "ID";
                    dgvClientes.Columns["Nombre"].HeaderText = "Nombre";
                    dgvClientes.Columns["Ciudad"].HeaderText = "Ciudad";
                }
                dgvClientes.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvClientes.Rows[e.RowIndex];
                txtId.Text = row.Cells["IdCliente"].Value.ToString();
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtCiudad.Text = row.Cells["Ciudad"].Value.ToString();

                
                txtId.Enabled = false;
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    MessageBox.Show("Ingrese el ID del cliente", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtId.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Ingrese el nombre del cliente", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCiudad.Text))
                {
                    MessageBox.Show("Ingrese la ciudad del cliente", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCiudad.Focus();
                    return;
                }

                Cliente cliente = new Cliente
                {
                    IdCliente = int.Parse(txtId.Text),
                    Nombre = txtNombre.Text.Trim(),
                    Ciudad = txtCiudad.Text.Trim()
                };

                if (ClientesController.Insertar(cliente))
                {
                    MessageBox.Show("Cliente insertado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarDatos();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("El ID debe ser un número entero válido", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    MessageBox.Show("Seleccione un cliente de la lista para actualizar", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Ingrese el nombre del cliente", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCiudad.Text))
                {
                    MessageBox.Show("Ingrese la ciudad del cliente", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCiudad.Focus();
                    return;
                }

                DialogResult resultado = MessageBox.Show(
                    "¿Está seguro de actualizar los datos del cliente?",
                    "Confirmar Actualización",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    Cliente cliente = new Cliente
                    {
                        IdCliente = int.Parse(txtId.Text),
                        Nombre = txtNombre.Text.Trim(),
                        Ciudad = txtCiudad.Text.Trim()
                    };

                    if (ClientesController.Actualizar(cliente))
                    {
                        MessageBox.Show("Cliente actualizado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarDatos();
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("El ID debe ser un número entero válido", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClientes.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["IdCliente"].Value);
                    string nombre = dgvClientes.SelectedRows[0].Cells["Nombre"].Value.ToString();

                    DialogResult resultado = MessageBox.Show(
                        $"¿Está seguro de eliminar el cliente '{nombre}'?\n\nEsta acción no se puede deshacer.",
                        "Confirmar Eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        if (ClientesController.Eliminar(id))
                        {
                            MessageBox.Show("Cliente eliminado correctamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarDatos();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un cliente de la lista", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message +
                    "\n\nVerifique que el cliente no tenga ventas registradas.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtCiudad.Clear();
            txtId.Enabled = true; 
            txtId.Focus();
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}