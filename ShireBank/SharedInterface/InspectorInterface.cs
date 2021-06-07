using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SharedInterface
{
    [ServiceContract]
    class InspectorInterface
    {
        [OperationContract]
        string GetFullSummary();

        [OperationContract]
        void StartInspection();

        [OperationContract]
        void FinishInspection();
    }
}
