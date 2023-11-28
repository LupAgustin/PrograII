using EjercicioFacturacion.Servicios;
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
    public partial class FrmPrincipal : Form
    {
        private FabricaServicio fab;
        public FrmPrincipal(FabricaServicio fabricaServicio)
        {
            InitializeComponent();
            fab = fabricaServicio;

        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void nuevaFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNuevaFactura n = new FrmNuevaFactura(fab);
            n.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro que quiere Salir de la Aplicacion?", "Saliendo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }
    }
}
