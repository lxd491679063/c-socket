using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTestApp
{
    class ClassTest
    {
        public int age { get; private set; }
        public string name { get; set; }
        public int money { get; set; }

        public ClassTest():
            this(0,"normal",0)
        {
            
        }

        public ClassTest(int input_age,string input_name,int input_money)
        {
            age = input_age;
            name = input_name;
            money = input_money;
        }
    }
}
