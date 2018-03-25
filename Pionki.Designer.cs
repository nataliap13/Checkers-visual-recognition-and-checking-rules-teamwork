namespace Warcaby {
    partial class wndPionki {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.pctPlansza = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctPlansza)).BeginInit();
            this.SuspendLayout();
            // 
            // pctPlansza
            // 
            this.pctPlansza.Location = new System.Drawing.Point(12, 12);
            this.pctPlansza.Name = "pctPlansza";
            this.pctPlansza.Size = new System.Drawing.Size(321, 321);
            this.pctPlansza.TabIndex = 0;
            this.pctPlansza.TabStop = false;
            // 
            // wndPionki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 346);
            this.Controls.Add(this.pctPlansza);
            this.Name = "wndPionki";
            this.Text = "Pionki";
            ((System.ComponentModel.ISupportInitialize)(this.pctPlansza)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox pctPlansza;
    }
}