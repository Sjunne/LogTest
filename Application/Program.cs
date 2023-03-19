using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogComponent;
using LogUsers.Builder;


namespace LogUsers
{
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            var logger = new LoggerBuilder()
                .LogToFile(@"C:\LogTest")
                .Build();

            var logger2 = new LoggerBuilder()
               .LogToFile(@"C:\LogTest")
               .Build();

            for (int i = 0; i < 15; i++)
            {
                logger.WriteSingleItem("Number with Flush: " + i.ToString());
                Thread.Sleep(50);
            }
            logger.StopWithFlush();

    
            for (int i = 50; i > 0; i--)
            {
                logger2.WriteSingleItem("Number with No flush: " + i.ToString());
                Thread.Sleep(20);
            }
            logger2.StopWithoutFlush();

            Console.WriteLine("finished");
            Console.ReadLine();
        }
    }
}
