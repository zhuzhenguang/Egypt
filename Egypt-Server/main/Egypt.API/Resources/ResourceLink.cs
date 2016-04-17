using System.Linq;

namespace Egypt.API.Resources
{
    public class ResourceLink
    {
        public string Rel { get; private set; }
        public string Uri { get; private set; }

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