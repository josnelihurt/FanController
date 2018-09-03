using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.ServiceProcess;
using System.Diagnostics;
using System.Reflection;

namespace FanControl
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //Debug.Assert(false);
            string codeBase = Assembly.GetExecutingAssembly().CodeBase + ".log";
            codeBase = codeBase.Replace("file:///", "");
            TextWriterTraceListener[] listeners = new TextWriterTraceListener[] {
            new TextWriterTraceListener(codeBase),
            new TextWriterTraceListener(Console.Out)};
            Debug.Listeners.AddRange(listeners);
            Debug.WriteLine("StartApp", "MAIN");
            Debug.Flush();
            try
            {
                Settings settings = new Settings()
                {
                    MaximunTemperature = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("max_value_celsius")),
                    MinimunTemperature = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("min_value_celsius")),
                    SerialPort = ConfigurationSettings.AppSettings.Get("port")
                };

                Debug.WriteLine("Settings : " + settings.ToString(), "MAIN");
                if (Environment.UserInteractive)
                {
                    Service serivce = new Service(settings);
                    serivce.Run();
                    while (true)
                    {
                        Thread.Sleep(10000);
                    }
                }
                else
                {
                    ServiceBase[] servicesToRun;
                    servicesToRun = new ServiceBase[]
                    {
                        new Service(settings)
                    };
                    ServiceBase.Run(servicesToRun);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString(), "MAIN_EX");
            }
            
        }
                
    }
}
