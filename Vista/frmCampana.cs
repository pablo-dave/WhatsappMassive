using System;
using System.IO;
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
    public partial class frmCampana : Form
    {
        private DataTable dt = new DataTable();
        private string path = AppDomain.CurrentDomain.BaseDirectory;

        public frmCampana()
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

            if (result == DialogResult.OK)
            {
                try
                {
                    foreach (DataGridViewRow row in datalistado.Rows)
                    {
                        Boolean marcado = Convert.ToBoolean(row.Cells["Eliminar"].Value);
                        if (marcado)
                        {
                            int onekey = Convert.ToInt32(row.Cells["id"].Value);
                            Campana campana = new Campana();
                            CampanaController func = new CampanaController();

                            campana.id = onekey;

                            if (func.eliminar(campana))
                            {
                                cbEliminar.Checked = false;
                            }
                            else
                            {
                                MessageBox.Show("Campana no fue eliminado", "Eliminando Registros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    mostrar();
                }
                catch (Exception ex)
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
        public void limpiar()
        {
            btnGuardar.Visible = true;
            btnEditar.Visible = false;
            txtIdPaciente.Text = "";
            txtNombre.Text = "";
            txtMensaje.Text = "";
            txtImagenName.Text = "";
            imagen.Image = null;
        }

        public void mostrar()
        {
            try
            {
                CampanaController func = new CampanaController();
                dt = func.mostrar();
                datalistado.Columns["Eliminar"].Visible = false;

                if (dt.Rows.Count != 0)
                {
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

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
            btnEditar.Enabled = false;
            buscar();
        }

        private void buscar()
        {
            try
            {
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
            }
            catch (Exception e)
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
            if (this.ValidateChildren() == true && txtNombre.Text != "" && txtMensaje.Text != "")
            {
                try
                {
                    Campana dts = new Campana();
                    CampanaController func = new CampanaController();
                    dts.nombre = txtNombre.Text;
                    dts.mensaje = txtMensaje.Text;

                    //guardar imagen
                    MemoryStream ms = new MemoryStream();
                    if(imagen.Image != null)
                    {
                        dts.imagen = txtImagenName.Text;
                        imagen.Image.Save(ms, imagen.Image.RawFormat);

                        FileStream file = new FileStream(path+dts.imagen, FileMode.Create, FileAccess.Write);
                        ms.WriteTo(file);
                        file.Close();
                    }
                    else
                    {
                        MessageBox.Show("Seleccione una imagen","Aviso");
                        return; 
                    }

                    if (func.insertar(dts))
                    {
                        MessageBox.Show("Campana registrado correctamente", "Guardando registros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnNuevo.Visible = true;
                        mostrar();
                        limpiar();
                    }
                }
                catch (Exception ex)
                {
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
            txtMensaje.Enabled = true;

            try
            {
                txtIdPaciente.Text = datalistado.SelectedCells[1].Value.ToString();
                txtNombre.Text = datalistado.SelectedCells[2].Value.ToString();
                txtMensaje.Text = datalistado.SelectedCells[3].Value.ToString();
                txtImagenName.Text = datalistado.SelectedCells[4].Value.ToString();

                imagen.BackgroundImage = null;

                imagen.Image = Image.FromFile(path + datalistado.SelectedCells[4].Value.ToString());
                imagen.SizeMode = PictureBoxSizeMode.StretchImage;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }            

            btnNuevo.Visible = true;
            btnGuardar.Visible = false;
            btnGuardar.Visible = false;
            btnEditar.Visible = true;
            btnEditar.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Realmente desea editar los datos del Campana", "Modificando Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                if (this.ValidateChildren() == true && txtNombre.Text != "" && txtMensaje.Text != "")
                {
                    try
                    {
                        Campana dts = new Campana();
                        CampanaController func = new CampanaController();

                        dts.id = Convert.ToInt32(txtIdPaciente.Text);
                        dts.nombre = txtNombre.Text;
                        dts.mensaje = txtMensaje.Text;

                        if (func.editar(dts))
                        {
                            MessageBox.Show("Campana modificado correctamente", "Modificando registros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mostrar();
                            limpiar();
                        }
                        else
                        {
                            MessageBox.Show("Campana no fue modificado", "Ingrese de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            mostrar();
                            limpiar();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
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

        private void btnEnviarCampana_Click(object sender, EventArgs e)
        {
            frmEnviarCampana fc = new frmEnviarCampana(this);
            fc.ShowDialog();
        }

        private void frmCampana_FormClosing(object sender, FormClosingEventArgs e)
        {
            // mostrar nuevamente los iconos de fondo
            Form1 frm = this.MdiParent as Form1;
            if(frm != null)
            {
                frm.showIcons();
            }
        }

        private void btncargar_Click(object sender, EventArgs e)
        {
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                imagen.BackgroundImage = null;
                imagen.Image = new Bitmap(dlg.FileName);
                imagen.SizeMode = PictureBoxSizeMode.StretchImage;

                txtImagenName.Text = dlg.SafeFileName;
            }
        }
    }
}
