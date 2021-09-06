using System.IO;

namespace TrashCleaner.Models.FileControl
{
   public class FileModel
   {
      #region - Fields & Properties
      public string FullPath { get; private set; }
      public FileInfo Info { get; private set; }
      #endregion

      #region - Constructors
      public FileModel(string path)
      {
         FullPath = path;
         if (File.Exists(path))
         {
            Info = new FileInfo(path);
         }
      }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties

      #endregion
   }
}
