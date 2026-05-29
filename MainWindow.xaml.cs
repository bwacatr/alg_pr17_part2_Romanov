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
using System.IO;

namespace PR17_romanov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Country> countries = new List<Country>();

        
        public MainWindow()
        {
            InitializeComponent(); 
        }

        private void Task1_Click(object sender, RoutedEventArgs e) // кнопка задание 1
        {
            if (Task2_Panel.Visibility == Visibility.Visible)
            {
                Task2_Panel.Visibility = Visibility.Hidden;
            }
            Task1_Panel.Visibility = Visibility.Visible;
            
        }

        private void Task2_Click(object sender, RoutedEventArgs e)  // кнопка задание 2
        {
            if (Task1_Panel.Visibility == Visibility.Visible)
            {
                Task1_Panel.Visibility = Visibility.Hidden;
            }
            Task2_Panel.Visibility = Visibility.Visible;

            LoadFile("File.txt");

        }

        private void Exit_Click(object sender, RoutedEventArgs e)  // кнопка выход
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Кнопка Поиск
        {
            resultListBox1.Items.Clear();
            resultListBox2.Items.Clear();

            if (!string.IsNullOrWhiteSpace(txtInputWord.Text))
            {
                string input = txtInputWord.Text;
                string[] lines = input.Split();

                for (int i = 0; i < lines.Length; i++)
                {
                    if (!double.TryParse(lines[i], out double result))
                    {
                        MessageBox.Show("Во введенном поле присутствуют лишние символы");
                        return;
                    }

                    var count = lines.Count(x => x == lines[i]);
                    if (!resultListBox1.Items.Contains($"{lines[i]} - {count}"))
                    {
                        resultListBox1.Items.Add($"{lines[i]} - {count}");
                        resultListBox2.Items.Add($"{double.Parse(lines[i]) * count} - {count}");
                    }
                    
                }

            }
            else
            {
                MessageBox.Show("Заполните поле");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Кнопка Начать
        {
            if (string.IsNullOrWhiteSpace(NumberTextBox.Text))
            {
                MessageBox.Show("Введите целое число в поле");
                return;
            }

            if (int.TryParse(NumberTextBox.Text, out int result))
            {
                var sortedCountries = from c in countries where (c.Population > result) select c;
                var group = sortedCountries.OrderBy(c => c.Name.Length);
                foreach (var item in group)
                {
                    ListBoxSorted.Items.Add($"{item.Name} - {item.Population}");
                }
            }
            else
            {
                MessageBox.Show("Введен лишний символ");
            }
        }

        public void LoadFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                countries.Clear();
                ListBoxFile.Items.Clear();
                using (StreamReader sr = File.OpenText(fileName))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] lines = sr.ReadLine().Split(' ');
                        if (lines.Length == 2)
                        {
                            Country country = new Country(lines[0], int.Parse(lines[1]));
                            countries.Add(country);
                            ListBoxFile.Items.Add($"{country.Name} - {country.Population}");
                        }
                        else
                        {
                            MessageBox.Show("Файл некорректно заполнен");
                            return;
                        }
                        
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Файл не найден");
            }
        }
    }
}
