using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ISC_app
{
    /// <summary>
    /// Interaction logic for UserForm.xaml
    /// </summary>
    public partial class UserForm : Window
    {
		private Service service = new Service();
		User user;

        public UserForm()
        {
            InitializeComponent();
        }

		public UserForm(User user)
		{
			InitializeComponent();
			this.user = user;
			LoadUser();
			btnAdd.Visibility = Visibility.Collapsed;
			btnUpdate.Visibility = Visibility.Visible;
		}

		public void LoadUser()
		{
			inputName.Text = this.user.Name;
			inputCreditCard.Text = this.user.Credit_card;
			inputCVV.Text = this.user.CVV;
			inputBalance.Text = Convert.ToString(this.user.Balance);
		}

		private User Ellenorzes()
        {
            if(inputName.Text.Trim().Length < 1)
            {
                MessageBox.Show("Adjon meg nevet!");
                inputName.BorderBrush = Brushes.Red;
				inputName.BorderThickness = new Thickness(3);
				return null;
            }
			if(int.TryParse(inputName.Text, out int result))
			{
				MessageBox.Show("Nem adhat meg számokat a névben!");
				inputName.BorderBrush = Brushes.Red;
				return null;
			}
            if (!Regex.IsMatch(inputCreditCard.Text,"^[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}$"))
            {
				MessageBox.Show("Hibás formátum!");
				inputCreditCard.BorderBrush = Brushes.Red;
				inputCreditCard.BorderThickness = new Thickness(3);
				return null;
			}
			if (inputCreditCard.Text.Trim().Length < 1)
			{
				MessageBox.Show("Adjon meg egy kártya!");
				inputCreditCard.BorderBrush = Brushes.Red;
				inputCreditCard.BorderThickness = new Thickness(3);
				return null;
			}
			if (inputCVV.Text.Trim().Length != 3)
            {
				MessageBox.Show("A CVV három számból kell állnija!");
				inputCVV.BorderBrush = Brushes.Red;
				inputCVV.BorderThickness = new Thickness(3);
				return null;
			}
			if(inputBalance.Text.Trim().Length < 1)
			{
				MessageBox.Show("Adjon meg egyenleget!");
				inputBalance.BorderBrush = Brushes.Red;
				inputBalance.BorderThickness = new Thickness(3);
				return null;
			}

			User user = new User();
			user.Name = inputName.Text;
			user.Credit_card = inputCreditCard.Text;
			user.CVV = inputCVV.Text;
			user.Balance = Convert.ToInt32(inputBalance.Text);			
			return user;
        }

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			User user = Ellenorzes();
			if(user!=null)
			{
				service.Add(user);

				inputName.BorderBrush = Brushes.LightGray;
				inputCreditCard.BorderBrush = Brushes.LightGray;
				inputCVV.BorderBrush = Brushes.LightGray;
				inputBalance.BorderBrush = Brushes.LightGray;

				inputName.BorderThickness = new Thickness(1);
				inputCreditCard.BorderThickness = new Thickness(1);
				inputCVV.BorderThickness = new Thickness(1);
				inputBalance.BorderThickness = new Thickness(1);

				MessageBox.Show("Sikeres adatfelvétel!");
				inputName.Text = "";
				inputCreditCard.Text = "";
				inputCVV.Text = "";
				inputBalance.Text = "";
			}
        }

		private void inputNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
			{
				e.Handled = true;
			}
		}

		private void inputText_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
			{
				e.Handled = true;
			}
		}

		private void inputCreditCard_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (inputCreditCard.Text.Length == 4)
			{
				inputCreditCard.Text = inputCreditCard.Text + "-";
				inputCreditCard.SelectionStart = inputCreditCard.Text.Length;
				inputCreditCard.SelectionLength = 0;
			}
			else if (inputCreditCard.Text.Length == 9)
			{
				inputCreditCard.Text = inputCreditCard.Text + "-";
				inputCreditCard.SelectionStart = inputCreditCard.Text.Length;
				inputCreditCard.SelectionLength = 0;
			}
			else if (inputCreditCard.Text.Length == 14)
			{
				inputCreditCard.Text = inputCreditCard.Text + "-";
				inputCreditCard.SelectionStart = inputCreditCard.Text.Length;
				inputCreditCard.SelectionLength = 0;
			}
		}

		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			User user = Ellenorzes();
			if(user != null)
			{
				User update = service.Update(this.user.Id, user);
				if (update.Id != 0)
				{
					MessageBox.Show("Sikeres módosítás!");
					this.Close();
				}
				else
				{
					MessageBox.Show("Hiba a módosítás során!");
				}
			}		
		}
	}
}
