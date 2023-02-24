using FirstTaskRadency.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskRadency.View
{
    internal class ViewConsole
    {
        internal ViewConsole()
        {

            ViewModelBase.Error += ErrorMessage;
        }

        internal void ViewShow()
        {

            bool workProgram = true;

            Console.WriteLine("Command :  Start  Reset  End ");



            while (workProgram)
            {
                string commandConsole = Console.ReadLine();

                if (commandConsole == "Start")
                {
                    ViewModelBase.Start.Invoke();
                    Console.WriteLine("Programm Work");

                }

                else if (commandConsole == "Reset")
                {
                    Console.WriteLine("Programm Reset");
                    ViewModelBase.Reset.Invoke();


                }

                else if (commandConsole == "End")
                {
                    ViewModelBase.End.Invoke();
                    Console.WriteLine("Program stop, click Enter");
                    workProgram = false;

                }
            }

            Console.ReadLine();

        }

        private void ErrorMessage(string error)
        {
            Console.WriteLine(error);
        }
    }
}
