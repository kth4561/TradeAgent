﻿namespace TradeAgent
{
    partial class MainForm
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
            this.rbConsole = new TradeAgent.Control.RichTextBoxEx();
            this.btSync = new System.Windows.Forms.Button();
            this.btGetData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbConsole
            // 
            this.rbConsole.BackColor = System.Drawing.Color.Black;
            this.rbConsole.ForeColor = System.Drawing.Color.White;
            this.rbConsole.Location = new System.Drawing.Point(12, 12);
            this.rbConsole.Name = "rbConsole";
            this.rbConsole.ReadOnly = true;
            this.rbConsole.Size = new System.Drawing.Size(599, 515);
            this.rbConsole.TabIndex = 0;
            this.rbConsole.Text = "";
            this.rbConsole.TextChanged += new System.EventHandler(this.rbConsole_TextChanged);
            // 
            // btSync
            // 
            this.btSync.Location = new System.Drawing.Point(618, 13);
            this.btSync.Name = "btSync";
            this.btSync.Size = new System.Drawing.Size(125, 23);
            this.btSync.TabIndex = 1;
            this.btSync.Text = "종목정보 동기화";
            this.btSync.UseVisualStyleBackColor = true;
            this.btSync.Click += new System.EventHandler(this.btSync_Click);
            // 
            // btGetData
            // 
            this.btGetData.Location = new System.Drawing.Point(618, 43);
            this.btGetData.Name = "btGetData";
            this.btGetData.Size = new System.Drawing.Size(125, 23);
            this.btGetData.TabIndex = 2;
            this.btGetData.Text = "btGetData";
            this.btGetData.UseVisualStyleBackColor = true;
            this.btGetData.Click += new System.EventHandler(this.btGetData_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 539);
            this.Controls.Add(this.btGetData);
            this.Controls.Add(this.btSync);
            this.Controls.Add(this.rbConsole);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

//        private System.Windows.Forms.RichTextBox rbConsole;
        private TradeAgent.Control.RichTextBoxEx rbConsole;
        private System.Windows.Forms.Button btSync;
        private System.Windows.Forms.Button btGetData;
    }
}