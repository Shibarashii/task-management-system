namespace TASK_MANAGEMENT_SYSTEM
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.RecoveryCodeButton = new Guna.UI2.WinForms.Guna2Button();
            this.ShowPasswordButton = new Guna.UI2.WinForms.Guna2ImageCheckBox();
            this.InvalidAccountLabel = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel2 = new Guna.UI.WinForms.GunaLabel();
            this.LoginButton = new Guna.UI2.WinForms.Guna2CircleButton();
            this.EmailTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            this.PasswordTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            this.XButton = new Guna.UI2.WinForms.Guna2CircleButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.guna2CustomGradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 100;
            this.guna2Elipse1.TargetControl = this.guna2CustomGradientPanel1;
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.Controls.Add(this.RecoveryCodeButton);
            this.guna2CustomGradientPanel1.Controls.Add(this.ShowPasswordButton);
            this.guna2CustomGradientPanel1.Controls.Add(this.InvalidAccountLabel);
            this.guna2CustomGradientPanel1.Controls.Add(this.gunaLabel2);
            this.guna2CustomGradientPanel1.Controls.Add(this.LoginButton);
            this.guna2CustomGradientPanel1.Controls.Add(this.EmailTextbox);
            this.guna2CustomGradientPanel1.Controls.Add(this.PasswordTextbox);
            this.guna2CustomGradientPanel1.Controls.Add(this.XButton);
            this.guna2CustomGradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.guna2CustomGradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(143)))), ((int)(((byte)(250)))));
            this.guna2CustomGradientPanel1.FillColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.guna2CustomGradientPanel1.FillColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(474, -4);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.ShadowDecoration.Parent = this.guna2CustomGradientPanel1;
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(589, 590);
            this.guna2CustomGradientPanel1.TabIndex = 0;
            // 
            // RecoveryCodeButton
            // 
            this.RecoveryCodeButton.Animated = true;
            this.RecoveryCodeButton.BackColor = System.Drawing.Color.Transparent;
            this.RecoveryCodeButton.BorderRadius = 10;
            this.RecoveryCodeButton.CheckedState.Parent = this.RecoveryCodeButton;
            this.RecoveryCodeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RecoveryCodeButton.CustomImages.Parent = this.RecoveryCodeButton;
            this.RecoveryCodeButton.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecoveryCodeButton.ForeColor = System.Drawing.Color.White;
            this.RecoveryCodeButton.HoverState.Parent = this.RecoveryCodeButton;
            this.RecoveryCodeButton.Image = ((System.Drawing.Image)(resources.GetObject("RecoveryCodeButton.Image")));
            this.RecoveryCodeButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.RecoveryCodeButton.ImageSize = new System.Drawing.Size(30, 30);
            this.RecoveryCodeButton.Location = new System.Drawing.Point(493, 529);
            this.RecoveryCodeButton.Name = "RecoveryCodeButton";
            this.RecoveryCodeButton.ShadowDecoration.Parent = this.RecoveryCodeButton;
            this.RecoveryCodeButton.Size = new System.Drawing.Size(51, 43);
            this.RecoveryCodeButton.TabIndex = 16;
            this.RecoveryCodeButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.RecoveryCodeButton.TextOffset = new System.Drawing.Point(10, 0);
            this.RecoveryCodeButton.Click += new System.EventHandler(this.RecoveryCodeButton_Click);
            // 
            // ShowPasswordButton
            // 
            this.ShowPasswordButton.BackColor = System.Drawing.Color.Transparent;
            this.ShowPasswordButton.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("ShowPasswordButton.CheckedState.Image")));
            this.ShowPasswordButton.CheckedState.Parent = this.ShowPasswordButton;
            this.ShowPasswordButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ShowPasswordButton.HoverState.Parent = this.ShowPasswordButton;
            this.ShowPasswordButton.Image = ((System.Drawing.Image)(resources.GetObject("ShowPasswordButton.Image")));
            this.ShowPasswordButton.Location = new System.Drawing.Point(417, 313);
            this.ShowPasswordButton.Name = "ShowPasswordButton";
            this.ShowPasswordButton.PressedState.Parent = this.ShowPasswordButton;
            this.ShowPasswordButton.Size = new System.Drawing.Size(20, 20);
            this.ShowPasswordButton.TabIndex = 6;
            this.ShowPasswordButton.UseTransparentBackground = true;
            this.ShowPasswordButton.CheckedChanged += new System.EventHandler(this.ShowPasswordButton_CheckedChanged);
            // 
            // InvalidAccountLabel
            // 
            this.InvalidAccountLabel.AutoSize = true;
            this.InvalidAccountLabel.BackColor = System.Drawing.Color.Transparent;
            this.InvalidAccountLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InvalidAccountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.InvalidAccountLabel.Location = new System.Drawing.Point(210, 352);
            this.InvalidAccountLabel.Name = "InvalidAccountLabel";
            this.InvalidAccountLabel.Size = new System.Drawing.Size(168, 24);
            this.InvalidAccountLabel.TabIndex = 3;
            this.InvalidAccountLabel.Text = "Invalid Account";
            this.InvalidAccountLabel.Visible = false;
            // 
            // gunaLabel2
            // 
            this.gunaLabel2.AutoSize = true;
            this.gunaLabel2.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gunaLabel2.Location = new System.Drawing.Point(240, 118);
            this.gunaLabel2.Name = "gunaLabel2";
            this.gunaLabel2.Size = new System.Drawing.Size(108, 32);
            this.gunaLabel2.TabIndex = 3;
            this.gunaLabel2.Text = "LOG IN";
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.Color.Transparent;
            this.LoginButton.CheckedState.Parent = this.LoginButton;
            this.LoginButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoginButton.CustomImages.Parent = this.LoginButton;
            this.LoginButton.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginButton.ForeColor = System.Drawing.Color.White;
            this.LoginButton.HoverState.Parent = this.LoginButton;
            this.LoginButton.Image = ((System.Drawing.Image)(resources.GetObject("LoginButton.Image")));
            this.LoginButton.Location = new System.Drawing.Point(260, 400);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.LoginButton.ShadowDecoration.Parent = this.LoginButton;
            this.LoginButton.Size = new System.Drawing.Size(62, 61);
            this.LoginButton.TabIndex = 2;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // EmailTextbox
            // 
            this.EmailTextbox.AutoRoundedCorners = true;
            this.EmailTextbox.BackColor = System.Drawing.Color.Transparent;
            this.EmailTextbox.BorderRadius = 24;
            this.EmailTextbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.EmailTextbox.DefaultText = "";
            this.EmailTextbox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.EmailTextbox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.EmailTextbox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.EmailTextbox.DisabledState.Parent = this.EmailTextbox;
            this.EmailTextbox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.EmailTextbox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.EmailTextbox.FocusedState.Parent = this.EmailTextbox;
            this.EmailTextbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailTextbox.ForeColor = System.Drawing.Color.Black;
            this.EmailTextbox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.EmailTextbox.HoverState.Parent = this.EmailTextbox;
            this.EmailTextbox.Location = new System.Drawing.Point(117, 218);
            this.EmailTextbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EmailTextbox.Name = "EmailTextbox";
            this.EmailTextbox.PasswordChar = '\0';
            this.EmailTextbox.PlaceholderText = "Email";
            this.EmailTextbox.SelectedText = "";
            this.EmailTextbox.ShadowDecoration.Parent = this.EmailTextbox;
            this.EmailTextbox.Size = new System.Drawing.Size(337, 50);
            this.EmailTextbox.TabIndex = 0;
            this.EmailTextbox.TextChanged += new System.EventHandler(this.EmailTextbox_TextChanged);
            // 
            // PasswordTextbox
            // 
            this.PasswordTextbox.AutoRoundedCorners = true;
            this.PasswordTextbox.BackColor = System.Drawing.Color.Transparent;
            this.PasswordTextbox.BorderRadius = 24;
            this.PasswordTextbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PasswordTextbox.DefaultText = "";
            this.PasswordTextbox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PasswordTextbox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PasswordTextbox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordTextbox.DisabledState.Parent = this.PasswordTextbox;
            this.PasswordTextbox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PasswordTextbox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordTextbox.FocusedState.Parent = this.PasswordTextbox;
            this.PasswordTextbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTextbox.ForeColor = System.Drawing.Color.Black;
            this.PasswordTextbox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PasswordTextbox.HoverState.Parent = this.PasswordTextbox;
            this.PasswordTextbox.Location = new System.Drawing.Point(117, 298);
            this.PasswordTextbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PasswordTextbox.Name = "PasswordTextbox";
            this.PasswordTextbox.PasswordChar = '•';
            this.PasswordTextbox.PlaceholderText = "Password";
            this.PasswordTextbox.SelectedText = "";
            this.PasswordTextbox.ShadowDecoration.Parent = this.PasswordTextbox;
            this.PasswordTextbox.Size = new System.Drawing.Size(337, 50);
            this.PasswordTextbox.TabIndex = 1;
            this.PasswordTextbox.TextChanged += new System.EventHandler(this.PasswordTextbox_TextChanged);
            // 
            // XButton
            // 
            this.XButton.BackColor = System.Drawing.Color.Transparent;
            this.XButton.CheckedState.Parent = this.XButton;
            this.XButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.XButton.CustomImages.Parent = this.XButton;
            this.XButton.FillColor = System.Drawing.Color.Transparent;
            this.XButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.XButton.ForeColor = System.Drawing.Color.White;
            this.XButton.HoverState.Parent = this.XButton;
            this.XButton.Image = ((System.Drawing.Image)(resources.GetObject("XButton.Image")));
            this.XButton.Location = new System.Drawing.Point(520, 13);
            this.XButton.Name = "XButton";
            this.XButton.PressedColor = System.Drawing.Color.DimGray;
            this.XButton.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.XButton.ShadowDecoration.Parent = this.XButton;
            this.XButton.Size = new System.Drawing.Size(24, 24);
            this.XButton.TabIndex = 9;
            this.XButton.Click += new System.EventHandler(this.XButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(34, 114);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(392, 393);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.AutoSize = true;
            this.gunaLabel1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel1.Location = new System.Drawing.Point(52, 56);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(358, 32);
            this.gunaLabel1.TabIndex = 2;
            this.gunaLabel1.Text = "Task Management System";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(217)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1026, 580);
            this.Controls.Add(this.gunaLabel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOG IN";
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.UI2.WinForms.Guna2CircleButton XButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private Guna.UI2.WinForms.Guna2CircleButton LoginButton;
        private Guna.UI2.WinForms.Guna2TextBox EmailTextbox;
        private Guna.UI2.WinForms.Guna2TextBox PasswordTextbox;
        private Guna.UI.WinForms.GunaLabel gunaLabel2;
        private Guna.UI.WinForms.GunaLabel InvalidAccountLabel;
        private Guna.UI2.WinForms.Guna2ImageCheckBox ShowPasswordButton;
        private Guna.UI2.WinForms.Guna2Button RecoveryCodeButton;
    }
}

