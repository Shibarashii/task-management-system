using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TASK_MANAGEMENT_SYSTEM.PROJECT_SECTION;
using TASK_MANAGEMENT_SYSTEM.TASK_SECTION;

namespace TASK_MANAGEMENT_SYSTEM.UserControls
{
    public partial class ActionButtons : UserControl
    {
        private readonly MainForm mainForm;
        private readonly string id;
        private readonly string projectId;
        private readonly UserTab userTab;
        private readonly ProjectTab projectTab;
        private readonly TaskTab taskTab;
        private readonly ViewProject viewProject;
        private readonly ViewTask viewTask;
        public ActionButtons(MainForm mainForm, string id, UserTab userTab = null, ProjectTab projectTab = null, TaskTab taskTab = null, ViewProject viewProject = null, string projectId = null, ViewTask viewTask = null)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.id = id;
            this.userTab = userTab;
            this.projectTab = projectTab;
            this.taskTab = taskTab;
            this.viewProject = viewProject;
            this.projectId = projectId;
            this.viewTask = viewTask;

            if (!MainForm.isSuperuser)
            {
                EditButton.Hide();
                DeleteButton.Hide();
            }

            if (userTab != null || viewTask != null)
            {
                ViewButton.Hide();
            }
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            if (projectTab != null)
            {
                projectTab.Hide();
                ViewProject viewProject = new ViewProject(mainForm, id);
                MainForm.viewProject = viewProject;
                viewProject.Location = new Point(363, 36);
                mainForm.Controls.Add(viewProject);
                viewProject.Show();
            }

            if (viewProject != null)
            {
                viewProject.Hide();
                ViewTask viewTask = new ViewTask(mainForm, id, projectId, false);
                MainForm.viewTask = viewTask;
                viewTask.Location = new Point(363, 36);
                mainForm.Controls.Add(viewTask);
                viewTask.Show();
            }

            if (taskTab != null)
            {
                taskTab.Hide();
                ViewTask viewTask = new ViewTask(mainForm, id, projectId, true);
                MainForm.viewTask = viewTask;
                viewTask.Location = new Point(363, 36);
                mainForm.Controls.Add(viewTask);
                viewTask.Show();
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (projectTab != null)
            {
                AddProjectForm addProjectForm = new AddProjectForm(true, id);
                addProjectForm.ShowDialog();
            }

            if (taskTab != null || viewProject != null)
            {
                AddTaskForm addTaskForm = new AddTaskForm(true, id);
                addTaskForm.ShowDialog();
            }

            if (userTab != null || viewTask != null)
            {
                AddUserForm addUserForm = new AddUserForm(id, true);
                addUserForm.ShowDialog();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (userTab != null)
            {
                DeleteUser();
            }
            if (projectTab != null)
            {
                DeleteProject();
            }
            if (taskTab != null)
            {
                DeleteTask();
            }
            if (viewProject != null)
            {
                DeleteTask();
                viewProject.RefreshTaskList();
            }
            if (viewTask != null)
            {
                DeleteUser();
                viewTask.RefreshViewTask();
            }
        }

        private void DeleteUser()
        {
            DialogResult resullt = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resullt == DialogResult.OK)
            {
                using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
                {
                    connection.Open();
                    string SET_ARCHIVE = $"UPDATE users SET is_archived = TRUE WHERE id ='{id}'";
                    using (MySqlCommand command = new MySqlCommand(SET_ARCHIVE, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User successfully deleted.", "User deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MainForm.userTab.RefreshFlowPanel();
                        }
                    }
                }
            }
        }

        private void DeleteProject()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this project?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
                {
                    connection.Open();

                    List<string> taskIds = new List<string>();
                    string fetchTaskIdsQuery = $"SELECT id FROM tasks WHERE project_id = {id}";

                    using (MySqlCommand fetchTaskIdsCommand = new MySqlCommand(fetchTaskIdsQuery, connection))
                    {
                        using (MySqlDataReader reader = fetchTaskIdsCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                taskIds.Add(reader["id"].ToString());
                            }
                        }
                    }

                    string setArchiveQuery = $"UPDATE projects SET is_archived = TRUE WHERE id ='{id}'";
                    using (MySqlCommand setArchiveCommand = new MySqlCommand(setArchiveQuery, connection))
                    {
                        int rowsAffected = setArchiveCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Project successfully deleted.", "Project deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MainForm.projectTab.RefreshFlowPanel();
                        }
                    }

                    string archiveTasksQuery = $"UPDATE tasks SET is_archived = TRUE WHERE project_id = {id}";
                    using (MySqlCommand archiveTasksCommand = new MySqlCommand(archiveTasksQuery, connection))
                    {
                        archiveTasksCommand.ExecuteNonQuery();
                    }

                    foreach (string taskId in taskIds)
                    {
                        string archiveTaskUsersQuery = $"UPDATE task_users SET is_archived = TRUE WHERE task_id = '{taskId}'";
                        using (MySqlCommand archiveTaskUsersCommand = new MySqlCommand(archiveTaskUsersQuery, connection))
                        {
                            archiveTaskUsersCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }


        private void DeleteTask()
        {
            DialogResult resullt = MessageBox.Show("Are you sure you want to delete this task?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resullt == DialogResult.OK)
            {
                using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
                {
                    connection.Open();
                    string SET_ARCHIVE = $"UPDATE tasks SET is_archived = TRUE WHERE id='{id}'";
                    using (MySqlCommand command = new MySqlCommand(SET_ARCHIVE, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Task successfully deleted.", "Task deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MainForm.taskTab.RefreshFlowPanel();
                            Main.UpdateTaskCount(projectId);
                        }
                        command.CommandText = $"UPDATE task_users SET is_archived = TRUE WHERE task_id = {id}";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
