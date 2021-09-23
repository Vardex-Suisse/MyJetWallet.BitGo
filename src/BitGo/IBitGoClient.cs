namespace MyJetWallet.BitGo
{
    public interface IBitGoClient
    {
        IBitGoApi MainNet { get; }
        
        IBitGoApi TestNet { get; }

        IBitGoApi Get(bool isMainNet);
    }
}