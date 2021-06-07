using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedInterface
{
    public class Constants
    {
        public const string BankBaseAddress = "http://localhost:6999";
        public const string ServiceName = "ShireBank";

        public static string FullBankAddress { get { return BankBaseAddress + "/" + ServiceName; } }
    } 
}
