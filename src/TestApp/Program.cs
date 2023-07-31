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
        private static string walletID = "648e02967df42d00070d7da7288629a9";
        private static string walletPassphrase = "4Yy.KeX3fmBCZn4Dp2T3wiD486*7Jsf@DbWciaJ!2AqGKBJRKzHiC3*qoqLW9M6ffqo.Uh49QErLxx6vnuQt4GBRe!qPyx-P6H_E";
        private static string accessToken = "v2x6f3ff89fb3179e7575f6b16f756bc423cd5eb99f367d30967ff72188d6718d69";
        private static string apiUrl = "https://10.114.16.3";

        static async Task Main(string[] args)
        {


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

            if (string.IsNullOrEmpty(walletPassphrase))
            {
                Console.WriteLine("WalletPassphrase_1 is empty. Please setup env variable");
                return;
            }

            IBitGoApi api = new BitGoApi(accessToken, apiUrl);

            try
            {
                await TestWithdrawMany(api);
            }
            catch (BitGoErrorException ex)
            {
                Console.WriteLine("BitGoErrorException");
                Console.WriteLine(JsonSerializer.Serialize(ex.Error));
                Console.WriteLine(ex);
            }
        }

        static async Task TestAddresses(IBitGoApi api, string coin, string walletId, string label = null)
        {
            Console.Clear();

            var index = 1;

            var addressList = await api.GetAddressesAsync(coin, walletId, limit: 5);

            var count = addressList.Data.TotalAddressCount;
            Console.WriteLine($"Address list ({count}):");

            foreach (var address in addressList.Data.Addresses)
            {
                Console.WriteLine($"[{index}]{address.AddressId}|{address.Label}|{address.Address}|{address.Chain}");
                index++;
            }

            while (!string.IsNullOrEmpty(addressList.Data.NextBatchPrevId))
            {
                addressList = await api.GetAddressesAsync(coin, walletId, limit: 5, prevId: addressList.Data.NextBatchPrevId);
                foreach (var address in addressList.Data.Addresses)
                {
                    Console.WriteLine($"[{index}]{address.AddressId}|{address.Label}|{address.Address}|{address.Chain}");
                    index++;
                }
            }

            Console.WriteLine("---------");



            label ??= $"test:{count + 1}";


            var existAddress = await api.GetAddressesAsync(coin, walletId, labelContains: label, limit: 50);
            if (existAddress.Data.TotalAddressCount == 0)
            {
                var newAddress = await api.CreateAddressAsync(coin, walletId, label);
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

        static async Task TestGetTransfer(IBitGoApi api, string coin, string walletId)
        {
            Console.Clear();

            var transferList = await api.GetTransfersAsync(coin, walletId, limit: 5);

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
                transferList = await api.GetTransfersAsync(coin, walletId, limit: 5, prevId: transferList.Data.NextBatchPrevId);

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

        static async Task TestGetTransferById(IBitGoApi api, string coin, string walletId, string transferId)
        {
            Console.Clear();

            var tr = await api.TryGetTransferAsync(coin, walletId, transferId);
            Console.WriteLine(JsonSerializer.Serialize(tr, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task TestEnterpise(IBitGoApi api, string coin, string enterprise)
        {
            Console.Clear();

            var feeBalance = await api.GetEnterpriseFeeWalletBalanceAsync(enterprise, coin);
            Console.WriteLine(JsonSerializer.Serialize(feeBalance, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task TestPendingApproval(IBitGoApi api)
        {
            Console.Clear();

            var pendingApproval = await api.GetPendingApprovalAsync("6131357fa574d80006ae9d4a079710fd");
            Console.WriteLine(JsonSerializer.Serialize(pendingApproval, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task TestWithdrawMany(IBitGoApi api)
        {
            Console.Clear();
            List<SendManyRequestData.Recipient> recipients = new List<SendManyRequestData.Recipient>
            {
                new SendManyRequestData.Recipient() { Address = "0xe281d56240817bd3623bbab09ecad901e9b5bc36", Amount = "10000000000000000" },
                new SendManyRequestData.Recipient() { Address = "0xe281d56240817bd3623bbab09ecad901e9b5bc36", Amount = "10000000000000000" },
                new SendManyRequestData.Recipient() { Address = "0xe281d56240817bd3623bbab09ecad901e9b5bc36", Amount = "10000000000000000" }
            };

            var limits = await api.SendManyAsync(
                "eth",
                walletID,
                walletPassphrase,
                recipients
                );

            Console.WriteLine(JsonSerializer.Serialize(limits, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task TestSpendingLimits(IBitGoApi api)
        {
            Console.Clear();

            var limits = await api.GetSpendingLimitsForWalletAsync("teth", "60a6155d49219200062e5dd0291177df");
            Console.WriteLine(JsonSerializer.Serialize(limits, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task TestExpress(IBitGoApi api, string coin, string fromWalletId, string toWalletId, string toUser, string amount)
        {
            Console.Clear();

            ((BitGoApi)api).ThrowThenErrorResponse = false;

            var ping = await api.PingExpressAsync();
            Console.WriteLine($"Ping result: {ping.Data.Status}");

            //var address = await client.CreateAddressAsync(coin, toWalletId, toUser);
            //var addr = address.Data.Address;

            var address = await api.GetAddressesAsync(coin, toWalletId, labelContains: toUser);
            if (address.Data.Addresses.Length != 1)
            {
                Console.WriteLine($"[ERROR] User {toUser} on the wallet {toWalletId} has {address.Data.Addresses.Length} addresses");
                return;
            }

            var addr = address.Data.Addresses.Last().Address;

            var verifyResult = await api.VerifyAddressAsync(coin, addr);
            Console.WriteLine($"Address [{addr}] verify: {verifyResult.Data.IsValid}");

            var sid = $"st_{DateTime.UtcNow:O}";


            var sendResult = await api.SendCoinsAsync(coin, fromWalletId, walletPassphrase, amount: amount, address: addr, sequenceId: sid);
            Console.WriteLine($"Send coin. Pending Approval: {sendResult.Data.IsRequireApproval}, Tx: {sendResult.Data.Transfer.TxId}");
            Console.WriteLine($"rid: {sendResult.Data.Transfer.TransferId}");
            Console.WriteLine(JsonConvert.SerializeObject(sendResult.Data, Formatting.Indented));


            sendResult = await api.SendCoinsAsync(coin, fromWalletId, walletPassphrase, amount: amount, address: addr, sequenceId: sid);
            Console.WriteLine(JsonConvert.SerializeObject(sendResult, Formatting.Indented));

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task TestExpress2(IBitGoApi api, string coin, string fromWalletId, string toWalletId, string toUser, string amount)
        {
            Console.Clear();

            var ping = await api.PingExpressAsync();
            Console.WriteLine($"Ping result: {ping.Data.Status}");

            //var address = await client.CreateAddressAsync(coin, toWalletId, toUser);
            //var addr = address.Data.Address;

            var address = await api.GetAddressesAsync(coin, toWalletId, labelContains: toUser);
            if (address.Data.Addresses.Length != 1)
            {
                Console.WriteLine($"[ERROR] User {toUser} on the wallet {toWalletId} has {address.Data.Addresses.Length} addresses");
                return;
            }

            var addr = address.Data.Addresses.Last().Address;

            var verifyResult = await api.VerifyAddressAsync(coin, addr);
            Console.WriteLine($"Address [{addr}] verify: {verifyResult.Data.IsValid}");

            var sid = $"st_{DateTime.UtcNow:O}";


            var request = new SendCoinsRequestData()
            {
                WalletPassphrase = walletPassphrase,
                Address = addr,
                Amount = amount,
                SequenceId = sid,
            };


            var sendResult = await api.SendCoinsAsync(coin, fromWalletId, request);
            Console.WriteLine($"Send coin. Pending Approval: {sendResult.Data.IsRequireApproval}, Tx: {sendResult.Data.Transfer.TxId}");
            Console.WriteLine($"rid: {sendResult.Data.Transfer.TransferId}");

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }

        static async Task ApplyWebhook(IBitGoApi api)
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
                var webhooks = await api.ListWebhooksAsync(walletInfo.Value[0], walletInfo.Key);
                Console.WriteLine(JsonSerializer.Serialize(webhooks, new JsonSerializerOptions() { WriteIndented = true }));

                foreach (var dataWebhook in webhooks.Data.Webhooks)
                {
                    var remove = await api.RemoveWebhookAsync(walletInfo.Value[0], walletInfo.Key,
                        dataWebhook.Type, dataWebhook.Url, dataWebhook.Id);
                    Console.WriteLine(JsonSerializer.Serialize(remove, new JsonSerializerOptions() { WriteIndented = true }));
                    var add = await api.AddWebhookAsync(walletInfo.Value[0], walletInfo.Key,
                        dataWebhook.Type, dataWebhook.AllToken, dataWebhook.Url, dataWebhook.Label, 1, false);
                    Console.WriteLine(JsonSerializer.Serialize(remove, new JsonSerializerOptions() { WriteIndented = true }));
                }

                webhooks = await api.ListWebhooksAsync(walletInfo.Value[0], walletInfo.Key);
                Console.WriteLine(JsonSerializer.Serialize(webhooks, new JsonSerializerOptions() { WriteIndented = true }));
            }

            Console.WriteLine();
            Console.WriteLine("Press to continue");
            Console.ReadLine();
        }
    }
}
