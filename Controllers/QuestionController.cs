using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuizManagement.Models;

namespace QuizManagement.Controllers
{
    [CheckAccess]
    public class QuestionController : Controller
    {
        private IConfiguration configuration;

        public QuestionController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }


        public IActionResult QuestionList()
        {
            QuestionLevelDropDown();
            QuestionDropDown();

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Question_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }


        //public IActionResult AddQuestion()
        //{
        //    QuestionLevelDropDown();
        //    UserDropDown();
        //    return View();
        //}

        public IActionResult AddQuestion(int? questionID)
        {

            QuestionLevelDropDown();
            UserDropDown();

            if (questionID == null)
            {
                var m = new QuestionModel
                {
                };
                return View(m);
            }

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Question_SelectByPK";
            command.Parameters.AddWithValue("@QuestionID", questionID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            QuestionModel model = new QuestionModel();

            foreach (DataRow dataRow in table.Rows)
            {
                model.QuestionText = dataRow["QuestionText"].ToString();
                model.QuestionLevelID = Convert.ToInt32(dataRow["QuestionLevelID"].ToString());
                model.OptionA = dataRow["OptionA"].ToString();
                model.OptionB = dataRow["OptionB"].ToString();
                model.OptionC = dataRow["OptionC"].ToString();
                model.OptionD = dataRow["OptionD"].ToString();
                model.CorrectOption = dataRow["CorrectOption"].ToString();
                model.QuestionMarks = Convert.ToInt32(dataRow["QuestionMarks"].ToString());
                model.UserID = Convert.ToInt32(dataRow["UserID"].ToString());
            }
            return View(model);
        }



        public IActionResult QuestionAddEdit(QuestionModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                if (model.QuestionID == 0)
                {
                    command.CommandText = "PR_MST_Question_Insert";
                }
                else
                {
                    command.CommandText = "PR_MST_Question_Update";
                    command.Parameters.Add("@QuestionID", SqlDbType.Int).Value = model.QuestionID;
                }
                command.Parameters.Add("@QuestionText", SqlDbType.NVarChar).Value = model.QuestionText;
                command.Parameters.Add("@QuestionLevelID", SqlDbType.Int).Value = model.QuestionLevelID;
                command.Parameters.Add("@OptionA", SqlDbType.NVarChar).Value = model.OptionA;
                command.Parameters.Add("@OptionB", SqlDbType.NVarChar).Value = model.OptionB;
                command.Parameters.Add("@OptionC", SqlDbType.NVarChar).Value = model.OptionC;
                command.Parameters.Add("@OptionD", SqlDbType.NVarChar).Value = model.OptionD;
                command.Parameters.Add("@CorrectOption", SqlDbType.VarChar).Value = model.CorrectOption;
                command.Parameters.Add("@QuestionMarks", SqlDbType.Int).Value = model.QuestionMarks;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                command.ExecuteNonQuery();
                return RedirectToAction("QuestionList");
            }

            return View("AddQuestion", model);
        }



        public IActionResult QuestionDelete(int QuestionID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_Question_Delete";
                    command.Parameters.Add("@QuestionID", SqlDbType.Int).Value = QuestionID;


                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "Question deleted successfully.";
                return RedirectToAction("QuestionList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the question: " + ex.Message;
                return RedirectToAction("QuestionList");
            }
        }


        public void QuestionLevelDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_MST_QuestionLevel_DropdownForQuestionLevel";
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);

            List<QuestionLevelDropDownModel> questionLevelList = new List<QuestionLevelDropDownModel>();
            
            foreach (DataRow data in dataTable2.Rows)
            {
                QuestionLevelDropDownModel model = new QuestionLevelDropDownModel();
                model.QuestionLevelID = Convert.ToInt32(data["QuestionLevelID"]);
                model.QuestionLevel = data["QuestionLevel"].ToString();
                questionLevelList.Add(model);
            }

            ViewBag.QuestionLevelList = questionLevelList.Count > 0 ? questionLevelList : new List<QuestionLevelDropDownModel>();
        }

        public void QuestionDropDown()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.CommandText = "PR_MST_Question_DropdownForQuestion";
            SqlDataReader reader2 = command2.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader2);

            List<QuestionDropDownModel> questionList = new List<QuestionDropDownModel>();

            foreach (DataRow data in dataTable2.Rows)
            {
                QuestionDropDownModel model = new QuestionDropDownModel();
                model.QuestionID = Convert.ToInt32(data["QuestionID"]);
                model.QuestionText = data["QuestionText"].ToString();
                questionList.Add(model);
            }

            ViewBag.QuestionList = questionList.Count > 0 ? questionList : new List<QuestionDropDownModel>();
        }

        public IActionResult QuestionDropdownAddEdit(QuestionModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("QuestionList");
            }
            QuestionLevelDropDown();
            UserDropDown();
            return View("AddQuestion", model);
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
            List<UserDropDownModel> userList = new List<UserDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                UserDropDownModel model = new UserDropDownModel();
                model.UserID = Convert.ToInt32(data["UserID"]);
                model.UserName = data["UserName"].ToString();
                userList.Add(model);
            }
            ViewBag.UserList = userList;
        }



        public IActionResult QuestionFilter(int? QuestionLevelID, int? QuestionID)
        {
            QuestionLevelDropDown();
            QuestionDropDown();

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_MST_Question_SelectByFilters";

                if (QuestionLevelID.HasValue && QuestionLevelID > 0)
                {
                    command.Parameters.Add("@QuestionLevelID", SqlDbType.Int).Value = QuestionLevelID.Value;
                }

                if (QuestionID.HasValue && QuestionID > 0)
                {
                    command.Parameters.Add("@QuestionID", SqlDbType.Int).Value = QuestionID.Value;
                }

                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                return View("QuestionList", dataTable);
            }
        }

    }
}
