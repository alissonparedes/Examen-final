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
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
            this.Text = "Gestión de Productos";
            this.StartPosition = FormStartPosition.CenterScreen;
            ConfigurarDataGridView();
            CargarDatos();
        }

        private void ConfigurarDataGridView()
        {
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
        }

        private void CargarDatos()
        {
            try
            {
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = ProductosController.Seleccionar();

                if (dgvProductos.Columns.Count > 0)
                {
                    dgvProductos.Columns["IdProducto"].HeaderText = "ID";
                    dgvProductos.Columns["Nombre"].HeaderText = "Nombre";
                    dgvProductos.Columns["Categoria"].HeaderText = "Categoría";
                }
                dgvProductos.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProductos.Rows[e.RowIndex];
                txtId.Text = row.Cells["IdProducto"].Value.ToString();
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtCategoria.Text = row.Cells["Categoria"].Value.ToString();

                
                txtId.Enabled = false;
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    MessageBox.Show("Ingrese el ID del producto", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtId.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Ingrese el nombre del producto", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCategoria.Text))
                {
                    MessageBox.Show("Ingrese la categoría del producto", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCategoria.Focus();
                    return;
                }

                Producto producto = new Producto
                {
                    IdProducto = int.Parse(txtId.Text),
                    Nombre = txtNombre.Text.Trim(),
                    Categoria = txtCategoria.Text.Trim()
                };

                if (ProductosController.Insertar(producto))
                {
                    MessageBox.Show("Producto insertado correctamente", "Éxito",
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
                    MessageBox.Show("Seleccione un producto de la lista para actualizar", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Ingrese el nombre del producto", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCategoria.Text))
                {
                    MessageBox.Show("Ingrese la categoría del producto", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCategoria.Focus();
                    return;
                }

                
                DialogResult resultado = MessageBox.Show(
                    "¿Está seguro de actualizar los datos del producto?",
                    "Confirmar Actualización",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    Producto producto = new Producto
                    {
                        IdProducto = int.Parse(txtId.Text),
                        Nombre = txtNombre.Text.Trim(),
                        Categoria = txtCategoria.Text.Trim()
                    };

                    if (ProductosController.Actualizar(producto))
                    {
                        MessageBox.Show("Producto actualizado correctamente", "Éxito",
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
                if (dgvProductos.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["IdProducto"].Value);
                    string nombre = dgvProductos.SelectedRows[0].Cells["Nombre"].Value.ToString();

                    DialogResult resultado = MessageBox.Show(
                        $"¿Está seguro de eliminar el producto '{nombre}'?\n\nEsta acción no se puede deshacer.",
                        "Confirmar Eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        if (ProductosController.Eliminar(id))
                        {
                            MessageBox.Show("Producto eliminado correctamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarDatos();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un producto de la lista", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message +
                    "\n\nVerifique que el producto no esté asociado a ventas.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtCategoria.Clear();
            txtId.Enabled = true; 
            txtId.Focus();
        }
    }
}