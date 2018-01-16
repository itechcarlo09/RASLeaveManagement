using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpUpdate;

namespace RAS_Leave_Management
{
    public partial class Employee : MaterialSkin.Controls.MaterialForm, ISharpUpdatable
    {
        SqlConnection cn = new SqlConnection(@"Data Source=rasleavemanagementwithlogs.cgz2qebdh3p2.us-east-2.rds.amazonaws.com;Initial Catalog=RASLeaveManagement;Persist Security Info=True;User ID=master;Password=rodandstaff2017");

        List<int> respondID = new List<int>();
        Timer t = new Timer();

        public Employee()
        {
            InitializeComponent();
            gridLeave.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            month();
            cn.Close();
            days_available();
            GetLeaveUpdate();

            //timer interval
            t.Interval = 1000; //in milliseconds

            t.Tick += new EventHandler(this.t_Thick);

            //start timer when form loads
            t.Start(); //this will use t_Tick() method
        }

        private void t_Thick(object sender,EventArgs e)
        {
            //get current time
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;

            //time
            string time = "";

            //padding leading zero
            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }
            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }
            time += ":";

            if (ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }

            //update label
            lblTime.Text = time;
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            Request request = new Request();
            request.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var Confirm = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo);
            if (Confirm == DialogResult.Yes)
            {
                Login log = new Login();
                log.Show();
                this.Close();
            }
        }

        private void days_available()
        {
            using (SqlCommand command = new SqlCommand("SELECT acc_leave FROM Account Where acc_id = '" + Login.id + "'", cn))
            {
                cn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            lblDays.Text = Convert.ToString(reader.GetValue(0));
                        }
                    }
                }
                cn.Close();
            }
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            ChangeUserAccount change = new ChangeUserAccount();
            change.ShowDialog();
        }

        private void month()
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM monthchanger", cn))
                {
                    cn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToInt16(reader.GetValue(0)) != DateTime.Now.Month)
                            {
                                monthchanger();
                            }
                        }
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void monthchanger()
        {
            cn.Close();
            cn.Open();
            if (DateTime.Now.Month == 1)
            {
                SqlCommand cm2 = cn.CreateCommand();
                cm2.CommandType = CommandType.Text;
                cm2.CommandText = "UPDATE Account SET acc_leave = 1.25 WHERE acc_position = 'Regular' AND acc_type = 'Employee'";
                cm2.ExecuteNonQuery();
                SqlCommand cm3 = cn.CreateCommand();
                cm3.CommandType = CommandType.Text;
                cm3.CommandText = "UPDATE Account SET acc_leave = 1.5 WHERE acc_position = 'Regular' AND acc_type = 'Manager'";
                cm3.ExecuteNonQuery();
            }
            else
            {
                SqlCommand cm = cn.CreateCommand();
                cm.CommandType = CommandType.Text;
                cm.CommandText = "UPDATE Account SET acc_leave = acc_leave + 1.25 WHERE acc_position = 'Regular'";
                cm.ExecuteNonQuery();
            }
            SqlCommand cm1 = cn.CreateCommand();
            cm1.CommandType = CommandType.Text;
            cm1.CommandText = "UPDATE monthchanger SET monthvalue = '" + DateTime.Now.Month + "'";
            cm1.ExecuteNonQuery();
        }

        private void GetLeaveUpdate()
        {
            cn.Open();
            SqlCommand cm = cn.CreateCommand();
            cm.CommandType = CommandType.Text;
            cm.CommandText = "SELECT CONVERT(VARCHAR(12), leave_request_date, 107) 'Date', CASE leave_span when .5 THEN 'Half Day' WHEN 1 THEN 'Whole Day' END AS 'Leave Span', leave_reason 'Reason', leave_type 'Type', CASE leave_status when 1 then 'Approved' when 0 then 'Rejected' ELSE 'Pending' END AS 'Status' FROM RequestLeave RL INNER JOIN Account A ON RL.leave_request = A.acc_id Where acc_id = '" + Login.id + "' ORDER BY leave_id DESC";
            cm.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(dt);
            gridLeave.DataSource = dt;
            gridLeave.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            cn.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            days_available();
            GetLeaveUpdate();
        }

        #region SharpUpdate
        public string ApplicationName
        {
            get { return "RAS Leave Management"; }
        }

        public string ApplicationID
        {
            get { return "RAS Leave Management"; }
        }

        public Assembly ApplicationAssembly
        {
            get { return Assembly.GetExecutingAssembly(); }
        }

        public Icon ApplicationIcon
        {
            get { return this.Icon; }
        }

        public Uri UpdateXmlLocation
        {
            get { return new Uri(""); }
        }

        public Form Context
        {
            get { return this; }
        }
        #endregion
    }
}
