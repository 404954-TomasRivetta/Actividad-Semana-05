using modeloParcial.Datos;
using modeloParcial.Entidades;
using modeloParcial.Servicios;
using modeloParcial.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace modeloParcial.Presentaciones
{
    public partial class FrmNuevaOrden : Form
    {

        Orden nueva;

        IServicio servicio = null;


        public FrmNuevaOrden(FabricaServicio fabrica)
        {
            InitializeComponent();

            nueva = new Orden();

            servicio = fabrica.CrearServicio();
        }

        private void FrmNuevaOrden_Load(object sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Now;
            dtpFecha.Enabled = false;
            txtResponsable.Text = string.Empty;
            cboMateriales.SelectedIndex = -1;
            numCantidad.Value = 0;

            CargarMateriales();
        }

        private void CargarMateriales()
        {
            cboMateriales.DataSource = servicio.TraerMateriales();
            cboMateriales.ValueMember = "CodMaterial";
            cboMateriales.DisplayMember = "NomMaterial"; 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro desea cancelar la operacion?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboMateriales.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un producto..", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (numCantidad.Value <= 0)
            {
                MessageBox.Show("Debe ingresar una cantidad valida", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (DataGridViewRow fila in dgvDetalle.Rows)
            {
                if (fila.Cells["ColMaterial"].Value.ToString().Equals(cboMateriales.Text))
                {
                    MessageBox.Show("Este producto ya esta presupuestado..", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            Material m = (Material)cboMateriales.SelectedItem;
            int cant = Convert.ToInt32(numCantidad.Value);
            DetalleOrden detalle = new DetalleOrden(m, cant);

            nueva.AgregarDetalle(detalle);

            dgvDetalle.Rows.Add(new object[] { m.CodMaterial, m.NomMaterial, m.Stock, cant, "Quitar" });
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalle.CurrentCell.ColumnIndex == 4)
            {
                if (MessageBox.Show("Seguro desea quitar el elemento?", "Control", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    nueva.QuitarDetalle(dgvDetalle.CurrentRow.Index);
                    dgvDetalle.Rows.RemoveAt(dgvDetalle.CurrentRow.Index);
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtResponsable.Text))
            {
                MessageBox.Show("Debe ingresar un responsable..", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgvDetalle.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos un material..", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            GrabarOrden();
        }

        private void GrabarOrden()
        {
            nueva.FechaOrden = Convert.ToDateTime(dtpFecha.Value);
            nueva.Responsable = txtResponsable.Text;

            if (servicio.GrabarOrden(nueva))
            {
                MessageBox.Show("Se grabo con exito la orden..",
                  "Informe",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Error al registrar la orden..",
                "Control",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
    }
}
