namespace UmugandaManagementSystem.Forms
{
    partial class VillageManagementForm
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
            this.lblVillageName = new System.Windows.Forms.Label();
            this.lblVillageCode = new System.Windows.Forms.Label();
            this.lblLeaderName = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtVillageName = new System.Windows.Forms.MaskedTextBox();
            this.txtVillageCode = new System.Windows.Forms.MaskedTextBox();
            this.txtLeaderName = new System.Windows.Forms.MaskedTextBox();
            this.dgvVillages = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVillages)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(186, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(103, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Village Management";
            // 
            // lblVillageName
            // 
            this.lblVillageName.AutoSize = true;
            this.lblVillageName.Location = new System.Drawing.Point(51, 123);
            this.lblVillageName.Name = "lblVillageName";
            this.lblVillageName.Size = new System.Drawing.Size(69, 13);
            this.lblVillageName.TabIndex = 1;
            this.lblVillageName.Text = "Village Name";
            // 
            // lblVillageCode
            // 
            this.lblVillageCode.AutoSize = true;
            this.lblVillageCode.Location = new System.Drawing.Point(51, 165);
            this.lblVillageCode.Name = "lblVillageCode";
            this.lblVillageCode.Size = new System.Drawing.Size(66, 13);
            this.lblVillageCode.TabIndex = 2;
            this.lblVillageCode.Text = "Village Code";
            // 
            // lblLeaderName
            // 
            this.lblLeaderName.AutoSize = true;
            this.lblLeaderName.Location = new System.Drawing.Point(51, 205);
            this.lblLeaderName.Name = "lblLeaderName";
            this.lblLeaderName.Size = new System.Drawing.Size(71, 13);
            this.lblLeaderName.TabIndex = 4;
            this.lblLeaderName.Text = "Leader Name";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(45, 260);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add Village";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(299, 260);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(94, 23);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update Village";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(170, 260);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(94, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete Village";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(328, 88);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // txtVillageName
            // 
            this.txtVillageName.Location = new System.Drawing.Point(189, 120);
            this.txtVillageName.Name = "txtVillageName";
            this.txtVillageName.Size = new System.Drawing.Size(151, 20);
            this.txtVillageName.TabIndex = 10;
            // 
            // txtVillageCode
            // 
            this.txtVillageCode.Location = new System.Drawing.Point(189, 162);
            this.txtVillageCode.Name = "txtVillageCode";
            this.txtVillageCode.Size = new System.Drawing.Size(151, 20);
            this.txtVillageCode.TabIndex = 11;
            // 
            // txtLeaderName
            // 
            this.txtLeaderName.Location = new System.Drawing.Point(189, 202);
            this.txtLeaderName.Name = "txtLeaderName";
            this.txtLeaderName.Size = new System.Drawing.Size(151, 20);
            this.txtLeaderName.TabIndex = 12;
            // 
            // dgvVillages
            // 
            this.dgvVillages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVillages.Location = new System.Drawing.Point(451, 59);
            this.dgvVillages.Name = "dgvVillages";
            this.dgvVillages.Size = new System.Drawing.Size(337, 163);
            this.dgvVillages.TabIndex = 13;
            // 
            // VillageManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvVillages);
            this.Controls.Add(this.txtLeaderName);
            this.Controls.Add(this.txtVillageCode);
            this.Controls.Add(this.txtVillageName);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblLeaderName);
            this.Controls.Add(this.lblVillageCode);
            this.Controls.Add(this.lblVillageName);
            this.Controls.Add(this.lblTitle);
            this.Name = "VillageManagementForm";
            this.Text = "VillageManagementForm";
            this.Load += new System.EventHandler(this.VillageManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVillages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVillageName;
        private System.Windows.Forms.Label lblVillageCode;
        private System.Windows.Forms.Label lblLeaderName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.MaskedTextBox txtVillageName;
        private System.Windows.Forms.MaskedTextBox txtVillageCode;
        private System.Windows.Forms.MaskedTextBox txtLeaderName;
        private System.Windows.Forms.DataGridView dgvVillages;
    }
}