using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Shapes;

namespace LoginFormWithDataBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            clearData();
        }
        public void clearData()
        {
            //search_txt.Clear();
            firstname_txt.Clear();
            lastname_txt.Clear();
            email_txt.Clear();
            password_txt.Clear();
            age_txt.Clear();
            gender_txt.Clear();
            city_txt.Clear();

        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-HT83850;Initial Catalog=LoginDB;Integrated Security=True");
        public bool isValid()
        {
            if (firstname_txt.Text == string.Empty)
            {
                MessageBox.Show("First Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (lastname_txt.Text == string.Empty)
            {
                MessageBox.Show("Last Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
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
            if (age_txt.Text == string.Empty)
            {
                MessageBox.Show("Age is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (gender_txt.Text == string.Empty)
            {
                MessageBox.Show("Gender is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (city_txt.Text == string.Empty)
            {
                MessageBox.Show("City name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void InsertBtn_Click_1(object sender, RoutedEventArgs e)
        {
                try
                {
                    if (isValid())
                    {
                        SqlCommand cmd = new SqlCommand("Insert into UserCredentials (FirstName,LastName,Email,Password,Age,Gender,City) values(@FirstName,@LastName,@Email,@Password,@Age,@Gender,@City)", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@FirstName", firstname_txt.Text);
                        cmd.Parameters.AddWithValue("@LastName", lastname_txt.Text);
                        cmd.Parameters.AddWithValue("@Email", email_txt.Text);
                        cmd.Parameters.AddWithValue("@Password", password_txt.Password);
                        cmd.Parameters.AddWithValue("@Age", age_txt.Text);
                        cmd.Parameters.AddWithValue("@Gender", gender_txt.Text);
                        cmd.Parameters.AddWithValue("@City", city_txt.Text);
                        con.Open();    
                        cmd.ExecuteNonQuery();
                        con.Close();
                        
                        // LoadGrid();
                        MessageBox.Show("Successfully Registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearData();
                    LoginScreen login = new LoginScreen();
                    login.Show();
                    this.Close();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
        
    }
}