using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsappMassive.Controlador;
using WhatsappMassive.Modelo;

namespace WhatsappMassive.Vista
{
    public partial class frmContacto : Form
    {
        private DataTable dt = new DataTable();

        public frmContacto()
        {
            InitializeComponent();
            limpiar();
            mostrar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Realmente desea eliminar los Registros seleccionados?", "Eliminando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if(result == DialogResult.OK)
            {
                try {
                    foreach (DataGridViewRow row in datalistado.Rows)
                    {
                        Boolean marcado = Convert.ToBoolean(row.Cells["Eliminar"].Value);
                        if (marcado)
                        {
                            int onekey = Convert.ToInt32(row.Cells["id"].Value);
                            Contacto contacto = new Contacto();
                            ContactoController func = new ContactoController();

                            contacto.id = onekey;

                            if (func.eliminar(contacto))
                            {
                                cbEliminar.Checked = false;
                            }
                            else
                            {
                                MessageBox.Show("Contacto no fue eliminado", "Eliminando Registros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    mostrar();
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                
            }
            else
            {
                MessageBox.Show("Cancelar eliminar registros", "Eliminando Registros", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNombre.Focus();
            limpiar();
            mostrar();

        }

        public void limpiar() {
            btnGuardar.Visible = true;
            btnEditar.Visible = false;
            txtIdPaciente.Text = "";
            txtNombre.Text = "";
            txtCelular.Text = "";
        }

        public void mostrar() {
            try {
                ContactoController func = new ContactoController();
                dt = func.mostrar();
                datalistado.Columns["Eliminar"].Visible = false;

                if (dt.Rows.Count != 0) {
                    datalistado.DataSource = dt;
                    txtBuscar.Enabled = true;
                    datalistado.ColumnHeadersVisible = true;
                    inexistente.Visible = true;
                }
                else
                {
                    datalistado.DataSource = null;
                    txtBuscar.Enabled = false;
                    datalistado.ColumnHeadersVisible = false;
                    inexistente.Visible = true;
                }

            } catch (Exception e) {
                MessageBox.Show(e.Message,"Error");
            }
            btnEditar.Enabled = false;
            buscar();
        }

        private void buscar()
        {
            try {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt.Copy());
                DataView dv = new DataView(ds.Tables[0]);
                dv.RowFilter = cbocampo.Text + " like '%" + txtBuscar.Text + "%'";

                if (dv.Count != 0)
                {
                    inexistente.Visible = false;
                    datalistado.DataSource = dv;
                    ocultarColumnas();
                }
                else
                {
                    inexistente.Visible = true;
                    datalistado.DataSource = null;
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void ocultarColumnas()
        {
            datalistado.Columns[1].Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren() == true && txtNombre.Text != "" && txtCelular.Text != "") {
                try {
                    Contacto dts = new Contacto();
                    ContactoController func = new ContactoController();
                    dts.nombre = txtNombre.Text;
                    dts.celular = txtCelular.Text;

                    if (func.insertar(dts)) {
                        MessageBox.Show("Contacto registrado correctamente", "Guardando registros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnNuevo.Visible = true;
                        mostrar();
                        limpiar();
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }

        private void datalistado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Enabled = true;
            txtCelular.Enabled = true;

            txtIdPaciente.Text = datalistado.SelectedCells[1].Value.ToString();
            txtNombre.Text = datalistado.SelectedCells[2].Value.ToString();
            txtCelular.Text = datalistado.SelectedCells[3].Value.ToString();

            btnNuevo.Visible = true;
            btnGuardar.Visible = false;
            btnGuardar.Visible = false;
            btnEditar.Visible = true;
            btnEditar.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Realmente desea editar los datos del Contacto", "Modificando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK) {
                if(this.ValidateChildren() == true && txtNombre.Text != "" && txtCelular.Text != "")
                {
                    try
                    {
                        Contacto dts = new Contacto();
                        ContactoController func = new ContactoController();

                        dts.id = Convert.ToInt32(txtIdPaciente.Text);
                        dts.nombre = txtNombre.Text;
                        dts.celular = txtCelular.Text;

                        if (func.editar(dts))
                        {
                            MessageBox.Show("Contacto modificado correctamente", "Modificando registros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mostrar();
                            limpiar();
                        }
                        else
                        {
                            MessageBox.Show("Contacto no fue modificado", "Ingrese de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            mostrar();
                            limpiar();
                        }

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message,"Error");
                    }
                }
                else
                {
                    MessageBox.Show("Falta ingresar algunos datos", "Modificando registros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEliminar.CheckState == CheckState.Checked)
            {
                datalistado.Columns["Eliminar"].Visible = true;
            }
            else
            {
                datalistado.Columns["Eliminar"].Visible = false;
            }

        }

        private void datalistado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.datalistado.Columns["Eliminar"].Index)
            {
                //DataGridViewCheckBoxCell chkcell = this.datalistado.Rows[e.RowIndex].Cells("Eliminar");
                DataGridViewCheckBoxCell chkcell = this.datalistado.CurrentCell as DataGridViewCheckBoxCell;
                chkcell.Value = !(Convert.ToBoolean(chkcell.Value));
            }
        }

        private void frmContacto_FormClosing(object sender, FormClosingEventArgs e)
        {
            // mostrar nuevamente los iconos de fondo
            Form1 frm = this.MdiParent as Form1;
            if (!frm.HasChildren)
            {
                if (frm != null)
                {
                    frm.showIcons();
                }
            }
            
        }
    }
}
