using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TASK_MANAGEMENT_SYSTEM.TASK_SECTION
{
    public partial class ImagePreviewForm : Form
    {
        private readonly byte[] imageData;

        public ImagePreviewForm(byte[] imageData)
        {
            this.imageData = imageData;
            InitializeComponent();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            if (imageData != null)
            {
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    pictureBox.Image = Image.FromStream(ms);
                }
            }

            Controls.Add(pictureBox);


            // Set form properties
            Text = "Image Preview";
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MinimumSize = new Size(700, 700); // Set a minimum size if needed
            Resize += (sender, e) => ResizeForm(pictureBox.Image);
        }

        private void ResizeForm(Image image)
        {
            if (image != null)
            {
                int newWidth = image.Width;
                int newHeight = image.Height;

                float aspectRatio = (float)newWidth / newHeight;

                int maxWidth = Screen.PrimaryScreen.WorkingArea.Width - 50; // Adjust as needed
                int maxHeight = Screen.PrimaryScreen.WorkingArea.Height - 50; // Adjust as needed

                if (newWidth > maxWidth)
                {
                    newWidth = maxWidth;
                    newHeight = (int)(newWidth / aspectRatio);
                }

                if (newHeight > maxHeight)
                {
                    newHeight = maxHeight;
                    newWidth = (int)(newHeight * aspectRatio);
                }

                Size = new Size(newWidth, newHeight);
            }
        }
    }
}
