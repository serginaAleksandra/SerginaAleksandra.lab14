using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
//using System.Security.Cryptography;

namespace lab13
{
    public partial class Form1 : Form
    {

        Random rnd = new Random();

        double[] pl = new double[8];

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch sWatch = new Stopwatch();

            double n = 1000;
            double M = Convert.ToDouble(textBox1.Text); ;
            double D = Convert.ToDouble(textBox2.Text);
            double[] Statistics = new double[8];
            double[] Frequency = new double[8];
            double Ex = 0;
            double Dx = 0;
            int i = 0;

            sWatch.Start();
            while (i < n)
            {
                double a_sum = SUMMING();
                Statistics[getIndex(a_sum)]++;
                Ex += a_sum;
                Dx += Math.Pow(a_sum, 2);
                i++;
            }
            sWatch.Stop();

            label8.Text = "Time: " + sWatch.ElapsedMilliseconds.ToString() + "ms";
            Ex *= 1 / n;
            Dx *= 1 / n;
            Dx -= Math.Pow(Ex, 2);
            Ex = Math.Abs(Ex);

            double err_abs = Math.Abs(Ex - 0);
            double err_var = Math.Abs(Dx - 1);

            label6.Text = "Average: " + Math.Round(Ex, 2) + " (error = " + Math.Round(err_abs, 2) + ")";
            label5.Text = "Variance: " + Math.Round(Dx, 2) + " (error = " + Math.Round(err_var, 2) + ")";

            Initpl(M, D);
            double chi_squared = 0;
            double _chi = 0;
            double l = -4;
            for (int j = 0; j < 8; j++)
            {
                double p = (l + 1 - l) * pl[j];


                _chi += Math.Pow(Statistics[j], 2) / (n * p);
                l++;
            }

            chi_squared = _chi - n;


            label4.Text = "Chi-squared: " + Math.Round(chi_squared, 2) + " < 20.09 is";
            for (int j = 0; j < 8; j++)
            {
                Frequency[j] = Statistics[j] / n;

                chart1.Series[0].Points.AddXY(j + 1, Frequency[j]);
                chart1.Series[1].Points.AddXY(j + 1, pl[j]);
            }

