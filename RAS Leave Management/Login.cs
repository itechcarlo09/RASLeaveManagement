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
    public partial class Login : MaterialSkin.Controls.MaterialForm
    {
        SqlConnection cn = new SqlConnection(@"Data Source=rasleavemanagementwithlogs.cgz2qebdh3p2.us-east-2.rds.amazonaws.com;Initial Catalog=RASLeaveManagement;Persist Security Info=True;User ID=master;Password=rodandstaff2017");
        string pass, acctype;
        public static string user;
        public static int id;
        public Login()
        {
            InitializeComponent();
            MaterialSkin.MaterialSkinManager skinManager = MaterialSkin.MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.DeepOrange600, MaterialSkin.Primary.Grey900, MaterialSkin.Primary.BlueGrey500, MaterialSkin.Accent.Orange700, MaterialSkin.TextShade.WHITE);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Account where acc_username = '"+ LoginUsername.Text +"' and acc_password = '"+ LoginPassword.Text +"'", cn))
                {
                    cn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt16(reader.GetValue(0));
                            acctype = Convert.ToString(reader.GetValue(8));
                            user = Convert.ToString(reader.GetValue(6));
                            pass = Convert.ToString(reader.GetValue(7));
                        }
                    }
                    cn.Close();
                }
                if(acctype == "Top Management")
                {
                    TopManagement frm = new TopManagement();
                    frm.Show();
                    this.Hide();
                }
                else if(acctype == "Manager")
                {
                    Manager frm = new Manager();
                    frm.Show();
                    this.Hide();
                }
                else if(acctype == "Employee")
                {
                    Employee frm = new Employee();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect Input!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check your connection!!");
            }
        }
    }
}
