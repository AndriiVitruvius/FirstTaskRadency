using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskRadency.ViewModel
{
    internal static class ViewModelBase
    {
        internal static Action Start;

        internal static Action End;

        internal static Action Reset;

        internal static Action<string> Error;
   
    }
}
