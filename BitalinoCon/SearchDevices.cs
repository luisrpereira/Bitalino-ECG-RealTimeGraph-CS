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
    public partial class SearchDevices : Form
    {
        Bitalino.DevInfo[] devices;

        public SearchDevices()
        {
            InitializeComponent();
            DeviceSingletone.Instance.EnconteredDevices += Instance_EnconteredDevices;
        }

        private async void buttonFind_Click(object sender, EventArgs e)
        {
            //listBoxDevices.Items.Clear();
            //buttonFind.Enabled = false;
            await DeviceSingletone.Instance.SearchDevices();
           
        }

        private void Instance_EnconteredDevices(Bitalino.DevInfo[] devs)
        {
           
            devices = devs;
            foreach(Bitalino.DevInfo devices in devs){
                AddTextToListBox(
                    String.Format("{0} - {1}", devices.name, devices.macAddr));
            }
        }

        delegate void AddDeviceDelegate(string text);

        private void AddTextToListBox(string text)
        {  
            if (this.listBoxDevices.InvokeRequired)
            {
                AddDeviceDelegate d = new AddDeviceDelegate(AddTextToListBox);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                //buttonFind.Enabled = true;
                listBoxDevices.Items.Add (text);
            }
        }

        private void listBoxDevices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBoxDevices.SelectedIndex;
            DeviceSingletone.Instance.Device = devices[index];
            this.Close();

        }
    }
}
