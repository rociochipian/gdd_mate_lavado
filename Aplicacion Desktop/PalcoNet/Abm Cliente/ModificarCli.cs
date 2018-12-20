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
using System.Data.SqlClient;

namespace PalcoNet.Abm_Cliente
{
    public partial class ModificarCli : MiForm
    {
        bool fueModificado = false;
        Cliente clienteViejo;
        Servidor servidor = Servidor.getInstance();
        SqlDataReader readerCliente;
        String calle;
        Int32 numeroCalle;
        Int32 piso;
        String depto;
        String codigoPostal;
        MiForm formAnt;

        public bool FueModificado
        {
            get { return fueModificado; }
            set { fueModificado = value; }
        }

        //con los datos que obtuvimos de la busqueda completamos todos los campos para que la persona 
        //pueda modificar el que desea
        public ModificarCli(Cliente cliente, MiForm formAnterior) : base(formAnterior)
        {
            formAnt = formAnterior;
            InitializeComponent();
            dateTimePickerNacimiento.MaxDate = Sesion.getInstance().fecha;
            comboBoxDocumento.DropDownStyle = ComboBoxStyle.DropDownList;
            textBoxNombre.Text += cliente.Nombre;
            textBoxApellido.Text += cliente.Apellido;
            textBoxMail.Text += cliente.Mail;
            var telefono = cliente.Telefono;
            textBoxTelefono.Text += telefono == 0 ? null : telefono.ToString();
            var documento = cliente.NumeroDeDocumento;
            textBoxDocumento.Text += documento == 0 ? null : documento.ToString();
            var cuil = cliente.Cuil;
            textBoxCuil.Text += cuil == 0 ? null : cuil.ToString();
            textBoxCiudad.Text = cliente.Ciudad;
            textBoxLocalidad.Text = cliente.Localidad;
            comboBoxDocumento.Text = cliente.TipoDocumento;
            dateTimePickerNacimiento.Value = cliente.FechaDeNacimiento;
            clienteViejo = cliente;

            Servidor servidor = Servidor.getInstance();
            readerCliente = servidor.query("EXEC MATE_LAVADO.obtenerDatosAdicionalesCliente '" + cliente.Id + "'");

            readerCliente.Read();

            textBoxNumeroCalle.Text += clienteViejo.NumeroDeCalle == 0 ? null : clienteViejo.NumeroDeCalle.ToString();
            textBoxPiso.Text += clienteViejo.Piso == 0 ? null : clienteViejo.Piso.ToString();
            textBoxCalle.Text += readerCliente["calle"].ToString();
            textBoxDepto.Text += readerCliente["depto"].ToString();
            textBoxCodigoPostal.Text += readerCliente["codigo_postal"].ToString();
            readerCliente.Close();
        }
        //verificamos que ninguno quede vacio
        public bool verificarCampos() {
            string errores = "";
            int numero;
            long num;
            bool camposCompletos = !string.IsNullOrWhiteSpace(textBoxNombre.Text)
                && !string.IsNullOrWhiteSpace(textBoxApellido.Text)
                && !string.IsNullOrWhiteSpace(textBoxTelefono.Text)
                && !string.IsNullOrWhiteSpace(textBoxMail.Text)
                && !string.IsNullOrWhiteSpace(textBoxCuil.Text)
                && !string.IsNullOrWhiteSpace(textBoxNumeroCalle.Text)
                && !string.IsNullOrWhiteSpace(textBoxCalle.Text)
                && !string.IsNullOrWhiteSpace(textBoxLocalidad.Text)
                && !string.IsNullOrWhiteSpace(textBoxDocumento.Text)
                && comboBoxDocumento.SelectedIndex > -1
                && dateTimePickerNacimiento.Value < dateTimePickerNacimiento.MinDate
                && Sesion.getInstance().fecha < dateTimePickerNacimiento.Value;

            if (!camposCompletos)
            {
                errores += "Todos los campos obligatorios deben estar completos.";
            }
            else
            {
                if (!int.TryParse(textBoxDocumento.Text, out numero)) { errores += "El DNI debe ser un valor numérico. \n"; }
                if (!long.TryParse(textBoxCuil.Text, out num)) { errores += "El CUIL debe ser un valor numérico. \n"; }
                if (!long.TryParse(textBoxTelefono.Text, out num)) { errores += "El teléfono debe ser un valor numérico. \n"; }
                if (!string.IsNullOrWhiteSpace(textBoxPiso.Text) && !int.TryParse(textBoxPiso.Text, out numero)) { errores += "El Piso debe ser un valor numérico. \n"; }
                if (!int.TryParse(textBoxNumeroCalle.Text, out numero)) { errores += "El Numero de la Calle debe ser un valor numérico. \n"; }
                if (!string.IsNullOrWhiteSpace(textBoxCodigoPostal.Text) && !int.TryParse(textBoxCodigoPostal.Text, out numero)) { errores += "El Codigo Postal debe ser un valor numérico. \n"; }
                if (dateTimePickerNacimiento.Value < dateTimePickerNacimiento.MinDate) { errores += "La fecha de nacimiento no puede ser anterior al 1900. \n"; }
                if (Sesion.getInstance().fecha < dateTimePickerNacimiento.Value) { errores += "La fecha de nacimiento no puede ser posterior a hoy. \n"; }
            }

            if (errores != "") { 
                MessageBox.Show(errores, "Error", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Anterior.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.verificarCampos() && this.fueModificado){
                Cliente clienteModificado = new Cliente();
                clienteModificado.Apellido = textBoxApellido.Text;
                clienteModificado.Nombre = textBoxNombre.Text;
                clienteModificado.Mail = textBoxMail.Text;
                clienteModificado.Telefono = long.Parse(textBoxTelefono.Text);
                clienteModificado.NumeroDeDocumento = Int32.Parse(textBoxDocumento.Text);
                clienteModificado.Cuil = long.Parse(textBoxCuil.Text);
                clienteModificado.TipoDocumento = comboBoxDocumento.Text;
                clienteModificado.FechaDeNacimiento = dateTimePickerNacimiento.Value;
                //Aca hay que hacer el update en la base
                //sp que le paso el cuil (validamos que el nuevo cuil no exista)del cliente que es unico 
                //para que busque el viejo y todos los datos nuevos para ser actualizados
                calle = textBoxCalle.Text;
                numeroCalle = Convert.ToInt32(textBoxNumeroCalle.Text);
                if (!string.IsNullOrWhiteSpace(textBoxPiso.Text)) { piso = Int32.Parse(textBoxPiso.Text); }
                if (!string.IsNullOrWhiteSpace(textBoxDepto.Text)) { depto = textBoxDepto.Text; }
                codigoPostal = textBoxCodigoPostal.Text;
                clienteModificado.Ciudad = textBoxCiudad.Text;
                clienteModificado.Localidad = textBoxLocalidad.Text;

                String query = clienteViejo.Id + ", '" + clienteModificado.Nombre + "', '" + clienteModificado.Apellido
                                + "', '" + clienteModificado.Mail + "', '" + comboBoxDocumento.Text + "', " + clienteModificado.NumeroDeDocumento + ", " + clienteModificado.Cuil
                                + ", " + clienteModificado.Telefono + ", '" + clienteModificado.FechaDeNacimiento + "', '"
                                + calle + "', " + numeroCalle + ", " + piso + ", '" + depto + "', '" + codigoPostal + "', '" + clienteModificado.Ciudad + "', '" + clienteModificado.Localidad + "'";

                servidor.realizarQuery("EXEC MATE_LAVADO.modificarCliente_sp " + query);
                MessageBox.Show("Los cambios se realizaron exitosamente.", "Modificar cliente", MessageBoxButtons.OK);
               
                new SeleccionarFuncionalidad().Show();
                this.Close();

              
                //formAnt.Show();
            }
        }

        private void comboBoxDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.fueModificado = true;
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            this.fueModificado = true;
        }

        private void textBoxApellido_TextChanged(object sender, EventArgs e)
        {
            this.fueModificado = true;
        }

        private void textBoxTelefono_TextChanged(object sender, EventArgs e)
        {
            this.fueModificado = true;
        }

        private void textBoxMail_TextChanged(object sender, EventArgs e)
        {
            this.fueModificado = true;
        }

        private void textBoxCuil_TextChanged(object sender, EventArgs e)
        {
            this.fueModificado = true;
        }

        private void textBoxDocumento_TextChanged(object sender, EventArgs e)
        {
            this.fueModificado = true;
        }

        private void dateTimePickerNacimiento_ValueChanged(object sender, EventArgs e)
        {
            this.fueModificado = true;
        }

        private void ModificarCli_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPiso_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxNumeroCalle_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
