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

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool clear;

        public MainWindow()
        {
            InitializeComponent();

            this.clear = false;
        }


        // КНОПКИ УПРАВЛЕНИЯ
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.numTextBox.Text != "0")
            {
                this.numTextBox.Text = this.numTextBox.Text.Remove(this.numTextBox.Text.Length - 1, 1);
                if (this.numTextBox.Text == "")
                {
                    this.numTextBox.Text = "0";
                }
            }
        }

        private void CleanEntryButton_Click(object sender, RoutedEventArgs e)
        {
            this.numTextBox.Text = "0";
        }

        private void CleanButton_Click(object sender, RoutedEventArgs e)
        {
            this.expTextBox.Text = string.Empty;
            this.numTextBox.Text = "0";
        }


        // КНОПКИ С ЧИСЛАМИ
        private void NumButton_Click(object sender, RoutedEventArgs e)
        {
            this.Clear();

            if (this.numTextBox.Text.Length < 17)
            {
                if (sender is Button button)
                {
                    if (this.numTextBox.Text == "0" && button.Content.ToString() != ",")
                    {
                        this.numTextBox.Text = button.Content.ToString();
                    }
                    else
                    {
                        this.numTextBox.Text += button.Content.ToString();
                    }
                }
            }
        }


        // КНОПКИ С МАТЕМАТИЧЕСКИМИ ОПЕРАЦИЯМИ
        private void SymButton_Click(object sender, RoutedEventArgs e)
        {
            this.Clear();

            if (sender is Button button)
            {   
                if (string.IsNullOrEmpty(this.expTextBox.Text))
                {
                    this.expTextBox.Text += this.numTextBox.Text + " "; // добавление введённого пользователем числа
                    this.expTextBox.Text += button.Content.ToString() + " "; // добавление математического знака

                    this.numTextBox.Text = "0"; // обнуление введённого пользователям числа
                }
                else
                {
                    this.ResultButton_Click(sender, e);
                    this.SymButton_Click(sender, e);
                }
            }
        }


        // КНОПКА РЕЗУЛЬТАТ
        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.expTextBox.Text))
            {
                string[] expTextBoxStr = this.expTextBox.Text.Split(' ');

                try
                {
                    decimal firstNum = Convert.ToDecimal(expTextBoxStr[0]); // получение первого числа
                    decimal secondNum = Convert.ToDecimal(this.numTextBox.Text); // получение второго числа
                    string mathSymbol = expTextBoxStr[1]; // получение математического знака

                    decimal result;
                    switch (mathSymbol)
                    {
                        case "/": result = firstNum / secondNum; break;
                        case "*": result = firstNum * secondNum; break;
                        case "-": result = firstNum - secondNum; break;
                        case "+": result = firstNum + secondNum; break;

                        default: result = 0; break;
                    }

                    this.expTextBox.Text += secondNum + " =";
                    this.numTextBox.Text = this.Round(result).ToString();
                }
                catch (DivideByZeroException)
                {
                    System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=Dd2QvzpM8ss");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                this.clear = true;
            }
        }


        // ВСОМОГАТЕЛЬНЫЕ МЕТОДЫ
        private void Clear()
        {
            if (this.clear)
            {
                this.expTextBox.Text = string.Empty;
                this.clear = false;
            }
        }

        private decimal Round(decimal number) // нужно, чтобы в случае, например, 20,1 - 0,1 = 20,0 - не таскать этот ноль бесячий
        {
            if (number == Math.Truncate(number))
            {
                return Math.Truncate(number);
            }
            return number;
        }
    }
}
