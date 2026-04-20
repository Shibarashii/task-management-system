using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Net.Mail;
using System.Net;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.VisualBasic;


namespace TASK_MANAGEMENT_SYSTEM
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string CHECK_USER = $"SELECT * FROM users WHERE email='{EmailTextbox.Text}' AND password='{PasswordTextbox.Text}'";
                using (MySqlCommand command = new MySqlCommand(CHECK_USER, connection))
                {
                    int match = Convert.ToInt32(command.ExecuteScalar());
                    if (match > 0)
                    {
                        using (MySqlDataReader  reader = command.ExecuteReader())
                        {
                            reader.Read();
                            string id = reader["id"].ToString();
                            bool isSuperUser = (bool)reader["is_superuser"];

                            MainForm mainForm = new MainForm(id, isSuperUser);
                            mainForm.Show();
                            Hide();
                        }
                    }
                    else
                    {
                        InvalidAccountLabel.Show();
                    }
                }
            }
        }


        #region TEXTBOXES
        private void EmailTextbox_TextChanged(object sender, EventArgs e)
        {
            InvalidAccountLabel.Hide();
        }

        private void PasswordTextbox_TextChanged(object sender, EventArgs e)
        {
            InvalidAccountLabel.Hide();
        }
        #endregion

        private void XButton_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void ShowPasswordButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPasswordButton.Checked)
            {
                PasswordTextbox.PasswordChar = '\0';
            }
            else
            {
                PasswordTextbox.PasswordChar = '●';
            }
        }

        private void RecoveryCodeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextbox.Text))
            {
                MessageBox.Show("Please enter your email in the textbox above.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string CHECK_EMAIL = $"SELECT * FROM users WHERE email = '{EmailTextbox.Text}'";

                using (MySqlCommand command = new MySqlCommand(CHECK_EMAIL, connection))
                {
                    int matchRecord = Convert.ToInt32(command.ExecuteScalar());

                    if (matchRecord > 0)
                    {
                        string recoveryCode = GenerateRandomCode(6);
                        SaveRecoveryCodeToDatabase(EmailTextbox.Text, recoveryCode);
                        SendRecoveryCodeByEmail(EmailTextbox.Text, recoveryCode);

                        MessageBox.Show("Recovery code sent to your email. Check your inbox.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        bool recoveryCodeEntered = false;

                        while (!recoveryCodeEntered)
                        {
                            string enteredCode = Interaction.InputBox("Enter the recovery code sent to your email:", "Recovery Code", "");

                            if (enteredCode == "") // User clicked Cancel
                            {
                                break; // Exit the loop
                            }

                            if (VerifyRecoveryCode(EmailTextbox.Text, enteredCode))
                            {
                                MessageBox.Show("Recovery code verified. Access granted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Open the MainForm
                                OpenMainFormViaRecoveryCode();

                                recoveryCodeEntered = true; // Set the flag to exit the loop
                            }
                            else
                            {
                                MessageBox.Show("Invalid recovery code. Try again or click Cancel to exit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Email not found in our database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void OpenMainFormViaRecoveryCode()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string CHECK_USER = $"SELECT * FROM users WHERE email='{EmailTextbox.Text}' AND is_archived = FALSE";
                using (MySqlCommand command = new MySqlCommand(CHECK_USER, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        string id = reader["id"].ToString();
                        bool isSuperUser = (bool)reader["is_superuser"];

                        MainForm mainForm = new MainForm(id, isSuperUser);
                        mainForm.Show();

                        AddUserForm addUserForm = new AddUserForm(id, true);
                        addUserForm.Show();
                        MessageBox.Show("Change your password as soon as possible.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Hide();
                    }
                }
            }
        }

        // Verify the recovery code against the stored code in the database
        private bool VerifyRecoveryCode(string email, string enteredCode)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string SELECT_RECOVERY_CODE = $"SELECT recovery_code FROM users WHERE email = '{email}'";
                using (MySqlCommand command = new MySqlCommand(SELECT_RECOVERY_CODE, connection))
                {
                    string storedCode = Convert.ToString(command.ExecuteScalar());
                    return enteredCode == storedCode;
                }
            }
        }

        private string GenerateRandomCode(int length)
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Save the recovery code to the database (you should implement this method)
        private void SaveRecoveryCodeToDatabase(string email, string recoveryCode)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string UPDATE_RECOVERY_CODE = $"UPDATE users SET recovery_code = '{recoveryCode}' WHERE email = '{email}'";
                using (MySqlCommand command = new MySqlCommand(UPDATE_RECOVERY_CODE, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void SendRecoveryCodeByEmail(string email, string recoveryCode)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("shiba.taskmanagementsystem@gmail.com", "Shiba Systems");
            var subject = "Recovery Code for TASK MANAGEMENT SYSTEM";
            var to = new EmailAddress(email);
            var plainTextContent = $"Your recovery code is: {recoveryCode}";
            var htmlContent = $"<p>Your recovery code is: <strong>{recoveryCode}</strong></p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg).Result;
        }
    }
}

