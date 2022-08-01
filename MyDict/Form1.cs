using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDict
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string url = "http://123.56.139.184/tudien/search.php" + "?tu=" + wordBox.Text.ToString();
            getBase(url);
        }
        private void getBase(string url)
        {
            //httpGet(url);
            try
            {

                var request = (HttpWebRequest)WebRequest.Create(url);
                WebProxy proxy = WebProxy.GetDefaultProxy();
                if (proxy.Address != null && proxy.Address.AbsoluteUri != string.Empty)
                {
                    request.Proxy = GlobalProxySelection.GetEmptyWebProxy();
                }
                request.Method = "GET";
                request.KeepAlive = false;
                request.Timeout = 10000;
                //解决远程连接不可用（503）问题
                request.Headers.Set("Pragma", "no-cache");
                //request.UserAgent = "Mozilla-Firefox-Spider(Wenanry)";
                request.UserAgent = "Little Verifier Process";
                var webResponse = request.GetResponse();
                var webStream = webResponse.GetResponseStream();
                var reader = new StreamReader(webStream);
                var data = reader.ReadToEnd();
                data = data.Replace("\n", "\n\t");
                definitionBox.Text = "";
                displayText("Result: \n\t", Color.Red);
                if (data.Contains('\n'))
                {
                    String[] items = data.Split('\n');
                    foreach (String item in items)
                    {
                        if (item.Contains(':'))
                        {
                            String[] result = item.Split(':');
                            displayText(result[0], Color.FromArgb(255, 255, 185, 0));
                            displayText(": ", Color.White);
                            displayText(result[1] + "\n", Color.FromArgb(255, 127, 186, 0));

                        }
                    }
                }
                else
                {
                    displayText(wordBox.Text, Color.FromArgb(255, 255, 185, 0));
                    displayText(": ", Color.White);
                    displayText(data + "\n", Color.FromArgb(255, 127, 186, 0));
                }
                
                
            }
            catch (Exception ex)
            {
            }
        }
        private void displayText(string text, Color color)
        {
            definitionBox.SelectionStart = definitionBox.TextLength;
            definitionBox.SelectionLength = 0;
            definitionBox.SelectionColor = color;
            definitionBox.AppendText(text);
        }
        static string r;
        static async Task httpGet(string url)
        {
            var client = new HttpClient();
            var result = await client.GetAsync(url);
            r = await result.Content.ReadAsStringAsync();
            Console.WriteLine(r); //
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string url = "http://123.56.139.184/tudien/add.php" + "?tu=" + wordBox.Text.ToString() + "&nghia=" + definitionBox.Text;
            getBase(url);
        }

        private void reverseButton_Click(object sender, EventArgs e)
        {
            string url = "http://123.56.139.184/tudien/reverse.php?nghia=" + wordBox.Text;
            getBase(url);
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            string url = "http://123.56.139.184/tudien/update.php?tu=" + wordBox.Text + "&nghia=" + definitionBox.Text;
            getBase(url);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

        }
    }
}
