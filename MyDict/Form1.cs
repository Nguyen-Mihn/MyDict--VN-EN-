﻿using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDict
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            
            wordBox.KeyUp += wordBox_KeyUp;

            instructionBox.GotFocus += giveUpFocus;
            instructionBox.Text = "Hot Keys:\n" +
                "\tEnter: Search\n" +
                "\tCtrl + L: Add\n" +
                "\tCtrl + C: Clear\n" +
                "\tCtrl + U: Update\n" +
                "\tCtrl + J: Delete\n" +
                "\tCtrl + O: Reverse\n";
        }
        private void giveUpFocus(object obj, EventArgs e)
        {
            wordBox.Focus();
        }
        private void wordBox_KeyUp(object obj, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string url = "http://123.56.139.184/tudien/search.php" + "?tu=[" + CharsetProcessor.ToUnicodeString(wordBox.Text) + "]";
                string result = getBase(url);
                processResult(result);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.L)
            {
                string url = "http://123.56.139.184/tudien/add.php" + "?tu=[" + CharsetProcessor.ToUnicodeString(wordBox.Text.ToString())
                + "]&nghia=" + definitionBox.Text;
                string result = getBase(url);
                processResult(result);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
            {
                definitionBox.Text = "";
                wordBox.Text = "";
                definitionBox.Focus();
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.U)
            {
                string url = "http://123.56.139.184/tudien/update.php?tu=[" + CharsetProcessor.ToUnicodeString(wordBox.Text)
                 + "]&nghia=" + definitionBox.Text;
                string result = getBase(url);
                processResult(result);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.J)
            {
                string url = "http://123.56.139.184/tudien/delete.php?tu=[" + CharsetProcessor.ToUnicodeString(wordBox.Text) + "]";
                string result = getBase(url);
                processResult(result);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.O) {
                string url = "http://123.56.139.184/tudien/reverse.php?nghia=" + wordBox.Text;
                string result = getBase(url);
                string pattern = @"\[.*?\]";
                Regex rg = new Regex(pattern);
                MatchCollection words = rg.Matches(result);
                for (int count=0; count<words.Count; count++)
                {
                    string wordInHex = words[count].Value.Substring(1, words[count].Value.Length - 2);
                    string tmp = string.Copy(wordInHex);
                    string word = CharsetProcessor.UnicodeToString(tmp);
                    var regex = new Regex(Regex.Escape(wordInHex));
                    result = regex.Replace(result, word, 1);
                }
                processResult(result);
            }
        }
       private void searchButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(ToUnicodeString(wordBox.Text));
            string url = "http://123.56.139.184/tudien/search.php" + "?tu=[" + CharsetProcessor.ToUnicodeString(wordBox.Text) + "]";
            string result = getBase(url);
            processResult(result);
        }
        private void processResult(String data)
        {
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
                        /*String[] result = item.Split(':');
                        displayText(result[0], Color.FromArgb(255, 255, 185, 0));
                        displayText(": ", Color.White);
                        displayText(result[1] + "\n", Color.FromArgb(255, 127, 186, 0));*/
                        string word = item.Substring(0, item.IndexOf(':'));
                        string definition = item.Substring(item.IndexOf(':'), item.Length - item.IndexOf(':'));
                        displayText(word, Color.FromArgb(255, 255, 185, 0));
                        displayText(": ", Color.White);
                        displayText(definition + "\n", Color.FromArgb(255, 127, 186, 0));
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
        private String getBase(string url)
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

                return data;
                
            }
            catch (Exception ex)
            {
                return "Failed";
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
            string url = "http://123.56.139.184/tudien/add.php" + "?tu=[" + CharsetProcessor.ToUnicodeString(wordBox.Text.ToString()) 
                + "]&nghia=" + definitionBox.Text;
            string result = getBase(url);
            processResult(result);
        }

        private void reverseButton_Click(object sender, EventArgs e)
        {
            string url = "http://123.56.139.184/tudien/reverse.php?nghia=" + wordBox.Text;
            string result = getBase(url);
            processResult(result);
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            string url = "http://123.56.139.184/tudien/update.php?tu=[" + CharsetProcessor.ToUnicodeString(wordBox.Text)
                + "]&nghia=" + definitionBox.Text;
            string result = getBase(url);
            processResult(result);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string url = "http://123.56.139.184/tudien/delete.php?tu=[" + CharsetProcessor.ToUnicodeString(wordBox.Text) +"]";
            string result = getBase(url);
            processResult(result);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            definitionBox.Text = "";
            wordBox.Text = "";
        }
    }
}
