﻿namespace SQLTester
{
    partial class frmTester
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
            this.grdRecord = new System.Windows.Forms.DataGridView();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // grdRecord
            // 
            this.grdRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRecord.Location = new System.Drawing.Point(12, 12);
            this.grdRecord.Name = "grdRecord";
            this.grdRecord.Size = new System.Drawing.Size(818, 337);
            this.grdRecord.TabIndex = 0;
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(13, 364);
            this.txtCommand.Multiline = true;
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCommand.Size = new System.Drawing.Size(569, 145);
            this.txtCommand.TabIndex = 1;
            // 
            // btnExecute
            // 
            this.btnExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.Location = new System.Drawing.Point(643, 364);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(187, 23);
            this.btnExecute.TabIndex = 2;
            this.btnExecute.Text = "Execute Statement";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(719, 409);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(15, 16);
            this.lblCount.TabIndex = 3;
            this.lblCount.Text = "0";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(640, 409);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Record Count";
            // 
            // frmTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 532);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.grdRecord);
            this.Name = "frmTester";
            this.Text = "SQL Tester";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFormClosing);
            this.Load += new System.EventHandler(this.frmTester_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdRecord)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdRecord;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label2;
    }
}

