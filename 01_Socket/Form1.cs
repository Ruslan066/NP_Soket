using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

namespace _01_Socket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Send(object sender, EventArgs e)
        {
            // Куда я иду - адресс узла
            // 207.46.197.32 - Microsoft
            //IPAddress ip = IPAddress.Parse("91.215.152.86");
            IPAddress ip = IPAddress.Parse(txtIP.Text);

            // В какую дверь (порт) я иду на этой машине
            IPEndPoint ep = new IPEndPoint(ip, 80); // 80 - http, 443 - https

            // Гдездо для соединения определенного типа
            Socket s = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.IP
                );

            try
            {
                // Установить соединение с узлом
                s.Connect(ep);
                if (s.Connected)
                {
                    // Послать комманду
                    String strSend = "GET\r\n\r\n";
                    // String strSend = @"GET / HTTP/1.1 \r\n Host: ninydev.com \r\n";
                    s.Send(System.Text.Encoding.ASCII.GetBytes(strSend));

                    byte[] buffer = new byte[1024];
                    int len;
                    do
                    {
                        len = s.Receive(buffer);
                        txtResult.Text += System.Text.Encoding.ASCII.GetString(buffer, 0, len);
                    } while (len > 0);
                } else
                {
                    MessageBox.Show(" No Connect ");
                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                s.Shutdown(SocketShutdown.Both);
                s.Close();
            }


        }
    }
}
