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
using WPF20.Models;

namespace WPF20
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Top = Properties.Settings.Default.Position.Top;
            this.Left = Properties.Settings.Default.Position.Left;
            this.Height = Properties.Settings.Default.Position.Height;
            this.Width = Properties.Settings.Default.Position.Width;
        }
        private double result = 0;
        private string operation = null;
        private string sign = "";
        //count - количество выполняемых операций
        private bool calculationOn = false;
        //calculationOn - индикатор выполнения операций

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Position = this.RestoreBounds;
            Properties.Settings.Default.Save();
        }

        private void Button_Del_Click(object sender, RoutedEventArgs e)
        {
            if (Result_Box.Text.Length == 1 || (Result_Box.Text.Length == 2) && sign == "-") Result_Box.Text = "0";
            else Result_Box.Text = Result_Box.Text.Substring(0, Result_Box.Text.Length - 1);
        }

        private void Button_C_Click(object sender, RoutedEventArgs e)
        {
            Result_Box.Text = "0";
        }

        private void Button_CE_Click(object sender, RoutedEventArgs e)
        {
            Result_Box.Text = "0";
            Expression.Content = null;
            sign = "";
            calculationOn = false;
            result = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Result_Box.Text == "0" || Result_Box.Text == "-0") Result_Box.Text = sign + (sender as Button).Content.ToString();
            else Result_Box.Text += (sender as Button).Content.ToString();
        }
        private void Button_0_Click(object sender, RoutedEventArgs e)
        {
            if (Result_Box.Text != "0") Result_Box.Text += "0";
        }
        private void Button_Point_Click(object sender, RoutedEventArgs e)
        {
            if (!Result_Box.Text.Contains(".")) Result_Box.Text += ".";
        }
        private void Button_Sign_Click(object sender, RoutedEventArgs e)
        {
            if (sign == "-")
            {
                sign = "";
                Result_Box.Text = Result_Box.Text.Substring(1);
            }
            else
            {
                sign = "-";
                Result_Box.Text = sign + Result_Box.Text;
            }
        }

        private void Button_Operation_Click(object sender, RoutedEventArgs e)
        {
            operationButton();
            operation = (sender as Button).Content.ToString();
        }

        private void operationButton()
        {
            try
            {
                if (calculationOn == false)
                {
                    result = Convert.ToDouble(Result_Box.Text);
                    Expression.Content = Result_Box.Text;
                    Result_Box.Text = "0";
                    calculationOn = true;
                }
                else
                {
                    result = Calculation.Operation(result, Convert.ToDouble(Result_Box.Text), operation);
                    Expression.Content += $" {operation} " + Result_Box.Text + " = ";
                    Result_Box.Text = Convert.ToString(result);
                    calculationOn = false;
                }
                sign = "";
            }
            catch
            {
                Result_Box.Text = "Деление на 0";
                result = 0;
                calculationOn = false;
            }
        }
    }
}
