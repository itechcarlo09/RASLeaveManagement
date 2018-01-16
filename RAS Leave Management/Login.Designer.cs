namespace RAS_Leave_Management
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.LoginUsername = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.LoginPassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.LoginButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.logo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // LoginUsername
            // 
            this.LoginUsername.Depth = 0;
            this.LoginUsername.Hint = "Username";
            this.LoginUsername.Location = new System.Drawing.Point(12, 307);
            this.LoginUsername.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoginUsername.Name = "LoginUsername";
            this.LoginUsername.PasswordChar = '\0';
            this.LoginUsername.SelectedText = "";
            this.LoginUsername.SelectionLength = 0;
            this.LoginUsername.SelectionStart = 0;
            this.LoginUsername.Size = new System.Drawing.Size(276, 23);
            this.LoginUsername.TabIndex = 1;
            this.LoginUsername.TabStop = false;
            this.LoginUsername.UseSystemPasswordChar = false;
            // 
            // LoginPassword
            // 
            this.LoginPassword.Depth = 0;
            this.LoginPassword.Hint = "Password";
            this.LoginPassword.Location = new System.Drawing.Point(12, 350);
            this.LoginPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoginPassword.Name = "LoginPassword";
            this.LoginPassword.PasswordChar = '*';
            this.LoginPassword.SelectedText = "";
            this.LoginPassword.SelectionLength = 0;
            this.LoginPassword.SelectionStart = 0;
            this.LoginPassword.Size = new System.Drawing.Size(276, 23);
            this.LoginPassword.TabIndex = 2;
            this.LoginPassword.TabStop = false;
            this.LoginPassword.UseSystemPasswordChar = false;
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LoginButton.Depth = 0;
            this.LoginButton.Location = new System.Drawing.Point(12, 393);
            this.LoginButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Primary = true;
            this.LoginButton.Size = new System.Drawing.Size(276, 23);
            this.LoginButton.TabIndex = 0;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = false;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // logo
            // 
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.Location = new System.Drawing.Point(24, 84);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(248, 202);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.logo.TabIndex = 3;
            this.logo.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(300, 438);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.LoginPassword);
            this.Controls.Add(this.LoginUsername);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField LoginUsername;
        private MaterialSkin.Controls.MaterialSingleLineTextField LoginPassword;
        private MaterialSkin.Controls.MaterialRaisedButton LoginButton;
        private System.Windows.Forms.PictureBox logo;
    }
}

