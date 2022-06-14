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

namespace WpfCalucation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        double m_Text = 0;
        string m_Operation = "";
        string m_Last = "";
        string m_TextStr = "";
        double m_LastNum = 0;

        List<int> canKey = new List<int>() { 1,2, 5, 6,32,
            34,35,36,37,38,39,40,41,42,43,
            74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89, 
            141,143,147,148,153,154};

        public MainWindow()
        {
            InitializeComponent();
        }

        public double Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = value;
            }
        }
        private void Button_Click(object sender, MouseButtonEventArgs e)
        {
            //检索当前按钮
            Button currentBtn = (Button)e.Source;
            string key = currentBtn.Content.ToString();
            Click(key);
        }

        private void key_Click(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.A))
            {
                //逻辑
            }
        }

        private void Click(string currentVal)
        {
            if (string.IsNullOrEmpty(currentVal))
            {
                return;
            }
            //判断是否为数字键
            if(currentVal == "Clear")
            {
                m_Text = 0;
                m_LastNum = 0;
                m_TextStr = "0";
            }
            else if (currentVal == "C")
            {
                if (m_Text.ToString().Length == 1)
                    m_Text = 0;
                else
                    m_Text = Convert.ToDouble((int)(m_Text / 10));
                m_TextStr = m_Text.ToString();
            }
            else if (currentVal == "+" || currentVal == "-" || currentVal == "*" || currentVal == "/")
            {
                m_Operation = currentVal;
                m_LastNum = m_Text;
                symbol.Text = m_Operation;
            }
            else if (currentVal == "=")
            {
                symbol.Text = "";
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    double textValue = Convert.ToDouble(textBox.Text);
                    switch (m_Operation)
                    {
                        case "+":
                            m_Text = m_LastNum + textValue;
                            break;
                        case "-":
                            m_Text = m_LastNum - textValue;
                            break;
                        case "*":
                            m_Text = m_LastNum * textValue;
                            break;
                        case "/":
                            m_Text = m_LastNum / textValue;
                            break;
                    }
                }
                m_TextStr = m_Text.ToString();
                m_LastNum = 0;
            }
            else if (currentVal == ".")
            {
                //如果是小数点：m_text有小数点时不写入
                if (m_Text % 1 == 0)
                {
                    m_TextStr = m_Text + ".";
                }
                //小数点：前一次点击不为数字时显示0.
                if (m_Last != "0" && m_Last != "1" && m_Last != "2" && m_Last != "3" && m_Last != "4" && m_Last != "5" && m_Last != "6" && m_Last != "7" && m_Last != "8" && m_Last != "9")
                {
                    m_TextStr = "0.";
                    m_Text = 0;
                }
            }
            else //数字
            {
                if (m_Last == "0" || m_Last == "1" || m_Last == "2" || m_Last == "3" || m_Last == "4" || m_Last == "5" || m_Last == "6" || m_Last == "7" || m_Last == "8" || m_Last == "9")
                {
                    //00时不写入
                    //前一个数不为0或为.时写入
                    if (currentVal != "0" && m_Last == "0" || Convert.ToDouble(textBox.Text) != 0)
                    {
                        m_TextStr = m_Text + "" + currentVal;
                        m_Text = Convert.ToDouble(m_TextStr);
                    }
                }
                else if (m_Last == ".")
                {
                    m_TextStr = m_Text + "." + currentVal;
                    m_Text = Convert.ToDouble(m_TextStr);
                }
                else
                {
                    m_TextStr = currentVal;
                    m_Text = Convert.ToDouble(currentVal);
                }
            }
            m_Last = currentVal;
            //修改textbox值
            textBox.Text = m_TextStr;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (CheckKey(e.Key))
            {
                string key = e.Key.ToString();
                string realKey = switchKey(key);
                Click(realKey);
            }
        }

        private string switchKey(string key)
        {
            string result = key;
            switch (result)
            {
                case "Add":
                case "OemPlus":
                    result = "+";
                    break;
                case "Multiply":
                    result = "*";
                    break;
                case "Subtract":
                    result = "-";
                    break;
                case "Decimal":
                case "OemPeriod":
                    result =".";
                    break;
                case "Divide":
                    result = "/";
                    break;
                case "Return":
                    result = "=";
                    break;
                case "Back":
                    result = "C";
                    break;
                case "Delete":
                    result = "Clear";
                    break;
                default:
                    break;
            }
            if (result.StartsWith("NumPad"))
            {
                result = result.Substring(6);
            }
            if (result.StartsWith("D"))
            {
                result = result.Substring(1);
            }
            return result;
        }
        private bool CheckKey(Key key)
        {
            if (canKey.Contains((int)key))
            {
                return true;
            }
            return false;
        }
    }
}
