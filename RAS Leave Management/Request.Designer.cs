namespace RAS_Leave_Management
{
    partial class Request
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Request));
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.dtpRequest = new MetroFramework.Controls.MetroDateTime();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.btnSubmit = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialRaisedButton2 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.rbtnHalf = new MaterialSkin.Controls.MaterialRadioButton();
            this.rbtnWhole = new MaterialSkin.Controls.MaterialRadioButton();
            this.radioUnpaid = new MaterialSkin.Controls.MaterialRadioButton();
            this.radioPaid = new MaterialSkin.Controls.MaterialRadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialLabel1
            // 
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(12, 81);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(66, 27);
            this.materialLabel1.TabIndex = 0;
            this.materialLabel1.Text = "Date :";
            // 
            // dtpRequest
            // 
            this.dtpRequest.Location = new System.Drawing.Point(84, 81);
            this.dtpRequest.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpRequest.Name = "dtpRequest";
            this.dtpRequest.Size = new System.Drawing.Size(215, 29);
            this.dtpRequest.TabIndex = 1;
            // 
            // materialLabel2
            // 
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(12, 198);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(90, 27);
            this.materialLabel2.TabIndex = 1;
            this.materialLabel2.Text = "Reason :";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(11, 239);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(288, 135);
            this.txtReason.TabIndex = 2;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Depth = 0;
            this.btnSubmit.Location = new System.Drawing.Point(224, 390);
            this.btnSubmit.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Primary = true;
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // materialRaisedButton2
            // 
            this.materialRaisedButton2.Depth = 0;
            this.materialRaisedButton2.Location = new System.Drawing.Point(143, 390);
            this.materialRaisedButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton2.Name = "materialRaisedButton2";
            this.materialRaisedButton2.Primary = true;
            this.materialRaisedButton2.Size = new System.Drawing.Size(75, 23);
            this.materialRaisedButton2.TabIndex = 3;
            this.materialRaisedButton2.Text = "Cancel";
            this.materialRaisedButton2.UseVisualStyleBackColor = true;
            this.materialRaisedButton2.Click += new System.EventHandler(this.materialRaisedButton2_Click);
            // 
            // rbtnHalf
            // 
            this.rbtnHalf.AutoSize = true;
            this.rbtnHalf.BackColor = System.Drawing.Color.Transparent;
            this.rbtnHalf.Depth = 0;
            this.rbtnHalf.Font = new System.Drawing.Font("Roboto", 10F);
            this.rbtnHalf.Location = new System.Drawing.Point(11, 119);
            this.rbtnHalf.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnHalf.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rbtnHalf.MouseState = MaterialSkin.MouseState.HOVER;
            this.rbtnHalf.Name = "rbtnHalf";
            this.rbtnHalf.Ripple = true;
            this.rbtnHalf.Size = new System.Drawing.Size(81, 30);
            this.rbtnHalf.TabIndex = 0;
            this.rbtnHalf.TabStop = true;
            this.rbtnHalf.Text = "Half Day";
            this.rbtnHalf.UseVisualStyleBackColor = false;
            // 
            // rbtnWhole
            // 
            this.rbtnWhole.AutoSize = true;
            this.rbtnWhole.BackColor = System.Drawing.Color.Transparent;
            this.rbtnWhole.Depth = 0;
            this.rbtnWhole.Font = new System.Drawing.Font("Roboto", 10F);
            this.rbtnWhole.Location = new System.Drawing.Point(139, 119);
            this.rbtnWhole.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnWhole.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rbtnWhole.MouseState = MaterialSkin.MouseState.HOVER;
            this.rbtnWhole.Name = "rbtnWhole";
            this.rbtnWhole.Ripple = true;
            this.rbtnWhole.Size = new System.Drawing.Size(94, 30);
            this.rbtnWhole.TabIndex = 0;
            this.rbtnWhole.TabStop = true;
            this.rbtnWhole.Text = "Whole Day";
            this.rbtnWhole.UseVisualStyleBackColor = false;
            // 
            // radioUnpaid
            // 
            this.radioUnpaid.AutoSize = true;
            this.radioUnpaid.BackColor = System.Drawing.Color.Transparent;
            this.radioUnpaid.Depth = 0;
            this.radioUnpaid.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioUnpaid.Location = new System.Drawing.Point(128, 0);
            this.radioUnpaid.Margin = new System.Windows.Forms.Padding(0);
            this.radioUnpaid.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioUnpaid.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioUnpaid.Name = "radioUnpaid";
            this.radioUnpaid.Ripple = true;
            this.radioUnpaid.Size = new System.Drawing.Size(72, 30);
            this.radioUnpaid.TabIndex = 5;
            this.radioUnpaid.TabStop = true;
            this.radioUnpaid.Text = "Unpaid";
            this.radioUnpaid.UseVisualStyleBackColor = false;
            // 
            // radioPaid
            // 
            this.radioPaid.AutoSize = true;
            this.radioPaid.BackColor = System.Drawing.Color.Transparent;
            this.radioPaid.Depth = 0;
            this.radioPaid.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioPaid.Location = new System.Drawing.Point(0, 0);
            this.radioPaid.Margin = new System.Windows.Forms.Padding(0);
            this.radioPaid.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioPaid.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioPaid.Name = "radioPaid";
            this.radioPaid.Ripple = true;
            this.radioPaid.Size = new System.Drawing.Size(56, 30);
            this.radioPaid.TabIndex = 6;
            this.radioPaid.TabStop = true;
            this.radioPaid.Text = "Paid";
            this.radioPaid.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioUnpaid);
            this.panel1.Controls.Add(this.radioPaid);
            this.panel1.Location = new System.Drawing.Point(11, 157);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(228, 29);
            this.panel1.TabIndex = 7;
            // 
            // Request
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 421);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rbtnWhole);
            this.Controls.Add(this.rbtnHalf);
            this.Controls.Add(this.materialRaisedButton2);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.dtpRequest);
            this.Controls.Add(this.materialLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Request";
            this.Sizable = false;
            this.Text = "Leave Request ";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MetroFramework.Controls.MetroDateTime dtpRequest;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private System.Windows.Forms.TextBox txtReason;
        private MaterialSkin.Controls.MaterialRaisedButton btnSubmit;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton2;
        private MaterialSkin.Controls.MaterialRadioButton rbtnHalf;
        private MaterialSkin.Controls.MaterialRadioButton rbtnWhole;
        private MaterialSkin.Controls.MaterialRadioButton radioUnpaid;
        private MaterialSkin.Controls.MaterialRadioButton radioPaid;
        private System.Windows.Forms.Panel panel1;

    }
}