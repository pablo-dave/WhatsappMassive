using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsappMassive.Controlador;
using WhatsappMassive.Modelo;

namespace WhatsappMassive.Vista
{
    public partial class frmEnviarCampana : Form
    {
        private frmCampana formCampana = new frmCampana();
        private DataTable dt = new DataTable();
        private string path = AppDomain.CurrentDomain.BaseDirectory;

        public frmEnviarCampana(frmCampana _formCampana)
        {
            InitializeComponent();
            this.formCampana = _formCampana;
        }

        public frmEnviarCampana()
        {
            InitializeComponent();
        }

        

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnEnviarCampana_Click(object sender, EventArgs e)
        {
            bool wc = writeCampanaJson();
            bool wcp = writeCelPhones();

            Process.Start(path+"prueba.exe");

            if(wc && wcp)
            {
                MessageBox.Show("Ejecutando Whatsapp...","Información");
            }
            else
            {
                MessageBox.Show("Error al generar archivos base", "Error");
            }
        }

        private Boolean writeCampanaJson()
        {
            bool result = false;           

            try
            {
                CampanaToSend cmp = new CampanaToSend();

                cmp.mensaje = formCampana.txtMensaje.Text;
                cmp.foto = formCampana.txtImagenName.Text;

                string jsonFile = JsonConvert.SerializeObject(cmp);
                //IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
                //IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("users.json", FileMode.Create, isoStore);

                TextWriter writer;
                using (writer = new StreamWriter(path+"campana.json", append: false))
                {
                    writer.WriteLine(jsonFile);
                }
                result = true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                result = false;
            }           

            return result;
        }

        private bool writeCelPhones()
        {
            bool result = false;
            ContactoController func = new ContactoController();

            try
            {
                dt = func.mostrar();

                string jsonFile = "";
                foreach (DataRow row in dt.Rows)
                {
                    string cell = row[2].ToString();

                    string cellPhone = Regex.Match(cell, @"(.{9})\s*$").ToString();

                    jsonFile += cellPhone + Environment.NewLine;
                }

                TextWriter writer;
                using (writer = new StreamWriter(path + "basecamoana.txt", append: false))
                {
                    writer.WriteLine(jsonFile);
                }
                result = true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                result = false;
            }

            return result;
        }
        
    }
}

