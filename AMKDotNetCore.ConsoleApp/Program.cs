using AMKDotNetCore.ConsoleApp.AdoDotNetExamples;
using System;
using System.Data.SqlClient;
using AMKDotNetCore.ConsoleApp.DapperExamples;
using AMKDotNetCore.ConsoleApp.EFCoreExamples;

namespace AMKDotNetCore.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // F5 => Run
            /*Console.WriteLine("Hello World!");
            AdoDotNetExample adoDotNetExample = new AdoDotNetExample(); 
            adoDotNetExample.Run();*/

            //DapperExample dapperExample = new DapperExample();
            //dapperExample.Run();

            EFCoreExample eFCoreExample=new EFCoreExample();
            eFCoreExample.Run();

            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();

        }
    }
}