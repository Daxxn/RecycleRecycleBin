using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TrashCleaner.Models.FileControl;
using TrashCleaner.Models.Filtering;
using TrashCleaner.Models.Logging;

namespace TrashCleaner.Models
{
   public class ServiceController
   {
      #region - Fields & Properties
      private Timer Timer { get; set; }
      private bool IsRunning { get; set; }
      #endregion

      #region - Constructors
      public ServiceController()
      {
         Timer = new Timer
         {
#if ShortInterval
            Interval = 1000 * 60 * 2,
#else
            Interval = 1000 * 60 * 60 * 24 * 1,
#endif
            AutoReset = true,
            Enabled = true
         };

         Timer.Elapsed += Timer_Elapsed;
         Timer.Start();
      }

      private void Timer_Elapsed(object sender, ElapsedEventArgs e)
      {
         if (!IsRunning)
         {
            IsRunning = true;
            var settingsLoader = new SettingsLoader();
            var settings = settingsLoader.ReadSettings();
            var logger = new Logger();
            logger.LoadLog(settings);
            var reader = new FileReader();
            reader.LoadFiles(settings, logger);
            logger.SaveLog(settings);
            IsRunning = false;
         }
      }
      #endregion

      #region - Methods
      public void Start() => Timer.Start();

      public void Stop() => Timer.Stop();
      #endregion

      #region - Full Properties

      #endregion
   }
}
