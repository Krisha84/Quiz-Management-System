using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuizManagement.Models;

namespace QuizManagement.Controllers
{
    [CheckAccess]
    public class QuizWiseQuestionsController : Controller
    {
        private IConfiguration configuration;

        public QuizWiseQuestionsController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }


        public IActionResult QuizWiseQuestionsList()
        {
            QuizDropDown(); 

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_QuizWiseQuestions_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);

            return View(table);
        }



        public IActionResult AddQuizWiseQuestions(int? quizWiseQuestionsID)
        {
            QuizDropDown();
            QuestionDropDown();
            UserDropDown();

            if (quizWiseQuestionsID == null)
            {
                var model = new QuizWiseQuestionsModel(); 
                return View(model);
            }

            string connectionString = configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_MST_QuizWiseQuestions_SelectByPK";
                command.Parameters.AddWithValue("@QuizWiseQuestionsID", quizWiseQuestionsID);
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                QuizWiseQuestionsModel model = new QuizWiseQuestionsModel();
                foreach (DataRow dataRow in table.Rows)
                {
                    model.QuizWiseQuestionsID = quizWiseQuestionsID.Value;
                    model.QuizID = Convert.ToInt32(dataRow["QuizID"]);
                    model.QuestionID = Convert.ToInt32(dataRow["QuestionID"]);
                    model.UserID = Convert.ToInt32(dataRow["UserID"]);
                }
                return View(model);
            }
        }





        public IActionResult QuizWiseQuestionsAddEdit(QuizWiseQuestionsModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                if (model.QuizWiseQuestionsID == 0)
                {
                    command.CommandText = "PR_MST_QuizWiseQuestions_Insert";
                }
                else
                {
                    command.CommandText = "PR_MST_QuizWiseQuestions_Update";
                    command.Parameters.Add("@QuizWiseQuestionsID", SqlDbType.Int).Value = model.QuizWiseQuestionsID;
                }
                command.Parameters.Add("@QuizID", SqlDbType.VarChar).Value = model.QuizID;
                command.Parameters.Add("@QuestionID", SqlDbType.VarChar).Value = model.QuestionID;
                command.Parameters.Add("@UserID", SqlDbType.VarChar).Value = model.UserID;
                command.ExecuteNonQuery();
                return RedirectToAction("QuizWiseQuestionsList");
            }

            return View("AddQuizWiseQuestions", model);
        }



        public IActionResult QuizWiseQuestionsDelete(int QuizWiseQuestionsID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_QuizWiseQuestions_Delete";
                    command.Parameters.Add("@QuizWiseQuestionsID", SqlDbType.Int).Value = QuizWiseQuestionsID;

                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "Quiz Wise Questions deleted successfully.";
                return RedirectToAction("QuizWiseQuestionsList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the quiz wise questions: " + ex.Message;
                return RedirectToAction("QuizWiseQuestionsList");
            }
        }

        public IActionResult QuizWiseQuestionsDetails()
        {
            return View();
        }



        public void QuizDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = CommandType.StoredProcedure;
            command2.CommandText = "PR_MST_Quiz_DropdownForQuiz";
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);

            List<QuizDropDownModel> QuizList = new List<QuizDropDownModel>();

            foreach (DataRow data in dataTable2.Rows)
            {
                QuizDropDownModel model = new QuizDropDownModel();
                model.QuizID = Convert.ToInt32(data["QuizID"]);
                model.QuizName = data["QuizName"].ToString();
                QuizList.Add(model);
            }

            ViewBag.QuizList = QuizList.Count > 0 ? QuizList : new List<QuizDropDownModel>(); // Always assign a list
        }


        public void QuestionDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_MST_question_DropdownForQuestion";
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<QuestionDropDownModel> QuestionList = new List<QuestionDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                QuestionDropDownModel model = new QuestionDropDownModel();
                model.QuestionID = Convert.ToInt32(data["QuestionID"]);
                model.QuestionText = data["QuestionText"].ToString();
                QuestionList.Add(model);
            }
            ViewBag.QuestionList = QuestionList;
        }
        


        public void UserDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_MST_User_DropdownForUser";
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);
            List<UserDropDownModel> UserList = new List<UserDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                UserDropDownModel model = new UserDropDownModel();
                model.UserID = Convert.ToInt32(data["UserID"]);
                model.UserName = data["UserName"].ToString();
                UserList.Add(model);
            }
            ViewBag.UserList = UserList;
        }



        public IActionResult QuizWiseQuestionsDropDownAddEdit(QuizWiseQuestionsModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("QuizWiseQuestionsList");
            }

            QuizDropDown();
            QuestionDropDown();
            UserDropDown();
            return View("AddQuizWiseQuestions", model);
        }



        public IActionResult QuizWiseQuestionsFilter(int? QuizID)
        {
            QuizDropDown();

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_MST_QuizWiseQuestions_SelectByQuizID";

                if (QuizID.HasValue && QuizID > 0)
                {
                    command.Parameters.Add("@QuizID", SqlDbType.Int).Value = QuizID.Value;
                }

                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                return View("QuizWiseQuestionsList", dataTable);
            }
        }
    }
}
