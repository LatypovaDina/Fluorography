﻿using System;
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
using System.Globalization;
using System.Diagnostics;
using System.Xml.Linq;

namespace Fluorography
{
	/// <summary>
	/// Логика взаимодействия для Flu.xaml
	/// </summary>
	public partial class Flu : Window
	{

		private DataTable _db;
		Podkl _podkl = new Podkl();
		public Flu()
		{
			InitializeComponent();
		}

		private void getFluor_Click(object sender, RoutedEventArgs e)
		{
			if (selo.Text != string.Empty && street.Text != string.Empty)
			{
				const string connectionString = (@"Data Source=DINA_ILNUROVNA\SQLEXPRESS;Trusted_Connection=Yes;DataBase=AtnSRB");
				var con = new SqlConnection(connectionString);
				con.Open();
				var cmd = new SqlCommand("INSERT INTO [dbo].[Registr] (DateSurvay, Street, Selo, ID_patient)  VALUES (@dateSurvay, @street, @selo, @id)", con);
				cmd.Parameters.AddWithValue("@dateSurvay", data.SelectedDate);
				cmd.Parameters.AddWithValue("@street", street.Text);
				cmd.Parameters.AddWithValue("@selo", selo.Text);
				cmd.Parameters.AddWithValue("@id", Convert.ToInt32(patient.Content));
				string cmm = "UPDATE Patients SET Info = 'пройдена' WHERE ID = '" + Convert.ToInt32(patient.Content) + "'";
				bool success = _podkl.adding_deleting_changing(cmm);
				try
				{

					cmd.ExecuteNonQuery();
					con.Close();
					MessageBox.Show("Отмечено");

				}
				catch
				{
					MessageBox.Show("Возможно вы что-то забыли:)");
				}
			}
			else
			{
				MessageBox.Show("Какое - то поле пропущено");
			}
			//finally
			//{
			//	con.Close();
			//}

			//MessageBox.Show("Отмечено");

		}

		private void data_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
		{
			DateTime? selectedDate = data.SelectedDate;
		}

		private void updating_Click(object sender, RoutedEventArgs e)
		{
			if (selo.Text != string.Empty && street.Text != string.Empty)
			{
				string cmm = "UPDATE Registr SET DateSurvay = '" + data.SelectedDate + "', Street = '" + street.Text + "', Selo = '" + selo.Text + "' WHERE ID_patient = " + Convert.ToInt32(patient.Content) + "";
				bool success = _podkl.adding_deleting_changing(cmm);
				if (success)
				{
					MessageBox.Show("Изменения внесены!");

				}
				else
				{
					MessageBox.Show("Может вы что-то забыли ввести?!");
				}
			}
			else
			{
				MessageBox.Show("Какое - то поле пропущено");
			}
		}
	}
}
