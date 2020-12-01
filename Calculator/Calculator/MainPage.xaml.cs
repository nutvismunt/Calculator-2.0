using System;
using System.Linq;
using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        private const string startValue = "0";
        private bool isNextNum = true;
        private short numLength;

        public MainPage()
        {
            InitializeComponent();
            resultLabel.Text = startValue;
            resultLabel.FontSize = 35;
        }

        void OnClear(object sender, EventArgs e)
        {
            resultLabel.Text = resultLabel.Text.Substring(0, resultLabel.Text.Length - 1);
            if (resultLabel.Text.Count() == 0)
                resultLabel.Text = startValue;
            MaxLength(resultLabel.Text.Count());
            numLength--;
        }

        void OnDelete(object sender, EventArgs e)
        {
            resultLabel.Text = resultLabel.Text.Replace(resultLabel.Text, "");
            resultLabel.Text = startValue;
            MaxLength(resultLabel.Text.Count());
            numLength = 0;
        }

        void OnSelectNum(object sender, EventArgs e)
        {
            var num = sender as Button;
            var result = resultLabel.Text;
            if (numLength>=14)
            {
                goto End;
            }
            if (isNextNum)
            {
                switch (result)
                {
                    case "NaN":
                        resultLabel.Text = result.Replace(result, num.Text);
                        break;
                    case startValue:
                        resultLabel.Text = result.Replace(result, num.Text);
                        break;
                    default:
                        resultLabel.Text += num.Text; break;
                }
                numLength++;
            }
            else
                goto End;
            End:
            MaxLength(result.Count());
        }

        void OnSelectOperator(object sender, EventArgs e)
        {
            var result = resultLabel.Text;
            var num = sender as Button;
            var operation =num.Text!="."? ' '+ num.Text + ' ': num.Text;
            MaxLength(resultLabel.Text.Count());
            resultLabel.Text = char.IsDigit(result.Last()) ? result + operation : result.Remove(result.Length - 1).Insert(result.Length - 1, operation);
            isNextNum = true;
            numLength = 0;
        }

        void OnCalculate(object sender, EventArgs e)
        {
            var labelText = resultLabel.Text;
            if (char.IsDigit(labelText.Last()))
            {
                try
                {
                    char check = ' ';
                    string [] numbers = labelText.Split(check);
                    var answer = Calculation(numbers);
                    resultLabel.Text = answer.ToString();
                    MaxLength(resultLabel.Text.Count());
                }
                catch (Exception) { 
                    resultLabel.Text = "Ошибка"; 
                }
            }
        }

        bool MaxLength(int length)
        {
            var isLength = length >= 14;
            switch (isLength)
            {
                case true:
                    resultLabel.FontSize = 30; return false;
                default:
                    resultLabel.FontSize = 40; return true;
            }
        }

        public double Calculation(string [] enumer)
        {
            double result = double.Parse(enumer[0]);
            for (int i=0; i<enumer.Length; i++)
            {
                
                switch (enumer[i])
                {
                    case "+":
                        result += double.Parse(enumer[i + 1]);
                        break;
                    case "-":
                        result -= double.Parse(enumer[i + 1]);
                        break;
                    case "/":
                        result /=double.Parse(enumer[i + 1]);
                        break;
                    case "*":
                        result *= double.Parse(enumer[i + 1]);
                        break;
                }
            }
            return result;
        }

}
}
