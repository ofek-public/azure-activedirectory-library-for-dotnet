using System;

namespace Microsoft.IdentityModel.Clients.ActiveDirectory
{
    internal static class Mono
    {
        public static bool IsMono()
        {
            Type t = Type.GetType("Mono.Runtime");
            return t != null;
        }
    }
}
