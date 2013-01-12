using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WRS20_Logic;
using Newtonsoft.Json.Linq;

namespace WRS20_ClientAI
{
    class Program
    {   
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            AICore theBigOne = new AICore();
            theBigOne.Start();
            Console.ReadKey();
        }
    }
}
