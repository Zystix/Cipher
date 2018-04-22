using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Cipher
{
    public partial class Form1 : Form
    {

        long p = 0;
        long n = 0;
        long q = 0;
        long _e = 0;

        Random seed = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "";
            char[] splitMessage = txtInput.Text.ToCharArray();


            Engine.eMessage = Engine.encrypt(Engine.convertToNum(splitMessage), n, _e);

            for (int i = 0; i < Engine.eMessage.Count; i++)
            {
                txtOutput.Text += Engine.eMessage[i] + " ";
            }
        }

        private void btnGenKeys_Click(object sender, EventArgs e)
        {
            n = 0;
            while (n < 100)
            {
                p = Engine.primeNumbers[seed.Next(Engine.primeNumbers.Length - 1)];
                //Console.WriteLine("p = " + p);


                q = Engine.primeNumbers[seed.Next(Engine.primeNumbers.Length - 1)];
                //Console.WriteLine("q = " + q);

                n = p * q;
            }

            long z = (p - 1) * (q - 1);

            _e = Engine.GenEKey(z);

            long d = Engine.GenDKey(_e, z);

            txtKeyD.Text = Convert.ToString(d);
            txtKeyN.Text = Convert.ToString(n);
            txtKeyE.Text = Convert.ToString(_e);

        }
    }
}
