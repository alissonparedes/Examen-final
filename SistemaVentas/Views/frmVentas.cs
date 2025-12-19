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
    public partial class frmVentas : Form
    {
        public frmVentas()
        {
            InitializeComponent();
            this.Text = "Gestión de Ventas";
            this.StartPosition = FormStartPosition.CenterScreen;
            ConfigurarDataGridView();
            CargarComboClientes();
            CargarComboProductos();
            CargarDatos();
        }

        private void ConfigurarDataGridView()
        {
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.MultiSelect = false;
            dgvVentas.ReadOnly = true;
            dgvVentas.AllowUserToAddRows = false;
        }

        private void CargarDatos()
        {
            try
            {
                dgvVentas.DataSource = null;
                dgvVentas.DataSource = VentasController.Seleccionar();

                if (dgvVentas.Columns.Count > 0)
                {
                    dgvVentas.Columns["IdVenta"].HeaderText = "ID Venta";
                    dgvVentas.Columns["Fecha"].HeaderText = "Fecha";
                    dgvVentas.Columns["NombreCliente"].HeaderText = "Cliente";
                    dgvVentas.Columns["NombreProducto"].HeaderText = "Producto";
                    dgvVentas.Columns["Cantidad"].HeaderText = "Cantidad";
                    dgvVentas.Columns["Total"].HeaderText = "Total";
                    dgvVentas.Columns["Total"].DefaultCellStyle.Format = "C2";                
                    if (dgvVentas.Columns.Contains("IdCliente")) dgvVentas.Columns["IdCliente"].Visible = false;
                    if (dgvVentas.Columns.Contains("IdProducto")) dgvVentas.Columns["IdProducto"].Visible = false;
                }

                dgvVentas.Refresh();          
                decimal totalVentas = VentasController.ObtenerTotalVentas();
                lblTotalVentas.Text = $"Total Ventas: {totalVentas:C2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComboClientes()
        {
            try
            {
                cboClientes.DataSource = ClientesController.Seleccionar();
                cboClientes.DisplayMember = "Nombre";
                cboClientes.ValueMember = "IdCliente";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComboProductos()
        {
            try
            {
                cboProductos.DataSource = ProductosController.Seleccionar();
                cboProductos.DisplayMember = "Nombre";
                cboProductos.ValueMember = "IdProducto";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos()) return;

                Venta venta = new Venta
                {
                    IdVenta = int.Parse(txtId.Text),
                    Fecha = dtpFecha.Value.Date,
                    IdCliente = Convert.ToInt32(cboClientes.SelectedValue),
                    IdProducto = Convert.ToInt32(cboProductos.SelectedValue),
                    Cantidad = int.Parse(txtCantidad.Text),
                    Total = decimal.Parse(txtTotal.Text)
                };

                if (VentasController.Insertar(venta))
                {
                    MessageBox.Show("Venta registrada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarDatos();
                }
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
                    MessageBox.Show("Seleccione una venta de la lista para actualizar", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos()) return;

                DialogResult resultado = MessageBox.Show("¿Está seguro de actualizar esta venta?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    Venta venta = new Venta
                    {
                        IdVenta = int.Parse(txtId.Text),
                        Fecha = dtpFecha.Value.Date,
                        IdCliente = Convert.ToInt32(cboClientes.SelectedValue),
                        IdProducto = Convert.ToInt32(cboProductos.SelectedValue),
                        Cantidad = int.Parse(txtCantidad.Text),
                        Total = decimal.Parse(txtTotal.Text)
                    };

                    if (VentasController.Actualizar(venta))
                    {
                        MessageBox.Show("Venta actualizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarDatos();
                    }
                }
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
                if (dgvVentas.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgvVentas.SelectedRows[0].Cells["IdVenta"].Value);
                    string cliente = dgvVentas.SelectedRows[0].Cells["NombreCliente"].Value.ToString();
                    string producto = dgvVentas.SelectedRows[0].Cells["NombreProducto"].Value.ToString();

                    DialogResult resultado = MessageBox.Show(
                        $"¿Está seguro de eliminar la venta?\n\nCliente: {cliente}\nProducto: {producto}",
                        "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        if (VentasController.Eliminar(id))
                        {
                            MessageBox.Show("Venta eliminada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarDatos();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una venta de la lista", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtId.Text)) { MessageBox.Show("Ingrese ID"); return false; }
            if (cboClientes.SelectedValue == null) { MessageBox.Show("Seleccione cliente"); return false; }
            if (cboProductos.SelectedValue == null) { MessageBox.Show("Seleccione producto"); return false; }

            if (!int.TryParse(txtCantidad.Text, out int cant) || cant <= 0)
            {
                MessageBox.Show("Cantidad debe ser un número mayor a 0");
                return false;
            }
            if (!decimal.TryParse(txtTotal.Text, out decimal tot) || tot <= 0)
            {
                MessageBox.Show("El total debe ser mayor a 0");
                return false;
            }
            return true;
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtCantidad.Clear();
            txtTotal.Clear();
            if (txtPrecioUnitario != null) txtPrecioUnitario.Clear();
            dtpFecha.Value = DateTime.Now;
            txtId.Enabled = true;
            if (cboClientes.Items.Count > 0) cboClientes.SelectedIndex = 0;
            if (cboProductos.Items.Count > 0) cboProductos.SelectedIndex = 0;
            txtId.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void CalcularTotal()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtCantidad.Text) && !string.IsNullOrWhiteSpace(txtPrecioUnitario.Text))
                {
                    int cantidad = int.Parse(txtCantidad.Text);
                    decimal precio = decimal.Parse(txtPrecioUnitario.Text);
                    txtTotal.Text = (cantidad * precio).ToString("N2");
                }
            }
            catch { }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e) => CalcularTotal();
        private void txtPrecioUnitario_TextChanged(object sender, EventArgs e) => CalcularTotal();

        private void frmVentas_Load(object sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Now;
        }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvVentas.Rows[e.RowIndex];
                txtId.Text = row.Cells["IdVenta"].Value.ToString();
                dtpFecha.Value = Convert.ToDateTime(row.Cells["Fecha"].Value);
                cboClientes.SelectedValue = Convert.ToInt32(row.Cells["IdCliente"].Value);
                cboProductos.SelectedValue = Convert.ToInt32(row.Cells["IdProducto"].Value);
                txtCantidad.Text = row.Cells["Cantidad"].Value.ToString();
                txtTotal.Text = row.Cells["Total"].Value.ToString();
                txtId.Enabled = false;
            }
        }
    }
}