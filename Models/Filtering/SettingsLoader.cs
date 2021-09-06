using JsonReaderLibrary;
using System;
using System.IO;

namespace TrashCleaner.Models.Filtering
{
   public class SettingsLoader
   {
      #region - Fields & Properties
      public SettingsModel Settings { get; set; }
      private string FilterSettingsPath { get; set; }
      #endregion

      #region - Constructors
      public SettingsLoader() { }
      #endregion

      #region - Methods
      public SettingsModel ReadSettings()
      {
         try
         {
            FilterSettingsPath = Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
               "RecycleBinCleaner",
               "Options.json"
            );
            return JsonReader.OpenJsonFile<SettingsModel>(FilterSettingsPath);
         }
         catch (Exception)
         {
            throw;
         }
      }
      #endregion

      #region - Full Properties
      #endregion
   }
}
