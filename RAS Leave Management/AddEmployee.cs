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
    public partial class AddEmployee : MaterialSkin.Controls.MaterialForm
    {
        SqlConnection cn = new SqlConnection(@"Data Source=rasleavemanagementwithlogs.cgz2qebdh3p2.us-east-2.rds.amazonaws.com;Initial Catalog=RASLeaveManagement;Persist Security Info=True;User ID=master;Password=rodandstaff2017");
        string sex, status;
        List<String> Managers = new List<String>();
        List<int> IDs = new List<int>();
        int accID;

        public AddEmployee()
        {
            InitializeComponent();
        }

        public void GetManagers()
        {
            cn.Open();
            using (SqlCommand command = new SqlCommand("SELECT acc_id,acc_lastname + ', ' + acc_firstname 'Full Name' FROM Account where acc_type = 'Manager'", cn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i+=2)
                        {
                            IDs.Add(Convert.ToInt32(reader.GetValue(i)));
                            Managers.Add(Convert.ToString(reader.GetValue(i+1)));
                        }
                    }
                }
            }
            cn.Close();
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            GetManagers();
            for (int i = 0; i < Managers.Count(); i++)
            {
                comboManager.Items.Add(Managers[i]);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            accID = comboManager.SelectedIndex;

            if(txtFirstName.Text != "" && txtLastName.Text != "" && radioMale.Checked == true || radioFemale.Checked == true && txtUsername.Text != "" && txtPassword.Text != "" && txtConfirmPassword.Text != "" && comboManager.Text != "" && radioNew.Checked == true || radioRegular.Checked == true)
            {
                var confirm = MessageBox.Show("Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    if(txtPassword.Text == txtConfirmPassword.Text)
                    {
                        if (radioMale.Checked == true)
                            sex = "Male";
                        else if (radioFemale.Checked == true)
                            sex = "Female";
                        if (radioNew.Checked == true)
                            status = "New";
                        else if (radioRegular.Checked == true)
                            status = "Regular";

                        cn.Open();
                        accID = IDs[accID];
                        SqlCommand cmd = cn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO Account VALUES ('" + txtFirstName.Text + "','"+ txtMiddleName.Text +"','" + txtLastName.Text + "','" + sex + "', '0', '"+ txtUsername.Text +"', '"+ txtConfirmPassword.Text +"', 'Employee', '1', '"+ accID +"', '"+ status +"')";
                        cmd.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Employee has successfully added!!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password does not match!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Incomplete Information!");
            }
        }
    }
}
