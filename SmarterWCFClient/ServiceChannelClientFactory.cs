using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Linq;

namespace SmarterWCFClient
{
    public class ServiceChannelClientFactory
    {
        private IGenerateCodeForChannelClient channelClientCodeGenerator;
        private IGenerateAssembly assemblyGenerator;

        public ServiceChannelClientFactory(IGenerateCodeForChannelClient channelClientCodeGenerator, IGenerateAssembly assemblyGenerator)
        {
            this.channelClientCodeGenerator = channelClientCodeGenerator;
            this.assemblyGenerator = assemblyGenerator;
        }

        public ServiceChannelClientFactory() : this(new ChannelClientCodeGenerator(), new AssemblyGenerator()) { }

        public TChannel Build<TChannel>() where TChannel : class
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(channelClientCodeGenerator.CodeFor<TChannel>());
            var assembly = assemblyGenerator.GenerateAssemblyFrom(syntaxTree, typeof(TChannel).Assembly);
            var clientType = assembly.GetTypes().First(x => typeof(TChannel).IsAssignableFrom(x));
            var ctor = clientType.GetConstructor(new Type[] { });
            return (TChannel)ctor.Invoke(new object[] { });
        }
    }
}
