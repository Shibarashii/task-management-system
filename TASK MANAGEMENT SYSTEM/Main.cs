using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Net.NetworkInformation;

namespace TASK_MANAGEMENT_SYSTEM
{
    public class Main
    {
        public static string ConnectionString = "server=localhost; database=task_management_system_db; username=root; password=;";

        public static Label CreateLabel(string text, Font font, Color foreColor, Color backColor, Point location, bool autoSize = true, bool autoEllipsis = true, Size? size = null, Cursor cursor = null)
        {
            var label = new Label
            {
                Text = text,
                Font = font,
                ForeColor = foreColor,
                AutoSize = autoSize,
                Location = location,
                AutoEllipsis = autoEllipsis,
                BackColor = backColor,
                Cursor = cursor,
            };

            if (size.HasValue)
            {
                label.Size = size.Value;
            }

            return label;
        }

        public static void UpdateTaskCount(string id)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string UPDATE_TASK_COUNT = $"SELECT COUNT(*) FROM tasks WHERE project_id = {id} AND is_archived = FALSE";
                using (MySqlCommand command = new MySqlCommand(UPDATE_TASK_COUNT, connection))
                {
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    command.CommandText = $"UPDATE projects SET total_tasks = {count} WHERE id = {id} AND is_archived = FALSE";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
