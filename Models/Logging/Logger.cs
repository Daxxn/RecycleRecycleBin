using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TrashCleaner.Models.Filtering;

namespace TrashCleaner.Models.Logging
{
   public class Logger
   {
      #region - Fields & Properties
      private List<string> OldLines { get; set; }
      private List<string> LogLines { get; set; }
      private int? LogLineLength => OldLines.Count;
      private readonly string DateFormat = "MM/dd/yy hh-mm-ss";
      #endregion

      #region - Constructors
      public Logger() => LogLines = new List<string>();
      #endregion

      #region - Methods
      public void LoadLog(SettingsModel settings)
      {
         if (settings != null)
         {
            if (File.Exists(settings.LoggerPath))
            {
               using (StreamReader reader = new StreamReader(settings.LoggerPath))
               {
                  OldLines = new List<string>();
                  while (!reader.EndOfStream)
                  {
                     OldLines.Add(reader.ReadLine());
                  }
               }
            }
            else
            {
               File.Create(settings.LoggerPath).Close();
            }
         }
      }

      public void SaveLog(SettingsModel settings)
      {
         if (LogLines.Count > 0)
         {
            if (!settings.VerboseLogging)
            {
               if (LogLineLength + LogLines.Count > settings.LoggerFileLength)
               {
                  for (int i = 0; i < LogLines.Count; i++)
                  {
                     OldLines.RemoveAt(i);
                  }
               }
            }

            using (StreamWriter writer = new StreamWriter(settings.LoggerPath, true))
            {
               foreach (var line in OldLines)
               {
                  writer.WriteLine(line);
               }
               foreach (var line in LogLines)
               {
                  writer.WriteLine(line);
               }
               writer.Flush();
            }
         }
      }

      public void Log(string input, params object[] args)
      {
         StringBuilder builder = new StringBuilder(input);
         builder.Append(" | ");
         builder.Append(String.Join(", ", args));
         builder.Append($" | {DateTime.Now.ToString(DateFormat)}");
         LogLines.Add(builder.ToString());
      }

      public void Log(string input, bool verbose, params object[] args)
      {
         if (verbose)
         {
            StringBuilder builder = new StringBuilder(input);
            builder.AppendLine(" | Args: ");
            foreach (var arg in args)
            {
               builder.AppendLine($"\t{arg.GetType().Name} : {arg}");
            }
            builder.AppendLine($"\t\t{DateTime.Now.ToString(DateFormat)}");
            LogLines.Add(builder.ToString());
         }
         else
         {
            Log(input, args);
         }
      }

      public void Error(Exception error, SettingsModel settings, string message = null)
      {
         StringBuilder builder = new StringBuilder();
         if (settings.VerboseLogging)
         {
            if (message != null)
            {
               builder.AppendLine(message);
            }
            builder.AppendLine($"Error: {error.GetType().Name}");
            builder.AppendLine($"\t{error.Message}");
            builder.AppendLine($"\tSource: {error.Source}");
            builder.AppendLine($"\tTarget: {error.TargetSite}");
            builder.AppendLine($"\t\t{DateTime.Now.ToString(DateFormat)}");
         }
         else
         {
            if (message != null)
            {
               builder.Append($"{message} : ");
            }
            builder.Append($"\tType: {error.GetType().Name}");
            builder.Append($"\tError Message: {error.Message}");
            builder.AppendLine($" | {DateTime.Now.ToString(DateFormat)}");
         }
      }
      #endregion

      #region - Full Properties
      #endregion
   }
}
