namespace MyJetWallet.BitGo
{
    public class BitGoClient : IBitGoClient
    {
        public BitGoClient(string accessTokenMainnet, string accessTokenTestnet)
        {
            MainNet = new BitGoApi(accessTokenMainnet, BitGoNetwork.Main);
            TestNet = new BitGoApi(accessTokenTestnet, BitGoNetwork.Test);
        }
        
        public BitGoClient(
            string accessTokenMainnet, string apiRootUrlMainnet,
            string accessTokenTestnet, string apiRootUrlTestnet)
        {
            MainNet = new BitGoApi(accessTokenMainnet, apiRootUrlMainnet);
            TestNet = new BitGoApi(accessTokenTestnet, apiRootUrlTestnet);
        }
        
        public IBitGoApi MainNet { get; }
        public IBitGoApi TestNet { get; }
    }
}