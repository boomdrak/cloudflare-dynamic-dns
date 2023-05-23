using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using RestSharp.Authenticators.OAuth2;
using RestSharp.Authenticators;
using Config;
using System.Reflection;
using System.Net.Http;

#nullable disable
namespace cloudflare_ddns.Util
{
    class Http
    {
        public static RestClient client;
        public static RestClient GetClient()
        {
            //var authenticator = new JwtAuthenticator(App.settings.api_key);
            var options = new RestClientOptions("https://api.cloudflare.com/client/v4");

            client = new RestClient(options);
            client.AddDefaultHeader("Authorization", "Bearer " + App.settings.api_key);

            return client;
        }

        public static string GetZoneID()
        {
            string res = "";
            ZoneConfig zoneConfig = null;
            RestResponse response = null;

            try
            {
                var client = GetClient();
                var request = new RestRequest("zones", Method.Get);
                request.AddHeader("X-Auth-Email", App.settings.api_email);
                //request.AddHeader("Content-Type", "application/json");
                response = client.ExecuteGet(request);
                try
                {
                    zoneConfig = JsonSerializer.Deserialize<ZoneConfig>(response.Content);
                    res = zoneConfig.result[0].id;
                } catch (Exception)
                {
                    Console.WriteLine("ERROR!! GetZoneID() ex: " + response.Content.ToString());
                    Environment.Exit(99);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR!! GetZoneID() ex: " + response.Content.ToString());
                Environment.Exit(99);
            }
            return res;

        }

        public static CF.DNS.Result GetDNSRecord(string zoneId, string DNSRecord)
        {
            CF.DNS.Result res = null;
            CF.DNS.Zone zones = null;
            RestResponse response = null;

            try
            {
                var client = GetClient();
                var request = new RestRequest("zones/" + zoneId + "/dns_records", Method.Get);
                request.AddHeader("X-Auth-Email", App.settings.api_email);
                response = client.ExecuteGet(request);



                zones = JsonSerializer.Deserialize<CF.DNS.Zone>(response.Content);
                foreach (CF.DNS.Result z in zones.result)
                {

                    if (z.name == DNSRecord)
                    {
                        Console.WriteLine("Found DNS record: " + z.name);
                        return z;
                    }
                }
                Console.WriteLine("ERROR!! GetDNSRecord() failed, cloud not obtain DNS record for: " + App.settings.dns_record);
                Environment.Exit(99);

            }
            catch (Exception)
            {
                Console.WriteLine("ERROR!! GetDNSRecord() ex: " + response.Content.ToString());
                Environment.Exit(99);
            }
            return res;

        }

        public static Boolean UpdateDNSRecord(string zoneId, string DNSID, string publicIP)
        {
            Boolean res = false;

            try
            {
                var client = GetClient();
                var request = new RestRequest("zones/" + zoneId + "/dns_records" + "/" + DNSID, Method.Put);
                request.AddHeader("X-Auth-Email", App.settings.api_email);
                request.AddHeader("Accept", "application/json");
                var body = new
                {
                    content = publicIP,
                    name = App.settings.dns_record,
                    proxied = false,
                    type = "A",
                    comment = "Autoupdated by cloudflare-ddns app",
                    ttl = 3600
                };
                request.AddJsonBody(body);

                var response = client.Execute(request);

                Model.UpdateZone updateZone = JsonSerializer.Deserialize<Model.UpdateZone>(response.Content);
                if (updateZone.success)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR!! UpdateDNSRecord() ex: " + e.Message.ToString());
                Environment.Exit(99);
            }
            return res;

        }

        public static string GetPublicIp()
        {
            var url = "https://ipinfo.io/ip";
            var client = new HttpClient();
            var response = client.GetAsync(url);
            var contents = response.Result.Content.ReadAsStringAsync().Result;
            client.Dispose();
            return contents;

        }
    }
}
