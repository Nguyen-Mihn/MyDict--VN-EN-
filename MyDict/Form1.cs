using System;
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
                string url = "http://123.56.139.184/tudien/search.php" + "?tu=[" + ToUnicodeString(wordBox.Text) + "]";
                string result = getBase(url);
                processResult(result);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.L)
            {
                string url = "http://123.56.139.184/tudien/add.php" + "?tu=[" + ToUnicodeString(wordBox.Text.ToString())
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
                string url = "http://123.56.139.184/tudien/update.php?tu=[" + ToUnicodeString(wordBox.Text)
                 + "]&nghia=" + definitionBox.Text;
                string result = getBase(url);
                processResult(result);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.J)
            {
                string url = "http://123.56.139.184/tudien/delete.php?tu=[" + ToUnicodeString(wordBox.Text) + "]";
                string result = getBase(url);
                processResult(result);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.O) {
                string url = "http://123.56.139.184/tudien/reverse.php?nghia=" + wordBox.Text;
                string result = getBase(url);
                string pattern = @"\[.*?\]";
                Regex rg = new Regex(pattern);
                MatchCollection words = rg.Matches(result);
                //StringBuilder sb = new StringBuilder();
                for (int count=0; count<words.Count; count++)
                {
                    //sb.Append(words[count].Value);
                    string wordInHex = words[count].Value.Substring(1, words[count].Value.Length - 2);
                    string word = UnicodeToString(wordInHex);
                    result = result.Replace(wordInHex, word);
                }
                processResult(result);

            }
        }
        public string UnicodeToString(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;

            string temp = null;
            bool flag = false;

            int len = text.Length / 4;
            if (text.StartsWith("0x") || text.StartsWith("0X"))
            {
                len = text.Length / 6;//0x in Unicode string
                flag = true;
            }

            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                if (flag)
                    temp = text.Substring(i * 6, 6).Substring(2);
                else
                    temp = text.Substring(i * 4, 4);

                byte[] bytes = new byte[2];
                bytes[1] = byte.Parse(int.Parse(temp.Substring(0, 2), NumberStyles.HexNumber).ToString());
                bytes[0] = byte.Parse(int.Parse(temp.Substring(2, 2), NumberStyles.HexNumber).ToString());
                sb.Append(Encoding.Unicode.GetString(bytes));
            }
            return sb.ToString();
        }
        public string ToUnicodeString(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in str)
            {
                sb.Append(((int)c).ToString("X4"));
            }
            return sb.ToString();
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(ToUnicodeString(wordBox.Text));
            string url = "http://123.56.139.184/tudien/search.php" + "?tu=[" + ToUnicodeString(wordBox.Text) + "]";
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
            string url = "http://123.56.139.184/tudien/add.php" + "?tu=[" + ToUnicodeString(wordBox.Text.ToString()) 
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
            string url = "http://123.56.139.184/tudien/update.php?tu=[" + ToUnicodeString(wordBox.Text)
                + "]&nghia=" + definitionBox.Text;
            string result = getBase(url);
            processResult(result);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string url = "http://123.56.139.184/tudien/delete.php?tu=[" + ToUnicodeString(wordBox.Text) +"]";
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
