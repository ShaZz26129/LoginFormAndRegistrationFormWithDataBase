using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginFormWithDataBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }
        public bool IsValid1()
        {
            if (email_txt.Text == string.Empty)
            {
                MessageBox.Show("Email is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (password_txt.Password == string.Empty)
            {
                MessageBox.Show("Password is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-HT83850;Initial Catalog=LoginDB;Integrated Security=True");
            try
            {
                if (IsValid1())
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    String query = "SELECT COUNT(1) FROM UserCredentials WHERE Email =@Email and Password =@Password";
                    SqlCommand sqlCmd = new SqlCommand(query, con);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Email", email_txt.Text);
                    sqlCmd.Parameters.AddWithValue("@Password", password_txt.Password);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {
                       Window1 win = new Window1();
                        win.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Email And Password is Invalid");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { con.Close(); }
        }
        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow dashboard = new MainWindow();
            dashboard.Show();
            this.Close();
        }
    }
}
