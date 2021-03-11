using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MyJetWallet.BitGo;
using MyJetWallet.BitGo.Models;
using MyJetWallet.BitGo.Models.Address;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TestApp
{
    class Program
    {
        private static string _walletPassphrase1;

        static async Task Main(string[] args)
        {
            var accessToken = Environment.GetEnvironmentVariable("AccessToken");
            var apiUrl = Environment.GetEnvironmentVariable("ApiUrl");
            _walletPassphrase1 = Environment.GetEnvironmentVariable("WalletPassphrase_1");

            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("AccessToken is empty. Please setup env variable");
                return;
            }

            if (string.IsNullOrEmpty(apiUrl))
            {
                Console.WriteLine("ApiUrl is empty. Please setup env variable");
                return;
            }

            if (string.IsNullOrEmpty(_walletPassphrase1))
            {
                Console.WriteLine("WalletPassphrase_1 is empty. Please setup env variable");
                return;
            }

            IBitGoClient client = new BitGoClient(accessToken, apiUrl);

            try
            {

                //await TestAddresses(client, "txlm", "601176c94b46f40446749cb183f843c0");
                //await TestAddresses(client, "txlm", "6048c3e46fd304026642e95b6a28f976");
                await TestGetTransfer(client, "txlm", "601176c94b46f40446749cb183f843c0");
                //await TestExpress(client, "txlm", "601176c94b46f40446749cb183f843c0", "6048c3e46fd304026642e95b6a28f976", "test:2");

            }
            catch (BitGoErrorException ex)
            {
                Console.WriteLine("BitGoErrorException");
                Console.WriteLine(JsonSerializer.Serialize(ex.Error));
                Console.WriteLine(ex);
            }
        }

        static async Task TestAddresses(IBitGoClient client, string coin, string walletId)
        {
            Console.Clear();

            var index = 1;

            var addressList =  await client.GetAddressesAsync(coin, walletId, limit: 5);

            var count = addressList.Data.TotalAddressCount;
            Console.WriteLine($"Address list ({count}):");

            foreach (var address in addressList.Data.Addresses)
            {
                Console.WriteLine($"[{index}]{address.AddressId}|{address.Label}|{address.Address}|{address.Chain}");
                index++;
            }

            while (!string.IsNullOrEmpty(addressList.Data.NextBatchPrevId))
            {
                addressList = await client.GetAddressesAsync(coin, walletId, limit: 5, prevId: addressList.Data.NextBatchPrevId);
                foreach (var address in addressList.Data.Addresses)
                {
                    Console.WriteLine($"[{index}]{address.AddressId}|{address.Label}|{address.Address}|{address.Chain}");
                    index++;
                }
            }

            Console.WriteLine("---------");

            var label = $"test:{count + 1}";
            var newAddress = await client.CreateAddressAsync(coin, walletId, 0, label, null, false);
            Console.WriteLine("New address:");
            Console.WriteLine($"[{index}]{newAddress.Data.AddressId}|{newAddress.Data.Label}|{newAddress.Data.Address}");

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task TestGetTransfer(IBitGoClient client, string coin, string walletId)
        {
            Console.Clear();

            var transferList = await client.GetTransfersAsync(coin, walletId, limit: 5);

            Console.WriteLine("Transfer List:");
            
            foreach (var transfer in transferList.Data.Transfers)
            {
                var label = transfer.Entries.FirstOrDefault(e => e.Value > 0)?.Label;
                Console.WriteLine($"{transfer.Type}|{transfer.TransferId}|{transfer.BaseValueString}|{transfer.Coin}|{label}|{transfer.SequenceId}");
            }

            while (!string.IsNullOrEmpty(transferList.Data.NextBatchPrevId))
            {
                transferList = await client.GetTransfersAsync(coin, walletId, limit: 5, prevId: transferList.Data.NextBatchPrevId);

                foreach (var transfer in transferList.Data.Transfers)
                {
                    var label = transfer.Entries.FirstOrDefault(e => e.Value > 0)?.Label;
                    Console.WriteLine($"{transfer.Type}|{transfer.TransferId}|{transfer.BaseValueString}|{transfer.Coin}|{label}|{transfer.SequenceId}");
                }
            }

            Console.WriteLine();
            var tr = await client.TryGetTransferAsync(coin, walletId, "60117704aa058f0006f7c2d8414f1b13");
            Console.WriteLine($"{tr.Type}|{tr.TransferId}|{tr.BaseValueString}|{tr.Coin}|{tr.SequenceId}");

            tr = await client.TryGetTransferAsync(coin, walletId, "60117704aa058f0006f7c2d8414f1b22");
            if (tr == null)
            {
                Console.WriteLine("Transfer do not exist");
            }

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task TestExpress(IBitGoClient client, string coin, string fromWalletId, string toWalletId, string toUser)
        {
            Console.Clear();

            var ping = await client.PingExpressAsync();
            Console.WriteLine($"Ping result: {ping.Data.Status}");

            var address = await client.GetAddressesAsync(coin, toWalletId, labelContains: toUser);
            if (address.Data.Addresses.Length != 1)
            {
                Console.WriteLine($"[ERROR] User {toUser} on the wallet {toWalletId} has {address.Data.Addresses.Length} addresses");
                return;
            }

            var addr = address.Data.Addresses.First().Address;

            var verifyResult = await client.VerifyAddressAsync(coin, addr);
            Console.WriteLine($"Address [{addr}] verify: {verifyResult.Data.IsValid}");

            var sid = $"st_{DateTime.UtcNow:O}";

            var sendResult = await client.SendCoinsAsync(coin, fromWalletId, _walletPassphrase1, amount: "1000000", address: addr, sequenceId: sid);
            Console.WriteLine($"Send coin. Pending Approval: {sendResult.Data.IsRequireApproval}, Tx: {sendResult.Data.Transfer.TxId}");
            




            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }
    }
}
