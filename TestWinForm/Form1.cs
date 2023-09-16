using MyJetWallet.BitGo;
using System.Text;

namespace TestWinForm
{
    public partial class Form1 : Form
    {
        private static string walletID = "648e02967df42d00070d7da7288629a9";
        private static string walletPassphrase = "4Yy.KeX3fmBCZn4Dp2T3wiD486*7Jsf@DbWciaJ!2AqGKBJRKzHiC3*qoqLW9M6ffqo.Uh49QErLxx6vnuQt4GBRe!qPyx-P6H_E";
        private static string AccessToken = "v2x6f3ff89fb3179e7575f6b16f756bc423cd5eb99f367d30967ff72188d6718d69";
        private static string apiUrl = "https://10.114.16.3";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            string URL = $"{apiUrl}/eth/wallet/{walletID}";
            var sddd = GetAsync(URL);
            string result = sddd.GetAwaiter().GetResult();
           */
            IBitGoApi api = new BitGoApi("v2x1ac252aa292247d16e933b6b526ef77ba12c1726f38dfdd71092233bd4ca704c", "https://10.114.16.3");
            var walletResponse = api.GetWallet("eth", "6479a604b6025100076b42eafac7e0aa").Data;

            Console.WriteLine(walletResponse);
        }


        private async Task<string> GetAsync(string url)
        {
            var client = new HttpClient();
            
            var response = await client.GetAsync($"{url}");
            return await response.Content.ReadAsStringAsync();

        }
    }
}