using ETA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETA
{
    public partial class frmMain : Form
    {
        private CancellationTokenSource cancellationTokenSource;
        private string taskId = string.Empty;
        private List<TaskInfo> tasks;
        private Random random = new Random();

        /// <summary>
        /// Initializes a new instance of the frmMain class.
        /// Starts background tasks to update task statuses.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            StartBackgroundTaskUpdateToInProgress();
            StartBackgroundTaskUpdateToCompleted();
        }

        /// <summary>
        /// Starts a background task that updates tasks' status to "Completed".
        /// </summary>
        private void StartBackgroundTaskUpdateToCompleted()
        {
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    if (tasks == null)
                    {
                        tasks = Core.ReadFromFile();
                    }
                    int delay = random.Next(3000, 8000);
                    await Task.Delay(delay);
                    foreach (var task in tasks)
                    {
                        if (task.Status == "In Progress")
                        {
                            task.Status = "Completed";
                            break;
                        }
                    }
                    Core.ReWriteToFile(tasks);
                    Invoke((Action)bindGrid);
                }
            }, cancellationTokenSource.Token);
        }

        /// <summary>
        /// Stops all background tasks by canceling the cancellation token.
        /// </summary>
        private void StopBackgroundTasks()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
        }


        /// <summary>
        /// Starts a background task that updates tasks' status to "In Progress".
        /// </summary>
        private void StartBackgroundTaskUpdateToInProgress()
        {
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    if (tasks == null)
                    {
                        tasks = Core.ReadFromFile();
                    }
                    int delay = random.Next(3000, 8000);
                    await Task.Delay(delay);
                    foreach (var task in tasks)
                    {
                        if (task.Status == "Pending")
                        {
                            task.Status = "In Progress";
                            break;
                        }
                    }
                    Core.ReWriteToFile(tasks);
                    Invoke((Action)bindGrid);
                }
            }, cancellationTokenSource.Token);
        }

        /// <summary>
        /// Handles the click event for the Add Task button.
        /// Opens a modal to add a new task and updates the task list.
        /// </summary>
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            frmModal modal = new frmModal();
            modal.TopMost = true;
            modal.StartPosition = FormStartPosition.CenterScreen;
            if (modal.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                tasks.Add(modal.taskInfo);
                Core.ReWriteToFile(tasks);
                bindingSource1.ResetBindings(true);
                //bindGrid();
                modal.Dispose();
            }
        }

        /// <summary>
        /// Binds the task list to the DataGridView and maintains scroll position.
        /// </summary>
        private void bindGrid()
        {
            int verticalScrollPosition = 0;
            int horizontalScrollPosition = 0;

            if (gvMain.CurrentCell != null)
            {
                verticalScrollPosition = gvMain.FirstDisplayedScrollingRowIndex;
                horizontalScrollPosition = gvMain.FirstDisplayedScrollingColumnIndex;
            }
            bindingSource1.ResetBindings(true);
            gvMain.BeginInvoke(new Action(() =>
            {
                if (verticalScrollPosition >= 0 && verticalScrollPosition < gvMain.RowCount)
                {
                    gvMain.FirstDisplayedScrollingRowIndex = verticalScrollPosition;
                }

                if (horizontalScrollPosition >= 0 && horizontalScrollPosition < gvMain.ColumnCount)
                {
                    gvMain.FirstDisplayedScrollingColumnIndex = horizontalScrollPosition;
                }
            }));
        }

        /// <summary>
        /// Loads tasks from a file when the form is loaded.
        /// </summary>
        private void frmMain_Load(object sender, EventArgs e)
        {
            tasks = Core.ReadFromFile();
            bindingSource1.DataSource = tasks;
            //bindGrid();
        }

        /// <summary>
        /// Handles the selection change event for the DataGridView.
        /// Updates the taskId and enables/disables the Completed button based on the selected task's status.
        /// </summary>
        private void gvMain_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView gv = sender as DataGridView;
            if (gv != null && gv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = gv.SelectedRows[0];
                if (row != null)
                {
                    if (row.Cells["Status"].Value.ToString().ToLower() == "completed")
                        btnCompleted.Enabled = false;
                    else
                        btnCompleted.Enabled = true;

                    taskId = row.Cells["TaskID"].Value.ToString();
                }
            }
        }

        /// <summary>
        /// Handles the click event for the Completed button.
        /// Marks the selected task as completed and updates the task list.
        /// </summary>
        private void btnCompleted_Click(object sender, EventArgs e)
        {
            var data = Core.ReadFromFile();
            var task = data.FirstOrDefault(t => t.TaskID == taskId);
            if (task != null)
            {
                task.Status = "Completed";
                Core.ReWriteToFile(data);
                bindingSource1.ResetBindings(true);
                //bindGrid();
            }
        }

        
        /// Handles the click event for the Delete button.
        /// Removes the selected task from the list and updates the task list.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var itemRemove = tasks.FirstOrDefault((x) => x.TaskID == taskId);
            if (itemRemove == null)
                return;
            tasks.Remove(itemRemove);
            Core.ReWriteToFile(tasks);
            bindingSource1.ResetBindings(true);
            //bindGrid();
        }

        /// <summary>
        /// Handles the click event for the Load button.
        /// Loads tasks from a selected file and restarts background tasks.
        /// </summary>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StopBackgroundTasks();
                    tasks = Core.LoadFromFile(dlg.FileName);
                    bindingSource1.DataSource = tasks;
                    bindingSource1.ResetBindings(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Invalid Template", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
                finally
                {
                    StartBackgroundTaskUpdateToInProgress();
                    StartBackgroundTaskUpdateToCompleted();
                }
            }
        }
    }
}
