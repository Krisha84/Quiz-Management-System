using System.ComponentModel.DataAnnotations;

namespace QuizManagement.Models
{
    public class QuizModel
    {
        public int QuizID { get; set; }

        [Required]
        public string QuizName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total Questions must be at least 1.")]
        public int TotalQuestions { get; set; }

        [Required]
        public DateTime QuizDate { get; set; }

        [Required]
        public int UserID { get; set; }
    }

    public class QuizDropDownModel
    {
        public int QuizID { get; set; }
        public string QuizName { get; set; }
    }
}
