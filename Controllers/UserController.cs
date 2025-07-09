using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using QuizManagement.Models;

namespace QuizManagement.Controllers
{
    [CheckAccess]
    public class UserController : Controller
    {
        private IConfiguration configuration;

        public UserController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }


        public IActionResult UserList()
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_User_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }



       public IActionResult AddUser(int? userID)
        {
            if (userID == null)
            {
                var m = new UserModel
                {
                };
                return View(m);
            }

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_MST_User_SelectByPK";
            command.Parameters.AddWithValue("@UserID", userID);
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            UserModel model = new UserModel();

            foreach (DataRow dataRow in table.Rows)
            {
                model.UserName = dataRow["UserName"].ToString();
                model.Password = dataRow["Password"].ToString();
                model.Email = dataRow["Email"].ToString();
                model.Mobile = dataRow["Mobile"].ToString();
            }
            return View(model);
        }



        public IActionResult UserDelete(int UserID)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_MST_User_Delete";
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "User deleted successfully!";
                return RedirectToAction("UserList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the user: " + ex.Message;
                return RedirectToAction("UserList");
            }
        }





        public IActionResult UserAddEdit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;

                if (model.UserID == 0)
                {
                    command.CommandText = "PR_MST_User_Insert";
                }
                else
                {
                    command.CommandText = "PR_MST_User_Update";
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = model.UserID;
                }
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = model.UserName;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = model.Password;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = model.Email;
                command.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = model.Mobile;
                command.ExecuteNonQuery();
                return RedirectToAction("UserList");
            }

            return View("AddUser", model);
        }




        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 

            string connectionString = configuration.GetConnectionString("ConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "PR_MST_User_SelectAll";

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(sqlDataReader);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DataSheet");

                worksheet.Cells[1, 1].Value = "UserID";
                worksheet.Cells[1, 2].Value = "UserName";
                worksheet.Cells[1, 3].Value = "Password";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "MobileNo";

                int row = 2;
                foreach (DataRow item in data.Rows)
                {
                    worksheet.Cells[row, 1].Value = item["UserID"];
                    worksheet.Cells[row, 2].Value = item["UserName"];
                    worksheet.Cells[row, 3].Value = item["Password"];
                    worksheet.Cells[row, 4].Value = item["Email"];
                    worksheet.Cells[row, 5].Value = item["Mobile"];
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

