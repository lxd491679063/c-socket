using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //string temp_out_str = "100";
            //string total_cost_res = outTest(3, 10, out string temp_total);
            //string total_cost_res = outTest(3, 10, out temp_out_str);
            //string total_cost_res = refTest(3, 10, ref temp_out_str);
            //MessageBox.Show("total_cost = " + total_cost_res);
            //MessageBox.Show("temp_total = " + temp_total);
            //MessageBox.Show("temp_total = " + temp_out_str);

            ClassTest test_class_1 = new ClassTest();
            ClassTest test_class_2 = new ClassTest(10,"Joy",30000);

            //showClassTest(test_class_1,"test_class_1");
            //showClassTest(test_class_2,"test_class_2");
            

        }

        private void showClassTest(ClassTest target_class,string class_name)
        {
            string class_str = "ClassTest " + class_name + " some infos\nname = " + target_class.name + "\nage = " + target_class.age.ToString() + "\nmoney = " + target_class.money.ToString() + "\n";
            MessageBox.Show(class_str);
        }

        private string outTest(int count,int price,out string total_price)
        {
            int total_cost = count * price;
            string res = total_cost.ToString();

            //total_price = total_price;
            total_price = (total_cost + 520).ToString();

            return res;
        }

        private string refTest(int count, int price, ref string total_price)
        {
            int total_cost = count * price;
            string res = total_cost.ToString();

            //total_price = total_price;
            MessageBox.Show("In refTest total_price = " + total_price);

            return res;
        }
    }
}
