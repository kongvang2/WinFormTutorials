namespace AirlineReservationSystem
{
    partial class frmReservation
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbSeatRow = new System.Windows.Forms.ComboBox();
            this.radA = new System.Windows.Forms.RadioButton();
            this.radD = new System.Windows.Forms.RadioButton();
            this.radC = new System.Windows.Forms.RadioButton();
            this.radB = new System.Windows.Forms.RadioButton();
            this.btnAddPassenger = new System.Windows.Forms.Button();
            this.btnShowPassenger = new System.Windows.Forms.Button();
            this.btnSearchPassenger = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(65, 15);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(135, 20);
            this.txtName.TabIndex = 0;
            // 
            // cmbSeatRow
            // 
            this.cmbSeatRow.FormattingEnabled = true;
            this.cmbSeatRow.Location = new System.Drawing.Point(65, 64);
            this.cmbSeatRow.Name = "cmbSeatRow";
            this.cmbSeatRow.Size = new System.Drawing.Size(66, 21);
            this.cmbSeatRow.TabIndex = 1;
            // 
            // radA
            // 
            this.radA.AutoSize = true;
            this.radA.Location = new System.Drawing.Point(78, 122);
            this.radA.Name = "radA";
            this.radA.Size = new System.Drawing.Size(32, 17);
            this.radA.TabIndex = 2;
            this.radA.TabStop = true;
            this.radA.Text = "A";
            this.radA.UseVisualStyleBackColor = true;
            this.radA.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radD
            // 
            this.radD.AutoSize = true;
            this.radD.Location = new System.Drawing.Point(116, 153);
            this.radD.Name = "radD";
            this.radD.Size = new System.Drawing.Size(33, 17);
            this.radD.TabIndex = 3;
            this.radD.TabStop = true;
            this.radD.Text = "D";
            this.radD.UseVisualStyleBackColor = true;
            // 
            // radC
            // 
            this.radC.AutoSize = true;
            this.radC.Location = new System.Drawing.Point(78, 153);
            this.radC.Name = "radC";
            this.radC.Size = new System.Drawing.Size(32, 17);
            this.radC.TabIndex = 4;
            this.radC.TabStop = true;
            this.radC.Text = "C";
            this.radC.UseVisualStyleBackColor = true;
            // 
            // radB
            // 
            this.radB.AutoSize = true;
            this.radB.Location = new System.Drawing.Point(116, 122);
            this.radB.Name = "radB";
            this.radB.Size = new System.Drawing.Size(32, 17);
            this.radB.TabIndex = 5;
            this.radB.TabStop = true;
            this.radB.Text = "B";
            this.radB.UseVisualStyleBackColor = true;
            // 
            // btnAddPassenger
            // 
            this.btnAddPassenger.Location = new System.Drawing.Point(65, 206);
            this.btnAddPassenger.Name = "btnAddPassenger";
            this.btnAddPassenger.Size = new System.Drawing.Size(135, 23);
            this.btnAddPassenger.TabIndex = 6;
            this.btnAddPassenger.Text = "&Add Passenger";
            this.btnAddPassenger.UseVisualStyleBackColor = true;
            this.btnAddPassenger.Click += new System.EventHandler(this.btnAddPassenger_Click);
            // 
            // btnShowPassenger
            // 
            this.btnShowPassenger.Location = new System.Drawing.Point(65, 246);
            this.btnShowPassenger.Name = "btnShowPassenger";
            this.btnShowPassenger.Size = new System.Drawing.Size(135, 23);
            this.btnShowPassenger.TabIndex = 7;
            this.btnShowPassenger.Text = "Sh&ow Passenger";
            this.btnShowPassenger.UseVisualStyleBackColor = true;
            this.btnShowPassenger.Click += new System.EventHandler(this.btnShowPassenger_Click);
            // 
            // btnSearchPassenger
            // 
            this.btnSearchPassenger.Location = new System.Drawing.Point(65, 286);
            this.btnSearchPassenger.Name = "btnSearchPassenger";
            this.btnSearchPassenger.Size = new System.Drawing.Size(135, 23);
            this.btnSearchPassenger.TabIndex = 8;
            this.btnSearchPassenger.Text = "&Search Passenger";
            this.btnSearchPassenger.UseVisualStyleBackColor = true;
            this.btnSearchPassenger.Click += new System.EventHandler(this.btnSearchPassenger_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(65, 326);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(135, 23);
            this.btnQuit.TabIndex = 9;
            this.btnQuit.Text = "&Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.Location = new System.Drawing.Point(233, 34);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(138, 316);
            this.lstOutput.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Seat:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "(1A, 1B, 1C, 1D, ... 10D)";
            // 
            // frmReservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 373);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnSearchPassenger);
            this.Controls.Add(this.btnShowPassenger);
            this.Controls.Add(this.btnAddPassenger);
            this.Controls.Add(this.radB);
            this.Controls.Add(this.radC);
            this.Controls.Add(this.radD);
            this.Controls.Add(this.radA);
            this.Controls.Add(this.cmbSeatRow);
            this.Controls.Add(this.txtName);
            this.Name = "frmReservation";
            this.Text = "Airline Reservations";
            this.Load += new System.EventHandler(this.frmReservation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbSeatRow;
        private System.Windows.Forms.RadioButton radA;
        private System.Windows.Forms.RadioButton radD;
        private System.Windows.Forms.RadioButton radC;
        private System.Windows.Forms.RadioButton radB;
        private System.Windows.Forms.Button btnAddPassenger;
        private System.Windows.Forms.Button btnShowPassenger;
        private System.Windows.Forms.Button btnSearchPassenger;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

