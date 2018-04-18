namespace Warcaby {
    partial class Form1 {
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
            this.btnPokaz = new System.Windows.Forms.Button();
            this.btnKalPionki = new System.Windows.Forms.Button();
            this.btnKalPlansze = new System.Windows.Forms.Button();
            this.btnRozpoznajPionki = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPokaz
            // 
            this.btnPokaz.Location = new System.Drawing.Point(12, 12);
            this.btnPokaz.Name = "btnPokaz";
            this.btnPokaz.Size = new System.Drawing.Size(102, 23);
            this.btnPokaz.TabIndex = 0;
            this.btnPokaz.Text = "Pokaż obraz";
            this.btnPokaz.UseVisualStyleBackColor = true;
            this.btnPokaz.Click += new System.EventHandler(this.btnPokaz_Click);
            // 
            // btnKalPionki
            // 
            this.btnKalPionki.Location = new System.Drawing.Point(12, 41);
            this.btnKalPionki.Name = "btnKalPionki";
            this.btnKalPionki.Size = new System.Drawing.Size(102, 23);
            this.btnKalPionki.TabIndex = 1;
            this.btnKalPionki.Text = "Kalibruj pionki";
            this.btnKalPionki.UseVisualStyleBackColor = true;
            this.btnKalPionki.Click += new System.EventHandler(this.btnKalPionki_Click);
            // 
            // btnKalPlansze
            // 
            this.btnKalPlansze.Location = new System.Drawing.Point(12, 70);
            this.btnKalPlansze.Name = "btnKalPlansze";
            this.btnKalPlansze.Size = new System.Drawing.Size(102, 23);
            this.btnKalPlansze.TabIndex = 2;
            this.btnKalPlansze.Text = "Kalibruj planszę";
            this.btnKalPlansze.UseVisualStyleBackColor = true;
            this.btnKalPlansze.Click += new System.EventHandler(this.btnKalPlansze_Click);
            // 
            // btnRozpoznajPionki
            // 
            this.btnRozpoznajPionki.Location = new System.Drawing.Point(12, 99);
            this.btnRozpoznajPionki.Name = "btnRozpoznajPionki";
            this.btnRozpoznajPionki.Size = new System.Drawing.Size(102, 23);
            this.btnRozpoznajPionki.TabIndex = 3;
            this.btnRozpoznajPionki.Text = "Rozpoznaj pionki";
            this.btnRozpoznajPionki.UseVisualStyleBackColor = true;
            this.btnRozpoznajPionki.Click += new System.EventHandler(this.btnRozpoznajPionki_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnRozpoznajPionki);
            this.Controls.Add(this.btnKalPlansze);
            this.Controls.Add(this.btnKalPionki);
            this.Controls.Add(this.btnPokaz);
            this.Name = "Form1";
            this.Text = "Warcaby";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPokaz;
        private System.Windows.Forms.Button btnKalPionki;
        private System.Windows.Forms.Button btnKalPlansze;
        private System.Windows.Forms.Button btnRozpoznajPionki;
    }
}

