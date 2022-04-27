using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Threading;
using System.IO.Ports;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace POV_Globe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            getAvailableComPorts();
            RotateAngleTextBox.Text = "5693";
        }

        private String[] ports;
        private SerialPort port;
        private bool isConnected = false;
        private bool motorOn = false;

        void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
            Debug.WriteLine("a");
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
                Debug.WriteLine("rado");
            }
            if (ports.Length != 0)
            {
                comboBox1.SelectedItem = ports[0];
            }
        }

        private void connectController()
        {
            if (!isConnected)
            {
                string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
                port = new SerialPort(selectedPort, 921600, Parity.None, 8, StopBits.One);
                port.Open();
                isConnected = true;
                button1.Text = "Connected";
            }
        }

        /*
        private void sendPicture()
        {
            var Image = (Bitmap)pictureBox1.Image;

            int ARRAY_SIZE = (144 * 72) * 3 + 1 + 1; // RGB values + start byte + end byte

            byte[] inline = new byte[ARRAY_SIZE]; 

            inline[0] = (byte)'I';

            for (int i = 0; i < Image.Width; i++)
            {
                for (int j = 0; j < Image.Height; j++)
                {
                    inline[1 + (i * Image.Height * 3) + (j * 3) + 0] = Image.GetPixel(i, j).G;
                    inline[1 + (i * Image.Height * 3) + (j * 3) + 1] = Image.GetPixel(i, j).B;
                    inline[1 + (i * Image.Height * 3) + (j * 3) + 2] = Image.GetPixel(i, j).R;
                }
            }

            inline[ARRAY_SIZE - 1] = (byte)'\n';

            if (isConnected)
            {
                port.Write(inline, 0, ARRAY_SIZE);
            }
        }
        */





        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectController();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            int ARRAY_SIZE = 2; // RGB values + start byte + end byte

            byte[] inline = new byte[ARRAY_SIZE];

            if (motorOn)
            {
                inline[0] = (byte)'L';
            }
            else
            {
                inline[0] = (byte)'L';
            }

            inline[ARRAY_SIZE - 1] = (byte)'\n';

            if (isConnected)
            {
                port.Write(inline, 0, ARRAY_SIZE);
                Debug.WriteLine(inline[0]);
                motorOn = !motorOn;
                if (motorOn)
                {
                    button7.Text = "STOP";
                }
                else
                {
                    button7.Text = "START";
                }
            }
        }

        private void RotateRightButton_Click(object sender, EventArgs e)
        {
            int ARRAY_SIZE = 4;

            byte[] inline = new byte[ARRAY_SIZE];

            if (motorOn)
            {
                inline[0] = (byte)'R';
                inline[1] = (byte)'\n';
                inline[2] = (byte)'R';
            }
            else
            {
                inline[0] = (byte)'R';
                inline[1] = (byte)'\n';
                inline[2] = (byte)'R';
            }

            inline[ARRAY_SIZE - 1] = (byte)'\n';

            if (isConnected)
            {
                port.Write(inline, 0, ARRAY_SIZE);
            }
        }

        private void RotateLeftButton_Click(object sender, EventArgs e)
        {
            int ARRAY_SIZE = 4;

            byte[] inline = new byte[ARRAY_SIZE];

            if (motorOn)
            {
                inline[0] = (byte)'R';
                inline[1] = (byte)'\n';
                inline[2] = (byte)'L';
            }
            else
            {
                inline[0] = (byte)'R';
                inline[1] = (byte)'\n';
                inline[2] = (byte)'L';
            }

            inline[ARRAY_SIZE - 1] = (byte)'\n';

            if (isConnected)
            {
                port.Write(inline, 0, ARRAY_SIZE);
                Debug.WriteLine(inline[0]);
                Debug.WriteLine(inline[1]);
                Debug.WriteLine(inline[2]);
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            int ARRAY_SIZE = 2;

            byte[] inline = new byte[ARRAY_SIZE];

            if (motorOn)
            {
                inline[0] = (byte)'S';
            }
            else
            {
                inline[0] = (byte)'S';
            }

            inline[ARRAY_SIZE - 1] = (byte)'\n';

            if (isConnected)
            {
                port.Write(inline, 0, ARRAY_SIZE);
            }
        }

        private void AngleRotateButton_Click(object sender, EventArgs e)
        {
            int angle = Int32.Parse(RotateAngleTextBox.Text);
            int thousands = (int)Math.Floor(angle / 1000f);
            int hundreads = (int)Math.Floor(angle % 1000 / 100f);
            int tens = (int)Math.Floor(angle % 100 / 10f);
            int ones = (int)(angle % 10);

            int ARRAY_SIZE = 7;

            byte[] inline = new byte[ARRAY_SIZE];

            if (motorOn)
            {
                inline[0] = (byte)'R';
                inline[1] = (byte)'\n';
                inline[2] = (byte)thousands;
                inline[3] = (byte)hundreads;
                inline[4] = (byte)tens;
                inline[5] = (byte)ones;

            }
            else
            {
                inline[0] = (byte)'R';
                inline[1] = (byte)'\n';
                inline[2] = (byte)thousands;
                inline[3] = (byte)hundreads;
                inline[4] = (byte)tens;
                inline[5] = (byte)ones;
            }

            inline[ARRAY_SIZE - 1] = (byte)'\n';
            if (isConnected)
            {
                port.Write(inline, 0, ARRAY_SIZE);
                Debug.WriteLine(inline[0]);
            }
        }
    }
}
