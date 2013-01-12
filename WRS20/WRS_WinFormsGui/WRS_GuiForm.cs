using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WRS_WinFormsGui
{
    public partial class WRS_GuiForm : Form
    {
        public WRS_GuiForm()
        {
            InitializeComponent();
        }

        WRS20_Logic.WRS_SpectatingClient wrsClient;
        private void button1_Click(object sender, EventArgs e)
        {
            wrsClient = new WRS20_Logic.WRS_SpectatingClient();
            wrsClient.ClientConnected += wrsClient_ClientConnected;
            wrsClient.ClientDisconnected += wrsClient_ClientDisconnected;
            wrS_Gui1.Start();
        }

        void wrsClient_ClientDisconnected(WRS20_Logic.JsonObjects.JClient client)
        {
            listBox1.Invoke((MethodInvoker)delegate { if (listBox1.FindString(client.Name) != -1) listBox1.Items.Remove(client.Name); });
        }

        void wrsClient_ClientConnected(WRS20_Logic.JsonObjects.JClient client)
        {
            listBox1.Invoke((MethodInvoker)delegate { listBox1.Items.Add(client.Name); });
        }

        private void WRS_GuiForm_Load(object sender, EventArgs e)
        {
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            wrS_Gui1.focusedClient = null;
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            wrS_Gui1.focusedClient = wrsClient.ClientArr.FirstOrDefault(P => P.Name == listBox1.Items[listBox1.SelectedIndex].ToString());
        }
        
    }
}
