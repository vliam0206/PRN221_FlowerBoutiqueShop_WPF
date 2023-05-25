using Repositories.UnitOfWork;
using System.Windows;

namespace VoNgocTrucLamWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        IUnitOfWork unitOfWork;
        public LoginWindow(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValidate())
            {
                string username = txtUsername.Text;
                string password = txtPassword.Password;
                if (unitOfWork.AccountRepository.AdminLogin(username, password))
                {
                    var adminWindow = new AdminFlowerMngWindow(unitOfWork);
                    adminWindow.Show();
                    this.Close();                    
                } else if (unitOfWork.AccountRepository.CustomerLogin(username, password))
                {
                    var customerWindow = new CustomerInfoMngWindow(unitOfWork)
                    {
                        Customer = unitOfWork.CustomerRepository.GetCustomer(username)
                    };
                    customerWindow.Show();
                    this.Close();
                } else
                {
                    MessageBox.Show("Invalid username or password!", "Login Failed");
                }
            }
        }
        private bool CheckValidate()
        {
            if (txtUsername.Text == "" || txtPassword.Password == "")
            {
                MessageBox.Show("The input field must not be empty!", "Login Failed");
                return false;
            }
            return true;
        }
    }
}
