using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitalinoCon
{
    public partial class Form4 : Form
    {

        DadosPaciente clienteParaEditar;
        public DadosPaciente ClienteParaEditar
        {
            get
            {
                return clienteParaEditar;
            }

            set
            {
                clienteParaEditar = value;
            }
        }

        // Modelo de dados
        DataStore datahelper;

        public Form4(DadosPaciente cliente)
        {
            this.clienteParaEditar = cliente;
            InitializeComponent();
            this.Text = cliente.Nome;


            datahelper = new DataStore();
            DataView dataView = datahelper.DataSet.Tables[DataStore.DATATABLE_EXAMES].DefaultView;
            dataView.RowFilter = string.Format("[{0}] = '{1}'", DataStore.EXAME_CLIENT_ID, cliente.Id);
            dataGridView1.DataSource = dataView;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoResizeColumns();
        }

        public Form4()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Gráfico g = new Gráfico(this.dataGridView1.CurrentRow.Cells[4].Value.ToString());
            g.ShowDialog();
            
          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 sistema = new Form1(clienteParaEditar);
            sistema.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int indexToRemove = dataGridView1.CurrentCell.RowIndex;
            if (indexToRemove > -1)
            {
                DialogResult result = MessageBox.Show("Tem a certeza que quer Remover o Exame selecionado?", "Atencao", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    DadosExame.removerDaBaseDados(datahelper, indexToRemove, clienteParaEditar.Id);
                }
            }
            else
            {
                MessageBox.Show("Por favor selecione um paciente!");
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}