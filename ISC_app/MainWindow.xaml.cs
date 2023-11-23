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

namespace ISC_app
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Service service = new Service();

		public MainWindow()
		{
			InitializeComponent();
			data.ItemsSource = service.GetAll();
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			UserForm userForm = new UserForm();
			userForm.Closed += (_, _) =>
			{
				data.ItemsSource = service.GetAll();
			};
			userForm.ShowDialog();
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			User selected = data.SelectedItem as User;
			if (selected == null)
			{
				MessageBox.Show("Törléshez előbb válasszon egy elemt!");
				return;
			}

			if (MessageBoxResult.Yes == MessageBox.Show("Biztos törli az elemet: " + selected.Name, "Törlés", MessageBoxButton.YesNo, MessageBoxImage.Warning))
			{
				if (service.Delete(selected))
				{
					MessageBox.Show("Sikeres törlés");
				}
				else
				{
					Console.WriteLine("Hiba történt a törlés során");
				}
				data.ItemsSource = service.GetAll();
			}
		}

		private void Update_Click(object sender, RoutedEventArgs e)
		{
			User selected = data.SelectedItem as User;
			if (selected == null)
			{
				MessageBox.Show("Módosításhoz előbb válasszon egy elemt!");
				return;
			}

			UserForm userForm = new UserForm(selected);
			userForm.Closed += (_, _) =>
			{
				data.ItemsSource = service.GetAll();
			};
			userForm.ShowDialog();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			User selected = data.SelectedItem as User;
			if (selected == null)
			{
				MessageBox.Show("Válaszon ki valakit, akinek az egyenlegét csökkenti 5000-rel!");
				return;
			}

			selected.Balance = selected.Balance - 5000;
			service.Update(selected.Id,selected);
			data.ItemsSource = service.GetAll();
		}
	}
}
