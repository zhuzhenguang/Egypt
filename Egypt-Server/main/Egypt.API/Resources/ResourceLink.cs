using System.Linq;

namespace Egypt.API.Resources
{
    public class ResourceLink
    {
        public string Rel { get; set; }
        public string Uri { get; set; }

        public ResourceLink(string rel, string uri)
        {
            Rel = rel;
            Uri = uri;
        }

        public long ExtractId()
        {
            return long.Parse(Uri.Split('/').Last());
        }
    }
}