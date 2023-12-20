using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_final_METEO.ViewModels.Delegates
{
    public class ViewModelDelegates
    {
        public delegate void MessageErreur(string message);
        public delegate void OpenConfig();
        public delegate string OpenFile();
    }
}
