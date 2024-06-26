using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Adventofcode_day1.Settings
{
    public class Utils
    {
       
        public static void GenerateModelClass(string modelName)
        {
            string modelTemplate = $@"
            using System.Threading.Tasks;

            namespace Adventofcode_day1.Models
            {{
                public class {modelName}
                {{
                    public static SqlBuilder builder;
                    public static Sql sql;
                    public static string table = ""{modelName}s"";
                    
                    public static void Initialize(Settings.Settings settings)
                    {{
                        builder = new SqlBuilder(settings);
                        sql = new Sql(builder.Database);
                    }}

                    public static void Migrate()
                    {{
                        var migrate = new Migration(table)
                            .Id() 
                            .String(""message"") 
                            .Timestamps();
                        
                        migrate.Build(Sql.Connection);
                    }}
                    
                    public static async Task<string> get{modelName}s()
                    {{
                        return await sql.Select().From(table).ExecuteReadAsync();
                    }}
                }}
            }}";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", $"{modelName}.cs");
            File.WriteAllText(filePath, modelTemplate);
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            

            // Logic to add the model class to the .csproj file
            string csprojPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LaraSharp.csproj"); // Adjust the csproj path as necessary
            XDocument csproj = XDocument.Load(csprojPath);
            XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

            // Check if the Compile ItemGroup exists, if not, create it
            XElement compileItemGroup = csproj.Root.Elements(ns + "ItemGroup")
                .FirstOrDefault(ig => ig.Elements(ns + "Compile").Any());
            if (compileItemGroup == null)
            {
                compileItemGroup = new XElement(ns + "ItemGroup");
                csproj.Root.Add(compileItemGroup);
            }

            // Add the model class file to the Compile Include
            XElement newCompile = new XElement(ns + "Compile", new XAttribute("Include", $"Models\\{modelName}.cs"));
            compileItemGroup.Add(newCompile);
            
            csproj.Save(csprojPath);
            
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Model class ");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(modelName);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" has been added to the .csproj file.");
            
            Console.Write("Model class ");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(modelName);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" has been created.");
            
            Console.ForegroundColor = originalColor;

            Environment.Exit(0);

        }
    }
}