using System;
using System.Threading;
using CustomerTest.Services;
using Grpc.Net.Client;
using SharedInterface;

namespace CustomerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (GrpcChannel channel = GrpcChannel.ForAddress(Constants.FullBankAddress, new GrpcChannelOptions
            {
                DisposeHttpClient = true
                
            }))
            {
                ManualResetEvent[] endOfWorkEvents = { new ManualResetEvent(false), new ManualResetEvent(false), new ManualResetEvent(false) };

                var historyPrintLock = new object();

                // Customer 1
                new Thread(async () =>
                   {
                       var channel = GrpcChannel.ForAddress(Constants.FullBankAddress, new GrpcChannelOptions
                       {
                           DisposeHttpClient = true

                       });

                       var customer = new CustomerService(channel);

                       Thread.Sleep(TimeSpan.FromSeconds(10));

                       var accountId = await customer.OpenAccount("Henrietta", "Baggins", 100.0f);
                       if (accountId == null)
                       {
                           throw new Exception("Failed to open account");
                       }

                       await customer.Deposit(accountId.Value, 500.0f);

                       Thread.Sleep(TimeSpan.FromSeconds(10));

                       await customer.Deposit(accountId.Value, 500.0f);
                       await customer.Deposit(accountId.Value, 1000.0f);

                       if (2000.0f != await customer.Withdraw(accountId.Value, 2000.0f))
                       {
                           throw new Exception("Can't withdraw a valid amount");
                       }

                       lock (historyPrintLock)
                       {
                           Console.WriteLine("=== Customer 1 ===");
                           Console.Write(customer.GetHistory(accountId.Value));
                       }

                       if (!await customer.CloseAccount(accountId.Value))
                       {
                           throw new Exception("Failed to close account"); ;
                       }

                       endOfWorkEvents[0].Set();
                   }).Start();

                // Customer 2
                new Thread(async () =>
               {
                   var channel = GrpcChannel.ForAddress(Constants.FullBankAddress, new GrpcChannelOptions
                   {
                       DisposeHttpClient = true

                   });

                   var customer = new CustomerService(channel);

                   var accountId = await customer.OpenAccount("Barbara", "Tuk", 50.0f);
                   if (accountId == null)
                   {
                       throw new Exception("Failed to open account");
                   }

                   if ((await customer.OpenAccount("Barbara", "Tuk", 500.0f)) != null)
                   {
                       throw new Exception("Opened account for the same name twice!");
                   }

                   if (50.0f != await customer.Withdraw(accountId.Value, 2000.0f))
                   {
                       throw new Exception("Can only borrow up to debit limit only");
                   }

                   Thread.Sleep(TimeSpan.FromSeconds(10));

                   if (await customer.CloseAccount(accountId.Value))
                   {
                       throw new Exception("Can't close the account with outstanding debt");
                   }

                   await customer.Deposit(accountId.Value, 100.0f);
                   if (await customer.CloseAccount(accountId.Value))
                   {
                       throw new Exception("Can't close the account before clearing all funds");
                   }

                   if (50.0f != await customer.Withdraw(accountId.Value, 50.0f))
                   {
                       throw new Exception("Can't withdraw a valid amount");
                   }

                   lock (historyPrintLock)
                   {
                       Console.WriteLine("=== Customer 2 ===");
                       Console.Write(customer.GetHistory(accountId.Value));
                   }

                   if (!await customer.CloseAccount(accountId.Value))
                   {
                       throw new Exception("Failed to close account"); ;
                   }

                   endOfWorkEvents[1].Set();
               }).Start();


                // Customer 3
                new Thread(async () =>
               {
                   var channel = GrpcChannel.ForAddress(Constants.FullBankAddress, new GrpcChannelOptions
                   {
                       DisposeHttpClient = true

                   });

                   var customer = new CustomerService(channel);

                   var accountId = await customer.OpenAccount("Gandalf", "Grey", 10000.0f);
                   if (accountId == null)
                   {
                       throw new Exception("Failed to open account");
                   }

                   var toProcess = 200;
                   var resetEvent = new ManualResetEvent(false);

                   for (var i = 0; i < 100; i++)
                   {
                       ThreadPool.QueueUserWorkItem(async (stateInfo) =>
                       {
                           if (await customer.Withdraw(accountId.Value, 10.0f) != 10.0f)
                           {
                               throw new Exception("Can't withdraw a valid amount!");
                           }
                           if (Interlocked.Decrement(ref toProcess) == 0)
                               resetEvent.Set();
                       });
                   }

                   for (var i = 0; i < 100; i++)
                   {
                       ThreadPool.QueueUserWorkItem(async (stateInfo) =>
                           {
                               await customer.Deposit(accountId.Value, 10.0f);
                               if (Interlocked.Decrement(ref toProcess) == 0)
                                   resetEvent.Set();
                           });
                   }

                   Thread.Sleep(TimeSpan.FromSeconds(10));

                   resetEvent.WaitOne();

                   lock (historyPrintLock)
                   {
                       Console.WriteLine("=== Customer 3 ===");
                       Console.Write(customer.GetHistory(accountId.Value));
                   }

                   if (!await customer.CloseAccount(accountId.Value))
                   {
                       throw new Exception("Failed to close account"); ;
                   }

                   endOfWorkEvents[2].Set();
               }).Start();

                WaitHandle.WaitAll(endOfWorkEvents);
            }
            Console.ReadKey();
        }
    }
}
