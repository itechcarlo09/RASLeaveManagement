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
using System.Reflection;
using SharpUpdate;

namespace RAS_Leave_Management
{
    public partial class Manager : MaterialSkin.Controls.MaterialForm,ISharpUpdatable
    {
        SqlConnection cn = new SqlConnection(@"Data Source=rasleavemanagementwithlogs.cgz2qebdh3p2.us-east-2.rds.amazonaws.com;Initial Catalog=RASLeaveManagement;Persist Security Info=True;User ID=master;Password=rodandstaff2017");

        string respond,requesttype,requeststatus;
        List<int> respondID = new List<int>();
        int selectedROW, requestID, currentmonth, dbmonth;
        double day,temp;
        Timer t = new Timer();
        private SharpUpdater updater;

        public Manager()
        {
            InitializeComponent();
            gridRequest.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridLeave.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            listEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            updater = new SharpUpdater(this);
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            updater.DoUpdate();
            month();
            cn.Close();
            GetEmployees();
            leave_request();
            days_available();
            //timer interval
            t.Interval = 1000; //in milliseconds

            t.Tick += new EventHandler(this.t_Thick);

            //start timer when form loads
            t.Start(); //this will use t_Tick() method

            if (lblDays.Text == "0" || lblDays.Text == "0.25")
            {
                btnRequest.Enabled = false;
            }
            if (currentmonth != dbmonth)
            {
                cn.Open();
                SqlCommand cm = cn.CreateCommand();
                cm.CommandType = CommandType.Text;
                cm.CommandText = "UPDATE Account SET acc_leave = acc_leave + 1.25 Where acc_id = '" + Login.id + "'";
                cm.ExecuteNonQuery();
                cn.Close();
            }
            GetLeaveUpdate();
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

        private void days_available()
        {
            using (SqlCommand command = new SqlCommand("SELECT acc_leave FROM Account Where acc_id = '"+ Login.id +"'", cn))
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

                    using (SqlCommand command = new SqlCommand("SELECT acc_leave FROM Account Where acc_id = '"+ requestID +"'", cn))
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
                    GetEmployees();
                }
            }
            catch(Exception ex)
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
                            cm.CommandText = "UPDATE RequestLeave SET leave_respond_date = '" + DateTime.Now + "', leave_status = 0  WHERE leave_id = '" + ID + "'";
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
                    GetEmployees();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Doesn't have request yet.");
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var Confirm = MessageBox.Show("Are you sure you want to log out?","Log Out",MessageBoxButtons.YesNo);
            if(Confirm == DialogResult.Yes)
            {
                Login log = new Login();
                log.Show();
                this.Close();
            }
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            Request request = new Request();
            request.ShowDialog();
            GetLeaveUpdate();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            ChangeUserAccount change = new ChangeUserAccount();
            change.ShowDialog();
        }

        private void GetEmployees()
        {
            cn.Open();
            SqlCommand cm = cn.CreateCommand();
            cm.CommandType = CommandType.Text;
            cm.CommandText = "SELECT acc_lastname + ', ' + acc_firstname 'Name', acc_leave 'Leave Available' FROM Account Where acc_leader = '"+ Login.id +"'";
            cm.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(dt);
            listEmployees.DataSource = dt;
            listEmployees.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            listEmployees.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            listEmployees.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            cn.Close();
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
            GetEmployees();
            leave_request();
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
