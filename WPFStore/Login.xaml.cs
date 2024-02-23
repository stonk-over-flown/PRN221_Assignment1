using Repositories;
using Services;
using System;
using System.Collections.Generic;
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

namespace WPFStore
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        IAccountService accountService = new AccountService();

        public Login()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            string email = txt_Email.Text;
            string password = txt_Password.Password;
            bool checkLogin = accountService.CheckLogin(email, password);

            if (checkLogin)
            {
                ManagerScreen managerScreen = new ManagerScreen();
                managerScreen.LoggedInAccount = accountService.GetAllAccounts().FirstOrDefault(x => x.AccountEmail == email && x.AccountPassword == password);
                var checkrole = accountService.GetAllAccounts().FirstOrDefault(x => x.AccountEmail == email && x.AccountPassword == password).RoleId;
                switch (checkrole)
                {
                    case 1:
                        MessageBox.Show("Login as Admin");
                        managerScreen.CheckRole();
                        managerScreen.Show();
                        this.Close();
                        break;
                    case 2:
                        MessageBox.Show("Login as Manager");
                        managerScreen.CheckRole();
                        managerScreen.Show();
                        this.Close();
                        break;
                    case 3:
                        MessageBox.Show("Login as Staff: No Permission");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Login failed");
            }
        }
    }
}
