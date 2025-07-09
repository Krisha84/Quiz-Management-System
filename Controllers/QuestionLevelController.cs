using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuizManagement.Models;
using System.Reflection;

namespace QuizManagement.Controllers
{
    [CheckAccess]
    public class QuestionLevelController : Controller
    {

        private IConfiguration configuration;

        public QuestionLevelController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }


        public IActionResult QuestionLevelList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_QestionLevel_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }



        public IActionResult AddQuestionLevel(int? questionLevelID)
        {
            UserDropDown();

            if (questionLevelID == null)
            {
                var m = new QuestionLevelModel
                {
                };
                return View(m);
            }

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_QestionLevel_SelectByPK";
            command.Parameters.AddWithValue("@QuestionLevelID", questionLevelID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            QuestionLevelModel model = new QuestionLevelModel();

            foreach (DataRow dataRow in table.Rows)
            {
                model.QuestionLevel = dataRow["QuestionLevel"].ToString();
                model.UserID = Convert.ToInt32(dataRow["UserID"]);
            }
            return View(model);
        }



        public IActionResult QuestionLevelAddEdit(QuestionLevelModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                if (model.QuestionLevelID == 0)
                {
                    command.CommandText = "PR_MST_QestionLevel_Insert";
                }
                else
                {
                    command.CommandText = "PR_MST_QestionLevel_Update";
                    command.Parameters.Add("@QuestionLevelID", SqlDbType.Int).Value = model.QuestionLevelID;
                }
                command.Parameters.Add("@QuestionLevel", SqlDbType.NVarChar).Value = model.QuestionLevel;
                command.Parameters.Add("@UserID", SqlDbType.VarChar).Value = model.UserID;
                command.ExecuteNonQuery();
                return RedirectToAction("QuestionLevelList");
            }

            return View("AddQuestionLevel", model);
        }



        public IActionResult QuestionLevelDelete(int QuestionLevelID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_QestionLevel_Delete";
                    command.Parameters.Add("@QuestionLevelID", SqlDbType.Int).Value = QuestionLevelID;


                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "Question Level deleted successfully.";
                return RedirectToAction("QuestionLevelList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the question level: " + ex.Message;
                return RedirectToAction("QuestionLevelList");
            }
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



        public IActionResult QuestionLevelDropdownAddEdit(QuestionLevelModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("QuestionLevelList");
            }
            UserDropDown();
            return View("AddQuestionLevel", model);
        }



        public IActionResult QuestionLevelWiseQuestionList(int QuestionLevelID)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_MST_Question_QuestionLevelWiseSelect";
                command.Parameters.Add("@QuestionLevelID", SqlDbType.Int).Value = QuestionLevelID;

                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);

                return View(table);
            }
        }

    }
}
