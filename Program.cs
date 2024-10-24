using System.Reflection;

//[assembly: AssemblyTitle("HL7 Collect from ASCOM DAS3 Driver Logs")] // this become to "File Description"
[assembly: AssemblyDescription("HL7 Collect from ASCOM DAS3 Driver Logs")]
//[assembly: AssemblyConfiguration("Debug")] // Debug | Release
//[assembly: AssemblyCompany("ASCOM UMS srl unip.")]
//[assembly: AssemblyProduct("HL7CollectFromLogs")] // become to "Product name"
//[assembly: AssemblyVersion("1.0.0")] // become to "Product name"
//[assembly: AssemblyFileVersion("1.0.0")] // become to "Product name"
[assembly: AssemblyCopyright("©2023 ASCOM UMS srl unip. - Gabriele PANCANI")] // Become to "Copyright"
[assembly: AssemblyTrademark("©2023 ASCOM UMS srl unip.")]

internal class Program
{
    private static void Main(string[] args)
    {
        
        string? textFile = null;

        if (args.Length == 0)
        {
            Console.WriteLine("HL7 Collect From Logs\r\n---------------------\r\nPlease specify a valid log file name and path (i.e.: \"C:\\Test\\20230307_NHSWalesLaboratory__43101.log\")");
            Environment.Exit(1);
        }


        else
        {
            textFile = args[0];
        }


        // string textFile = @"C:\Users\itgp\Downloads\oldlab\LAB SIT Logs\20230307_NHSWalesLaboratory__43101.log";


        // Read file using StreamReader. Reads file line by line

        // Create a file to save the incoming data

        string fileToSave = textFile + ".hl7";

        if (File.Exists(textFile))
        {
            // https://learn.microsoft.com/it-it/dotnet/standard/io/how-to-write-text-to-a-file
            StreamWriter outputFile = new StreamWriter(fileToSave);
           

            using (StreamReader file = new StreamReader(textFile))
            {
                int counter = 0;
                int counterHL7Messages = 0;
                const string breakstr = "\r\n\r\n";
                string? ln;

                while ((ln = file.ReadLine()) != null)
                {
                    if (ln.Contains("MSH|") && ln.Length >= 3)
                    {
                        if(counterHL7Messages > 0)
                        {
                            outputFile.WriteLine(breakstr);
                        }
                        outputFile.WriteLine(@ln.Substring(ln.IndexOf("MSH|"), @ln.Length - @ln.IndexOf("MSH|")));
                        Console.WriteLine(ln.Substring(ln.IndexOf("MSH|"), ln.Length - ln.IndexOf("MSH|")));
                        counterHL7Messages++;

                    }
                    if (ln.IndexOf("|") == 3)
                    {
                        outputFile.WriteLine(@ln);
                        Console.WriteLine(ln);
                    }

                    /*
                    if (ln.Length == 0)
                    {
                        outputFile.WriteLine("\r\n");
                        Console.WriteLine("\r\n");

                    }
                    */
                    counter++;
                }


                file.Close();
                outputFile.Close();
                Console.WriteLine("================================================");
                Console.WriteLine($"Source file lines readed: {counter}.");
                Console.WriteLine($"HL7 Message counter: {counterHL7Messages}.");

            }
        }
        else
        {
            Console.WriteLine($"HL7 Collect From Logs\r\n---------------------\r\nThe provided file and path: {textFile} are not valid.\r\nProcess ended.");
        }
    }

 


}