using cloudflare_ddns.Util;
using CommandLine;
using CommandLine.Text;
using Config.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

#nullable disable
namespace cloudflare_ddns
{

    public interface IMySettings
    {
        string api_email { get; set; }

        string api_key { get; set; }
        string dns_record { get; set; }

    }
    class App
    {
        public class Options

        {
            [CommandLine.Option('u', "update", Required = true, HelpText = "Update DDNS record")]
            public bool Update { get; set; }
        }

        public static string configFile = Environment.CurrentDirectory + "/config.json";
        public static IMySettings settings;
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                  .WithParsed<Options>(o =>
                  {
                      if (o.Update == true) {

                          if (!System.IO.File.Exists(configFile))
                          {
                              Console.WriteLine("Missing config.json, writing new file in " + configFile + " .. and exiting");
                              settings = new ConfigurationBuilder<IMySettings>().UseJsonFile(configFile).Build();
                              settings.api_email = "";
                              settings.api_key = "";
                              settings.dns_record = "";
                              Environment.Exit(5);
                          }
                          settings = new ConfigurationBuilder<IMySettings>().UseJsonFile(configFile).Build();

                          if (settings.api_email == null || settings.api_email == "")
                          {
                              Console.WriteLine("Missing or emprty " + nameof(settings.api_email) + " in config.json, exiting");
                              Environment.Exit(5);
                          }
                          if (settings.api_key == null || settings.api_key == "")
                          {
                              Console.WriteLine("Missing or emprty" + nameof(settings.api_key) + " in config.json, exiting");
                              Environment.Exit(5);
                          }
                          if (settings.dns_record == null || settings.dns_record == "")
                          {
                              Console.WriteLine("Missing or emprty " + nameof(settings.dns_record) + " in config.json, exiting");
                              Environment.Exit(5);
                          }

                          var zoneId = Util.Http.GetZoneID();
                          string publicIP = Util.Http.GetPublicIp();

                          Console.WriteLine("Found zone ID: " + zoneId);
                          Console.WriteLine("Found public IP: " + publicIP);

                          CF.DNS.Result DNSZone = Util.Http.GetDNSRecord(zoneId, settings.dns_record);

                          if (DNSZone == null) Console.WriteLine("Error!! DNS Zone not found");
                          if (DNSZone.content == publicIP)
                          {
                              Console.WriteLine("No need to update, public IP " + publicIP + " equals record " + DNSZone.content);
                              Environment.Exit(0);
                          }

                          Console.WriteLine("Updateing DNS record to IP " + publicIP + " ...");

                          Boolean DNSUpdated = Util.Http.UpdateDNSRecord(zoneId, DNSZone.id, publicIP);

                          if (DNSUpdated) Console.WriteLine("Successfully updated DNS record :)");
                      }
                      else
                      {
                          Console.WriteLine($"Current Arguments: -u {o.Update}");
                          Console.WriteLine("Quick Start Example!");
                      }
                  });


        }
    }
}
