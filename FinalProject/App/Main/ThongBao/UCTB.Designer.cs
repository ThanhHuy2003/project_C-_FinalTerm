﻿namespace FinalProject.App.Main
{
    partial class UCTB
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuNotification = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // menuNotification
            // 
            this.menuNotification.Location = new System.Drawing.Point(49, 30);
            this.menuNotification.Margin = new System.Windows.Forms.Padding(4);
            this.menuNotification.Name = "menuNotification";
            this.menuNotification.Size = new System.Drawing.Size(1289, 790);
            this.menuNotification.TabIndex = 0;
            // 
            // UCTB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.menuNotification);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UCTB";
            this.Size = new System.Drawing.Size(1400, 923);
            this.Load += new System.EventHandler(this.UCTB_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel menuNotification;
    }
}