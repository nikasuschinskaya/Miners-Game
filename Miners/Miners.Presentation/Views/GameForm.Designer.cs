namespace Miners.Presentation.Views
{
    partial class GameForm
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
            this.glControl = new OpenTK.GLControl();
            this.firstNameLabel = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.secondNameLabel = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Location = new System.Drawing.Point(0, 65);
            this.glControl.Margin = new System.Windows.Forms.Padding(5);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(993, 518);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = false;
            // 
            // firstNameLabel
            // 
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.firstNameLabel.Location = new System.Drawing.Point(73, 16);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Size = new System.Drawing.Size(86, 29);
            this.firstNameLabel.TabIndex = 1;
            this.firstNameLabel.Text = "Miners";
            // 
            // groupBox
            // 
            this.groupBox.BackColor = System.Drawing.Color.LimeGreen;
            this.groupBox.Controls.Add(this.secondNameLabel);
            this.groupBox.Controls.Add(this.firstNameLabel);
            this.groupBox.Location = new System.Drawing.Point(0, -1);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(993, 67);
            this.groupBox.TabIndex = 2;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Miners";
            // 
            // secondNameLabel
            // 
            this.secondNameLabel.AutoSize = true;
            this.secondNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.secondNameLabel.Location = new System.Drawing.Point(847, 16);
            this.secondNameLabel.Name = "secondNameLabel";
            this.secondNameLabel.Size = new System.Drawing.Size(86, 29);
            this.secondNameLabel.TabIndex = 2;
            this.secondNameLabel.Text = "Miners";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 583);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.glControl);
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameForm";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl glControl;
        private System.Windows.Forms.Label firstNameLabel;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label secondNameLabel;
    }
}