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

        DateTime alarmTime; 


        /// <summary>
        /// Make and Update Listener and create log file at 23, 59, 50
        /// </summary>
        internal ListenerService()
        {
            FilesListener = new FilesListener();

            MakeTimer();

            Logger.filesListener = FilesListener;
        }

        private  void MakeTimer()
        {

            DateTime alarmTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 50);
            TimeSpan timeUntilAlarm = alarmTime - DateTime.Now;

            if (timeUntilAlarm < TimeSpan.Zero)
               alarmTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 15, 42, 40);

            timeUntilAlarm = alarmTime - DateTime.Now;


            timer = new Timer(OnTimerElapsed, null, timeUntilAlarm, Timeout.InfiniteTimeSpan);
        }

        public void OnTimerElapsed(object state)
        {

            FilesListener.WriteMetaLog();
            FilesListener.Close();
            FilesListener = new FilesListener();
            MakeTimer();


        }

    }
}
