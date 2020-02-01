using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WhatsappMassive.Modelo;
using MySql.Data;
using System.Data;
using System.Windows.Forms;

namespace WhatsappMassive.Controlador
{
    class CampanaController:Conexion
    {
        MySqlCommand cmd;

        public DataTable mostrar()
        {
            try
            {
                conectado();
                cmd = new MySqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = "mostrar_campana";
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
            finally
            {
                desconectado();
            }
        }

        public Boolean insertar(Campana dts)
        {
            try
            {
                conectado();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = "insertar_campana";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", dts.nombre);
                cmd.Parameters.AddWithValue("@mensaje", dts.mensaje);
                cmd.Parameters.AddWithValue("@imagen", dts.imagen);

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

        public Boolean editar(Campana dts)
        {
            try
            {
                conectado();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = "editar_campana";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_id", dts.id);
                cmd.Parameters.AddWithValue("@nombre", dts.nombre);
                cmd.Parameters.AddWithValue("@mensaje", dts.mensaje);
                cmd.Parameters.AddWithValue("@imagen", dts.imagen);

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
        public Boolean eliminar(Campana dts)
        {
            try
            {
                conectado();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = "eliminar_campana";
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
