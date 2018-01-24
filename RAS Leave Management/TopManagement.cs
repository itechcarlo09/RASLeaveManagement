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
using SharpUpdate;
using System.Reflection;

namespace RAS_Leave_Management
{
    public partial class TopManagement : MaterialSkin.Controls.MaterialForm,ISharpUpdatable
    {
        SqlConnection cn = new SqlConnection(@"Data Source=rasleavemanagementwithlogs.cgz2qebdh3p2.us-east-2.rds.amazonaws.com;Initial Catalog=RASLeaveManagement;Persist Security Info=True;User ID=master;Password=rodandstaff2017");
        Timer t = new Timer();
        int selectedROW, requestID;
        double day,temp;
        string requesttype, requeststatus;
        private SharpUpdater updater;

        public TopManagement()
        {
            InitializeComponent();
            gridRequest.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            listLeaders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            updater = new SharpUpdater(this);
        }

        private void TopManagement_Load(object sender, EventArgs e)
        {
            updater.DoUpdate();
            GetManagers();
            leave_request();

            //timer interval
            t.Interval = 1000; //in milliseconds

            t.Tick += new EventHandler(this.t_Thick);

            //start timer when form loads
            t.Start(); //this will use t_Tick() method
        }

        private void t_Thick(object sender, EventArgs e)
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

        private void leave_request()
        {
            cn.Open();
            SqlCommand cm = cn.CreateCommand();
            cm.CommandType = CommandType.Text;
            cm.CommandText = "SELECT leave_id 'Leave ID', acc_lastname + ', ' + acc_firstname 'Name', CONVERT(VARCHAR(12), leave_request_date, 107) 'Date', CASE leave_span when .5 THEN 'Half Day' WHEN 1 THEN 'Whole Day' END AS 'Leave Span', leave_reason 'Reason', leave_type 'Type', CASE leave_status when 1 then 'Approved' when 0 then 'Rejected' ELSE 'Pending' END AS 'Status' FROM RequestLeave RL INNER JOIN Account A ON RL.leave_request = A.acc_id Where RL.leave_respond = '"+ Login.id +"' ORDER BY leave_id DESC";
            cm.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(dt);
            gridRequest.DataSource = dt;
            gridRequest.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            cn.Close();
        }

        private void gridRequest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedROW = e.RowIndex;
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                var approve = MessageBox.Show("Do you want to approve the request?", "Respond", MessageBoxButtons.OKCancel);
                if (approve == DialogResult.OK)
                {
                    DataGridViewRow datagrid = gridRequest.Rows[selectedROW];
                    int ID = Convert.ToInt32(datagrid.Cells[0].Value);
                    string span = Convert.ToString(datagrid.Cells[3].Value);
                    DateTime requestdate = Convert.ToDateTime(datagrid.Cells[2].Value);
                    if (span == "Whole Day")
                        day = 1;
                    else if (span == "Half Day")
                        day = .5;

                    cn.Open();

                    using (SqlCommand command = new SqlCommand("SELECT leave_request, leave_type FROM RequestLeave Where leave_id = '" + ID + "'", cn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                requestID = Convert.ToInt32(reader.GetValue(0));
                                requesttype = Convert.ToString(reader.GetValue(1));
                            }
                        }
                    }

                    using (SqlCommand command = new SqlCommand("SELECT acc_leave FROM Account Where acc_id = '" + requestID + "'", cn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                temp = Convert.ToDouble(reader.GetValue(0));
                            }
                        }
                    }

