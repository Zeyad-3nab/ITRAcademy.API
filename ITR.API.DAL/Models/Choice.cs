using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Models
{
    public class Choice
    {
         public int Id { get; set; }
         public string Text { get; set; } = string.Empty;
         public bool IsCorrect { get; set; }
    }
}
