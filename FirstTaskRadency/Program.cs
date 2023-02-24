using FirstTaskRadency.ViewModel;
using FirstTaskRadency.Model;
using FirstTaskRadency.Service;
using FirstTaskRadency.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FirstTaskRadency
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            var viewModel = new ViewModelWatcher();

            ViewConsole viewConsole = new ViewConsole();

            viewConsole.ViewShow();

        }

    

  


    }
}
