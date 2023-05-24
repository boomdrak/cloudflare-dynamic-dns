
# Cloudflare DNS record updates

I created a C# .NET 6 console tool that updates a Cloudflare DNS record with your external IP address. In the same manner that DDNS works.

The code targets Cloudflare v4 API: https://developers.cloudflare.com/api/

The project uses a config.json file that has three parameters

## Config params

```bash
  api_email
  api_key
  dns_record
```


## Usage/Examples

```bash
cloudflare-dynamic-dns.exe  --helpg
cloudflare-dynamic-dns.exe  --version
cloudflare-dynamic-dns.exe  --update
```

Feel free to contribute and create a pull request :)
