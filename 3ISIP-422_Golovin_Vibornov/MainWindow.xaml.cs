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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3ISIP_422_Golovin_Vibornov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Functions.Items.Add("sh(x)");
            this.Functions.Items.Add("x^2");
            this.Functions.Items.Add("e^x");
            this.Functions.SelectedIndex = 0;
        }

        double firstEquation(double x, double y)
        {
            return Math.Pow((x - y), 3) + Math.Atan(x);
        }

        double secondEquation(double x, double y)
        {
            return Math.Pow((y - x), 3) + Math.Atan(x);
        }

        double thirdEquation(double x, double y)
        {

            return Math.Pow((y + x), 3) + 0.5;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = this.Functions.SelectedIndex;
            double x, y, result;
            x = 0;
            y = 0;
            result = 0;
            if (this.X.Text=="") {
                MessageBox.Show("Введите значение мега X.");
                return;
            }
            if (this.Y.Text == "")
            {
                MessageBox.Show("Введите значение мега Y.");
                return;
            }

            y = Double.Parse(this.Y.Text);
            switch (index)
            {
                case 0:
                    x = Math.Sinh(Double.Parse(this.X.Text));
                    break;
                case 1:
                    x = Math.Pow(Double.Parse(this.X.Text), 2);
                    break;
                case 2:
                    x = Math.Pow(Math.E, Double.Parse(this.X.Text));
                    break;
                default:
                    break;
            }

            double x_prev = Double.Parse(this.X.Text);
            if (x_prev > y)
            {
                result = firstEquation(x, y);
            } 
            else if(y > x_prev)
            {
                result = secondEquation(x, y);
            } 
            else if (y == x_prev)
            {
                result = thirdEquation(x, y);
            }

            this.Answer.Text = result.ToString();
        }

        private void PreviewTextInputCheck(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null) return;

            string currentText = textBox.Text;

            string newText = currentText.Insert(textBox.SelectionStart, e.Text);

            Regex regex = new Regex("[^0-9.-]+");
            if (regex.IsMatch(e.Text))
            {
                e.Handled = true; 
                return;
            }

            if (e.Text == "-")
            {
                if (textBox.SelectionStart != 0 || currentText.Contains("-"))
                {
                    e.Handled = true; 
                    return;
                }
            }

            if (e.Text == ".")
            {
                if (currentText.Contains(".") || textBox.SelectionStart == 0)
                {
                    e.Handled = true; 
                    return;
                }
            }

            e.Handled = false;
        }
        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null) return;

            string currentText = textBox.Text;

            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pasteText = e.DataObject.GetData(typeof(string)) as string;

                Regex regex = new Regex("[^0-9.-]+");
                if (regex.IsMatch(pasteText))
                {
                    e.CancelCommand();
                    return;
                }

                if (pasteText.Contains("-"))
                {
                    if (textBox.SelectionStart != 0 || currentText.Contains("-"))
                    {
                        e.CancelCommand(); 
                        return;
                    }
                }

                if (pasteText.Contains("."))
                {
                    if (currentText.Contains(".") || textBox.SelectionStart == 0)
                    {
                        e.CancelCommand();
                        return;
                    }
                }
            }
            else
            {
                e.CancelCommand(); 
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Answer.Text = "";
            this.X.Text = "";
            this.Y.Text = "";
            this.Functions.SelectedIndex = 0;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Вы уверены, что хотите выйти?",
                "Мега выход.",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true; 
            }
        }
    }
}
