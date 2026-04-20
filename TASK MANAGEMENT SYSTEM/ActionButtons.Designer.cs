namespace TASK_MANAGEMENT_SYSTEM.UserControls
{
    partial class ActionButtons
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionButtons));
            this.EditButton = new Guna.UI2.WinForms.Guna2Button();
            this.DeleteButton = new Guna.UI2.WinForms.Guna2Button();
            this.ViewButton = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // EditButton
            // 
            this.EditButton.Animated = true;
            this.EditButton.BorderRadius = 5;
            this.EditButton.CheckedState.Parent = this.EditButton;
            this.EditButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EditButton.CustomImages.Parent = this.EditButton;
            this.EditButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.EditButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.EditButton.ForeColor = System.Drawing.Color.White;
            this.EditButton.HoverState.Parent = this.EditButton;
            this.EditButton.Image = ((System.Drawing.Image)(resources.GetObject("EditButton.Image")));
            this.EditButton.ImageSize = new System.Drawing.Size(15, 15);
            this.EditButton.Location = new System.Drawing.Point(39, 3);
            this.EditButton.Name = "EditButton";
            this.EditButton.ShadowDecoration.Parent = this.EditButton;
            this.EditButton.Size = new System.Drawing.Size(36, 35);
            this.EditButton.TabIndex = 0;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Animated = true;
            this.DeleteButton.BorderRadius = 5;
            this.DeleteButton.CheckedState.Parent = this.DeleteButton;
            this.DeleteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteButton.CustomImages.Parent = this.DeleteButton;
            this.DeleteButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DeleteButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DeleteButton.ForeColor = System.Drawing.Color.White;
            this.DeleteButton.HoverState.Parent = this.DeleteButton;
            this.DeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteButton.Image")));
            this.DeleteButton.ImageSize = new System.Drawing.Size(15, 15);
            this.DeleteButton.Location = new System.Drawing.Point(77, 3);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.ShadowDecoration.Parent = this.DeleteButton;
            this.DeleteButton.Size = new System.Drawing.Size(36, 35);
            this.DeleteButton.TabIndex = 1;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ViewButton
            // 
            this.ViewButton.Animated = true;
            this.ViewButton.BorderRadius = 5;
            this.ViewButton.CheckedState.Parent = this.ViewButton;
            this.ViewButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ViewButton.CustomImages.Parent = this.ViewButton;
            this.ViewButton.FillColor = System.Drawing.Color.Teal;
            this.ViewButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ViewButton.ForeColor = System.Drawing.Color.White;
            this.ViewButton.HoverState.Parent = this.ViewButton;
            this.ViewButton.Image = ((System.Drawing.Image)(resources.GetObject("ViewButton.Image")));
            this.ViewButton.ImageSize = new System.Drawing.Size(15, 15);
            this.ViewButton.Location = new System.Drawing.Point(3, 3);
            this.ViewButton.Name = "ViewButton";
            this.ViewButton.ShadowDecoration.Parent = this.ViewButton;
            this.ViewButton.Size = new System.Drawing.Size(36, 35);
            this.ViewButton.TabIndex = 2;
            this.ViewButton.Click += new System.EventHandler(this.ViewButton_Click);
            // 
            // ActionButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ViewButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.EditButton);
            this.Name = "ActionButtons";
            this.Size = new System.Drawing.Size(117, 43);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button EditButton;
        private Guna.UI2.WinForms.Guna2Button DeleteButton;
        private Guna.UI2.WinForms.Guna2Button ViewButton;
    }
}
