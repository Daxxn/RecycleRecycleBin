using System;
using System.Collections.Generic;

namespace TrashCleaner.Models.Filtering
{
   public class SettingsModel
   {
      #region - Fields & Properties
      public List<string> DriveList { get; set; }
      public List<string> ExtensionList { get; set; }
      public int RemoveExtensionDays { get; set; }
      public int RemoveAnyFileDays { get; set; }
      public string UserSid { get; set; }
      public int LoggerFileLength { get; set; }
      public string LoggerPath { get; set; }
      public bool ActivateLog { get; set; }
      public int RunInterval { get; set; }
      public bool VerboseLogging { get; set; }

      public DateTime RemoveExtensionDateTime => DateTime.Now.AddDays(-RemoveExtensionDays);
      public DateTime RemoveAnyFileDateTime => DateTime.Now.AddDays(-RemoveAnyFileDays);
      #endregion
   }
}
