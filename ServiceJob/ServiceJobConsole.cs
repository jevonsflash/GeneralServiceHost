using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceJob
{
    public class ServiceJobConsole
    {
        public static void Main()
        {
            //Console.WriteLine("Hellow World");
            //Thread.Sleep(5000);
            //Console.WriteLine("this is a test console app");
            //Thread.Sleep(2000);

            //Console.WriteLine("I can read your mind");
            //Thread.Sleep(2000);

            //Console.WriteLine("do you belive it?");
            var n = 10;
            var a = 5;
            var b = 0;
            while (n > 0)
            {
                Console.WriteLine("I can read your mind, do you belive it?");
                Console.WriteLine("now is{0}", DateTime.Now);
                Thread.Sleep(1000);
                n--;
            }

            Console.WriteLine("about to exit!");
            var c =  a / b;
            //Thread.Sleep(1000);

            //Console.ReadLine();


        }


    }
}
