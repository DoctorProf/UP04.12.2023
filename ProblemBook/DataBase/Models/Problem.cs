using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProblemBook.DataBase.Models
{
    public class Problem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string CreateDate { get; set; } = "";

        public string ShortName { get; set; } = "";

        public string Tags { get; set; } = "";

        public string FullDescription { get; set; }

        public string PlannedDate { get; set; } = "";

        public string DaysLeft
        {
            get
            {
                if (DateTime.TryParse(PlannedDate, out DateTime plannedDate))
                {
                    TimeSpan timeSpan = plannedDate - DateTime.Now;
                    int daysLeft = int.Max(timeSpan.Days, 0);
                    return daysLeft.ToString();
                }
                return "-";
            }
            set 
            {

            }
        }
        public string DateСompletion { get; set; } = "";

        public ProblemType Type { get; set; }

        public Problem() { }
    }
}
