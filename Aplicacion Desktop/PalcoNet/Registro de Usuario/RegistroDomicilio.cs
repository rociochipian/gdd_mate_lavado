﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PalcoNet.Dominio;

namespace PalcoNet.Registro_de_Usuario
{
    public partial class RegistroDomicilio : MiForm
    {
        Usuario usuario;

        public Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public RegistroDomicilio(MiForm formAnterior, Usuario usuario) : base(formAnterior)
        {
            this.Usuario = usuario;
            InitializeComponent();
        }
        //verifica campos completos
        private bool camposCompletos()
        {
            string error = "";
            int x;
            if (string.IsNullOrWhiteSpace(textBoxCalle.Text)) { error += "El campo 'Calle' no puede estar vacío\n"; }
            if (string.IsNullOrWhiteSpace(textBoxNro.Text)) { error += "El campo 'Número de Calle' no puede estar vacío\n"; }
            if (!int.TryParse(textBoxNro.Text, out x)) { error += "El campo 'Número de Calle' debe ser numerico\n"; }
            if (string.IsNullOrWhiteSpace(textBoxCodigoPostal.Text)) { error += "El campo 'Código Postal' no puede estar vacío\n"; }
            if (string.IsNullOrWhiteSpace(textBoxCiudad.Text)) { error += "El campo 'Ciudad' no puede estar vacío\n"; }

            if (error != "")
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new SeleccionarFuncionalidad().Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
            this.Anterior.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Servidor servidor = Servidor.getInstance();
            bool error = false;
            string mensajeError = " ";

            //como los datos del domicilio son los mismo para ambos tipos se guardan en usuario tambien
            if (this.camposCompletos())
            {
                    string cambioContraseña = "";
                    this.Usuario.Calle = textBoxCalle.Text;
                    this.Usuario.NumeroDeCalle = Int32.Parse(textBoxNro.Text);
                    this.Usuario.Ciudad = textBoxCiudad.Text;
                    if (!string.IsNullOrWhiteSpace(textBoxLocalidad.Text)) { this.Usuario.Localidad = textBoxLocalidad.Text; }
                    if (!string.IsNullOrWhiteSpace(textBoxPiso.Text)) { this.Usuario.Piso = Int32.Parse(textBoxPiso.Text); }
                    this.Usuario.Departamento = textBoxDepto.Text;
                    this.Usuario.CodigoPostal = textBoxCodigoPostal.Text;
                
                //se pasan los parametros al stored procedure y persiste ya sea empresa o cliente
                    if (this.Usuario is Empresa)
                    {


                        if (this.Usuario.IdUsuario == 0)
                        {
                            string query = "'" + this.Usuario.NombreUsuario + "', '" + this.Usuario.Contrasenia + "', '"
                            + ((Empresa)this.Usuario).RazonSocial + "', '" + ((Empresa)this.Usuario).Mail + "', '"
                            + ((Empresa)this.Usuario).Cuit + "', '" + this.Usuario.Calle + "', '" + this.Usuario.NumeroDeCalle + "', '" + this.Usuario.Piso
                            + "', " + this.Usuario.Departamento + ", '" + Usuario.CodigoPostal + "', " + this.Usuario.DebeCambiarContraseña + ", '" + Sesion.getInstance().fecha.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                            Console.WriteLine(query);

                            try
                            {
                                servidor.realizarQuery("EXEC MATE_LAVADO.registroEmpresa_sp " + query);
                                if (this.Usuario.DebeCambiarContraseña) { cambioContraseña += " Deberá utilizar su CUIT como nombre de usuario y contraseña la primera vez que ingrese."; }
                            }
                            catch (Exception ee)
                            {
                                error = true;
                                mensajeError += ee.Message;
                            }
                        } else {
                            string query = "'" + this.Usuario.IdUsuario + "', '"
                            + ((Empresa)this.Usuario).RazonSocial + "', '" + ((Empresa)this.Usuario).Mail + "', '"
                            + ((Empresa)this.Usuario).Cuit + "', '" + this.Usuario.Calle + "', '" + this.Usuario.NumeroDeCalle + "', '" + this.Usuario.Piso
                            + "', " + this.Usuario.Departamento + ", '" + Usuario.CodigoPostal + "', '" + Sesion.getInstance().fecha.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                            Console.WriteLine(query);

                            try
                            {
                                servidor.realizarQuery("EXEC MATE_LAVADO.registroEmpresaConUsuario_sp " + query);
                                //if (this.Usuario.DebeCambiarContraseña) { cambioContraseña += " Deberá utilizar su CUIT como nombre de usuario y contraseña la primera vez que ingrese."; }
                            }
                            catch (Exception ee)
                            {
                                error = true;
                                mensajeError += ee.Message;
                            }
                        }

                        //atrapar error y mostrar mensaje si la empresa ya existe
                    }
                    else
                    {
                        if (this.Usuario.IdUsuario == 0)
                        {
                        
                            Console.Write("es 0");
                            string queryCli = "'" + this.Usuario.NombreUsuario + "', '" + this.Usuario.Contrasenia + "', '"
                                    + ((Cliente)this.Usuario).Nombre + "', '" + ((Cliente)this.Usuario).Apellido + "', '"
                                    + ((Cliente)this.Usuario).TipoDocumento + "', '" + ((Cliente)this.Usuario).NumeroDeDocumento + "', '"
                                    + ((Cliente)this.Usuario).Cuil + "', '" + ((Cliente)this.Usuario).Mail + "', '" + ((Cliente)this.Usuario).Telefono + "', '"
                                    + ((Cliente)this.Usuario).FechaDeNacimiento.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.Usuario.Calle + "','" + this.Usuario.NumeroDeCalle + "', '"
                                    + this.Usuario.Piso + "', " + this.Usuario.Departamento + ", '" + Usuario.CodigoPostal + "', " + this.Usuario.DebeCambiarContraseña
                                    + ", '" + Sesion.getInstance().fecha.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                            Console.WriteLine(queryCli);

                            try
                            {
                                servidor.realizarQuery("EXEC MATE_LAVADO.registroCliente_sp " + queryCli);
                                if (this.Usuario.DebeCambiarContraseña) { cambioContraseña += " Deberá utilizar su DNI como nombre de usuario y contraseña la primera vez que ingrese."; }
                            }
                            catch (Exception eee)
                            {
                                error = true;
                                mensajeError += eee.Message;
                            }
                        }
                        else
                        {
                            string queryCli = "'" + this.Usuario.IdUsuario + "', '"
                                    + ((Cliente)this.Usuario).Nombre + "', '" + ((Cliente)this.Usuario).Apellido + "', '"
                                    + ((Cliente)this.Usuario).TipoDocumento + "', '" + ((Cliente)this.Usuario).NumeroDeDocumento + "', '"
                                    + ((Cliente)this.Usuario).Cuil + "', '" + ((Cliente)this.Usuario).Mail + "', '" + ((Cliente)this.Usuario).Telefono + "', '"
                                    + ((Cliente)this.Usuario).FechaDeNacimiento.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.Usuario.Calle + "','" + this.Usuario.NumeroDeCalle + "', '"
                                    + this.Usuario.Piso + "', " + this.Usuario.Departamento + ", '" + Usuario.CodigoPostal
                                    + "', '" + Sesion.getInstance().fecha.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                            Console.WriteLine(queryCli);

                            try
                            {
                                servidor.realizarQuery("EXEC MATE_LAVADO.registroClienteConUsuario_sp " + queryCli);
                                //if (this.Usuario.DebeCambiarContraseña) { cambioContraseña += " Deberá utilizar su DNI como nombre de usuario y contraseña la primera vez que ingrese."; }
                            }
                            catch (Exception eee)
                            {
                                error = true;
                                mensajeError += eee.Message;
                            }
                        }     
                    }

                    if (!error)
                    {
                        MessageBox.Show("El usuario se creó exitosamente.\n" + cambioContraseña, "Creación completa", MessageBoxButtons.OK);
                    }
                    else {
                        MessageBox.Show("No se pudo crear el usuario.\n" + mensajeError, "Error", MessageBoxButtons.OK);
                    }

                
                    this.Hide();
                    new SeleccionarFuncionalidad().Show();
                    //this.cerrarAnteriores();
                }

            
             
        }

        private void textBoxCiudad_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxCodigoPostal_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
