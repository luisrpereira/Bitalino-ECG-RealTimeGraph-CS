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
    public partial class InserirPaciente : Form
    {
        int years;
        DataStore datastorer;
        int index;
        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }

        public InserirPaciente(int index)
        {
            this.Index = index;
        }


        public InserirPaciente()
        {
            datastorer = new DataStore();
            InitializeComponent();
        }

        DesportoType RadioButtonDesportoSelecionado()
        {
            DesportoType desporto;
            bool PraticaDesporto = radioButton4.Checked;
            if (PraticaDesporto)
            {
                desporto = DesportoType.Sim;
            }
            else
            {
                desporto = DesportoType.Não;
            }
            return desporto;
        }

        GenderType RadioButtonGenderSelecionado()
        {
            bool e_Masculino = radioButton1.Checked;
            GenderType genero;
            if (e_Masculino)
            {
                genero = GenderType.Masculino;
            }
            else
            {
                genero = GenderType.Feminino;
            }
            return genero;
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            years = DateTime.Now.Year - dateTimePicker1.Value.Year;

            if (dateTimePicker1.Value.AddYears(years) > DateTime.Now) years--;
            textBox4.Text = years.ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show("Tem a certeza que pretende adicionar um novo Cliente?", "Alerta:", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                DadosPaciente clienteParaAdicionar = new DadosPaciente(textBox1.Text, years, RadioButtonGenderSelecionado(), RadioButtonDesportoSelecionado(), textBox2.Text, textBox3.Text);

                DadosPaciente.AdicionarParaDataBase(datastorer, clienteParaAdicionar);


                this.Hide();
                Form3 sistema = new Form3();
                sistema.ShowDialog();
                this.Close();








            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
            Form3 sistema = new Form3();
            sistema.ShowDialog();
            this.Close();
        }
    }
}
