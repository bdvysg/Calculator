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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = selectedValue.ToString();
            }
            else
            {
                resultLabel.Content += selectedValue.ToString();
            }
        }

        private void operationButton_CLick(object sender, RoutedEventArgs e)
        {  
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber) && previousOperations.Content == "")
            {
                char operationChar = ' ';
                if (sender == plusButton)
                {
                    selectedOperator = SelectedOperator.Addition;
                    operationChar = '+';
                }
                if (sender == minusButton)
                {
                    selectedOperator = SelectedOperator.Substraction;
                    operationChar = '-';
                }
                if (sender == multiplyButton)
                {
                    selectedOperator = SelectedOperator.Multiplication;
                    operationChar = '*';
                }
                if (sender == divideButton)
                {
                    selectedOperator = SelectedOperator.Division;
                    operationChar = '/';
                }
                previousOperations.Content = resultLabel.Content + " " + operationChar;
                resultLabel.Content = "0";
            }


        }

        private void ACButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            previousOperations.Content = "";
        }

        private void percentageButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber / 100;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleOperations.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Substraction:
                        result = SimpleOperations.Subtract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleOperations.Multiplicate(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleOperations.Divide(lastNumber, newNumber);
                        break;
                }
            }
            previousOperations.Content = "";
            resultLabel.Content = result.ToString();
        }

        private void dotButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains(","))
            {
                resultLabel.Content += ",";
            }
        }

        private void plusMinusButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Substraction,
        Multiplication,
        Division
    }

    public class SimpleOperations
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static double Subtract(double a, double b)
        {
            return a - b;
        }

        public static double Multiplicate(double a, double b)
        {
            return a * b;

        }

        public static double Divide(double a, double b)
        {
            if (b == 0)
            {
                MessageBox.Show("Division by 0 is not supported", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return a / b;
        }
    }
}
