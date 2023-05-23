using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace Config
{

    public class ZoneConfig
    {
        public Result[] result { get; set; }
        public Result_Info result_info { get; set; }
        public bool success { get; set; }
        public object[] errors { get; set; }
        public object[] messages { get; set; }
    }

    public class Result_Info
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total_pages { get; set; }
        public int count { get; set; }
        public int total_count { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public bool paused { get; set; }
        public string type { get; set; }
        public int development_mode { get; set; }
        public string[] name_servers { get; set; }
        public object original_name_servers { get; set; }
        public object original_registrar { get; set; }
        public object original_dnshost { get; set; }
        public DateTime modified_on { get; set; }
        public DateTime created_on { get; set; }
        public DateTime activated_on { get; set; }
        public Meta meta { get; set; }
        public Owner owner { get; set; }
        public Account account { get; set; }
        public Tenant tenant { get; set; }
        public Tenant_Unit tenant_unit { get; set; }
        public string[] permissions { get; set; }
        public Plan plan { get; set; }
    }

    public class Meta
    {
        public int step { get; set; }
        public int custom_certificate_quota { get; set; }
        public int page_rule_quota { get; set; }
        public bool phishing_detected { get; set; }
        public bool multiple_railguns_allowed { get; set; }
    }

    public class Owner
    {
        public object id { get; set; }
        public string type { get; set; }
        public object email { get; set; }
    }

    public class Account
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Tenant
    {
        public object id { get; set; }
        public object name { get; set; }
    }

    public class Tenant_Unit
    {
        public object id { get; set; }
    }

    public class Plan
    {
        public string id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string currency { get; set; }
        public string frequency { get; set; }
        public bool is_subscribed { get; set; }
        public bool can_subscribe { get; set; }
        public string legacy_id { get; set; }
        public bool legacy_discount { get; set; }
        public bool externally_managed { get; set; }
    }

}
