using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

using Microsoft.Win32;

using Walterlv.Win32;

namespace Walterlv.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                RunOnWindows();
            }
        }

        [SupportedOSPlatform("Windows")]
        private static void RunOnWindows()
        {
            var wxPath = RegistryHive.CurrentUser.Read64(
                @"SOFTWARE\Tencent\WXWork",
                "Executable");
            if (!string.IsNullOrWhiteSpace(wxPath) && File.Exists(wxPath))
            {
                RegistryHive.CurrentUser.Write64(
                    @"SOFTWARE\Tencent\WXWork",
                    "multi_instances",
                    2);
                Process.Start(new ProcessStartInfo
                {
                    FileName = wxPath,
                    UseShellExecute = false,
                });
            }
        }
    }
}
