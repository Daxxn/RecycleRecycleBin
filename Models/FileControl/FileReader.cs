using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using TrashCleaner.Models.Filtering;
using TrashCleaner.Models.Logging;

namespace TrashCleaner.Models.FileControl
{
   public class FileReader
   {
      #region - Fields & Properties
      public List<FileModel> Files { get; private set; }
      public int DeletedFiles { get; private set; }
      #endregion

      #region - Constructors
      public FileReader() => Files = new List<FileModel>();
      #endregion

      #region - Methods
      public void LoadFiles(SettingsModel settings, Logger logger)
      {
         try
         {
            var allPaths = new List<string>();
            var UserSid = UserPrincipal.Current.Sid;

            foreach (var drive in settings.DriveList)
            {
               allPaths.AddRange(Directory.GetFiles($@"{drive}$Recycle.bin\{UserSid}", "*.*", SearchOption.AllDirectories));
            }

            foreach (var path in allPaths)
            {
               var file = new FileModel(path);
               if (settings.ExtensionList.Any((ext) => path.EndsWith(ext)))
               {
                  if (file.Info.LastWriteTime.CompareTo(settings.RemoveExtensionDateTime) < 0)
                  {
                     Files.Add(file);
                  }
               }
               else if (file.Info.LastWriteTime.CompareTo(settings.RemoveAnyFileDateTime) < 0)
               {
                  Files.Add(file);
               }
            }

            int deleteErrors = 0;

            foreach (var file in Files)
            {
               try
               {
#if NoDelete
                  Console.WriteLine($"Delete File... {file.Info.Name}");
#else
                  file.Info.Delete();
#endif
                  if (settings.VerboseLogging)
                  {
                     LogResult(file, logger);
                  }
               }
               catch (Exception e)
               {
                  logger.Error(e, settings, "File Delete Failed...");
                  deleteErrors++;
               }
            }

            logger.Log("Deleted Files", "\tCount", Files.Count - deleteErrors, "\tErrors", deleteErrors);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public void LogResult(Logger logger) => logger.Log($"Deleted Files: ", Files.Count);

      public void LogResult(FileModel file, Logger logger) => logger.Log("Deleted File: ", "Name", file.Info.Name, "Ext", file.Info.Extension, "date", file.Info.LastWriteTime.ToString("MM/dd/yy"));
      #endregion

      #region - Full Properties
      #endregion
   }
}
