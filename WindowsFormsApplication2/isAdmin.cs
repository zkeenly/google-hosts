using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
namespace WindowsFormsApplication2
{
    class isAdmin
    {
         public static bool IsAdministrator()
　　 {
　　 WindowsIdentity identity = WindowsIdentity.GetCurrent();
　　 WindowsPrincipal principal = new WindowsPrincipal(identity);
　　 return principal.IsInRole(WindowsBuiltInRole.Administrator);
　　 }  
    }
}
