using SharedInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ShireBank
{
    class Program
    {
        static void Main( string[] args )
        {
            var serviceHost = new ServiceHost( new CustomerServiceHost(), new[] { new Uri( Constants.BankBaseAddress ) } );
            serviceHost.AddServiceEndpoint( typeof( ICustomerInterface ), new BasicHttpBinding(), Constants.ServiceName );
            serviceHost.Open();
            Console.ReadKey();
        }
    }
}
