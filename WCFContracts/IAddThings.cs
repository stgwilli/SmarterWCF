using System.ServiceModel;

namespace WCFContracts
{
    [ServiceContract]
    public interface IAddThings
    {
        [OperationContract]
        AddResult Add(AddRequest request);
    }
}
