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
    public partial class Form1 : Form
    {

      DadosPaciente clienteParaEditar;
       DataStore datahelper;
        List<string> valoresExame = new List<string>();

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
        

        public Form1(DadosPaciente cliente)
        {
            InitializeComponent();
            DeviceSingletone.Instance.NewData += Instance_NewData;
            chart1.Series.Clear();
            chart1.Series.Add("Leituras");
            chart1.Series["Leituras"].SetDefault(true);
            chart1.Series["Leituras"].Enabled = true;
            chart1.Series["Leituras"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series["Leituras"].IsXValueIndexed = false;
            chart1.Series["Leituras"].IsVisibleInLegend = false;
            chart1.ChartAreas[0].AxisY.Minimum = 350;
            chart1.ChartAreas[0].AxisY.Maximum = 750;
            datahelper = new DataStore();

            this.clienteParaEditar = cliente;
 
            this.Text = cliente.Nome;

            button2.Enabled = false;




        }

        private void buttonSearchDevices_Click(object sender, EventArgs e)
        {
            SearchDevices sd = new SearchDevices();
            sd.Show();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            await DeviceSingletone.Instance.ReadDevice();
           
        }

        private void Instance_NewData(string data)
        {
            AddTextToTextBox(data);
        }


        delegate void AddTextDelegate(string text);




        private void AddTextToTextBox(string text)
        {
            if (this.chart1.InvokeRequired)
            {
                AddTextDelegate d = new AddTextDelegate(AddTextToTextBox);
                this.Invoke(d, new object[] { text });
            }
            else
            {


                valoresExame.Add(text);



                chart1.Series["Leituras"].Points.AddY(int.Parse(text));



                if (chart1.Series["Leituras"].Points.Count > 300)



                    chart1.Series["Leituras"].Points.RemoveAt(0);







            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            DeviceSingletone.Instance.disconnect();
            button2.Enabled = true;


        }

  

        private void chart1_Click(object sender, EventArgs e)
        {
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem a certeza que pretende guardar um novo Exame?", "Alerta:", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {

                DadosExame exameParaAdicionar = new DadosExame(DateTime.Now, ClienteParaEditar.Id, textBox1.Text, string.Join(",",valoresExame));
                DadosExame.AdicionarExameParaDataBase(datahelper, exameParaAdicionar);

                this.Hide();
                Form4 q = new Form4(clienteParaEditar);
                q.ShowDialog();
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
