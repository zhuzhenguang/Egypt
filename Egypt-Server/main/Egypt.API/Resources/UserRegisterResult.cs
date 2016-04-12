using System.Collections.Generic;

namespace Egypt.API.Resources
{
    public class UserRegisterResult 
    {
        public List<ResourceLink> Links { get; private set; }

        public UserRegisterResult()
        {
            Links = new List<ResourceLink>();
        }

        public void AddLink(string rel, string uri)
        {
            Links.Add(new ResourceLink(rel, uri));
        }
    }
}