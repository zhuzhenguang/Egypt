namespace Egypt.API.Resources
{
    public static class ObjectExtention
    {
        public static bool AnyBlank(params object[] objects)
        {
            foreach (var o in objects)
            {
                if (o == null)
                {
                    return true;
                }

                var s = o as string;
                if (s !=null && string.IsNullOrWhiteSpace(s))
                {
                    return true;
                }
            }
            return false;
        }
    }
}