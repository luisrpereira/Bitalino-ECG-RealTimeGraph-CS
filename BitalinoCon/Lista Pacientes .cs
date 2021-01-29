using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitalinoCon
{
    public partial class Form3 : Form
    {
        
        DataStore datastore;
        public Form3()
        {
            InitializeComponent();
            datastore = new DataStore();
            dataGridViewClientes.DataSource = datastore.DataSet;
            dataGridViewClientes.DataMember = DataStore.DATATABLE_CLIENTES;
            dataGridViewClientes.AutoGenerateColumns = true;
            //   dataGridViewClientes.AutoResizeColumns();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {



        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            InserirPaciente adicionarCliente = new InserirPaciente();

            adicionarCliente.ShowDialog();

        }

        private void dataGridViewClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridViewClientes.CurrentCell.RowIndex;
            if (index >= 0)
            {
                DadosPaciente cliente = DadosPaciente.lerNaBaseDados(datastore, index);
                Form4 exames = new Form4(cliente);
                exames.ShowDialog();
                
            }
            
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //   Form1 form1 = new Form1();
            // form1.ShowDialog();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
                int indexParaRemover = dataGridViewClientes.CurrentCell.RowIndex;
                if (indexParaRemover >= 0)
                {
                    DialogResult result = MessageBox.Show("Tem a certeza que pretende remover o Cliente seleccionado?", "Alerta:", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                       DadosPaciente.removerDaBaseDados(datastore, indexParaRemover);
                    }
                }
                else
                {
                    MessageBox.Show("Alerta: Não selecionou nenhum cliente. Tente novamente!");
                }
        }
    }
}
