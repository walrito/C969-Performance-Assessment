using Elements.Database;
using MySql.Data.MySqlClient;
using System;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;

namespace C969_Performance_Assessment
{
    public partial class LoginForm : Form
    {
        private ResourceManager resMan;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            CultureInfo.CurrentCulture.ClearCachedData();
            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
            {
                case "de":
                    resMan = new ResourceManager("C969_Performance_Assessment.lang_de", Assembly.GetExecutingAssembly());
                    break;
                default:
                    resMan = new ResourceManager("C969_Performance_Assessment.lang_en", Assembly.GetExecutingAssembly());
                    break;
            }

            Text = resMan.GetString("btnLogin");
            lblUsername.Text = resMan.GetString("lblUsername");
            lblPassword.Text = resMan.GetString("lblPassword");
            btnLogin.Text = resMan.GetString("btnLogin");
            btnCancel.Text = resMan.GetString("btnCancel");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(mtbPassword.Text))
            {
                MessageBox.Show(resMan.GetString("emptyFields"));
                return;
            }
            else
            {
                MySqlDatabase.spl.Add(new MySqlParameter("@Username", txtUsername.Text));
                MySqlDatabase.spl.Add(new MySqlParameter("@Password", mtbPassword.Text));
                MySqlDataReader dr = MySqlDatabase.ExecuteReader("select userId, UserName from user where userName = @Username and password = @Password", MySqlDatabase.spl, GlobalVariables.DbConn);
                if (dr != null && dr.HasRows)
                {
                    dr.Read();
                    GlobalVariables.UserId = dr.GetInt32(0);
                    GlobalVariables.UserName = dr.GetString(1);
                    GlobalVariables.LoggedIn = true;
                    Close();
                }
                else
                {
                    MessageBox.Show(resMan.GetString("loginMismatch")); ;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}