using ETA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.CodeDom;

namespace ETA
{
    public partial class frmModal : Form
    {
        private string FILE_NAME = "taskList.json";
        public TaskInfo taskInfo;
        public frmModal()
        {
            taskInfo = new TaskInfo();
            InitializeComponent();
        }
  

        private void Validation()
        {
            if (string.IsNullOrEmpty(txtTaskName.Text.ToString().Trim()))
                throw new Exception("Task Name can not empty");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Validation();
                Core.CheckFile();
                taskInfo.TaskID = Guid.NewGuid().ToString();
                taskInfo.TaskName = txtTaskName.Text;
                taskInfo.Status = cmbStatus.Text;
                taskInfo.Priority = cmbPriority.Text;
                this.DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
