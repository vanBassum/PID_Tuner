namespace PID_Tuner
{
    partial class F_Filter
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
            this.pN_Viewer1 = new PID_Tuner.PN_Viewer();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // pN_Viewer1
            // 
            this.pN_Viewer1.Location = new System.Drawing.Point(12, 12);
            this.pN_Viewer1.Name = "pN_Viewer1";
            this.pN_Viewer1.Size = new System.Drawing.Size(419, 426);
            this.pN_Viewer1.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(437, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 1;
            // 
            // F_Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.pN_Viewer1);
            this.Name = "F_Filter";
            this.Text = "F_Filter";
            this.Load += new System.EventHandler(this.F_Filter_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PN_Viewer pN_Viewer1;
        private System.Windows.Forms.ListBox listBox1;
    }
}