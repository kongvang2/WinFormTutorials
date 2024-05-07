namespace AirlineReservationSystem
{
    partial class frmEditDelete
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
            this.txtPassengerID = new System.Windows.Forms.TextBox();
            this.txtPassengerName = new System.Windows.Forms.TextBox();
            this.txtSeatID = new System.Windows.Forms.TextBox();
            this.cmbSeatRow = new System.Windows.Forms.ComboBox();
            this.cmbSeatColumn = new System.Windows.Forms.ComboBox();
            this.chbWaiting = new System.Windows.Forms.CheckBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPassengerID
            // 
            this.txtPassengerID.Location = new System.Drawing.Point(114, 34);
            this.txtPassengerID.Name = "txtPassengerID";
            this.txtPassengerID.ReadOnly = true;
            this.txtPassengerID.Size = new System.Drawing.Size(148, 20);
            this.txtPassengerID.TabIndex = 0;
            // 
            // txtPassengerName
            // 
            this.txtPassengerName.Location = new System.Drawing.Point(114, 72);
            this.txtPassengerName.Name = "txtPassengerName";
            this.txtPassengerName.Size = new System.Drawing.Size(148, 20);
            this.txtPassengerName.TabIndex = 1;
            // 
            // txtSeatID
            // 
            this.txtSeatID.Location = new System.Drawing.Point(114, 110);
            this.txtSeatID.Name = "txtSeatID";
            this.txtSeatID.ReadOnly = true;
            this.txtSeatID.Size = new System.Drawing.Size(148, 20);
            this.txtSeatID.TabIndex = 2;
            // 
            // cmbSeatRow
            // 
            this.cmbSeatRow.FormattingEnabled = true;
            this.cmbSeatRow.Items.AddRange(new object[] {
            "None",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbSeatRow.Location = new System.Drawing.Point(114, 148);
            this.cmbSeatRow.Name = "cmbSeatRow";
            this.cmbSeatRow.Size = new System.Drawing.Size(148, 21);
            this.cmbSeatRow.TabIndex = 3;
            // 
            // cmbSeatColumn
            // 
            this.cmbSeatColumn.FormattingEnabled = true;
            this.cmbSeatColumn.Items.AddRange(new object[] {
            "None",
            "A",
            "B",
            "C",
            "D"});
            this.cmbSeatColumn.Location = new System.Drawing.Point(114, 187);
            this.cmbSeatColumn.Name = "cmbSeatColumn";
            this.cmbSeatColumn.Size = new System.Drawing.Size(148, 21);
            this.cmbSeatColumn.TabIndex = 4;
            // 
            // chbWaiting
            // 
            this.chbWaiting.AutoSize = true;
            this.chbWaiting.Location = new System.Drawing.Point(114, 226);
            this.chbWaiting.Name = "chbWaiting";
            this.chbWaiting.Size = new System.Drawing.Size(15, 14);
            this.chbWaiting.TabIndex = 5;
            this.chbWaiting.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(20, 251);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "Commit &Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(114, 251);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(214, 251);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Passenger ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Passenger Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Seat ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Seat Row";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Seat Column";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "On Waiting List";
            // 
            // frmEditDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 292);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.chbWaiting);
            this.Controls.Add(this.cmbSeatColumn);
            this.Controls.Add(this.cmbSeatRow);
            this.Controls.Add(this.txtSeatID);
            this.Controls.Add(this.txtPassengerName);
            this.Controls.Add(this.txtPassengerID);
            this.Name = "frmEditDelete";
            this.Text = "Edit or Delete Passenger Information";
            this.Load += new System.EventHandler(this.frmEditDelete_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassengerID;
        private System.Windows.Forms.TextBox txtPassengerName;
        private System.Windows.Forms.TextBox txtSeatID;
        private System.Windows.Forms.ComboBox cmbSeatRow;
        private System.Windows.Forms.ComboBox cmbSeatColumn;
        private System.Windows.Forms.CheckBox chbWaiting;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}