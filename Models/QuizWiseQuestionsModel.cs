using System.ComponentModel.DataAnnotations;

namespace QuizManagement.Models
{
    public class QuizWiseQuestionsModel
    {
        public int QuizWiseQuestionsID { get; set; }

        [Required]
        public int QuizID { get; set; }

        [Required]
        public int QuestionID { get; set; }

        [Required]
        public int UserID { get; set; }

        //public string? UserName { get; set; }

        //public string? QuizName { get; set; }

        //public string? QuestionText { get; set; }

        //public int TotalQuestions { get; set; }
    }
}
