using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows;

namespace CipherGUI
{
    public class Engine
    {
        public static List<BigInteger> eMessage;
        public static long e = 0;
        public static Random seed = new Random();
        public static int[] primeNumbers = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 }; // 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };

        public static int[] convertToNum(char[] message)
        {
            /*
            *   Converts each character in an array to its corresponding letter acording to the rule of a = 1, b = 2, c = 3 etc. 
            *   Returning an array of integers in the order of the chars provided.
            */

            int[] convMessage = new int[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                convMessage[i] = GetPosNum(message[i]);
            }

            return convMessage;
        }

        public static List<BigInteger> encrypt(int[] message, long n, long e)
        {
            /*
             * encrypts the message, performing the algorith (message ^ e) % n to each set of numbers. I.e each letter.  
             * Utilises the BigInteger class for large keys.
             */
            List<BigInteger> encryptedmessage = new List<BigInteger>();

            for (int i = 0; i < message.Length; i++)
            {
                encryptedmessage.Add((BigInteger.Pow(new BigInteger(message[i]), Convert.ToInt32(e))) % n);
            }

            return encryptedmessage;
        }


        public static string decrypt(List<BigInteger> encMessage, long n, long d)
        {
            /*
             *  Runs the opposite algorith to the encrypt function to decrypt each set of numbers. (Encrypted message ^ d) % n
             *  It is then run through the GetPosChar function to the find the character that is associated with that number.
             *  the decrypted text is then returned as a single string. 
             *  
             *  I had to use the BigInteger class here as the number produced using the Math.Pow() method as used in the encrypt method were being truncated
             *  as the numbers were too big for the double type. And thus, the number returned was wrong and didn't decrypt properly.
             *  This took over an hour for me to work out and I was almost drawn to mass murder as the encrypt and decrypt methods
             *  are damn near identical but this one was not working. But what matters is that I worked it out in the end.
             */
            string _message = "";
            for (int i = 0; i < encMessage.Count; i++)
            {
                _message += GetPosChar((BigInteger.Pow(encMessage[i], Convert.ToInt32(d)) % n));
            }

            return _message;
        }


        public static bool commonFactors(int p, int q)
        {
            int count = 0;

            if (q > p)
            {
                for (int i = 1; i < q; i++)
                {
                    if ((q % i == 0) && (p % i == 0))
                    {
                        count++;
                    }
                }
            }
            else if (p > q)
            {
                for (int i = 1; i < p; i++)
                {
                    if ((p % i == 0) && (q % i == 0))
                    {
                        count++;
                    }
                }
            }
            else
            {
                count = 2;
            }

            if (count > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static long GenEKey(long z)
        {
            List<long> possibleLongs = new List<long>();
            for (int i = 1; i < 1000; i++)
            {
                if (commonFactors(i, (int)z) == false)
                {
                    if (possibleLongs.Contains(i) == false)
                    {
                        possibleLongs.Add(i);
                    }
                }
            }

            return possibleLongs[seed.Next(possibleLongs.Count)];
        }


        public static int GetPosNum(char letter)
        {
            /*
             * Gets the corresponding number from a letter. 
             */

            switch (letter)
            {
                case 'a':
                case 'A':
                    return 1;
                case 'b':
                case 'B':
                    return 2;
                case 'c':
                case 'C':
                    return 3;
                case 'd':
                case 'D':
                    return 4;
                case 'e':
                case 'E':
                    return 5;
                case 'f':
                case 'F':
                    return 6;
                case 'g':
                case 'G':
                    return 7;
                case 'h':
                case 'H':
                    return 8;
                case 'i':
                case 'I':
                    return 9;
                case 'j':
                case 'J':
                    return 10;
                case 'k':
                case 'K':
                    return 11;
                case 'l':
                case 'L':
                    return 12;
                case 'm':
                case 'M':
                    return 13;
                case 'n':
                case 'N':
                    return 14;
                case 'o':
                case 'O':
                    return 15;
                case 'p':
                case 'P':
                    return 16;
                case 'q':
                case 'Q':
                    return 17;
                case 'r':
                case 'R':
                    return 18;
                case 's':
                case 'S':
                    return 19;
                case 't':
                case 'T':
                    return 20;
                case 'u':
                case 'U':
                    return 21;
                case 'v':
                case 'V':
                    return 22;
                case 'w':
                case 'W':
                    return 23;
                case 'x':
                case 'X':
                    return 24;
                case 'y':
                case 'Y':
                    return 25;
                case 'z':
                case 'Z':
                    return 26;
                case ' ':
                    return 27;
                case '.':
                    return 28;
                case ',':
                    return 29;
                case '?':
                    return 30;
                case '!':
                    return 31;
                case '@':
                    return 32;
                case '#':
                    return 33;
                case '$':
                    return 34;
                case '^':
                    return 35;
                case '&':
                    return 36;
                case '*':
                    return 37;
                case '(':
                    return 38;
                case ')':
                    return 39;
                case '1':
                    return 40;
                case '2':
                    return 41;
                case '3':
                    return 42;
                case '4':
                    return 43;
                case '5':
                    return 44;
                case '6':
                    return 45;
                case '7':
                    return 46;
                case '8':
                    return 47;
                case '9':
                    return 48;
                case '0':
                    return 49;
                default:              
                    return 0;

            }
        }
        public static char GetPosChar(BigInteger number)
        {
            /*
             *  Gets the corresponding letter from a number, had to use the ToString method of the BigInterger class as simply switching
             *  the number arguement was causing errors, because number is an object not an int.
             *
             */

            switch (number.ToString())
            {
                case "1":
                    return 'a';
                case "2":
                    return 'b';
                case "3":
                    return 'c';
                case "4":
                    return 'd';
                case "5":
                    return 'e';
                case "6":
                    return 'f';
                case "7":
                    return 'g';
                case "8":
                    return 'h';
                case "9":
                    return 'i';
                case "10":
                    return 'j';
                case "11":
                    return 'k';
                case "12":
                    return 'l';
                case "13":
                    return 'm';
                case "14":
                    return 'n';
                case "15":
                    return 'o';
                case "16":
                    return 'p';
                case "17":
                    return 'q';
                case "18":
                    return 'r';
                case "19":
                    return 's';
                case "20":
                    return 't';
                case "21":
                    return 'u';
                case "22":
                    return 'v';
                case "23":
                    return 'w';
                case "24":
                    return 'x';
                case "25":
                    return 'y';
                case "26":
                    return 'z';
                case "27":
                    return ' ';
                case "28":
                    return '.';
                case "29":
                    return ',';
                case "30":
                    return '?';
                case "31":
                    return '!';
                case "32":
                    return '@';
                case "33":
                    return '#';
                case "34":
                    return '$';
                case "35":
                    return '^';
                case "36":
                    return '&';
                case "37":
                    return '*';
                case "38":
                    return '(';
                case "39":
                    return ')';
                case "40":
                    return '1';
                case "41":
                    return '2';
                case "42":
                    return '3';
                case "43":
                    return '4';
                case "44":
                    return '5';
                case "45":
                    return '6';
                case "46":
                    return '7';
                case "47":
                    return '8';
                case "48":
                    return '9';
                case "49":
                    return '0';
                default:
                    return '_';
            }
        }

        public static long GenDKey(long e, long z)
        {
            List<long> options = new List<long>();

            for (long i = 0; i < 10000; i++)
            {
                if (((e * i) - 1) % z == 0)
                {
                    options.Add(i);
                }

            }

            return options[seed.Next(options.Count)];
        }
        

    }
}