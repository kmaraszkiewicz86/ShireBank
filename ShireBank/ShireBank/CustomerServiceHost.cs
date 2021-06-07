using SharedInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ShireBank
{
    [ServiceBehavior(
      ConcurrencyMode = ConcurrencyMode.Multiple,
      InstanceContextMode = InstanceContextMode.Single,
      IncludeExceptionDetailInFaults = true
      )]
    public class CustomerServiceHost : ICustomerInterface
    {
        public uint? OpenAccount( string firstName, string lastName, float debtLimit )
        {
            throw new NotImplementedException();
        }

        public float Withdraw( uint account, float amount )
        {
            throw new NotImplementedException();
        }

        public void Deposit( uint account, float amount )
        {
            throw new NotImplementedException();
        }

        public string GetHistory( uint account )
        {
            throw new NotImplementedException();
        }

        public bool CloseAccount( uint account )
        {
            throw new NotImplementedException();
        }
    }
}
