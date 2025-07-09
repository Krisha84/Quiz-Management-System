using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuizManagement.Models;
using System.Reflection.Metadata.Ecma335;
using OfficeOpenXml;

namespace QuizManagement.Controllers
{
    [CheckAccess]
    public class QuizController : Controller
    {
        private IConfiguration configuration;

        public QuizController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IActionResult QuizList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Quiz_SelectAll";
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = CommonVariables.UserID();
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }


        public IActionResult AddQuiz(int? quizID)
        {
            UserDropDown();

            if (quizID == null)
            {
                var m = new QuizModel
                {
                };
                return View(m);
            }

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_Quiz_SelectByPK";
            command.Parameters.AddWithValue("@QuizID", quizID);
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = CommonVariables.UserID();
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            QuizModel model = new QuizModel();

            foreach (DataRow dataRow in table.Rows)
            {
                model.QuizName = dataRow["QuizName"].ToString();
                model.TotalQuestions = Convert.ToInt32(dataRow["TotalQuestions"]);
                model.QuizDate = Convert.ToDateTime(dataRow["QuizDate"]);
                model.UserID = Convert.ToInt32(dataRow["UserID"]);
            }
            return View(model);
        }


        public IActionResult QuizAddEdit(QuizModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                if (model.QuizID == 0)
                {
                    command.CommandText = "PR_MST_Quiz_Insert";
                }
                else
                {
                    command.CommandText = "PR_MST_Quiz_Update";
                    command.Parameters.Add("@QuizID", SqlDbType.Int).Value = model.QuizID;
                }
                command.Parameters.Add("@QuizName", SqlDbType.NVarChar).Value = model.QuizName;
                command.Parameters.Add("@TotalQuestions", SqlDbType.Int).Value = model.TotalQuestions;
                command.Parameters.Add("@QuizDate", SqlDbType.DateTime).Value = model.QuizDate;
                //command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = CommonVariables.UserID();
                command.ExecuteNonQuery();
                return RedirectToAction("QuizList");
            }

            return View("AddQuiz", model);
        }


        public IActionResult QuizDelete(int QuizID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_Quiz_Delete";
                    command.Parameters.Add("@QuizID", SqlDbType.Int).Value = QuizID;
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = CommonVariables.UserID();  


                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "Quiz deleted successfully.";
                return RedirectToAction("QuizList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the quiz: " + ex.Message;
                return RedirectToAction("QuizList");
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

        public IActionResult QuizDropDownAddEdit(QuizModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("QuizList");
            }
            UserDropDown();
            return View("AddQuiz", model);
        }



        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // For non-commercial use

            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_MST_Quiz_SelectAll";

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(sqlDataReader);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Add this line

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DataSheet");

                // Add headers
                worksheet.Cells[1, 1].Value = "QuizID";
                worksheet.Cells[1, 2].Value = "QuizName";
                worksheet.Cells[1, 3].Value = "TotalQuestions";
                worksheet.Cells[1, 4].Value = "QuizDate";
                worksheet.Cells[1, 5].Value = "UserID";

                // Add data
                int row = 2;
                foreach (DataRow item in data.Rows)
                {
                    worksheet.Cells[row, 1].Value = item["QuizID"];
                    worksheet.Cells[row, 2].Value = item["QuizName"];
                    worksheet.Cells[row, 3].Value = item["TotalQuestions"];
                    worksheet.Cells[row, 4].Value = item["QuizDate"];
                    worksheet.Cells[row, 5].Value = item["UserID"];
                    row++;
                }
                
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"Data-{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }
    }
}
