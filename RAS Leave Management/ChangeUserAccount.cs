using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAS_Leave_Management
{
    public partial class ChangeUserAccount : MaterialSkin.Controls.MaterialForm
    {
        SqlConnection cn = new SqlConnection(@"Data Source=rasleavemanagementwithlogs.cgz2qebdh3p2.us-east-2.rds.amazonaws.com;Initial Catalog=RASLeaveManagement;Persist Security Info=True;User ID=master;Password=rodandstaff2017");
        string curpass;

        public ChangeUserAccount()
        {
            InitializeComponent();
        }

        private void ChangeUserAccount_Load(object sender, EventArgs e)
        {
            GetUsername();
        }

        private void GetUsername()
        {
            cn.Open();
            using (SqlCommand command = new SqlCommand("SELECT * FROM Account Where acc_id = '" + Login.id + "'", cn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        txtUsername.Text = Convert.ToString(reader.GetValue(7));
                    }
                }
            }
            cn.Close();
        }

        private void btnUpdateEmployees_Click(object sender, EventArgs e)
        {
            cn.Open();
            using (SqlCommand command = new SqlCommand("SELECT * FROM Account Where acc_id = '" + Login.id + "'", cn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        curpass = Convert.ToString(reader.GetValue(8));
                    }
                }
            }
            cn.Close();

            if(txtCurrentPassword.Text == curpass)
            {
                if(txtConfirmPassword.Text == txtNewPassword.Text)
                {
                    cn.Open();
                    SqlCommand cm = cn.CreateCommand();
                    cm.CommandType = CommandType.Text;
                    cm.CommandText = "UPDATE Account SET acc_username = '"+ txtUsername.Text +"', acc_password = '"+ txtConfirmPassword.Text +"' WHERE acc_id = '" + Login.id + "'";
                    cm.ExecuteNonQuery();
                    cn.Close();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password didn't match!");
                    txtNewPassword.Text = "";
                    txtConfirmPassword.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Incorrect Current Password!");
                txtCurrentPassword.Text = "";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
