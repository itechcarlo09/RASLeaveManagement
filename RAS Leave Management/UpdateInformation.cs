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
    public partial class UpdateInformation : MaterialSkin.Controls.MaterialForm
    {
        SqlConnection cn = new SqlConnection(@"Data Source=rasleavemanagementwithlogs.cgz2qebdh3p2.us-east-2.rds.amazonaws.com;Initial Catalog=RASLeaveManagement;Persist Security Info=True;User ID=master;Password=rodandstaff2017");


        List<String> Managers = new List<String>();
        List<int> IDs = new List<int>();
        int manager, promoteflag = 0, demoteflag = 0, newbie;
        string gender, status;

        string identifier = "Employee";
        

        public UpdateInformation()
        {
            InitializeComponent();
        }

        private void UpdateInformation_Load(object sender, EventArgs e)
        {
            radioNew.Enabled = false;
            GetInformation();
            GetManagers();
            for(int i = 0; i < Managers.Count(); i++)
            {
                comboManager.Items.Add(Managers[i]);
            }
            for (int i = 0; i < IDs.Count(); i++)
            {
                if (IDs[i] == manager)
                    comboManager.SelectedIndex = i;
            }
        }

        private void GetInformation()
        {
            cn.Open();
            using (SqlCommand command = new SqlCommand("SELECT * FROM Account Where acc_id = '"+ ManagePeople.ID +"'", cn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        txtFirstName.Text = Convert.ToString(reader.GetValue(1));
                        txtMiddleName.Text = Convert.ToString(reader.GetValue(2));
                        txtLastName.Text = Convert.ToString(reader.GetValue(3));
                        if (Convert.ToString(reader.GetValue(4)) == "Male")
                            radioMale.Checked = true;
                        else
                            radioFemale.Checked = true;
                        if (Convert.ToString(reader.GetValue(11)) == "New")
                            radioNew.Checked = true;
                        else
                            radioRegular.Checked = true;
                        manager = Convert.ToInt32(reader.GetValue(10));
                        if (Convert.ToString(reader.GetValue(8)) == "Manager")
                        {
                            btnPromote.Text = "Demote";
                            comboManager.Enabled = false;
                            identifier = "Manager";
                        }
                    }
                }
            }
            cn.Close();
        }
       
        private void GetManagers()
        {
            IDs.Clear();
            Managers.Clear();

            if(demoteflag == 1)
            {
                cn.Open();
                using (SqlCommand command = new SqlCommand("SELECT acc_id,acc_lastname + ', ' + acc_firstname 'Full Name' FROM Account where acc_type = 'Manager' AND acc_id != '"+ ManagePeople.ID +"'", cn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i += 2)
                            {
                                IDs.Add(Convert.ToInt32(reader.GetValue(i)));
                                Managers.Add(Convert.ToString(reader.GetValue(i + 1)));
                            }
                        }
                    }
                }
                cn.Close();
            }
            else
            {
                cn.Open();
                using (SqlCommand command = new SqlCommand("SELECT acc_id,acc_lastname + ', ' + acc_firstname 'Full Name' FROM Account where acc_type = 'Manager'", cn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i += 2)
                            {
                                IDs.Add(Convert.ToInt32(reader.GetValue(i)));
                                Managers.Add(Convert.ToString(reader.GetValue(i + 1)));
                            }
                        }
                    }
                }
                cn.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPromote_Click(object sender, EventArgs e)
        {
            if (btnPromote.Text == "Promote")
            {
                promoteflag++;
                btnPromote.Text = "Cancel";
                comboManager.Enabled = false;
            }
            else if (btnPromote.Text == "Demote")
            {
                demoteflag++;
                btnPromote.Text = "Cancel";
                comboManager.Enabled = true;
                comboManager.Items.Clear();
                GetManagers();
                for (int i = 0; i < Managers.Count(); i++)
                {
                    comboManager.Items.Add(Managers[i]);
                }
            }
            else
            {
                if(promoteflag == 1)
                {
                    btnPromote.Text = "Promote";
                    promoteflag = 0;
                    comboManager.Enabled = true;
                }
                else
                {
                    btnPromote.Text = "Demote";
                    demoteflag = 0;
                    comboManager.Enabled = false;
                }
            }
        }

        private void btnUpdateEmployees_Click(object sender, EventArgs e)
        {
            var manage = MessageBox.Show("Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo);
            if (manage == DialogResult.Yes)
            {
            if(txtFirstName.Text != null && txtLastName.Text != null && radioFemale.Checked == true || radioMale.Checked == true && radioNew.Checked == true || radioRegular.Checked == true)
            {
                if (radioFemale.Checked == true)
                        gender = "Female";
                    else
                        gender = "Male";
                    if (radioRegular.Checked == true)
                        status = "Regular";
                    else
                        status = "New";
                if(identifier == "Manager")
                {
                    if(demoteflag == 1)
                    {
                        if(ManagePeople.IDEmp.Count == 0)
                        {
                            cn.Open();
                            SqlCommand cm = cn.CreateCommand();
                            cm.CommandType = CommandType.Text;
                            cm.CommandText = "UPDATE Account SET acc_firstname = '" + txtFirstName.Text + "', acc_middlename = '" + txtMiddleName.Text + "', acc_lastname = '" + txtLastName.Text + "', acc_sex = '" + gender + "', acc_position = '" + status + "', acc_leader = '" + IDs[comboManager.SelectedIndex] + "', acc_type = 'Employee' WHERE acc_id = '" + ManagePeople.ID + "'";
                            cm.ExecuteNonQuery();
                            cn.Close();
                        }
                        else
                        {
                            MessageBox.Show("Remove all his/her under employee first to continue.");
                        }
                    }
                    else
                    {
                        cn.Open();
                        SqlCommand cm = cn.CreateCommand();
                        cm.CommandType = CommandType.Text;
                        cm.CommandText = "UPDATE Account SET acc_firstname = '" + txtFirstName.Text + "', acc_middlename = '" + txtMiddleName.Text + "', acc_lastname = '" + txtLastName.Text + "', acc_sex = '" + gender + "', acc_position = '" + status + "' WHERE acc_id = '" + ManagePeople.ID + "'";
                        cm.ExecuteNonQuery();
                        cn.Close();
                    }
                }
                else
                {
                    if (promoteflag == 1)
                    {
                        cn.Open();
                        SqlCommand cm = cn.CreateCommand();
                        cm.CommandType = CommandType.Text;
                        cm.CommandText = "UPDATE Account SET acc_firstname = '" + txtFirstName.Text + "', acc_middlename = '" + txtMiddleName.Text + "', acc_lastname = '" + txtLastName.Text + "', acc_sex = '" + gender + "', acc_position = '" + status + "', acc_leader = '1', acc_type = 'Manager' WHERE acc_id = '" + ManagePeople.ID + "'";
                        cm.ExecuteNonQuery();
                        cn.Close();
                    }
                    else
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("SELECT acc_position FROM Account Where acc_id = '" + ManagePeople.ID + "'", cn))
                            {
                                cn.Open();
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        if (Convert.ToString(reader.GetValue(0)) == "New")
                                        {
                                            if (radioRegular.Checked == true)
                                            {
                                                newbie = 1;
                                                provisionalupdate();
                                            }
                                        }
                                        else
                                        {
                                            provisionalupdate();
                                        }
                                    }
                                }
                            }
                        }
                        catch(Exception ex)
                        {

                        }
                    }
                }
                }
            }
        }

        private void provisionalupdate()
        {
            cn.Close();
            cn.Open();
            if(newbie == 1)
            {
                SqlCommand cm = cn.CreateCommand();
                cm.CommandType = CommandType.Text;
                cm.CommandText = "UPDATE Account SET acc_firstname = '" + txtFirstName.Text + "', acc_middlename = '" + txtMiddleName.Text + "', acc_lastname = '" + txtLastName.Text + "', acc_sex = '" + gender + "', acc_position = '" + status + "', acc_leader = '" + IDs[comboManager.SelectedIndex] + "', acc_leave = 7.5 WHERE acc_id = '" + ManagePeople.ID + "'";
                cm.ExecuteNonQuery();
            }
            else
            {
                SqlCommand cm = cn.CreateCommand();
                cm.CommandType = CommandType.Text;
                cm.CommandText = "UPDATE Account SET acc_firstname = '" + txtFirstName.Text + "', acc_middlename = '" + txtMiddleName.Text + "', acc_lastname = '" + txtLastName.Text + "', acc_sex = '" + gender + "', acc_position = '" + status + "', acc_leader = '" + IDs[comboManager.SelectedIndex] + "' WHERE acc_id = '" + ManagePeople.ID + "'";
                cm.ExecuteNonQuery();
            }
        }
    }
}
