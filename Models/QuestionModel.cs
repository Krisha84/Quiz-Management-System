using System.ComponentModel.DataAnnotations;

namespace QuizManagement.Models
{
    public class QuestionModel
    {
        public int QuestionID { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public int QuestionLevelID { get; set; }

        public string? QuestionLevel { get; set; }

        [Required]
        public string OptionA { get; set; }

        [Required]
        public string OptionB { get; set; }

        [Required]
        public string OptionC { get; set; }

        [Required]
        public string OptionD { get; set; }

        [Required]
        public string CorrectOption { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Marks should be between 1 and 100")]
        public int QuestionMarks { get; set; }

        [Required]
        public int UserID { get; set; }
    }

    public class QuestionDropDownModel
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
    }
}
