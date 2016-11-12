using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;//--added
using System.IO;//--added

namespace Bataille
{
    class CPort
    {
        private List<String> m_tListGoesOnToolsForm;
        private SerialPort m_SP;
        
        private string m_SelectedPortFromToolsForm;
        private bool m_Cancel;
        //private CData m_Data;
        
        public string SelectedPort
        {
            get { return m_SelectedPortFromToolsForm; }
            set { m_SelectedPortFromToolsForm = value; }
        }

        public SerialPort SP
        {
            get { return m_SP; }
            set { m_SP = value; }
        }
        public List<String> ListOfAvalaiblePorts
        {
            get { return m_tListGoesOnToolsForm; }
        }
        public CPort()
        {
            m_SP = new SerialPort();
            m_tListGoesOnToolsForm = new List<String>();
            m_SelectedPortFromToolsForm = "COM";
            COMPort();
        }
        public void ON()
        {
            if (!m_SP.IsOpen)
                m_SP.PortName = m_SelectedPortFromToolsForm;
            else
            {
                OFF();
                m_SP.PortName = m_SelectedPortFromToolsForm;
            }
            try { m_SP.Open(); }
            catch (System.UnauthorizedAccessException)
            {
                ShowSettings();
                if (m_Cancel != true)
                {
                    ON();
                }
                else
                {
                    m_Cancel = false;
                }
            }
            catch (System.IO.IOException)
            {
                ShowSettings();
                if (m_Cancel != true)
                {
                    ON();
                }
                else
                {
                    m_Cancel = false;
                }
            }
        }
        public void OFF()
        {
            m_SP.Close();
        }

        public void ShowSettings()
        {
            frmTools Settings = new frmTools(m_tListGoesOnToolsForm, m_SelectedPortFromToolsForm);
            m_SelectedPortFromToolsForm = "COM";
            Settings.ShowDialog();
            if (Settings.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                m_SelectedPortFromToolsForm = Settings.SelectedPort;
            }
            else
            {
                if (Settings.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    m_Cancel = true;
                }
            }
            Settings.Dispose();
        }
        private void COMPort()
        {
            m_tListGoesOnToolsForm.Clear();
            foreach (string Port in SerialPort.GetPortNames())
                m_tListGoesOnToolsForm.Add(Port);
            m_tListGoesOnToolsForm.Sort();
        }
    }
}
