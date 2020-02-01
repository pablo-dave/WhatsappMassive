using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsappMassive.Modelo;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using System.Windows.Forms;

namespace WhatsappMassive.Controlador
{
    class ContactoController:Conexion
    {
        MySqlCommand cmd;

        public DataTable mostrar() {
            try
            {
                conectado();
                cmd = new MySqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = "mostrar_contacto";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return null;
            }
            finally {
                desconectado();
            }
        }

        public Boolean insertar(Contacto dts) {
            try
            {
                conectado();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = "insertar_contacto";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", dts.nombre);
                cmd.Parameters.AddWithValue("@celular", dts.celular);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                else {
                    return false;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return false;
            }
            finally {
                desconectado();
            }
        }

        public Boolean editar(Contacto dts)
        {
            try
            {
                conectado();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = "editar_contacto";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_id", dts.id);
                cmd.Parameters.AddWithValue("@nombre", dts.nombre);
                cmd.Parameters.AddWithValue("@celular", dts.celular);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                else {
                    return false;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return false;
            }
            finally
            {
                desconectado();
            }
        }
        public Boolean eliminar(Contacto dts)
        {
            try
            {
                conectado();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = "eliminar_contacto";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_id", dts.id);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                else {
                    return false;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return false;
            }
            finally
            {
                desconectado();
            }
        }
    }
}
