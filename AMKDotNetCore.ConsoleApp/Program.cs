using AMKDotNetCore.ConsoleApp.AdoDotNetExamples;
using System;
using System.Data.SqlClient;
using AMKDotNetCore.ConsoleApp.DapperExamples;
using AMKDotNetCore.ConsoleApp.EFCoreExamples;
using AMKDotNetCore.ConsoleApp.HttpClientExamples;
using System.Threading.Tasks;

namespace AMKDotNetCore.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine("waiting for api... ");
            Console.ReadKey();
            Console.WriteLine("Hello World!");
            // F5 => Run
            /*Console.WriteLine("Hello World!");
            AdoDotNetExample adoDotNetExample = new AdoDotNetExample(); 
            adoDotNetExample.Run();*/

            //DapperExample dapperExample = new DapperExample();
            //dapperExample.Run();

            //EFCoreExample eFCoreExample=new EFCoreExample();
            //eFCoreExample.Run();

            HttpClientExample httpClientExample = new HttpClientExample();
            await httpClientExample.Run();



            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();

        }
    }
}