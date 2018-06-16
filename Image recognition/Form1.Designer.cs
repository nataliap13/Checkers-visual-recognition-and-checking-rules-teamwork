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
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnKalDamkiWrog = new System.Windows.Forms.Button();
            this.btnKalPionkiWrog = new System.Windows.Forms.Button();
            this.btnKalDamki = new System.Windows.Forms.Button();
            this.lblBlad = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPokaz
            // 
            this.btnPokaz.Location = new System.Drawing.Point(8, 23);
            this.btnPokaz.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPokaz.Name = "btnPokaz";
            this.btnPokaz.Size = new System.Drawing.Size(136, 28);
            this.btnPokaz.TabIndex = 0;
            this.btnPokaz.Text = "Pokaż obraz";
            this.btnPokaz.UseVisualStyleBackColor = true;
            this.btnPokaz.Click += new System.EventHandler(this.btnPokaz_Click);
            // 
            // btnKalPionki
            // 
            this.btnKalPionki.Location = new System.Drawing.Point(152, 23);
            this.btnKalPionki.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnKalPionki.Name = "btnKalPionki";
            this.btnKalPionki.Size = new System.Drawing.Size(187, 28);
            this.btnKalPionki.TabIndex = 1;
            this.btnKalPionki.Text = "Kalibruj własne pionki";
            this.btnKalPionki.UseVisualStyleBackColor = true;
            this.btnKalPionki.Click += new System.EventHandler(this.btnKalPionki_Click);
            // 
            // btnKalPlansze
            // 
            this.btnKalPlansze.Location = new System.Drawing.Point(8, 59);
            this.btnKalPlansze.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnKalPlansze.Name = "btnKalPlansze";
            this.btnKalPlansze.Size = new System.Drawing.Size(136, 28);
            this.btnKalPlansze.TabIndex = 2;
            this.btnKalPlansze.Text = "Kalibruj planszę";
            this.btnKalPlansze.UseVisualStyleBackColor = true;
            this.btnKalPlansze.Click += new System.EventHandler(this.btnKalPlansze_Click);
            // 
            // btnRozpoznajPionki
            // 
            this.btnRozpoznajPionki.Location = new System.Drawing.Point(8, 95);
            this.btnRozpoznajPionki.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRozpoznajPionki.Name = "btnRozpoznajPionki";
            this.btnRozpoznajPionki.Size = new System.Drawing.Size(136, 28);
            this.btnRozpoznajPionki.TabIndex = 3;
            this.btnRozpoznajPionki.Text = "Rozpoznaj pionki";
            this.btnRozpoznajPionki.UseVisualStyleBackColor = true;
            this.btnRozpoznajPionki.Click += new System.EventHandler(this.btnRozpoznajPionki_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnStart.Location = new System.Drawing.Point(16, 15);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(352, 92);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Rozpocznij rozgrywkę";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnKalDamkiWrog);
            this.groupBox1.Controls.Add(this.btnKalPionkiWrog);
            this.groupBox1.Controls.Add(this.btnKalDamki);
            this.groupBox1.Controls.Add(this.btnPokaz);
            this.groupBox1.Controls.Add(this.btnKalPionki);
            this.groupBox1.Controls.Add(this.btnRozpoznajPionki);
            this.groupBox1.Controls.Add(this.btnKalPlansze);
            this.groupBox1.Location = new System.Drawing.Point(16, 114);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(352, 174);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ustawienia";
            // 
            // btnKalDamkiWrog
            // 
            this.btnKalDamkiWrog.Location = new System.Drawing.Point(152, 130);
            this.btnKalDamkiWrog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnKalDamkiWrog.Name = "btnKalDamkiWrog";
            this.btnKalDamkiWrog.Size = new System.Drawing.Size(187, 28);
            this.btnKalDamkiWrog.TabIndex = 6;
            this.btnKalDamkiWrog.Text = "Kalibruj damki przeciwnika";
            this.btnKalDamkiWrog.UseVisualStyleBackColor = true;
            this.btnKalDamkiWrog.Click += new System.EventHandler(this.btnKalDamkiWrog_Click);
            // 
            // btnKalPionkiWrog
            // 
            this.btnKalPionkiWrog.Location = new System.Drawing.Point(152, 95);
            this.btnKalPionkiWrog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnKalPionkiWrog.Name = "btnKalPionkiWrog";
            this.btnKalPionkiWrog.Size = new System.Drawing.Size(187, 28);
            this.btnKalPionkiWrog.TabIndex = 5;
            this.btnKalPionkiWrog.Text = "Kalibruj pionki przeciwnika";
            this.btnKalPionkiWrog.UseVisualStyleBackColor = true;
            this.btnKalPionkiWrog.Click += new System.EventHandler(this.btnKalPionkiWrog_Click);
            // 
            // btnKalDamki
            // 
            this.btnKalDamki.Location = new System.Drawing.Point(152, 59);
            this.btnKalDamki.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnKalDamki.Name = "btnKalDamki";
            this.btnKalDamki.Size = new System.Drawing.Size(187, 28);
            this.btnKalDamki.TabIndex = 4;
            this.btnKalDamki.Text = "Kalibruj własne damki";
            this.btnKalDamki.UseVisualStyleBackColor = true;
            this.btnKalDamki.Click += new System.EventHandler(this.btnKalDamki_Click);
            // 
            // lblBlad
            // 
            this.lblBlad.AutoSize = true;
            this.lblBlad.Location = new System.Drawing.Point(13, 292);
            this.lblBlad.Name = "lblBlad";
            this.lblBlad.Size = new System.Drawing.Size(0, 17);
            this.lblBlad.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 336);
            this.Controls.Add(this.lblBlad);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Warcaby";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPokaz;
        private System.Windows.Forms.Button btnKalPionki;
        private System.Windows.Forms.Button btnKalPlansze;
        private System.Windows.Forms.Button btnRozpoznajPionki;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnKalDamkiWrog;
        private System.Windows.Forms.Button btnKalPionkiWrog;
        private System.Windows.Forms.Button btnKalDamki;
        private System.Windows.Forms.Label lblBlad;
    }
}

