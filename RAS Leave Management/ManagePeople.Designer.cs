namespace RAS_Leave_Management
{
    partial class ManagePeople
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagePeople));
            this.panelEmployees = new System.Windows.Forms.Panel();
            this.listLeaders = new System.Windows.Forms.DataGridView();
            this.btnUpdateLeaders = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialLabel10 = new MaterialSkin.Controls.MaterialLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listEmployees = new System.Windows.Forms.DataGridView();
            this.btnUpdateEmployees = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.btnCancel = new MaterialSkin.Controls.MaterialRaisedButton();
            this.panelEmployees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listLeaders)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listEmployees)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEmployees
            // 
            this.panelEmployees.Controls.Add(this.listLeaders);
            this.panelEmployees.Controls.Add(this.btnUpdateLeaders);
            this.panelEmployees.Controls.Add(this.materialLabel10);
            this.panelEmployees.Location = new System.Drawing.Point(12, 79);
            this.panelEmployees.Name = "panelEmployees";
            this.panelEmployees.Size = new System.Drawing.Size(236, 345);
            this.panelEmployees.TabIndex = 0;
            // 
            // listLeaders
            // 
            this.listLeaders.AllowUserToAddRows = false;
            this.listLeaders.AllowUserToDeleteRows = false;
            this.listLeaders.AllowUserToResizeRows = false;
            this.listLeaders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listLeaders.Location = new System.Drawing.Point(18, 40);
            this.listLeaders.Name = "listLeaders";
            this.listLeaders.ReadOnly = true;
            this.listLeaders.RowHeadersVisible = false;
            this.listLeaders.Size = new System.Drawing.Size(206, 259);
            this.listLeaders.TabIndex = 73;
            this.listLeaders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.listLeaders_CellClick);
            // 
            // btnUpdateLeaders
            // 
            this.btnUpdateLeaders.BackColor = System.Drawing.SystemColors.Control;
            this.btnUpdateLeaders.Depth = 0;
            this.btnUpdateLeaders.Location = new System.Drawing.Point(18, 310);
            this.btnUpdateLeaders.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnUpdateLeaders.Name = "btnUpdateLeaders";
            this.btnUpdateLeaders.Primary = true;
            this.btnUpdateLeaders.Size = new System.Drawing.Size(206, 23);
            this.btnUpdateLeaders.TabIndex = 24;
            this.btnUpdateLeaders.Text = "Manage Information";
            this.btnUpdateLeaders.UseVisualStyleBackColor = false;
            this.btnUpdateLeaders.Click += new System.EventHandler(this.btnUpdateLeaders_Click);
            // 
            // materialLabel10
            // 
            this.materialLabel10.AutoSize = true;
            this.materialLabel10.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel10.Depth = 0;
            this.materialLabel10.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel10.Location = new System.Drawing.Point(70, 12);
            this.materialLabel10.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel10.Name = "materialLabel10";
            this.materialLabel10.Size = new System.Drawing.Size(104, 19);
            this.materialLabel10.TabIndex = 48;
            this.materialLabel10.Text = "Team Leaders";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listEmployees);
            this.panel1.Controls.Add(this.btnUpdateEmployees);
            this.panel1.Controls.Add(this.materialLabel1);
            this.panel1.Location = new System.Drawing.Point(254, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(236, 345);
            this.panel1.TabIndex = 74;
            // 
            // listEmployees
            // 
            this.listEmployees.AllowUserToAddRows = false;
            this.listEmployees.AllowUserToDeleteRows = false;
            this.listEmployees.AllowUserToResizeRows = false;
            this.listEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listEmployees.Location = new System.Drawing.Point(18, 40);
            this.listEmployees.Name = "listEmployees";
            this.listEmployees.ReadOnly = true;
            this.listEmployees.RowHeadersVisible = false;
            this.listEmployees.Size = new System.Drawing.Size(206, 259);
            this.listEmployees.TabIndex = 73;
            this.listEmployees.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.listEmployees_CellClick);
            // 
            // btnUpdateEmployees
            // 
            this.btnUpdateEmployees.BackColor = System.Drawing.SystemColors.Control;
            this.btnUpdateEmployees.Depth = 0;
            this.btnUpdateEmployees.Location = new System.Drawing.Point(18, 310);
            this.btnUpdateEmployees.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnUpdateEmployees.Name = "btnUpdateEmployees";
            this.btnUpdateEmployees.Primary = true;
            this.btnUpdateEmployees.Size = new System.Drawing.Size(206, 23);
            this.btnUpdateEmployees.TabIndex = 24;
            this.btnUpdateEmployees.Text = "Manage Information";
            this.btnUpdateEmployees.UseVisualStyleBackColor = false;
            this.btnUpdateEmployees.Click += new System.EventHandler(this.btnUpdateEmployees_Click);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(73, 12);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(83, 19);
            this.materialLabel1.TabIndex = 48;
            this.materialLabel1.Text = "Employees";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Depth = 0;
            this.btnCancel.Location = new System.Drawing.Point(410, 430);
            this.btnCancel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primary = true;
            this.btnCancel.Size = new System.Drawing.Size(80, 23);
            this.btnCancel.TabIndex = 74;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ManagePeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 463);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelEmployees);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManagePeople";
            this.Sizable = false;
            this.Text = " Manage People";
            this.Load += new System.EventHandler(this.ManagePeople_Load);
            this.panelEmployees.ResumeLayout(false);
            this.panelEmployees.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listLeaders)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listEmployees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelEmployees;
        private MaterialSkin.Controls.MaterialLabel materialLabel10;
        private MaterialSkin.Controls.MaterialRaisedButton btnUpdateLeaders;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView listEmployees;
        private MaterialSkin.Controls.MaterialRaisedButton btnUpdateEmployees;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialRaisedButton btnCancel;
        public System.Windows.Forms.DataGridView listLeaders;



    }
}