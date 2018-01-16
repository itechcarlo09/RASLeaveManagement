using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RAS_Leave_Management
{
    public partial class Request : MaterialSkin.Controls.MaterialForm
    {
        SqlConnection cn = new SqlConnection(@"Data Source=rasleavemanagementwithlogs.cgz2qebdh3p2.us-east-2.rds.amazonaws.com;Initial Catalog=RASLeaveManagement;Persist Security Info=True;User ID=master;Password=rodandstaff2017");
        double span, value;
        int responder, datestatus;
        string type;

        public Request()
        {
            InitializeComponent();
            MaterialSkin.MaterialSkinManager skinManager = MaterialSkin.MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.DeepOrange600, MaterialSkin.Primary.BlueGrey900, MaterialSkin.Primary.BlueGrey500, MaterialSkin.Accent.Orange700, MaterialSkin.TextShade.WHITE);
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            cn.Open();
            using (SqlCommand command = new SqlCommand("SELECT CONVERT(VARCHAR(10), leave_request_date, 101) FROM RequestLeave Where leave_request = '" + Login.id + "'", cn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for(int i = 0; i < reader.FieldCount; i++)
                        {
                            if(Convert.ToString(reader.GetValue(i)) == dtpRequest.Value.ToString("MM/dd/yyyy"))
                            {
                                datestatus = 1;
                            }
                        }
                    }
                }
            }
            cn.Close();

            if(datestatus == 1)
            {
                MessageBox.Show("This date is already been set!");
                datestatus = 0;
            }
            else
            {
                if (rbtnHalf.Checked == true || rbtnWhole.Checked == true && radioPaid.Checked == true || radioUnpaid.Checked == true)
                {
                    if (radioPaid.Checked == true || radioUnpaid.Checked == true)
                    {
                        if (txtReason.Text != "")
                        {
                            System.DayOfWeek weeks = dtpRequest.Value.DayOfWeek;
                            if ((weeks == System.DayOfWeek.Sunday) || (weeks == System.DayOfWeek.Saturday))
                            {
                                MessageBox.Show("This day wasn't available!");
                            }
                            else
                            {
                                var continue2 = MessageBox.Show("Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo);
                                if (continue2 == DialogResult.Yes)
                                {
                                    if (rbtnWhole.Checked == true)
                                        span = 1;
                                    else if (rbtnHalf.Checked == true)
                                        span = .5;

                                    if (radioPaid.Checked == true)
                                        type = "Paid";
                                    else if (radioUnpaid.Checked == true)
                                        type = "Unpaid";
                                    cn.Open();

                                    using (SqlCommand command = new SqlCommand("SELECT acc_leader FROM Account Where acc_id = '" + Login.id + "'", cn))
                                    {
                                        using (SqlDataReader reader = command.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                responder = Convert.ToInt32(reader.GetValue(0));
                                            }
                                        }
                                    }

                                    SqlCommand cmd = cn.CreateCommand();
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "INSERT INTO RequestLeave (leave_request_date, leave_status, leave_request, leave_span, leave_reason, leave_respond, leave_type) VALUES ('" + dtpRequest.Value + "','2','" + Login.id + "','" + span + "','" + txtReason.Text + "', '" + responder + "', '" + type + "')";
                                    cmd.ExecuteNonQuery();
                                    cn.Close();
                                    MessageBox.Show("Request Sent!!");
                                    this.Close();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incomplete Information");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incomplete Information");
                    }
                }
                else
                {
                    MessageBox.Show("Incomplete Information");
                }
            }
        }
    }
}
