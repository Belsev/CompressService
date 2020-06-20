using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace TrackService
{
    public partial class TrackService : ServiceBase
    {
        private readonly CompressorManager compressorManager = null;
        private readonly EventLog eventLog;

        public TrackService()
        {
            string eventSourceName = "TrackServiceSource";
            string logName = "TrackServiceLog";

            eventLog = new EventLog();

            if (!EventLog.SourceExists(eventSourceName))
            {
                EventLog.CreateEventSource(eventSourceName, logName);
            }

            eventLog.Source = eventSourceName;
            eventLog.Log = logName;

            try
            {
                compressorManager = new CompressorManager();
            }
            catch(Exception ex)
            {
                eventLog.WriteEntry(ex.ToString());
            }

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                compressorManager.Start();
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry(ex.ToString());
            }
        }

        protected override void OnPause()
        {
            try
            {
                compressorManager.Pause();
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry(ex.ToString());
            }
        }

        protected override void OnContinue()
        {
            try
            {
                compressorManager.Continue();
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry(ex.ToString());
            }
        }

        protected override void OnStop()
        {
            try
            {
                compressorManager.Stop();
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry(ex.ToString());
            }
        }
    }
}
