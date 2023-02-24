using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstTaskRadency.Service
{
    internal class ListenerService
    {

        internal FilesListener FilesListener { get; private set; }
        internal Timer timer { get; private set; }


        /// <summary>
        /// Make and Update Listener and create log file at 23, 59, 50
        /// </summary>
        internal ListenerService()
        {
            FilesListener = new FilesListener();

          //  DateTime alarmTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 50);
            DateTime alarmTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 00, 40);

            TimeSpan timeUntilAlarm = alarmTime - DateTime.Now;

             timer = new Timer(OnTimerElapsed, null, timeUntilAlarm, Timeout.InfiniteTimeSpan);

            Logger.filesListener = FilesListener;
        }

        public void OnTimerElapsed(object state)
        {

            FilesListener.WriteMetaLog();
            FilesListener.Close();
            FilesListener = new FilesListener();
            
        }

    }
}
