using BO;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Security.Principal;
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

namespace WPFStore
{
    /// <summary>
    /// Interaction logic for ManagerScreen.xaml
    /// </summary>
    public partial class ManagerScreen : Window
    {
        IAccountService accountService = new AccountService();
        IRoleService roleService = new RoleService();
        IProductService productService = new ProductService();
        ICategoryService categoryService = new CategoryService();

        public Account LoggedInAccount;
        public ManagerScreen()
        {
            InitializeComponent();
            refreshAccountList();
            refreshProductList();

            var listcombo = roleService.GetAllRoles().Select(x => new { x.RoleId, x.RoleName }).ToList();
            cb_Role.DisplayMemberPath = "RoleName";
            cb_Role.SelectedValuePath = "RoleId";
            cb_Role.ItemsSource = listcombo;

            var listcombo2 = categoryService.GetAllCategories().Select(x => new { x.CategoryId, x.CategoryName });
            cb_Category.DisplayMemberPath = "CategoryName";
            cb_Category.SelectedValuePath = "CategoryId";
            cb_Category.ItemsSource = listcombo2;
        }

        public void CheckRole()
        {
            if (LoggedInAccount.RoleId == 1)
            {
                tb_CategoryTab.Visibility = Visibility.Hidden;
                tb_AccountTab.Visibility = Visibility.Visible;
                tb_ProductTab.Visibility = Visibility.Visible;
            }
            else
            {
                tb_CategoryTab.Visibility = Visibility.Hidden;
                tb_AccountTab.Visibility = Visibility.Hidden;
                tb_ProductTab.IsSelected = true;
            }
        }

        private void refreshAccountList()
        {
            dgv_AccountList.UnselectAll();
            dgv_AccountList.ItemsSource = accountService.GetAllAccounts().Join(roleService.GetAllRoles(), a => a.RoleId, b => b.RoleId, (a, b) => new
            {
                a.AccountId,
                a.FullName,
                a.DateOfBirth,
                a.PhoneNumber,
                a.HomeAddress,
                a.AccountEmail,
                b.RoleName,
                a.AccountStatus
            }).ToList();
        }

