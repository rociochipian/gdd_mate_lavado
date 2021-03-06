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
    public partial class EditarMPs : MiForm
    {
        List<Tarjeta> tarjetas = new List<Tarjeta>();
        Cliente cliente;

        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public List<Tarjeta> Tarjetas
        {
            get { return tarjetas; }
            set { tarjetas = value; }
        }

        //Nos trae la informacion del form anterior y del cliente que estabamos modificando la información
        public EditarMPs(MiForm anterior, Cliente cliente) : base(anterior)
        {
            InitializeComponent();
            this.Cliente = cliente;
            this.Anterior = anterior;
            this.actualizarMP();
        }

        //función para actualizar los medios de pago actuales que se utilizará luego de modificaciones
        public void actualizarMP() {
            this.Tarjetas.Clear();
            Servidor servidor = Servidor.getInstance();
            SqlDataReader reader = servidor.query("exec MATE_LAVADO.getMediosDePago_sp " + this.Cliente.Id);
            //traemos los medios de pago del cliente para guardarlos en la data grid y que pueda seleccionar cual modificar
            while (reader.Read())
            {
                Tarjeta tarjeta = new Tarjeta();
                tarjeta.NumeroDeTarjeta = long.Parse(reader["digitos"].ToString());
                tarjeta.Id = int.Parse(reader["id_medio_de_pago"].ToString());
                tarjeta.Titular = reader["titular"].ToString();
                if (tarjeta.NumeroDeTarjeta != 0)
                {
                    this.tarjetas.Add(tarjeta);
                }

                var bindingList = new BindingList<Tarjeta>(this.tarjetas);
                var source = new BindingSource(bindingList, null);
                dataGridView1.DataSource = source;
            }
        }

        //botón para volver a la modificación del cliente
        private void button1_Click(object sender, EventArgs e)
        {
            this.Anterior.Show();
            this.Hide();
        }

        //Una vez seleccionada la tarjeta, éste botón nos llevará a editar su información
        private void button4_Click(object sender, EventArgs e)
        {
            Tarjeta tarjeta = (Tarjeta)dataGridView1.CurrentRow.DataBoundItem;
            new EditarTarjeta(this, tarjeta, this.Cliente).Show();
            this.Hide();
        }
    }
}
