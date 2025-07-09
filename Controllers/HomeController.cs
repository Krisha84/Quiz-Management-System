using System.Diagnostics;
using QuizManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace QuizManagement.Controllers
{
    [CheckAccess]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        #region configuration
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        public IActionResult Index()
        {
            string connectionString = _configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            HomeModel model = new HomeModel();

            SqlCommand cmdQuiz = connection.CreateCommand();
            cmdQuiz.CommandType = CommandType.StoredProcedure;
            cmdQuiz.CommandText = "PR_MST_Quiz_Count";
            model.TotalQuizes = Convert.ToInt32(cmdQuiz.ExecuteScalar());

            SqlCommand cmdQuestion = connection.CreateCommand();
            cmdQuestion.CommandType = CommandType.StoredProcedure;
            cmdQuestion.CommandText = "PR_MST_Question_Count";
            model.TotalQuestions= Convert.ToInt32(cmdQuestion.ExecuteScalar());

            SqlCommand cmdQuestionLevel = connection.CreateCommand();
            cmdQuestionLevel.CommandType = CommandType.StoredProcedure;
            cmdQuestionLevel.CommandText = "PR_MST_QuestionLevel_Count";
            model.TotalQuestionLevels = Convert.ToInt32(cmdQuestionLevel.ExecuteScalar());

            SqlCommand cmdEasy = connection.CreateCommand();
            cmdEasy.CommandType = CommandType.StoredProcedure;
            cmdEasy.CommandText = "PR_MST_Question_CountEasy";
            model.Easy = Convert.ToInt32(cmdEasy.ExecuteScalar());

            SqlCommand cmdMedium = connection.CreateCommand();
            cmdMedium.CommandType = CommandType.StoredProcedure;
            cmdMedium.CommandText = "PR_MST_Question_CountMedium";
            model.Medium = Convert.ToInt32(cmdMedium.ExecuteScalar());

            SqlCommand cmdHard = connection.CreateCommand();
            cmdHard.CommandType = CommandType.StoredProcedure;
            cmdHard.CommandText = "PR_MST_Question_CountHard";
            model.Hard = Convert.ToInt32(cmdHard.ExecuteScalar());

            connection.Close();

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
