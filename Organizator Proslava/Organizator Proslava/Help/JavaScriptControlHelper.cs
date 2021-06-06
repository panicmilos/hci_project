using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows;

namespace Organizator_Proslava.Help
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class JavaScriptControlHelper
    {
        private readonly Window _window;

        public JavaScriptControlHelper(Window w)
        {
            _window = w;
        }

        public void RunFromJavascript(string param)
        {
        }
    }
}