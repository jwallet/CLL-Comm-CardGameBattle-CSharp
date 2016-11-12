using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bataille
{
    public partial class frmTools : Form
    {
        private string m_SelectedPort;

        public string SelectedPort
        {
            get { return m_SelectedPort; }
            set { m_SelectedPort = value; }
        }

        public frmTools(List<String> ListOfAvalaiblePorts, string CurrentPort)
        {
            InitializeComponent();
            m_SelectedPort = CurrentPort;
            cboListOfPorts.Items.Clear();
            cboListOfPorts.Items.AddRange(ListOfAvalaiblePorts.ToArray());
            if(cboListOfPorts.Items.Count==0)
            {
                cboListOfPorts.Enabled = false;
                btnOK.Enabled = false;
            }
            else
            {
                if(cboListOfPorts.Items.Count==1)
                {
                    cboListOfPorts.SelectedIndex = 0;
                    m_SelectedPort = cboListOfPorts.Text;
                    cboListOfPorts.Enabled = false;
                }
                else
                {
                    cboListOfPorts.SelectedIndex = 0;

                    m_SelectedPort = cboListOfPorts.Text;
                    cboListOfPorts.Enabled = true;
                }
            }
            if(CurrentPort!="COM0")
            {
                cboListOfPorts.SelectedItem = CurrentPort;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cboListOfPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_SelectedPort = cboListOfPorts.SelectedItem.ToString();
        }
    }
}
