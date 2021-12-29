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
            //判断是否为数字键
            string currentVal = currentBtn.Content.ToString();
            if (currentVal == "C")
            {
                if (m_Text.ToString().Length == 1)
                    m_Text = 0;
                else
                    m_Text = Convert.ToDouble((int)(m_Text / 10));
                m_TextStr = m_Text.ToString();
            }
            else if(currentVal == "+" || currentVal == "-" || currentVal == "*" || currentVal == "/")
            {
                m_Operation = currentVal;
                m_LastNum = m_Text;
            }
            else if(currentVal == "=")
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
                if(m_Last != "0" && m_Last != "1" && m_Last != "2" && m_Last != "3" && m_Last != "4" && m_Last != "5" && m_Last != "6" && m_Last != "7" && m_Last != "8" && m_Last != "9")
                {
                    m_TextStr = "0.";
                    m_Text = 0;
                }
            }
            else //数字
            {
                if(m_Last == "0" || m_Last == "1" || m_Last == "2" || m_Last == "3" || m_Last == "4" || m_Last == "5" || m_Last == "6" || m_Last == "7" || m_Last == "8" || m_Last == "9")
                {
                    //00时不写入
                    //前一个数不为0或为.时写入
                    if(currentVal != "0" && m_Last == "0" || m_Last != "0")
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
                else{
                    m_TextStr = currentVal;
                    m_Text = Convert.ToDouble(currentVal);
                }
            }
            m_Last = currentVal;
            //修改textbox值
            textBox.Text = m_TextStr;
        }
    }
}
