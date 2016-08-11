using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmarterWCFClient
{
    public interface IGenerateCodeForChannelClient
    {
        string CodeFor<TChannel>();
    }

    public class ChannelClientCodeGenerator : IGenerateCodeForChannelClient
    {
        public string CodeFor<TChannel>()
        {
            var channelType = typeof(TChannel);
            var sb = new StringBuilder();

            // the usings
            sb.Append($@"
using System;
using SmarterWCFClient;
using {channelType.Namespace};
");

            sb.Append("namespace SmarterWCFClient {");
            sb.Append($@"public class {normalize(channelType.Name)}ServiceClient : ServiceChannelClient<{channelType.Name}>, {channelType.Name} {{");

            // loop over the interface methods and generate the code for each
            foreach(var methodInfo in channelType.GetMethods())
            {
                sb.Append(generateMethodProxy(methodInfo));
            }

            sb.Append("}}");

            return sb.ToString();
        }

        private string generateMethodProxy(MethodInfo methodInfo)
        {
            return $@"
public {methodInfo.ReturnType.Name} {methodInfo.Name}({generateMethodParameters(methodInfo.GetParameters())}) {{
return InvokeMethod(x => x.{methodInfo.Name}({string.Join(", ", methodInfo.GetParameters().Select(x => x.Name))}));
}}
";
        }

        private string normalize(string channelName)
        {
            if (channelName.Substring(0, 1) == "I")
            {
                return channelName.Substring(1, channelName.Length - 1);
            }

            return channelName;
        }

        private string generateMethodParameters(ParameterInfo[] parameters)
        {
            return string.Join(", ", parameters.Select(x => $"{x.ParameterType.Name} {x.Name}"));
        }
    }
}
