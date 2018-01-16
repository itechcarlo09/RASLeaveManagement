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
    public partial class ManagePeople : MaterialSkin.Controls.MaterialForm
    {
        SqlConnection cn = new SqlConnection(@"Data Source=rasleavemanagementwithlogs.cgz2qebdh3p2.us-east-2.rds.amazonaws.com;Initial Catalog=RASLeaveManagement;Persist Security Info=True;User ID=master;Password=rodandstaff2017");

        List<String> Managers = new List<String>();
        List<int> IDs = new List<int>();
        List<String> Employees = new List<String>();
        public static List<int> IDEmp = new List<int>();
        int selectedROWLeader,selectedROWEmployee;
        string sex, status;
        public static int ID;

        public ManagePeople()
        {
            InitializeComponent();
        }

        public void GetManagers()
        {
            IDs.Clear();
            Managers.Clear();
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
            SqlCommand cm = cn.CreateCommand();
            cm.CommandType = CommandType.Text;
            cm.CommandText = "SELECT acc_lastname + ', ' + acc_firstname 'Name' FROM Account where acc_type = 'Manager'";
            cm.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(dt);
            listLeaders.DataSource = dt;
            listLeaders.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            listLeaders.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            cn.Close();
        }

        public void GetEmployees()
        {
            IDEmp.Clear();
            Employees.Clear();
            try
            {
                cn.Open();
                using (SqlCommand command = new SqlCommand("SELECT acc_id, acc_lastname + ', ' + acc_firstname FROM Account Where acc_leader = '" + IDs[selectedROWLeader] + "'", cn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i += 2)
                            {
                                IDEmp.Add(Convert.ToInt32(reader.GetValue(i)));
                                Employees.Add(Convert.ToString(reader.GetValue(i + 1)));
                            }
                        }
                    }
                }
                SqlCommand cm = cn.CreateCommand();
                cm.CommandType = CommandType.Text;
                cm.CommandText = "SELECT acc_lastname + ', ' + acc_firstname 'Name' FROM Account Where acc_leader = '" + IDs[selectedROWLeader] + "'";
                cm.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cm);
                da.Fill(dt);
                listEmployees.DataSource = dt;
                listEmployees.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                listEmployees.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                selectedROWLeader = 0;
                GetManagers();
                GetEmployees();
            }
        }

        private void listLeaders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedROWLeader = e.RowIndex;
            GetEmployees();
        }

        private void ManagePeople_Load(object sender, EventArgs e)
        {
            GetManagers();
            GetEmployees();
        }

        private void listEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                selectedROWEmployee = e.RowIndex;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdateLeaders_Click(object sender, EventArgs e)
        {
            ID = IDs[selectedROWLeader];
            UpdateInformation UI = new UpdateInformation();
            UI.ShowDialog();
            GetManagers();
            selectedROWLeader = 0;
            GetEmployees();
        }

        private void btnUpdateEmployees_Click(object sender, EventArgs e)
        {
            ID = IDEmp[selectedROWEmployee];
            UpdateInformation UI = new UpdateInformation();
            UI.ShowDialog();
            GetManagers();
            selectedROWLeader = 0;
            GetEmployees();
        }
    }
}
