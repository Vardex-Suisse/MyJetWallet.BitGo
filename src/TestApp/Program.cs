using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MyJetWallet.BitGo;
using MyJetWallet.BitGo.Models;
using MyJetWallet.BitGo.Models.Address;
using MyJetWallet.BitGo.Models.Express;
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

                //await TestAddresses(client, "talgo", "604f7e965095850076f7d697fcea9995", "jetwallet|-|alex|-|SP-alex");

                //await TestAddresses(client, "tbtc", "6054ba9ca9cc0e0024a867a7d8b401b2", "jetwallet|-|alex|-|SP-alex");
                //await TestAddresses(client, "tbtc", "6054ba9ca9cc0e0024a867a7d8b401b2");

                //await TestAddresses(client, "txrp", "60584aaded0090000628ce59c01f3a5e");

                //await TestAddresses(client, "xlm", "6059baa1db5c7e02866ab97d17e557b6");

                //await TestGetTransferById(client, "teos", "60584dcc6f5d31001d5a59371aeeb60a", "605b162bc6cfd60006c880f8f140e5f9");

                // await TestGetTransfer(client, "teos", "60584dcc6f5d31001d5a59371aeeb60a");
                // await ApplyWebhook(client);
                // await TestEnterpise(client, "teth", "605d56ec87a7fd0006730d335d16b81b");
                await TestPendingApproval(client);
                //await TestGetTransfer(client, "txlm", "601176c94b46f40446749cb183f843c0");
                //await TestGetTransfer(client, "txlm", "6048c3e46fd304026642e95b6a28f976");

                //await TestGetTransfer(client, "txrp", "60584aaded0090000628ce59c01f3a5e");

                //await TestGetTransfer(client, "talgo", "604f7e965095850076f7d697fcea9995");
                //await TestGetTransfer(client, "txrp", "604f8990e32c2f000600f5411c68dacd");



                //await TestExpress(client, "txlm", "601176c94b46f40446749cb183f843c0", "6048c3e46fd304026642e95b6a28f976", "jetwallet|-|alex|-|SP-alex", "100000000");

                //await TestExpress(client, "tbtc", "6013e7b3d11c3704c6b47cf6191e74a8", "604f5afa9ca16d000682de35465fc6e8", "jetwallet|-|alex|-|SP-alex", "10000");

                //await TestExpress(client, "txrp", "60584aaded0090000628ce59c01f3a5e", "60584aaded0090000628ce59c01f3a5e", "jetwallet|-|alex|-|SP-alex", "11000000");


                //transfer":"605b162bc6cfd60006c880f8f140e5f9","coin":"teos","type":"transfer","state":"confirmed","wallet":"60584dcc6f5d31001d5a59371aeeb60a"

            }
            catch (BitGoErrorException ex)
            {
                Console.WriteLine("BitGoErrorException");
                Console.WriteLine(JsonSerializer.Serialize(ex.Error));
                Console.WriteLine(ex);
            }
        }

        static async Task TestAddresses(IBitGoClient client, string coin, string walletId, string label = null)
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

            

            label ??= $"test:{count + 1}";


            var existAddress = await client.GetAddressesAsync(coin, walletId, labelContains: label, limit: 50);
            if (existAddress.Data.TotalAddressCount == 0)
            {
                var newAddress = await client.CreateAddressAsync(coin, walletId, label);
                Console.WriteLine("New address:");
                Console.WriteLine($"[{index}]{newAddress.Data.AddressId}|{newAddress.Data.Label}|{newAddress.Data.Address}");
            }
            else
            {
                var newAddress = existAddress.Data.Addresses.Last();
                Console.WriteLine("Existing address:");
                Console.WriteLine($"[{index}]{newAddress.AddressId}|{newAddress.Label}|{newAddress.Address}");
            }
            
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
                var amountSend = transfer.Entries.FirstOrDefault(e => e.Value < 0)?.Value;
                var amountReceive = transfer.Entries.FirstOrDefault(e => e.Value > 0)?.Value;
                Console.WriteLine($"{transfer.Type}|{transfer.TransferId}|{transfer.BaseValueString}|{transfer.Coin}|{label}|{transfer.SequenceId}|{amountSend}|{amountReceive}");
            }

            while (!string.IsNullOrEmpty(transferList.Data.NextBatchPrevId))
            {
                transferList = await client.GetTransfersAsync(coin, walletId, limit: 5, prevId: transferList.Data.NextBatchPrevId);

                foreach (var transfer in transferList.Data.Transfers)
                {
                    var label = transfer.Entries.FirstOrDefault(e => e.Value > 0)?.Label;
                    var amountSend = transfer.Entries.FirstOrDefault(e => e.Value < 0)?.Value;
                    var amountReceive = transfer.Entries.FirstOrDefault(e => e.Value > 0)?.Value;
                    Console.WriteLine($"{transfer.Type}|{transfer.TransferId}|{transfer.BaseValueString}|{transfer.Coin}|{label}|{transfer.SequenceId}|{amountSend}|{amountReceive}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task TestGetTransferById(IBitGoClient client, string coin, string walletId, string transferId)
        {
            Console.Clear();

            var tr = await client.TryGetTransferAsync(coin, walletId, transferId);
            Console.WriteLine(JsonSerializer.Serialize(tr, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task TestEnterpise(IBitGoClient client, string coin, string enterprise)
        {
            Console.Clear();

            var feeBalance = await client.GetEnterpriseFeeWalletBalanceAsync(enterprise, coin);
            Console.WriteLine(JsonSerializer.Serialize(feeBalance, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task TestPendingApproval(IBitGoClient client)
        {
            Console.Clear();

            var pendingApproval = await client.GetPendingApprovalAsync("6131357fa574d80006ae9d4a079710fd");
            Console.WriteLine(JsonSerializer.Serialize(pendingApproval, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task TestExpress(IBitGoClient client, string coin, string fromWalletId, string toWalletId, string toUser, string amount)
        {
            Console.Clear();

            ((BitGoClient)client).ThrowThenErrorResponse = false;

            var ping = await client.PingExpressAsync();
            Console.WriteLine($"Ping result: {ping.Data.Status}");

            //var address = await client.CreateAddressAsync(coin, toWalletId, toUser);
            //var addr = address.Data.Address;

            var address = await client.GetAddressesAsync(coin, toWalletId, labelContains: toUser);
            if (address.Data.Addresses.Length != 1)
            {
                Console.WriteLine($"[ERROR] User {toUser} on the wallet {toWalletId} has {address.Data.Addresses.Length} addresses");
                return;
            }

            var addr = address.Data.Addresses.Last().Address;

            var verifyResult = await client.VerifyAddressAsync(coin, addr);
            Console.WriteLine($"Address [{addr}] verify: {verifyResult.Data.IsValid}");

            var sid = $"st_{DateTime.UtcNow:O}";


            var sendResult = await client.SendCoinsAsync(coin, fromWalletId, _walletPassphrase1, amount: amount, address: addr, sequenceId: sid);
            Console.WriteLine($"Send coin. Pending Approval: {sendResult.Data.IsRequireApproval}, Tx: {sendResult.Data.Transfer.TxId}");
            Console.WriteLine($"rid: {sendResult.Data.Transfer.TransferId}");
            Console.WriteLine(JsonConvert.SerializeObject(sendResult.Data, Formatting.Indented));


            sendResult = await client.SendCoinsAsync(coin, fromWalletId, _walletPassphrase1, amount: amount, address: addr, sequenceId: sid);
            Console.WriteLine(JsonConvert.SerializeObject(sendResult, Formatting.Indented));





            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task TestExpress2(IBitGoClient client, string coin, string fromWalletId, string toWalletId, string toUser, string amount)
        {
            Console.Clear();

            var ping = await client.PingExpressAsync();
            Console.WriteLine($"Ping result: {ping.Data.Status}");

            //var address = await client.CreateAddressAsync(coin, toWalletId, toUser);
            //var addr = address.Data.Address;

            var address = await client.GetAddressesAsync(coin, toWalletId, labelContains: toUser);
            if (address.Data.Addresses.Length != 1)
            {
                Console.WriteLine($"[ERROR] User {toUser} on the wallet {toWalletId} has {address.Data.Addresses.Length} addresses");
                return;
            }

            var addr = address.Data.Addresses.Last().Address;

            var verifyResult = await client.VerifyAddressAsync(coin, addr);
            Console.WriteLine($"Address [{addr}] verify: {verifyResult.Data.IsValid}");

            var sid = $"st_{DateTime.UtcNow:O}";


            var request = new SendCoinsRequestData()
            {
                WalletPassphrase = _walletPassphrase1,
                Address = addr,
                Amount = amount,
                SequenceId = sid,
            };


            var sendResult = await client.SendCoinsAsync(coin, fromWalletId, request);
            Console.WriteLine($"Send coin. Pending Approval: {sendResult.Data.IsRequireApproval}, Tx: {sendResult.Data.Transfer.TxId}");
            Console.WriteLine($"rid: {sendResult.Data.Transfer.TransferId}");

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task ApplyWebhook(IBitGoClient client)
        {
            var wallets = new Dictionary<string, string[]>()
            {
                {"608be313dafba70006b7daa94a40d640", new []{"tbtc", "POC-Bitcoin-2"}},
                // {"6054bc003dc1af002b0d54bf5b552f28", new []{"txlm", "POC-Stellar-5"}},
                // {"6054be73b765620006aa87311f43bd47", new []{"TLTC", "POC-Litecoin-2"}},
                // {"60584aaded0090000628ce59c01f3a5e", new []{"TXRP", "POC-Ripple-3"}},
                // {"60584b79fd3e0500669e2cf9654d726b", new []{"TBCASH", "POC-BitcoinCash-3"}},
                // {"60584becbc3e2600240548d78e61c02b", new []{"TALGO", "POC-ALGO-3"}},
                // {"60584dcc6f5d31001d5a59371aeeb60a", new []{"TEOS", "POC-EOS-3"}}
            };
            
            Console.Clear();
            
            foreach (var walletInfo in wallets)
            {
                var webhooks = await client.ListWebhooksAsync(walletInfo.Value[0], walletInfo.Key);
                Console.WriteLine(JsonSerializer.Serialize(webhooks, new JsonSerializerOptions() { WriteIndented = true }));
                
                foreach (var dataWebhook in webhooks.Data.Webhooks)
                {
                    var remove = await client.RemoveWebhookAsync(walletInfo.Value[0], walletInfo.Key,
                        dataWebhook.Type, dataWebhook.Url, dataWebhook.Id);
                    Console.WriteLine(JsonSerializer.Serialize(remove, new JsonSerializerOptions() { WriteIndented = true }));
                    var add = await client.AddWebhookAsync(walletInfo.Value[0], walletInfo.Key,
                        dataWebhook.Type, dataWebhook.AllToken, dataWebhook.Url, dataWebhook.Label, 1, false);
                    Console.WriteLine(JsonSerializer.Serialize(remove, new JsonSerializerOptions() { WriteIndented = true }));
                }
                
                webhooks = await client.ListWebhooksAsync(walletInfo.Value[0], walletInfo.Key);
                Console.WriteLine(JsonSerializer.Serialize(webhooks, new JsonSerializerOptions() { WriteIndented = true }));
            }
            
            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }
    }
}
