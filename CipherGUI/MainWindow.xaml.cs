using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace CipherGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        long p = 0;
        long n = 0;
        long q = 0;
        long _e = 0;
        long d = 0;

        public bool noSpaces = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGenerateKeys_Click(object sender, RoutedEventArgs e)
        {
                n = 0;
                while (n < 100)
                {
                    p = Engine.primeNumbers[Engine.seed.Next(Engine.primeNumbers.Length - 1)];

                    q = Engine.primeNumbers[Engine.seed.Next(Engine.primeNumbers.Length - 1)];

                    n = p * q;
                }

                long z = (p - 1) * (q - 1);

                _e = Engine.GenEKey(z);

                d = Engine.GenDKey(_e, z);

                txtKeyN.Text = Convert.ToString(n);
                txtKeyD.Text = Convert.ToString(d);
                txtKeyE.Text = Convert.ToString(_e);
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            if (txtKeyD.Text != "" && txtKeyE.Text != "" && txtKeyN.Text != "")
            {

                n = Convert.ToInt64(txtKeyN.Text);
                d = Convert.ToInt64(txtKeyD.Text);
                _e = Convert.ToInt64(txtKeyE.Text);

                txtOutput.Text = "";
                char[] splitMessage = txtInput.Text.ToCharArray();

                int messageLength = Engine.encrypt(Engine.convertToNum(splitMessage), n, _e).Count;

                pgrProgress.Maximum = messageLength;
                pgrProgress.Minimum = 0;
                pgrProgress.Value = 0;

                for (int i = 0; i < messageLength; i++)
                {
                    if (noSpaces)
                    {
                        txtOutput.Text += Engine.encrypt(Engine.convertToNum(splitMessage), n, _e)[i];
                    }

                    else
                    {


                        if (i + 1 == Engine.encrypt(Engine.convertToNum(splitMessage), n, _e).Count)
                        {
                            txtOutput.Text += Engine.encrypt(Engine.convertToNum(splitMessage), n, _e)[i];
                        }
                        else
                        {
                            txtOutput.Text += Engine.encrypt(Engine.convertToNum(splitMessage), n, _e)[i] + " ";
                        }

                    }

                    

                }

            }
            else
            {
                MessageBox.Show("Please Press Generate \nKeys Before Encrypting.");
            }
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            n = Convert.ToInt64(txtKeyN.Text);
            d = Convert.ToInt64(txtKeyD.Text);
            _e = Convert.ToInt64(txtKeyE.Text);
            List<BigInteger> encMessage = new List<BigInteger>();
            string[] numbers = txtInput.Text.Split(' ');
            for (int i = 0; i < txtInput.Text.Split(' ').Length; i++)
            {
                encMessage.Add(Convert.ToInt32(numbers[i]));
            }

            txtOutput.Text = Engine.decrypt(encMessage, n, d);
            
        }

        private void chkNoSpaces_Checked(object sender, RoutedEventArgs e)
        {
            noSpaces = true;
        }

        private void chkNoSpaces_Unchecked(object sender, RoutedEventArgs e)
        {
            noSpaces = false;
        }

        private void btnSwitch_Click(object sender, RoutedEventArgs e)
        {
            string input = txtInput.Text;
            string output = txtOutput.Text;

            txtOutput.Text = input;
            txtInput.Text = output;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text = "";
            txtOutput.Text = "";
        }

        private void btnCopyToClip_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtOutput.Text);
        }
    }
}
