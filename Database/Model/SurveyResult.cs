using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    public class SurveyResult
    {
        public int Id { get; set; }
        public int formID { get; set; }
        public string Result { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
