namespace UmugandaManagementSystem.Forms
{
    partial class ActivityManagementForm
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
            this.lblVillage = new System.Windows.Forms.Label();
            this.lblActivityType = new System.Windows.Forms.Label();
            this.lblObjective = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtObjective = new System.Windows.Forms.MaskedTextBox();
            this.dgvActivities = new System.Windows.Forms.DataGridView();
            this.btnAddActivity = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbVillage = new System.Windows.Forms.ComboBox();
            this.cmbActivityType = new System.Windows.Forms.ComboBox();
            this.dtpActivityDate = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.dtpNewDate = new System.Windows.Forms.DateTimePicker();
            this.grpActivityFilters = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFilterDate = new System.Windows.Forms.DateTimePicker();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.btnApplyFilters = new System.Windows.Forms.Button();
            this.cmbFilterStatus = new System.Windows.Forms.ComboBox();
            this.cmbFilterActivityType = new System.Windows.Forms.ComboBox();
            this.cmbFilterVillage = new System.Windows.Forms.ComboBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnUpdateActivity = new System.Windows.Forms.Button();
            this.btnDeleteActivity = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivities)).BeginInit();
            this.grpActivityFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(304, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(106, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Activity Management";
            // 
            // lblVillage
            // 
            this.lblVillage.AutoSize = true;
            this.lblVillage.Location = new System.Drawing.Point(19, 81);
            this.lblVillage.Name = "lblVillage";
            this.lblVillage.Size = new System.Drawing.Size(71, 13);
            this.lblVillage.TabIndex = 1;
            this.lblVillage.Text = "Select Village";
            // 
            // lblActivityType
            // 
            this.lblActivityType.AutoSize = true;
            this.lblActivityType.Location = new System.Drawing.Point(19, 117);
            this.lblActivityType.Name = "lblActivityType";
            this.lblActivityType.Size = new System.Drawing.Size(68, 13);
            this.lblActivityType.TabIndex = 2;
            this.lblActivityType.Text = "Activity Type";
            // 
            // lblObjective
            // 
            this.lblObjective.AutoSize = true;
            this.lblObjective.Location = new System.Drawing.Point(19, 190);
            this.lblObjective.Name = "lblObjective";
            this.lblObjective.Size = new System.Drawing.Size(52, 13);
            this.lblObjective.TabIndex = 3;
            this.lblObjective.Text = "Objective";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(19, 156);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(67, 13);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "Activity Date";
            // 
            // txtObjective
            // 
            this.txtObjective.Location = new System.Drawing.Point(132, 190);
            this.txtObjective.Name = "txtObjective";
            this.txtObjective.Size = new System.Drawing.Size(200, 20);
            this.txtObjective.TabIndex = 5;
            // 
            // dgvActivities
            // 
            this.dgvActivities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActivities.Location = new System.Drawing.Point(342, 78);
            this.dgvActivities.Name = "dgvActivities";
            this.dgvActivities.Size = new System.Drawing.Size(446, 133);
            this.dgvActivities.TabIndex = 6;
            // 
            // btnAddActivity
            // 
            this.btnAddActivity.Location = new System.Drawing.Point(51, 227);
            this.btnAddActivity.Name = "btnAddActivity";
            this.btnAddActivity.Size = new System.Drawing.Size(75, 23);
            this.btnAddActivity.TabIndex = 8;
            this.btnAddActivity.Text = "Plan Activity";
            this.btnAddActivity.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(318, 52);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // cmbVillage
            // 
            this.cmbVillage.FormattingEnabled = true;
            this.cmbVillage.Location = new System.Drawing.Point(132, 81);
            this.cmbVillage.Name = "cmbVillage";
            this.cmbVillage.Size = new System.Drawing.Size(200, 21);
            this.cmbVillage.TabIndex = 10;
            // 
            // cmbActivityType
            // 
            this.cmbActivityType.FormattingEnabled = true;
            this.cmbActivityType.Location = new System.Drawing.Point(132, 117);
            this.cmbActivityType.Name = "cmbActivityType";
            this.cmbActivityType.Size = new System.Drawing.Size(200, 21);
            this.cmbActivityType.TabIndex = 11;
            // 
            // dtpActivityDate
            // 
            this.dtpActivityDate.Location = new System.Drawing.Point(132, 152);
            this.dtpActivityDate.Name = "dtpActivityDate";
            this.dtpActivityDate.Size = new System.Drawing.Size(200, 20);
            this.dtpActivityDate.TabIndex = 12;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(19, 286);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(74, 13);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Activity Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(132, 283);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(121, 21);
            this.cmbStatus.TabIndex = 14;
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.Location = new System.Drawing.Point(147, 393);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(120, 23);
            this.btnUpdateStatus.TabIndex = 15;
            this.btnUpdateStatus.Text = "Update Status";
            this.btnUpdateStatus.UseVisualStyleBackColor = true;
            // 
            // dtpNewDate
            // 
            this.dtpNewDate.Location = new System.Drawing.Point(92, 354);
            this.dtpNewDate.Name = "dtpNewDate";
            this.dtpNewDate.Size = new System.Drawing.Size(200, 20);
            this.dtpNewDate.TabIndex = 16;
            // 
            // grpActivityFilters
            // 
            this.grpActivityFilters.Controls.Add(this.label4);
            this.grpActivityFilters.Controls.Add(this.label3);
            this.grpActivityFilters.Controls.Add(this.label2);
            this.grpActivityFilters.Controls.Add(this.label1);
            this.grpActivityFilters.Controls.Add(this.dtpFilterDate);
            this.grpActivityFilters.Controls.Add(this.btnClearFilters);
            this.grpActivityFilters.Controls.Add(this.btnApplyFilters);
            this.grpActivityFilters.Controls.Add(this.cmbFilterStatus);
            this.grpActivityFilters.Controls.Add(this.cmbFilterActivityType);
            this.grpActivityFilters.Controls.Add(this.cmbFilterVillage);
            this.grpActivityFilters.Location = new System.Drawing.Point(342, 288);
            this.grpActivityFilters.Name = "grpActivityFilters";
            this.grpActivityFilters.Size = new System.Drawing.Size(446, 221);
            this.grpActivityFilters.TabIndex = 17;
            this.grpActivityFilters.TabStop = false;
            this.grpActivityFilters.Text = "                                    ";
            this.grpActivityFilters.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Activity Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Activity Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Village";
            // 
            // dtpFilterDate
            // 
            this.dtpFilterDate.Location = new System.Drawing.Point(91, 178);
            this.dtpFilterDate.Name = "dtpFilterDate";
            this.dtpFilterDate.ShowCheckBox = true;
            this.dtpFilterDate.Size = new System.Drawing.Size(200, 20);
            this.dtpFilterDate.TabIndex = 5;
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Location = new System.Drawing.Point(349, 19);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(75, 23);
            this.btnClearFilters.TabIndex = 4;
            this.btnClearFilters.Text = "Clear";
            this.btnClearFilters.UseVisualStyleBackColor = true;
            // 
            // btnApplyFilters
            // 
            this.btnApplyFilters.Location = new System.Drawing.Point(238, 19);
            this.btnApplyFilters.Name = "btnApplyFilters";
            this.btnApplyFilters.Size = new System.Drawing.Size(75, 23);
            this.btnApplyFilters.TabIndex = 3;
            this.btnApplyFilters.Text = "Apply";
            this.btnApplyFilters.UseVisualStyleBackColor = true;
            // 
            // cmbFilterStatus
            // 
            this.cmbFilterStatus.FormattingEnabled = true;
            this.cmbFilterStatus.Location = new System.Drawing.Point(95, 146);
            this.cmbFilterStatus.Name = "cmbFilterStatus";
            this.cmbFilterStatus.Size = new System.Drawing.Size(121, 21);
            this.cmbFilterStatus.TabIndex = 2;
            // 
            // cmbFilterActivityType
            // 
            this.cmbFilterActivityType.FormattingEnabled = true;
            this.cmbFilterActivityType.Location = new System.Drawing.Point(95, 107);
            this.cmbFilterActivityType.Name = "cmbFilterActivityType";
            this.cmbFilterActivityType.Size = new System.Drawing.Size(121, 21);
            this.cmbFilterActivityType.TabIndex = 1;
            // 
            // cmbFilterVillage
            // 
            this.cmbFilterVillage.FormattingEnabled = true;
            this.cmbFilterVillage.Location = new System.Drawing.Point(95, 69);
            this.cmbFilterVillage.Name = "cmbFilterVillage";
            this.cmbFilterVillage.Size = new System.Drawing.Size(121, 21);
            this.cmbFilterVillage.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(11, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 18;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(724, 12);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(75, 23);
            this.btnLogOut.TabIndex = 19;
            this.btnLogOut.Text = "LogOut";
            this.btnLogOut.UseVisualStyleBackColor = true;
            // 
            // btnUpdateActivity
            // 
            this.btnUpdateActivity.Location = new System.Drawing.Point(374, 227);
            this.btnUpdateActivity.Name = "btnUpdateActivity";
            this.btnUpdateActivity.Size = new System.Drawing.Size(114, 23);
            this.btnUpdateActivity.TabIndex = 20;
            this.btnUpdateActivity.Text = "Update Activity";
            this.btnUpdateActivity.UseVisualStyleBackColor = true;
            // 
            // btnDeleteActivity
            // 
            this.btnDeleteActivity.Location = new System.Drawing.Point(619, 227);
            this.btnDeleteActivity.Name = "btnDeleteActivity";
            this.btnDeleteActivity.Size = new System.Drawing.Size(111, 23);
            this.btnDeleteActivity.TabIndex = 21;
            this.btnDeleteActivity.Text = "Delete Activity";
            this.btnDeleteActivity.UseVisualStyleBackColor = true;
            // 
            // ActivityManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 562);
            this.Controls.Add(this.btnDeleteActivity);
            this.Controls.Add(this.btnUpdateActivity);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.grpActivityFilters);
            this.Controls.Add(this.dtpNewDate);
            this.Controls.Add(this.btnUpdateStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dtpActivityDate);
            this.Controls.Add(this.cmbActivityType);
            this.Controls.Add(this.cmbVillage);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAddActivity);
            this.Controls.Add(this.dgvActivities);
            this.Controls.Add(this.txtObjective);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblObjective);
            this.Controls.Add(this.lblActivityType);
            this.Controls.Add(this.lblVillage);
            this.Controls.Add(this.lblTitle);
            this.Name = "ActivityManagementForm";
            this.Text = "ActivityManagementForm";
            this.Load += new System.EventHandler(this.ActivityManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivities)).EndInit();
            this.grpActivityFilters.ResumeLayout(false);
            this.grpActivityFilters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVillage;
        private System.Windows.Forms.Label lblActivityType;
        private System.Windows.Forms.Label lblObjective;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.MaskedTextBox txtObjective;
        private System.Windows.Forms.DataGridView dgvActivities;
        private System.Windows.Forms.Button btnAddActivity;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox cmbVillage;
        private System.Windows.Forms.ComboBox cmbActivityType;
        private System.Windows.Forms.DateTimePicker dtpActivityDate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.DateTimePicker dtpNewDate;
        private System.Windows.Forms.GroupBox grpActivityFilters;
        private System.Windows.Forms.DateTimePicker dtpFilterDate;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.Button btnApplyFilters;
        private System.Windows.Forms.ComboBox cmbFilterStatus;
        private System.Windows.Forms.ComboBox cmbFilterActivityType;
        private System.Windows.Forms.ComboBox cmbFilterVillage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnUpdateActivity;
        private System.Windows.Forms.Button btnDeleteActivity;
    }
}