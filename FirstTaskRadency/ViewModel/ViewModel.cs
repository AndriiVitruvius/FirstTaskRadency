using FirstTaskRadency.ViewModel;
using FirstTaskRadency.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstTaskRadency.Model;

namespace FirstTaskRadency.ViewModel
{
    internal class ViewModelWatcher
    {
        FileSystemWatcher watcher;
        ListenerService ServListener;

        internal ViewModelWatcher()
        {
            FirstTaskRadency.ViewModel.ViewModelBase.Start += StartProgram;
            FirstTaskRadency.ViewModel.ViewModelBase.End += StopProgram;

            FirstTaskRadency.ViewModel.ViewModelBase.Reset += StopProgram;
            FirstTaskRadency.ViewModel.ViewModelBase.Reset += StartProgram;


        }


        private  void StartProgram()
        {

            try
            {
                 ServListener = new ListenerService();

                string pathMainFolder = ConfigurationManager.AppSettings["FolderA"];

                watcher = new FileSystemWatcher(pathMainFolder);
                watcher.EnableRaisingEvents = true;
                watcher.Created += OnWorkWithFile;


                async void OnWorkWithFile(object sender, FileSystemEventArgs e)
                {
                    Task task = Task.Run(() => MainModel.SaveFileInNewFormat(e));
                    await task;

                }
            }
            catch (Exception e)
            {
                FirstTaskRadency.ViewModel.ViewModelBase.Error.Invoke(e.Message.ToString());
            }


        }

        private void StopProgram()
        {
            watcher?.Dispose();
            ServListener?.timer?.Dispose();
            ServListener?.FilesListener.WriteMetaLog();
            ServListener?.FilesListener?.Close();
        }

     
  
    }
}
