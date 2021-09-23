namespace MyJetWallet.BitGo
{
    public class BitGoClient : IBitGoClient
    {
        private BitGoApi _main;
        private BitGoApi _test;
        
        public BitGoClient(string accessTokenMainnet, string accessTokenTestnet)
        {
            _main = new BitGoApi(accessTokenMainnet, BitGoNetwork.Main);
            _test = new BitGoApi(accessTokenTestnet, BitGoNetwork.Test);
        }
        
        public BitGoClient(
            string accessTokenMainnet, string apiRootUrlMainnet,
            string accessTokenTestnet, string apiRootUrlTestnet)
        {
            _main = new BitGoApi(accessTokenMainnet, apiRootUrlMainnet);
            _test = new BitGoApi(accessTokenTestnet, apiRootUrlTestnet);
        }
        
        public IBitGoApi MainNet => _main;
        public IBitGoApi TestNet => _test;

        public bool ThrowIfErrorResponse
        {
            get => _main.ThrowThenErrorResponse;
            set
            {
                _main.ThrowThenErrorResponse = value;
                _test.ThrowThenErrorResponse = value;
            }
        }
    }
}