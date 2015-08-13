using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Microsoft.IdentityModel.Clients.ActiveDirectory
{
    internal class DeviceAuthHelper : IDeviceAuthHelper
    {
        public bool CanHandleDeviceAuthChallenge {
            get { return false; }
        }
        public string CreateDeviceAuthChallengeResponse(IDictionary<string, string> challengeData)
        {
            throw new NotImplementedException();
        }
    }
}