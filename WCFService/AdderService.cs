using System.Linq;
using WCFContracts;

namespace WCFService
{
    public class AdderService : IAddThings
    {
        public AddResult Add(AddRequest request)
        {
            return new AddResult
            {
                Result = request.FirstNumber + request.SecondNumber
            };
        }
    }
}