                    if (requesttype == "Paid")
                    {
                        if (day == 1)
                        {
                            if (temp >= 1)
                            {
                                SqlCommand cm = cn.CreateCommand();
                                cm.CommandType = CommandType.Text;
                                cm.CommandText = "UPDATE RequestLeave SET leave_respond_date = '" + DateTime.Now + "', leave_status = 1  WHERE leave_id = '" + ID + "'";
                                cm.ExecuteNonQuery();
                                SqlCommand cm1 = cn.CreateCommand();
                                cm1.CommandType = CommandType.Text;
                                cm1.CommandText = "UPDATE A SET A.acc_leave = A.acc_leave -'" + day + "' FROM Account A INNER JOIN RequestLeave RL ON RL.leave_request = A.acc_id WHERE RL.leave_request = '" + requestID + "'";
                                cm1.ExecuteNonQuery();
                            }
                            else
                                MessageBox.Show("The employee doesn't have enough leave available!");
                        }
                        else if (day == .5)
                        {
                            if (temp >= .5)
                            {
                                SqlCommand cm = cn.CreateCommand();
                                cm.CommandType = CommandType.Text;
                                cm.CommandText = "UPDATE RequestLeave SET leave_respond_date = '" + DateTime.Now + "', leave_status = 1  WHERE leave_id = '" + ID + "'";
                                cm.ExecuteNonQuery();
                                SqlCommand cm1 = cn.CreateCommand();
                                cm1.CommandType = CommandType.Text;
                                cm1.CommandText = "UPDATE A SET A.acc_leave = A.acc_leave -'" + day + "' FROM Account A INNER JOIN RequestLeave RL ON RL.leave_request = A.acc_id WHERE RL.leave_request = '" + requestID + "'";
                                cm1.ExecuteNonQuery();
                            }
                            else
                                MessageBox.Show("The employee doesn't have enough leave available!");
                        }
                    }
                    else if (requesttype == "Unpaid")
                    {
                        SqlCommand cm = cn.CreateCommand();
                        cm.CommandType = CommandType.Text;
                        cm.CommandText = "UPDATE RequestLeave SET leave_respond_date = '" + DateTime.Now + "', leave_status = 1  WHERE leave_id = '" + ID + "'";
                        cm.ExecuteNonQuery();
                    }
                    cn.Close();
                    leave_request();
                    GetManagers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No selected request!");
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                var reject = MessageBox.Show("Do you want to reject the request?", "Respond", MessageBoxButtons.OKCancel);
                if (reject == DialogResult.OK)
                {
                    DataGridViewRow datagrid = gridRequest.Rows[selectedROW];
                    int ID = Convert.ToInt32(datagrid.Cells[0].Value);
                    string span = Convert.ToString(datagrid.Cells[3].Value);
                    DateTime requestdate = Convert.ToDateTime(datagrid.Cells[2].Value);
                    if (span == "Whole Day")
                        day = 1;
                    else if (span == "Half Day")
                        day = .5;

                    cn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT acc_id, leave_id 'Leave ID', acc_lastname + ', ' + acc_firstname 'Name', CONVERT(VARCHAR(12), leave_request_date, 107) 'Date', CASE leave_span when .5 THEN 'Half Day' WHEN 1 THEN 'Whole Day' END AS 'Leave Span', leave_reason 'Reason', leave_type 'Type', CASE leave_status when 1 then 'Approved' when 0 then 'Rejected' ELSE 'Pending' END AS 'Status' FROM RequestLeave RL INNER JOIN Account A ON RL.leave_request = A.acc_id Where RL.leave_id = '" + ID + "'", cn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                requestID = Convert.ToInt32(reader.GetValue(0));
                                requesttype = Convert.ToString(reader.GetValue(6));
                                requeststatus = Convert.ToString(reader.GetValue(7));
                            }
                        }
                    }

                    if (requesttype == "Paid" && requeststatus == "Approved")
                    {
                        if (day == 1)
                        {
                            SqlCommand cm = cn.CreateCommand();
                            cm.CommandType = CommandType.Text;
                            cm.CommandText = "UPDATE RequestLeave SET leave_respond_date = '" + DateTime.Now + "', leave_status = 0  WHERE leave_id = '" + ID + "'";
                            cm.ExecuteNonQuery();
                            SqlCommand cm1 = cn.CreateCommand();
                            cm1.CommandType = CommandType.Text;
                            cm1.CommandText = "UPDATE A SET A.acc_leave = A.acc_leave +'" + day + "' FROM Account A INNER JOIN RequestLeave RL ON RL.leave_request = A.acc_id WHERE RL.leave_request = '" + requestID + "'";
                            cm1.ExecuteNonQuery();
                        }
                        else if (day == .5)
                        {
                            SqlCommand cm = cn.CreateCommand();
                            cm.CommandType = CommandType.Text;
                            cm.CommandText = "UPDATE RequestLeave SET leave_respond_date = '" + DateTime.Now + "', leave_status = 1  WHERE leave_id = '" + ID + "'";
                            cm.ExecuteNonQuery();
                            SqlCommand cm1 = cn.CreateCommand();
                            cm1.CommandType = CommandType.Text;
                            cm1.CommandText = "UPDATE A SET A.acc_leave = A.acc_leave +'" + day + "' FROM Account A INNER JOIN RequestLeave RL ON RL.leave_request = A.acc_id WHERE RL.leave_request = '" + requestID + "'";
                            cm1.ExecuteNonQuery();
                        }
                    }
                    else if (requesttype == "Unpaid")
                    {
                        SqlCommand cm = cn.CreateCommand();
                        cm.CommandType = CommandType.Text;
                        cm.CommandText = "UPDATE RequestLeave SET leave_respond_date = '" + DateTime.Now + "', leave_status = 0  WHERE leave_id = '" + ID + "'";
                        cm.ExecuteNonQuery();
                    }
                    cn.Close();
                    leave_request();
                    GetManagers();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Doesn't have request yet.");
            }
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

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            AddEmployee add = new AddEmployee();
            add.ShowDialog();
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            ManagePeople man = new ManagePeople();
            man.ShowDialog();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            ChangeUserAccount change = new ChangeUserAccount();
            change.ShowDialog();
        }

        public void GetManagers()
        {
            cn.Open();
            SqlCommand cm = cn.CreateCommand();
            cm.CommandType = CommandType.Text;
            cm.CommandText = "SELECT acc_lastname + ', ' + acc_firstname 'Name', acc_leave 'Days Available' FROM Account where acc_type = 'Manager'";
            cm.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(dt);
            listLeaders.DataSource = dt;
            listLeaders.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            listLeaders.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            cn.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            leave_request();
            GetManagers();
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
            get { return new Uri("https://raw.githubusercontent.com/itechcarlo09/RAS-Leave-Management-with-Logs/new/RAS%20Leave%20Management/bin/Debug/update.xml"); }
        }

        public Form Context
        {
            get { return this; }
        }
        #endregion
    }
}
