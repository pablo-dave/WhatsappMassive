using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace WhatsappMassive.Modelo
{
    class Conexion
    {
        protected MySqlConnection cnn;

        protected bool conectado() {
            try {
                cnn = new MySqlConnection("data source=localhost; user id=root; password=; database=whatsapp_massive");
                cnn.Open();
                return true;
            }
            catch (Exception e) {
                MessageBox.Show(e.Message,"Error");
                return false;
            }
        }

        protected bool desconectado() {
            bool result = false;
            try {
                if(cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                    result = true;
                }
            } catch (Exception e) {
                MessageBox.Show(e.Message,"Error");
                result = false;
            }
            return result;
        }

    }
}
