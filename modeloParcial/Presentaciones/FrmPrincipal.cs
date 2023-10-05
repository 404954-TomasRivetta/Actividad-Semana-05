using modeloParcial.Presentaciones;
using modeloParcial.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace modeloParcial
{
    public partial class FrmPrincipal : Form
    {

        FabricaServicio fabrica = null;

        public FrmPrincipal(FabricaServicio fabrica)
        {
            InitializeComponent();

            this.fabrica = fabrica;

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro que desea salir?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void nuevaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNuevaOrden nuevaOrden = new FrmNuevaOrden(fabrica);
            nuevaOrden.ShowDialog();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReporteStock reporteStock = new FrmReporteStock();
            reporteStock.ShowDialog();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
