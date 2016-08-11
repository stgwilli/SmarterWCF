using SmarterWCFClient;
using System;
using WCFContracts;

namespace WCFConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();

            var client = new ServiceChannelClientFactory().Build<IAddThings>();
            var result = client.Add(new AddRequest { FirstNumber = 6, SecondNumber = 6 });
            Console.WriteLine($"Result {result.Result}");

            Console.ReadLine();
        }
    }

    /*
    public class AdderServiceClient : ServiceChannelClient<IAddThings>, IAddThings
    {
        public AddResult Add(AddRequest request)
        {
            return InvokeMethod(x => x.Add(request));
        }
    }
    */
}
