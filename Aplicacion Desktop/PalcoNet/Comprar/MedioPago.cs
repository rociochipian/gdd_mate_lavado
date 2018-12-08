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

namespace PalcoNet.Comprar
{
    public partial class MedioPago : MiForm
    {
        Compra compra;

        public Compra Compra
        {
            get { return compra; }
            set { compra = value; }
        }
        Cliente cliente;

        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        List<Tarjeta> tarjetas = new List<Tarjeta>();

        internal List<Tarjeta> Tarjetas
        {
            get { return tarjetas; }
            set { tarjetas = value; }
        }

        public MedioPago(MiForm anterior, Compra compra, Cliente cliente) : base(anterior)
        {
            this.Cliente = cliente;
            this.Compra = compra;
            InitializeComponent();

            //Aca hay que traer todas las tarjetas del cliente y guardarlas en la lista de arriba

            foreach (Tarjeta t in tarjetas) {
                this.comboBoxTarjeta.Items.Add(t.NumeroDeTarjeta);
            }
        }

        public bool verificarCampos() {
            string error = "";
            int i;
            bool camposCompletos = comboBoxTarjeta.SelectedIndex > -1 && !string.IsNullOrWhiteSpace(textBoxCodigo.Text);

            if (!camposCompletos){
                error += "Se debe seleccionar una tarjeta y proveer de un código de seguridad";
            } else {
                if (!int.TryParse(textBoxCodigo.Text, out i)) { error += "El código de seguridad debe ser un valor numérico."; }
            }

            if (error != "")
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Anterior.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.verificarCampos()) {
                int NumeroDeTarjeta = this.Tarjetas[this.comboBoxTarjeta.SelectedIndex].NumeroDeTarjeta;
                string codigoSeguridad = this.textBoxCodigo.Text;

                //Aca hay que verificar que la tarjeta exista en la base y que los datos coincidan
                if (/*verificacion es correcta*/true)
                {
                    this.Compra.MedioDePago = this.Tarjetas[this.comboBoxTarjeta.SelectedIndex];
                    new FinalizarCompra(this, this.Cliente, this.Compra).Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Los datos ingresados para el medio de pago con invalidos.", "Error", MessageBoxButtons.OK);
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NuevoMP nuevo = new NuevoMP();
            nuevo.Show();
            if (nuevo.Tarjeta != null) {
                this.comboBoxTarjeta.Items.Add(nuevo.Tarjeta);
                nuevo.Close();
            }
        }

        private void textBoxCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxTarjeta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