        private void dgv_AccountList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                string id = dgv_AccountList.SelectedCells[0].Item.ToString().Split(',')[0].Split('=')[1].Trim();
                var account = accountService.GetAccountById(int.Parse(id));
                if (account == null)
                {
                    dgv_AccountList.UnselectAll();
                }
                else
                {
                    txt_AccountId.Text = account.AccountId.ToString();
                    txt_FullName.Text = account.FullName;
                    dt_DateOfBirth.Text = account.DateOfBirth.ToString();
                    txt_PhoneNumber.Text = account.PhoneNumber;
                    txt_Address.Text = account.HomeAddress;
                    txt_Email.Text = account.AccountEmail;
                    txt_Password.Text = account.AccountPassword;
                    cb_Role.SelectedValue = account.RoleId;
                    bool_Status.Text = account.AccountStatus.ToString();
                }
            }
            catch
            {
                dgv_AccountList.UnselectAll();
            }
        }

        private void btn_AddAccount_Click(object sender, RoutedEventArgs e)
        {
            Account account = null;
            try
            {
                account = new Account
                {
                    AccountId = accountService.GetAllAccounts().Last().AccountId + 1,
                    FullName = txt_FullName.Text,
                    DateOfBirth = DateTime.Parse(dt_DateOfBirth.Text),
                    PhoneNumber = txt_PhoneNumber.Text,
                    HomeAddress = txt_Address.Text,
                    AccountEmail = txt_Email.Text,
                    AccountPassword = txt_Password.Text,
                    RoleId = (int)cb_Role.SelectedValue,
                    AccountStatus = bool.Parse(bool_Status.Text.ToLower())
                };
                accountService.AddAccount(account);
                MessageBox.Show("Account added successfully");
                refreshAccountList();
            }
            catch
            {
                string returnMessage = "";
                if (string.IsNullOrEmpty(txt_FullName.Text)
                    || string.IsNullOrEmpty(dt_DateOfBirth.Text)
                    || string.IsNullOrEmpty(txt_PhoneNumber.Text)
                    || string.IsNullOrEmpty(txt_Address.Text)
                    || string.IsNullOrEmpty(txt_Email.Text)
                    || string.IsNullOrEmpty(txt_Password.Text)
                    || string.IsNullOrEmpty(cb_Role.Text)
                    || string.IsNullOrEmpty(bool_Status.Text))
                {
                    returnMessage += "Some of the fields are empty!\n";
                }
                if (!DateTime.TryParse(dt_DateOfBirth.Text, out DateTime date))
                {
                    returnMessage += "Date of birth is not valid\n";
                }
                if (!bool.TryParse(bool_Status.Text.ToLower(), out bool status))
                {
                    returnMessage += "Status is not valid, either true or false\n";
                }
                try
                {
                    MailAddress mail = new MailAddress(txt_Email.Text);
                }
                catch (Exception exc)
                {
                    returnMessage += "Invalid email format";
                }
                MessageBox.Show("Error occurred: \n" + returnMessage);
            }
        }

        private void btn_EditAccount_Click(object sender, RoutedEventArgs e)
        {
            var checkExist = accountService.GetAccountById(int.Parse(txt_AccountId.Text));
            if (checkExist != null)
            {
                if (LoggedInAccount.RoleId < accountService.GetAccountById(int.Parse(txt_AccountId.Text)).RoleId)
                {
                    Account account = new Account
                    {
                        AccountId = int.Parse(txt_AccountId.Text),
                        FullName = txt_FullName.Text,
                        DateOfBirth = DateTime.Parse(dt_DateOfBirth.Text),
                        PhoneNumber = txt_PhoneNumber.Text,
                        HomeAddress = txt_Address.Text,
                        AccountEmail = txt_Email.Text,
                        AccountPassword = txt_Password.Text,
                        RoleId = (int)cb_Role.SelectedValue,
                        AccountStatus = bool.Parse(bool_Status.Text.ToLower())
                    };
                    if (accountService.UpdateAccount(account))
                    {
                        MessageBox.Show("Account updated successfully");
                        refreshAccountList();
                    } else
                    {
                        MessageBox.Show("Error occurred: \n" + "Cannot update account");
                    }
                }
                else
                {
                    MessageBox.Show("You cannot edit account with higher role than you");
                }
            }
            else
            {
                string returnMessage = "";
                if (string.IsNullOrEmpty(txt_FullName.Text)
                    || string.IsNullOrEmpty(dt_DateOfBirth.Text)
                    || string.IsNullOrEmpty(txt_PhoneNumber.Text)
                    || string.IsNullOrEmpty(txt_Address.Text)
                    || string.IsNullOrEmpty(txt_Email.Text)
                    || string.IsNullOrEmpty(txt_Password.Text)
                    || string.IsNullOrEmpty(cb_Role.Text)
                    || string.IsNullOrEmpty(bool_Status.Text))
                {
                    returnMessage += "Some of the fields are empty!\n";
                }
                if (!DateTime.TryParse(dt_DateOfBirth.Text, out DateTime date))
                {
                    returnMessage += "Date of birth is not valid\n";
                }
                if (!bool.TryParse(bool_Status.Text.ToLower(), out bool status))
                {
                    returnMessage += "Status is not valid, either true or false\n";
                }
                try
                {
                    MailAddress mail = new MailAddress(txt_Email.Text);
                }
                catch (Exception exc)
                {
                    returnMessage += "Invalid email format";
                }
                MessageBox.Show("Error occurred: \n" + returnMessage);
            }
        }

        private void btn_DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            bool checkExist = accountService.GetAllAccounts().Any(x => x.AccountId == int.Parse(txt_AccountId.Text));
            if (checkExist)
            {
                if (LoggedInAccount.RoleId < accountService.GetAllAccounts().FirstOrDefault(x => x.AccountId == int.Parse(txt_AccountId.Text)).RoleId)
                {
                    if (accountService.DeleteAccount(int.Parse(txt_AccountId.Text)))
                    {
                        MessageBox.Show("Account deleted successfully");
                        refreshAccountList();
                    }
                    else
                    {
                        MessageBox.Show("Error occurred: \n" + "Cannot delete account");
                    }
                }
                else
                {
                    MessageBox.Show("You cannot delete account with higher role than you");
                }
            }
        }

        private void txt_SearchAccount_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txt_Search.Text;
            dgv_AccountList.ItemsSource = accountService.GetAllAccounts().Join(roleService.GetAllRoles(), a => a.RoleId, b => b.RoleId, (a, b) => new
            {
                a.AccountId,
                a.FullName,
                a.DateOfBirth,
                a.PhoneNumber,
                a.HomeAddress,
                a.AccountEmail,
                b.RoleName,
                a.AccountStatus
            }).Where(x => x.FullName.ToLower().Contains(search.ToLower())).ToList();
        }

        private void btn_ClearFormAccount_Click(object sender, RoutedEventArgs e)
        {
            txt_AccountId.Text = "";
            txt_FullName.Text = "";
            dt_DateOfBirth.Text = "";
            txt_PhoneNumber.Text = "";
            txt_Address.Text = "";
            txt_Email.Text = "";
            txt_Password.Text = "";
            cb_Role.Text = "";
            bool_Status.Text = "";
        }

        private void refreshProductList()
        {
            dgv_ProductList.UnselectAll();
            dgv_ProductList.ItemsSource = productService.GetAllProducts().Join(categoryService.GetAllCategories(), a => a.CategoryId, b => b.CategoryId, (a, b) => new
            {
                a.ProductId,
                a.ProductName,
                a.Quantity,
                a.CalculationUnit,
                b.CategoryName
            }).ToList();
        }

        private void txt_SearchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txt_SearchProduct.Text;
            dgv_ProductList.ItemsSource = productService.GetAllProducts().Join(categoryService.GetAllCategories(), a => a.CategoryId, b => b.CategoryId, (a, b) => new
            {
                a.ProductId,
                a.ProductName,
                a.Quantity,
                a.CalculationUnit,
                b.CategoryName
            }).Where(x => x.ProductName.ToLower().Contains(search.ToLower())).ToList();
        }

        private void dgv_ProductList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                string id = dgv_ProductList.SelectedCells[0].Item.ToString().Split(',')[0].Split('=')[1].Trim();
                var product = productService.GetProductById(int.Parse(id));
                if (product == null)
                {
                    dgv_ProductList.UnselectAll();
                }
                else
                {
                    txt_ProductId.Text = product.ProductId.ToString();
                    txt_ProductName.Text = product.ProductName;
                    int_Quantity.Text = product.Quantity.ToString();
                    dt_CalculationUnit.Text = product.CalculationUnit;
                    cb_Category.SelectedValue = product.CategoryId;
                }
            }
            catch
            {
                dgv_ProductList.UnselectAll();
            }
        }

        private void btn_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Product product = null;
            try
            {
                product = new Product
                {
                    ProductId = productService.GetAllProducts().Last().ProductId + 1,
                    ProductName = txt_ProductName.Text,
                    Quantity = int.Parse(int_Quantity.Text),
                    CalculationUnit = dt_CalculationUnit.Text,
                    CategoryId = (int)cb_Category.SelectedValue
                };
                productService.AddProduct(product);
                MessageBox.Show("Product added successfully");
                refreshProductList();
            }
            catch
            {
                string returnMessage = "";
                if (string.IsNullOrEmpty(txt_ProductName.Text)
                    || string.IsNullOrEmpty(int_Quantity.Text)
                    || string.IsNullOrEmpty(dt_CalculationUnit.Text)
                    || string.IsNullOrEmpty(cb_Category.Text))
                {
                    returnMessage += "Some of the fields are empty!\n";
                }
                if (!int.TryParse(int_Quantity.Text, out int quantity))
                {
                    returnMessage += "Quantity is not valid\n";
                }
                else if (quantity < 0)
                {
                    returnMessage += "Quantity must be greater than 0\n";
                }
                MessageBox.Show("Error occurred: \n" + returnMessage);
            }
        }

        private void btn_EditProduct_Click(object sender, RoutedEventArgs e)
        {
            bool checkExist = productService.GetAllProducts().Any(x => x.ProductId == int.Parse(txt_ProductId.Text));
            if (checkExist)
            {
                Product product = new Product
                {
                    ProductId = int.Parse(txt_ProductId.Text),
                    ProductName = txt_ProductName.Text,
                    Quantity = int.Parse(int_Quantity.Text),
                    CalculationUnit = dt_CalculationUnit.Text,
                    CategoryId = (int)cb_Category.SelectedValue
                };
                productService.UpdateProduct(product);
                MessageBox.Show("Product updated successfully");
                refreshProductList();
            }
            else
            {
                string returnMessage = "";
                if (string.IsNullOrEmpty(txt_ProductName.Text)
                    || string.IsNullOrEmpty(int_Quantity.Text)
                    || string.IsNullOrEmpty(dt_CalculationUnit.Text)
                    || string.IsNullOrEmpty(cb_Category.Text))
                {
                    returnMessage += "Some of the fields are empty!\n";
                }
                if (!int.TryParse(int_Quantity.Text, out int quantity))
                {
                    returnMessage += "Quantity is not valid\n";
                }
                else if (quantity < 0)
                {
                    returnMessage += "Quantity must be greater than 0\n";
                }
                MessageBox.Show("Error occurred: \n" + returnMessage);
            }
        }

        private void btn_DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            bool checkExist = productService.GetAllProducts().Any(x => x.ProductId == int.Parse(txt_ProductId.Text));
            if (checkExist)
            {
                productService.DeleteProduct(int.Parse(txt_ProductId.Text));
                MessageBox.Show("Product deleted successfully");
                refreshProductList();
            }
        }

        private void btn_ClearFormProduct_Click(object sender, RoutedEventArgs e)
        {
            txt_ProductId.Text = "";
            txt_ProductName.Text = "";
            int_Quantity.Text = "";
            dt_CalculationUnit.Text = "";
            cb_Category.Text = "";
        }
    }
}
