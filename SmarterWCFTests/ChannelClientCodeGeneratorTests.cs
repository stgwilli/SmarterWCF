using SmarterWCFClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmarterWCFTests
{
    public class ChannelClientCodeGeneratorTests
    {
        [Fact]
        public void ShouldGenerateTheCorrectCodeBasedOnTheType()
        {
            var code = new ChannelClientCodeGenerator().CodeFor<IFoo>();

            Console.Write(code);

            Assert.Equal("", code);
        }
    }

    public interface IFoo
    {
        int Add(int value1, int value2);

        int Subtract(int value1, int value2);
    }

    public class AddRequest
    {

    }
}
