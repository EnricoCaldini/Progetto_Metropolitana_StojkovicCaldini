namespace metropolitana
{
    partial class Dettagli
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
            this.btnConf = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConf
            // 
            this.btnConf.Location = new System.Drawing.Point(12, 73);
            this.btnConf.Name = "btnConf";
            this.btnConf.Size = new System.Drawing.Size(434, 123);
            this.btnConf.TabIndex = 0;
            this.btnConf.Text = "Conferma";
            this.btnConf.UseVisualStyleBackColor = true;
            this.btnConf.Click += new System.EventHandler(this.btnConf_Click);
            // 
            // Dettagli
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(459, 206);
            this.Controls.Add(this.btnConf);
            this.Name = "Dettagli";
            this.Text = "Dettagli";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConf;
    }
}