using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashCleaner.Models
{
   public interface ILoader
   {
      void Start();
      void Stop();
   }
}
