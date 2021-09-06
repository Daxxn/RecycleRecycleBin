using System;
using System.Collections.Generic;
using Topshelf;
using TrashCleaner.Models;
using TrashCleaner.Models.FileControl;
using TrashCleaner.Models.Filtering;
using TrashCleaner.Models.Logging;

namespace TrashCleaner
{
   class Program
   {
      static void Main(string[] args)
      {
         try
         {
#if Test
            SettingsLoader.Instance.Start();
            Logger.Instance.Start();

            var settings = SettingsLoader.Instance;
            var fileReader = FileReader.Instance;
            fileReader.LoadFiles();

            SettingsLoader.Instance.Stop();
            Logger.Instance.Stop();
#else
            var exitCode = HostFactory.Run(x =>
            {
               x.Service<ServiceController>(s =>
               {
                  s.ConstructUsing(service => new ServiceController());

                  s.WhenStarted(service => service.Start());
                  s.WhenStopped(service => service.Stop());
               });

               x.RunAsLocalSystem();

               x.SetDisplayName("Recycle Cleaner");
               x.SetDescription("Removes old files from the recycle bin after a certain number of days.");
               x.SetServiceName("RecycleCleaner");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
#endif
         }
         catch (Exception e)
         {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.Source);
         }

         Console.WriteLine("\nDone...");
#if Test
         Console.ReadKey();
#endif
      }
   }
}
