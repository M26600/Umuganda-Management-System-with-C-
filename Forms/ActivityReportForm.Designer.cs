namespace UmugandaManagementSystem.Forms
{
    partial class ActivityReportForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblActivity = new System.Windows.Forms.Label();
            this.lblAttendeeCount = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.cmbActivity = new System.Windows.Forms.ComboBox();
            this.numAttendeeCount = new System.Windows.Forms.NumericUpDown();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtWorkDescription = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numAttendeeCount)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(301, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(76, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Activity Report";
            // 
            // lblActivity
            // 
            this.lblActivity.AutoSize = true;
            this.lblActivity.Location = new System.Drawing.Point(156, 67);
            this.lblActivity.Name = "lblActivity";
            this.lblActivity.Size = new System.Drawing.Size(74, 13);
            this.lblActivity.TabIndex = 1;
            this.lblActivity.Text = "Select Activity";
            // 
            // lblAttendeeCount
            // 
            this.lblAttendeeCount.AutoSize = true;
            this.lblAttendeeCount.Location = new System.Drawing.Point(156, 98);
            this.lblAttendeeCount.Name = "lblAttendeeCount";
            this.lblAttendeeCount.Size = new System.Drawing.Size(107, 13);
            this.lblAttendeeCount.TabIndex = 2;
            this.lblAttendeeCount.Text = "Number of Attendees";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(156, 130);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(89, 13);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Work Description";
            // 
            // cmbActivity
            // 
            this.cmbActivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActivity.FormattingEnabled = true;
            this.cmbActivity.Location = new System.Drawing.Point(315, 64);
            this.cmbActivity.Name = "cmbActivity";
            this.cmbActivity.Size = new System.Drawing.Size(175, 21);
            this.cmbActivity.TabIndex = 4;
            // 
            // numAttendeeCount
            // 
            this.numAttendeeCount.Location = new System.Drawing.Point(315, 96);
            this.numAttendeeCount.Name = "numAttendeeCount";
            this.numAttendeeCount.Size = new System.Drawing.Size(175, 20);
            this.numAttendeeCount.TabIndex = 5;
            this.numAttendeeCount.ValueChanged += new System.EventHandler(this.numAttendeeCount_ValueChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(254, 174);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Submit Report";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(512, 34);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // txtWorkDescription
            // 
            this.txtWorkDescription.Location = new System.Drawing.Point(315, 127);
            this.txtWorkDescription.Name = "txtWorkDescription";
            this.txtWorkDescription.Size = new System.Drawing.Size(175, 20);
            this.txtWorkDescription.TabIndex = 8;
            this.txtWorkDescription.Text = "Enter report details";
            // 
            // ActivityReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtWorkDescription);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.numAttendeeCount);
            this.Controls.Add(this.cmbActivity);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblAttendeeCount);
            this.Controls.Add(this.lblActivity);
            this.Controls.Add(this.lblTitle);
            this.Name = "ActivityReportForm";
            this.Text = "ActivityReportForm";
            this.Load += new System.EventHandler(this.ActivityReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numAttendeeCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblActivity;
        private System.Windows.Forms.Label lblAttendeeCount;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.ComboBox cmbActivity;
        private System.Windows.Forms.NumericUpDown numAttendeeCount;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.MaskedTextBox txtWorkDescription;
    }
}