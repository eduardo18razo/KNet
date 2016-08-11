using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using KinniNet.Core.Demonio;
using Timer = System.Timers.Timer;

namespace KiiniNet.Services.Windows
{
    public partial class ServiceNotificacion : ServiceBase
    {
        private readonly Timer _intervaloEjecucion = null;
        public ServiceNotificacion()
        {
            InitializeComponent();
            _intervaloEjecucion = new Timer(10000);
            _intervaloEjecucion.Elapsed += intervaloEjecucion_Elapsed;
        }

        void intervaloEjecucion_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                new BusinessDemonio().EnvioNotificacion();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Log("KiiniNet", "Service Send Notication", ex.Message);
                
            }
        }

        void Log(string source, string application, string mensaje)
        {
            try
            {
                MessageBox.Show(mensaje);
                if (!EventLog.SourceExists(source))
                    EventLog.CreateEventSource(source, application);

                EventLog.WriteEntry(source, mensaje);
                EventLog.WriteEntry(source, mensaje,
                    EventLogEntryType.Warning, 234);
            }
            catch (Exception ex)
            {
                Log("KiiniNet", "Service Send Notication", ex.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch();
            try
            {
                _intervaloEjecucion.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log("KiiniNet", "Service Send Notication", ex.Message);
            }
        }

        protected override void OnStop()
        {
            try
            {
                _intervaloEjecucion.Stop();
            }
            catch (Exception ex)
            {
                Log("KiiniNet", "Service Send Notication", ex.Message);
            }
        }
    }
}
