using System.ComponentModel.DataAnnotations;

namespace QuizManagement.Models
{
    public class QuestionLevelModel
    {
        public int QuestionLevelID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Question Level must be between 3 and 100 characters.", MinimumLength = 3)]
        public string QuestionLevel { get; set; }

        [Required]
        public int UserID { get; set; }

    }

    public class QuestionLevelDropDownModel
    {
        public int QuestionLevelID { get; set; }
        public string QuestionLevel { get; set; }
    }
}
