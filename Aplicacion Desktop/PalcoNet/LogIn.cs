﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using PalcoNet.Dominio;
using PalcoNet.Registro_de_Usuario;

namespace PalcoNet
{
    public partial class LogIn : Form
    {
        
        Sesion sesion = Sesion.getInstance();
        Usuario usuario = new Usuario();

        public Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        public LogIn()
        {
            this.Usuario = usuario;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Servidor servidor = Servidor.getInstance();
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(textBox2.Text.ToString()));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));

                Console.WriteLine("EL HASH ES:" + Sb);
            }
            try
            {

                servidor.realizarQuery("EXEC verificarLogin_sp '" + textBox1.Text.Trim() + "', '" + Sb.ToString() + "' , '" + textBox2.Text.Trim() + "'");
                usuario.NombreUsuario = textBox1.Text.ToString();
                sesion.usuario = this.Usuario;
                List<String> roles = new List<String>();

               
                SqlDataReader reader = servidor.query("EXEC dbo.getRolesDeUsuario_sp '" + sesion.usuario.NombreUsuario + "'");


                while (reader.Read())
                {
                    String rol = reader["nombre"].ToString();
                    roles.Add(rol);
                }
                reader.Close();

                if (roles.Count() > 1)
                {
                      new SeleccionarRol().Show();
                }
                else
                {
                    Console.Write("EL USUARIO ES: " + sesion.usuario.NombreUsuario);
                   // Console.Write("EL ROL ES: " + sesion.rol.Nombre);
                    new SeleccionarFuncionalidad().Show();
                }
            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBoxUsuario.SelectedIndex > -1){
                switch (comboBoxUsuario.Text) { 
                    case "Cliente":
                        new RegistroDeCliente(new Cliente(), null).Show();
                        break;
                    case "Empresa":
                        new RegistroDeEmpresa(new Empresa(), null).Show();
                        break;
                }
                this.Hide();
            }else {
                MessageBox.Show("Se debe seleccionar el tipo de usuario que se quiere crear.", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
