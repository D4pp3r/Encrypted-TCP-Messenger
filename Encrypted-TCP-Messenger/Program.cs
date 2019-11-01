using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Numerics;

namespace EncryptedMessenger
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter a message: ");
                string input = Console.ReadLine();
                Console.WriteLine("Enter Your Encryption Key: ");
                string key = Console.ReadLine();
                Console.WriteLine("Enter an IP Address: ");
                string ip = Console.ReadLine();
                input = input.Replace("~", "84").Replace("`", "83").Replace("?", "82").Replace("/", "81").Replace("=", "80").Replace("+", "79").Replace("_", "78").Replace("-", "77").Replace(")", "76").Replace("(", "75").Replace("*", "74").Replace("&", "73").Replace("^", "72").Replace("%", "71").Replace("$", "70").Replace("#", "69").Replace("@", "68").Replace("!", "67").Replace(";", "66").Replace(",", "65").Replace(".", "64").Replace("Z", "63").Replace("Y", "62").Replace("X", "61").Replace("W", "60").Replace("V", "59").Replace("U", "58").Replace("T", "57").Replace("S", "56").Replace("R", "55").Replace("Q", "54").Replace("P", "53").Replace("O", "52").Replace("N", "51").Replace("M", "50").Replace("L", "49").Replace("K", "48").Replace("J", "47").Replace("I", "46").Replace("H", "45").Replace("G", "44").Replace("F", "43").Replace("E", "42").Replace("D", "41").Replace("C", "40").Replace("B", "39").Replace("A", "38").Replace(" ", "37").Replace("a", "11").Replace("b", "12").Replace("c", "13").Replace("d", "14").Replace("e", "15").Replace("f", "16").Replace("g", "17").Replace("h", "18").Replace("i", "19").Replace("j", "20").Replace("k", "21").Replace("l", "22").Replace("m", "23").Replace("n", "24").Replace("o", "25").Replace("p", "26").Replace("q", "27").Replace("r", "28").Replace("s", "29").Replace("t", "30").Replace("u", "31").Replace("v", "32").Replace("w", "33").Replace("x", "34").Replace("y", "35").Replace("z", "36");
                key = key.Replace("~", "84").Replace("`", "83").Replace("?", "82").Replace("/", "81").Replace("=", "80").Replace("+", "79").Replace("_", "78").Replace("-", "77").Replace(")", "76").Replace("(", "75").Replace("*", "74").Replace("&", "73").Replace("^", "72").Replace("%", "71").Replace("$", "70").Replace("#", "69").Replace("@", "68").Replace("!", "67").Replace(";", "66").Replace(",", "65").Replace(".", "64").Replace("Z", "63").Replace("Y", "62").Replace("X", "61").Replace("W", "60").Replace("V", "59").Replace("U", "58").Replace("T", "57").Replace("S", "56").Replace("R", "55").Replace("Q", "54").Replace("P", "53").Replace("O", "52").Replace("N", "51").Replace("M", "50").Replace("L", "49").Replace("K", "48").Replace("J", "47").Replace("I", "46").Replace("H", "45").Replace("G", "44").Replace("F", "43").Replace("E", "42").Replace("D", "41").Replace("C", "40").Replace("B", "39").Replace("A", "38").Replace(" ", "37").Replace("a", "11").Replace("b", "12").Replace("c", "13").Replace("d", "14").Replace("e", "15").Replace("f", "16").Replace("g", "17").Replace("h", "18").Replace("i", "19").Replace("j", "20").Replace("k", "21").Replace("l", "22").Replace("m", "23").Replace("n", "24").Replace("o", "25").Replace("p", "26").Replace("q", "27").Replace("r", "28").Replace("s", "29").Replace("t", "30").Replace("u", "31").Replace("v", "32").Replace("w", "33").Replace("x", "34").Replace("y", "35").Replace("z", "36");
                Connect(ip, input, key);
                Console.ReadKey();
                Console.Clear();
            }
        }
        static void Connect(String server, String message, String key)
        {
            try
            {
                //BigInteger messageNumber = BigInteger.Parse(message);
                BigInteger keyNumber = BigInteger.Parse(key);
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 53;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(BigInteger.Parse(doEncrypt(message, keyNumber)).ToString());

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                // data = new Byte[256];

                // String to store the response ASCII representation.
                //   String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                /*Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);*/

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static string doEncrypt(string message, BigInteger key)
        {
            try
            {
                Random rand = new Random();

                BigInteger msgNumber = System.Numerics.BigInteger.Parse(message);
                int random = rand.Next(10000, 99999);
                BigInteger salt = (random ^ key) * (key ^ random);
                //Console.WriteLine("Random Number: " + random);
                BigInteger fibinachi = Fibonacci(random);
                //Console.WriteLine("Fibinacci: " + fibinachi);
                //for(int x = 0; x<=random; x++)
                //{
                msgNumber = msgNumber * (fibinachi ^ (salt * fibinachi));
                //}
                string message2 = random + msgNumber.ToString();
                //int msg2Number = int.Parse(message2);
                return message2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                //rTxtBoxLog.Text += "\nFailed. " + ex.Message;
                //MessageBox.Show(ex.Message);
                return "Failed. " + ex.Message;
            }
        }

        public static BigInteger Fibonacci(int len)
        {
            BigInteger a = 0, b = 1, c = 0;

            for (int i = 2; i < len; i++)
            {
                c = a + b;
                a = b;
                b = c;
            }

            return c;
        }
    }
}




