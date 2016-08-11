using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmarterWCFClient
{
    public class RoslynCompilerTest
    {
        public static IWriteMessages BuildConsoleWriter()
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(@"
                    using System;

                    namespace SmarterWCFClient
                    {
                        public class ConsoleWriter : IWriteMessages
                        {
                            public void Write(string message)
                            {
                                Console.WriteLine(message);
                            }
                        }
                    }");

            string assemblyName = Path.GetRandomFileName();
            var references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IWriteMessages).Assembly.Location)
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));


            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());

                    Type type = assembly.GetType("SmarterWCFClient.ConsoleWriter");
                    IWriteMessages obj =(IWriteMessages) Activator.CreateInstance(type);
                    return obj;
                }
            }
            return null;
        }
    }

    public interface IWriteMessages
    {
        void Write(string message);
    }
}
