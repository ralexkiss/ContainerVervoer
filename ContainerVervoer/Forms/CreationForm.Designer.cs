﻿namespace ContainerVervoer.Forms
{
    partial class CreationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreationForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.shipLengthValue = new System.Windows.Forms.NumericUpDown();
            this.shipWidthValue = new System.Windows.Forms.NumericUpDown();
            this.CreateShipBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shipLengthValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shipWidthValue)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.shipLengthValue);
            this.groupBox1.Controls.Add(this.shipWidthValue);
            this.groupBox1.Controls.Add(this.CreateShipBtn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create CargoShip";
            // 
            // shipLengthValue
            // 
            this.shipLengthValue.Location = new System.Drawing.Point(61, 83);
            this.shipLengthValue.Name = "shipLengthValue";
            this.shipLengthValue.Size = new System.Drawing.Size(95, 20);
            this.shipLengthValue.TabIndex = 11;
            // 
            // shipWidthValue
            // 
            this.shipWidthValue.Location = new System.Drawing.Point(61, 57);
            this.shipWidthValue.Name = "shipWidthValue";
            this.shipWidthValue.Size = new System.Drawing.Size(95, 20);
            this.shipWidthValue.TabIndex = 10;
            // 
            // CreateShipBtn
            // 
            this.CreateShipBtn.Location = new System.Drawing.Point(9, 109);
            this.CreateShipBtn.Name = "CreateShipBtn";
            this.CreateShipBtn.Size = new System.Drawing.Size(147, 23);
            this.CreateShipBtn.TabIndex = 7;
            this.CreateShipBtn.Text = "Create CargoShip";
            this.CreateShipBtn.UseVisualStyleBackColor = true;
            this.CreateShipBtn.Click += new System.EventHandler(this.CreateShipBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Length:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Width:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ship Dimensions";
            // 
            // CreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 153);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup CargoShip";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shipLengthValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shipWidthValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CreateShipBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown shipWidthValue;
        public System.Windows.Forms.NumericUpDown shipLengthValue;
    }
}
