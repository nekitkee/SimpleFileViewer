namespace BmpPlugin
{
    partial class BmpViewerForm
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
            this.BmpPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BmpPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // BmpPictureBox
            // 
            this.BmpPictureBox.Location = new System.Drawing.Point(12, 12);
            this.BmpPictureBox.Name = "BmpPictureBox";
            this.BmpPictureBox.Size = new System.Drawing.Size(776, 426);
            this.BmpPictureBox.TabIndex = 0;
            this.BmpPictureBox.TabStop = false;
            // 
            // BmpViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BmpPictureBox);
            this.Name = "BmpViewerForm";
            this.Text = "BmpViewerForm";
            ((System.ComponentModel.ISupportInitialize)(this.BmpPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox BmpPictureBox;
    }
}