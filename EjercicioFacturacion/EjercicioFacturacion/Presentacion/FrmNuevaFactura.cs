using EjercicioFacturacion.Entidades;
using EjercicioFacturacion.Servicios;
using EjercicioFacturacion.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjercicioFacturacion.Presentacion
{
    public partial class FrmNuevaFactura : Form
    {
        IServicio servicio;
        Factura facturanueva = null;

        public enum Estado
        {
            nuevo,
            existente
        }

        public FrmNuevaFactura(FabricaServicio fab)
        {
            InitializeComponent();
            servicio = fab.CrearServicio();
            facturanueva = new Factura();
        }

        private void FrmNuevaFactura_Load(object sender, EventArgs e)
        {
            cargarComboFormas();
            cargarComboArticulos();
        }

        private void cargarComboArticulos()
        {
            cboArticulos.DataSource = servicio.ObtenerArticulos();
            cboArticulos.ValueMember = "NroArt";
            cboArticulos.DisplayMember = "Nombre";
        }

        private void cargarComboFormas()
        {
            cboFormasPago.DataSource = servicio.ObtenerFormas();
            cboFormasPago.ValueMember = "id_formapago";
            cboFormasPago.DisplayMember = "nom_forma";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboArticulos.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un producto...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("Debe ingresar una cantidad válida...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (DataGridViewRow row in dgvArticulos.Rows)
            {
                if (row.Cells["ColArticulo"].Value.ToString().Equals(cboArticulos.Text) && row.Cells["ColCantidad"].Value.ToString().Equals(txtCantidad.Text))
                {
                    MessageBox.Show("Este producto ya está presupuestado...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            Articulo a = (Articulo)cboArticulos.SelectedItem;
            double cant = double.Parse(txtCantidad.Text);
            //DetalleFactura df = new DetalleFactura(a, cant);
            DetalleFactura df = new DetalleFactura();
            df.Art = a;
            df.Cantidad = cant;
            facturanueva.AgregarDetalle(df);
            dgvArticulos.Rows.Add(new object[] { a.Nombre, a.Precio, cant, "Quitar" });
            calcularTotal();
                

            
        }

        private void calcularTotal()
        {
            txtTotal.Text = facturanueva.CalcularTotal().ToString();
        }        

        private void dgvArticulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvArticulos.CurrentCell.ColumnIndex == 3)
            {
                facturanueva.QuitarDetalle(dgvArticulos.CurrentRow.Index);
                dgvArticulos.Rows.RemoveAt(dgvArticulos.CurrentRow.Index);
                calcularTotal();
            }
        }

        

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea cerrar el programa?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { this.Close(); }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCliente.Text))
            {
                MessageBox.Show("Debe ingresar un cliente...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgvArticulos.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos un detalle...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            cargarFactura();
        }

        private void cargarFactura()
        {
            facturanueva.Fecha = Convert.ToDateTime(dtpFecha.Text);
            facturanueva.FPago.NroForma = cboFormasPago.SelectedIndex + 1;
            facturanueva.FPago.Nombre = cboFormasPago.Text.ToString();
            facturanueva.Cliente = txtCliente.Text;
            if (servicio.GuardarFactura(facturanueva))
            {
                MessageBox.Show("Se ingreso la Factura con exito!", "Success!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("No se pudo ingresar la factura", "Failure!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
            }

        }
    }
}
