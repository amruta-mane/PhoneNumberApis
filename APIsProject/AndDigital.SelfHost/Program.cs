using AndDigitalApis;
using System;
using System.Diagnostics;
using System.ServiceModel.Web;

namespace AndDigital.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                WebServiceHost host = new WebServiceHost(typeof(AndDigitalApisService));
                host.Open();
                Console.WriteLine("Service is running.");
                Console.WriteLine("");
                Console.WriteLine("Press any key to stop the service.");
                Console.ReadKey();
                host.Close();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