            label7.Visible = true;
            if (chi_squared < 20.09)
            {
                label7.Text = "true";
                label7.ForeColor = Color.Green;
            }
            else
            {
                label7.Text = "false";
                label7.ForeColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stopwatch sWatch = new Stopwatch();

            double n = 1000;
            double M = Convert.ToDouble(textBox1.Text);
            double D = Convert.ToDouble(textBox2.Text);
            double[] Statistics = new double[8];
            double[] Frequency = new double[8];
            double Ex = 0;
            double Dx = 0;
            int i = 0;
            sWatch.Start();
            while (i < n)
            {
                double a_sum = SummingPLUS();
                Statistics[getIndex(a_sum)]++;
                Ex += a_sum;
                Dx += Math.Pow(a_sum, 2);
                i++;
            }
            sWatch.Stop();

            label8.Text = "Time: " + sWatch.ElapsedMilliseconds.ToString() + "ms";
            Ex *= 1 / n;
            Dx *= 1 / n;
            Dx -= Math.Pow(Ex, 2);
            Ex = Math.Abs(Ex);
            for (int j = 0; j < 8; j++)
            {
                Frequency[j] = Statistics[j] / n;

                chart1.Series[0].Points.AddXY(j + 1, Frequency[j]);
                chart1.Series[1].Points.AddXY(j + 1, pl[j]);
            }

            double err_abs = Math.Abs(Ex - 0);
            double err_var = Math.Abs(Dx - 1);

            label6.Text = "Average: " + Math.Round(Ex, 2) + " (error = " + Math.Round(err_abs, 2) + ")";
            label5.Text = "Variance: " + Math.Round(Dx, 2) + " (error = " + Math.Round(err_var, 2) + ")";

            Initpl(M, D);
            double chi_squared = 0;
            double _chi = 0;
            double l = -4;
            for (int j = 0; j < 8; j++)
            {
                double p = (l + 1 - l) * pl[j];


                _chi += Math.Pow(Statistics[j], 2) / (n * p);
                l++;
            }

            chi_squared = _chi - n;


            label4.Text = "Chi-squared: " + Math.Round(chi_squared, 2) + " < 20,09 is";


            label7.Visible = true;
            if (chi_squared < 20.09)
            {
                label7.Text = "true";
                label7.ForeColor = Color.Green;
            }
            else
            {
                label7.Text = "false";
                label7.ForeColor = Color.Red;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stopwatch sWatch = new Stopwatch();

            double n = 1000;
            double M = Convert.ToDouble(textBox1.Text);
            double D = Convert.ToDouble(textBox2.Text);
            double[] Statistics = new double[8];
            double[] Frequency = new double[8];
            double Ex = 0;
            double Dx = 0;
            int i = 0;
            sWatch.Start();
            while (i < n)
            {
                double a_sum = BoxMuller();
                Statistics[getIndex(a_sum)]++;
                Ex += a_sum;
                Dx += Math.Pow(a_sum, 2);
                i++;
            }
            sWatch.Stop();

            label8.Text = "Time: " + sWatch.ElapsedMilliseconds.ToString() + "ms";
            Ex *= 1 / n;
            Dx *= 1 / n;
            Dx -= Math.Pow(Ex, 2);
            Ex = Math.Abs(Ex);

            for (int j = 0; j < 8; j++)
            {
                Frequency[j] = Statistics[j] / n;

                chart1.Series[0].Points.AddXY(j + 1, Frequency[j]);
                chart1.Series[1].Points.AddXY(j + 1, pl[j]);
            }

            double err_abs = Math.Abs(Ex - 0);
            double err_var = Math.Abs(Dx - 1);

            label6.Text = "Average: " + Math.Round(Ex, 2) + " (error = " + Math.Round(err_abs, 2) + ")";
            label5.Text = "Variance: " + Math.Round(Dx, 2) + " (error = " + Math.Round(err_var, 2) + ")";

            Initpl(M, D);
            double chi_squared = 0;
            double _chi = 0;
            double l = -4;
            for (int j = 0; j < 8; j++)
            {
                double p = (l + 1 - l) * pl[j];


                _chi += Math.Pow(Statistics[j], 2) / (n * p);
                l++;
            }

            chi_squared = _chi - n;


            label4.Text = "Chi-squared: " + Math.Round(chi_squared, 2) + " < 20.09 is";


            label7.Visible = true;
            if (chi_squared < 20.09)
            {
                label7.Text = "true";
                label7.ForeColor = Color.Green;
            }
            else
            {
                label7.Text = "false";
                label7.ForeColor = Color.Red;
            }
        }

        private void Initpl(double M, double D)
        {

            pl[0] = 1 / (Math.Sqrt(D) * Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.E, -(Math.Pow(-3.5 - M, 2) / (2 * D)));
            pl[1] = 1 / (Math.Sqrt(D) * Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.E, -(Math.Pow(-2.5 - M, 2) / (2 * D)));
            pl[2] = 1 / (Math.Sqrt(D) * Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.E, -(Math.Pow(-1.5 - M, 2) / (2 * D)));
            pl[3] = 1 / (Math.Sqrt(D) * Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.E, -(Math.Pow(-0.5 - M, 2) / (2 * D)));
            pl[4] = 1 / (Math.Sqrt(D) * Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.E, -(Math.Pow(0.5 - M, 2) / (2 * D)));
            pl[5] = 1 / (Math.Sqrt(D) * Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.E, -(Math.Pow(1.5 - M, 2) / (2 * D)));
            pl[6] = 1 / (Math.Sqrt(D) * Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.E, -(Math.Pow(2.5 - M, 2) / (2 * D)));
            pl[7] = 1 / (Math.Sqrt(D) * Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.E, -(Math.Pow(3.5 - M, 2) / (2 * D)));
        }
        private double SUMMING()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            double a_sum = 0;

            int j;

            for (int i = 0; i < 12; i++)
            {
                a_sum += rnd.NextDouble();

            }
            a_sum -= 6;

            return a_sum;

        }

        private double SummingPLUS()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            double sup = SUMMING();

            double res = sup + (1 / 240 * (Math.Pow(sup, 3) - 3 * sup));

            return res;
        }

        private double BoxMuller()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            double a = rnd.NextDouble();

            double res = Math.Sqrt(-2 * Math.Log(a)) * Math.Cos(2 * Math.PI * rnd.NextDouble());

            return res;
        }



        private int getIndex(double a_sum)
        {

            if (a_sum >= -5 && a_sum < -3)
            {
                return 0;

            }
            else if (a_sum >= -3 && a_sum < -2)
            {
                return 1;

            }
            else if (a_sum >= -2 && a_sum < -1)
            {
                return 2;

            }
            else if (a_sum >= -1 && a_sum < 0)
            {

                return 3;
            }
            else if (a_sum >= 0 && a_sum < 1)
            {

                return 4;
            }
            else if (a_sum >= 1 && a_sum < 2)
            {

                return 5;
            }
            else if (a_sum >= 2 && a_sum < 3)
            {

                return 6;
            }
            else if (a_sum >= 3 && a_sum < 5)
            {

                return 7;
            }


            return -1;
        }
    }
}
