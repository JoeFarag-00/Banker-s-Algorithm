using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        #region variables

        static int m;
        static int n ;
        static int num_req , state , num,p_num;
        static bool safe;
        //static string print;
       
        //static int x;
        static string alloc_string;


        public static List<int> safe_seq = new List<int>();


        static List<List<int>> allocation = new List<List<int>>();
        static List<List<int>> max = new List<List<int>>();
        static List<List<int>> need = new List<List<int>>();
        static List<int> available = new List<int>();
        static List<int> work = new List<int>();
        static List<bool> finish = new List<bool>();
        static List<int> req = new List<int>();

        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            n = int.Parse(textBox1.Text);
            Console.WriteLine(n);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            m = int.Parse(textBox2.Text);
            Console.WriteLine(m);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // ********************************* Allocation Matrix ***********************************
            alloc_string = textBox3.Text;
            string[] matrix = Regex.Split(alloc_string, Environment.NewLine);
            foreach (string i in matrix)
            {
                string[] vector_string = i.Split(' '); // vector = row
                int[] vector_int = Array.ConvertAll(vector_string, int.Parse);
                List<int> vector = vector_int.ToList();
                allocation.Add(vector);

                Console.WriteLine(String.Join(" ; ", vector));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //   ******************************************* MAX matrix********************

            alloc_string = textBox4.Text;
            string[] matrix = Regex.Split(alloc_string, Environment.NewLine);
            foreach (string i in matrix)
            {
                string[] vector_string = i.Split(' '); // vector = row
                int[] vector_int = Array.ConvertAll(vector_string, int.Parse);
                List<int> vector = vector_int.ToList();
                max.Add(vector);

                Console.WriteLine(String.Join(" ; ", vector));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            alloc_string = textBox5.Text;
            string[] matrix = Regex.Split(alloc_string, Environment.NewLine);
            foreach (string i in matrix)
            {
                string[] vector_string = i.Split(' '); // vector = row
                int[] vector_int = Array.ConvertAll(vector_string, int.Parse);
                 available = vector_int.ToList();
                 work = vector_int.ToList();
                Console.WriteLine(String.Join(" ; ", work));
            }

            // **************** need ********

            for (int i = 0; i < n; i++)
            {
                List<int> v1 = new List<int>();
                for (int j = 0; j < m; j++)
                {
                    num = max[i][j] - allocation[i][j];
                    v1.Add(num);
                }
                need.Add(v1);
            }

            for (int i = 0; i < n; i++)
            {
                string row = "";
                for (int j = 0; j < m; j++)
                {
                    row += need[i][j] + " ";
                }
                print_fn(row , i);
            }
            for (int i = 0; i < n; i++)
            {
                finish.Add(false);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            state = int.Parse(textBox6.Text);
            Console.WriteLine(state);

            if (state == 1)
            {
                safe = isSafe(work, finish, need, allocation);
                if (safe)
                {

                    label4.Visible = true;
                    label4.Font = new Font("Calibri", 10);
                    label4.Text = "Yes , Safe state <";
                    //label4.Location = new Point(97, 469);
                    label4.AutoSize = true;
                    label4.Font = new Font("Calibri", 20);
                    label4.ForeColor = Color.Black;
                    string print = ""; 
                    for (int i = 0; i < n; i++)
                    {
                        if (i != (n - 1))
                        {
                            print += "p" + safe_seq[i] + " , ";
                        }
                        else
                        {
                            print += "p" + safe_seq[i] + " > ";
                        }
                    }
                    label7.Visible = true;
                    label7.Font = new Font("Calibri", 10);
                    state = 0;
                    label7.Text = print;
                    // label2.Location = new Point(120, 250);
                    label7.AutoSize = true;
                    label7.Font = new Font("Calibri", 20);
                    label7.ForeColor = Color.Black;


                    this.Controls.Add(label7);
                    // label2.Location = new Point(300, 250);
                    label7.AutoSize = true;
                }
                else
                {
                    MessageBox.Show("NO");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            state = int.Parse(textBox7.Text);
            Console.WriteLine(state);

        }

        private void button10_Click(object sender, EventArgs e)
        {

            if (state == 1)
            {
                state = 0;

                p_num = int.Parse(textBox8.Text);
                Console.WriteLine(p_num);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            alloc_string = textBox9.Text;
            string[] matrix = Regex.Split(alloc_string, Environment.NewLine);
            foreach (string i in matrix)
            {
                string[] vector_string = i.Split(' '); // vector = row
                int[] vector_int = Array.ConvertAll(vector_string, int.Parse);
                req = vector_int.ToList();
                Console.WriteLine(String.Join(" ; ", req));
            }

            while (p_num > (n - 1))
            {
                MessageBox.Show("number of process must be less than");
                p_num = int.Parse(textBox8.Text);
                Console.WriteLine(p_num);
            }
            request(allocation, max, need, available, work, finish, p_num , req);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        void print_fn (string row , int x)
        {
            Label label = new Label();
            label.Text = row;
            label.Location = new Point(798, 148 + x*20 + x*20);
            label.AutoSize = true;
            label.Font = new Font("Calibri", 10);
            label.ForeColor = Color.Black;
            label.Visible = true;



            this.Controls.Add(label);

        }


        bool isSafe(List<int> work, List<bool> finish, List<List<int>> need, List<List<int>> allocation)
        {
            int counter = 0, flag, f = 0;
            List<int> safe_s = new List<int>();
            while (counter < n)
            {
                flag = 0;
                for (int i = 0; i < n; i++)
                {
                    f = 0;
                    if (!finish[i])
                    {
                        for (int j = 0; j < m; j++)
                        {
                            if (need[i][j] > work[j])
                            {
                                f = 1;
                                break;
                            }
                        }

                        if (f == 0)
                        {
                            for (int k = 0; k < m; k++)
                            {
                                work[k] += allocation[i][k];
                            }
                            safe_seq.Add(i);
                            safe_s.Add(i);
                            counter++;

                            finish[i] = true;
                            flag = 1;
                        }
                    }
                }
                if (flag == 0)
                {
                    return false;
                }
            }

            return true;
        }


        int request(List<List<int>> allocation, List<List<int>> max, List<List<int>> need, List<int> available, List<int> work, List<bool> finish, int p_num, List<int> req)
        {
            // int num_req;
            bool safe1;

            for (int i = 0; i < m; i++)
            {
                if (req[i] > need[p_num][i])
                {
                    MessageBox.Show("Error! , process has exceeded its maximum claim");
                    return 0;
                }
            }

            for (int i = 0; i < m; i++)
            {
                if (req[i] > available[i])
                {
                    MessageBox.Show("process must wait, since resources are not available");
                    return 0;
                }
            }

            for (int i = 0; i < m; i++)
            {
                available[i] -= req[i];
                allocation[p_num][i] += req[i];
                need[p_num][i] -= req[i];
                work[i] = available[i];
            }
            finish.Clear();
            for (int i = 0; i < n; i++)
            {
                finish.Add(false);
            }
            safe1 = isSafe(work, finish, need, allocation);
            if (safe1)
            {
                label13.Visible = true;
                label13.Text = "Yes request can be granted with safe state , Safe state < P " + p_num + "req";
                //label4.Location = new Point(97, 469);
                label13.AutoSize = true;
                label13.Font = new Font("Calibri", 20);
                label13.ForeColor = Color.Black;

                string print = "";
                for (int i = 0; i < n; i++)
                {
                    if (i != (n - 1))
                    {
                        print += "p" + safe_seq[i] + " , ";
                    }
                    else
                    {
                        print += "p" + safe_seq[i] + " > ";
                    }
                }
                label12.Visible = true;
                state = 0;
                label12.Text = print;
                // label2.Location = new Point(120, 250);
                label12.AutoSize = true;
                label12.Font = new Font("Calibri", 20);
                label12.ForeColor = Color.Black;


                this.Controls.Add(label12);
                // label2.Location = new Point(300, 250);
                label12.AutoSize = true;
            }
            else
            {
                MessageBox.Show("No , request can't be granted");
            }
            return 0;
        }
    }
}
