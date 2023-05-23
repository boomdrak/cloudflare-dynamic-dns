﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace cloudflare_ddns.Model
{

    public class UpdateZone
    {
        public Result result { get; set; }
        public bool success { get; set; }
        public object[] errors { get; set; }
        public object[] messages { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string zone_id { get; set; }
        public string zone_name { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string content { get; set; }
        public bool proxiable { get; set; }
        public bool proxied { get; set; }
        public int ttl { get; set; }
        public bool locked { get; set; }
        public Meta meta { get; set; }
        public string comment { get; set; }
        public object[] tags { get; set; }
        public DateTime created_on { get; set; }
        public DateTime modified_on { get; set; }
    }

    public class Meta
    {
        public bool auto_added { get; set; }
        public bool managed_by_apps { get; set; }
        public bool managed_by_argo_tunnel { get; set; }
        public string source { get; set; }
    }

}
