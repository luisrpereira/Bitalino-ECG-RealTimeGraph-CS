using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;


namespace BitalinoCon
{
    public partial class Gráfico : Form
    {
        string[] valores;
        public Gráfico(string examedados)
        {
            InitializeComponent();
            
            chart1.Series.Clear();
            chart1.Series.Add("Leituras");
            chart1.Series["Leituras"].SetDefault(true);
            chart1.Series["Leituras"].Enabled = true;
            chart1.Series["Leituras"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series["Leituras"].IsXValueIndexed = false;
            chart1.Series["Leituras"].IsVisibleInLegend = false;
            chart1.ChartAreas[0].AxisY.Minimum = 350;
            chart1.ChartAreas[0].AxisY.Maximum = 750;
            chart1.ChartAreas[0].CursorX.AutoScroll = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, 300);


            valores = examedados.Split(',');
           
            foreach (string valor in valores)
            {
                if (valor != "")
                {

                    chart1.Series["Leituras"].Points.AddY(int.Parse(valor));
                    
                }

            }

        
    }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
         
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.FileName = "*";
            sv.DefaultExt = "csv";
            sv.ValidateNames = true;

            sv.Filter = "CSV File (.csv)|*.csv";
            if (sv.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(sv.FileName);
                StreamWriter sw = new StreamWriter(sv.FileName);
                foreach (string valor in valores)
                {
                    sw.WriteLine(valor);
                }

                sw.Flush();
                sw.Close();

            }
        }
    }
}
