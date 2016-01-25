using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimalityTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void test_button_Click(object sender, EventArgs e)
        {
            long candidateNumber = long.Parse(candidate_number_textbox.Text);
            long numberOfTests = long.Parse(number_of_tests_textbox.Text);
            result_textbox.Text = testPrime(candidateNumber, numberOfTests);
        }

        private string testPrime(long candidate_number, long number_of_tests) //overall it should be O(nlogn) including all of 
                                                                              //the calculated O() from below also 
                                                                              //space complexity should be O(n) because of garbage collecting
        {
            Random rand = new Random();
            ArrayList rands = new ArrayList();

            //Pick positive integers a1, a2, . . . , ak < N at random
            for (int i = 0; i < number_of_tests; i++)            //O(n) where n is the number of tests  * O(logn)  = O(nlogn) 
                                                                 //space complexity of O(n) for a for loop * modexp = O(nlogn)
                                                                 //because of garbage collecting
            {
                //generating a random number < N                    //O(logn) because you have to check to make sure the random 
                //number wasn't used before // space complexity O(logn)
                long num = rand.Next((Int32)candidate_number - 2)+1;
                //making sure the same random number hasn't been used before
                while (rands.Contains(num))
                {
                    num = rand.Next((Int32)candidate_number - 2)+1;
                }
                rands.Add(num);
                //1 mod x is always one, so we check. If it != 1 then it is composite
                if (modExp(num, candidate_number - 1, candidate_number) != 1)//O(logn) because y is divided by two after each call  
                                                                            // space complexity O(logn)
                {
                    return candidate_number + " is Composite";
                }
            }
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.PercentDecimalDigits = 15;
            //if modExp does not return 1 then it is prime a confidence of x^-k
            return candidate_number+" is prime with a probability of confidence "+
                (1-Math.Pow(2,-number_of_tests)).ToString("P", nfi);
        }
        //Part of fermats theorem    //O(logn) because y is divided by two after each call   //complexity of O(logn) because of recursion
        private long modExp(long x, long y, long N)
        {
            if(y == 0)
            {
                return 1;
            }
            long z = modExp(x, (y / 2), N);
            if (y%2 == 0)
            {
                return (long)z*z % N;
            }
            else
            {
                return (long)(x*z*z % N);
            }
        }
    }
}
;