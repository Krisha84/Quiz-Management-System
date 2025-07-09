using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuizManagement.Models;

namespace QuizManagement.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserLoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult LoginPage()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(UserLoginModel userLoginModel)
        {
            if (string.IsNullOrEmpty(userLoginModel.UserName) || string.IsNullOrEmpty(userLoginModel.Password))
            {
                TempData["ErrorMessage"] = "Username and Password are required.";
                return RedirectToAction("LoginPage");
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input data.";
                return RedirectToAction("LoginPage");
            }

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("PR_MST_User_SelectByUsernamePassword", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", userLoginModel.UserName);
                    cmd.Parameters.AddWithValue("@Password", userLoginModel.Password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            TempData["ErrorMessage"] = "Invalid User Name or Password";
                            return RedirectToAction("LoginPage");
                        }

                        DataTable dtLogin = new DataTable();
                        dtLogin.Load(reader);

                        foreach (DataRow dr in dtLogin.Rows)
                        {
                            HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                            HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        }
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage");
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult UserAddEdit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this._configuration.GetConnectionString("ConnectionString");
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
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = model.UserID;
                }

                // command.CommandText = "PR_MST_USER_INSERT";
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = model.UserName;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = model.Password;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = model.Email;
                command.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = model.Mobile;
                //command.Parameters.Add("@isActive", SqlDbType.Bit).Value = model.isActive;
                //command.Parameters.Add("@isAdmin", SqlDbType.Bit).Value = model.isAdmin;



                command.ExecuteNonQuery();

                ////////////////////////////////////////////////

                SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("ConnectionString"));
                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();

                objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                objCmd.CommandText = "PR_MST_User_SelectByUsernamePassword";
                objCmd.Parameters.AddWithValue("@UserName", model.UserName);
                objCmd.Parameters.AddWithValue("@Password", model.Password);

                SqlDataReader objSDR = objCmd.ExecuteReader();

                DataTable dtLogin = new DataTable();

                // Check if Data Reader has Rows or not
                // if row(s) does not exists that means either username or password or both are invalid.
                if (!objSDR.HasRows)
                {
                    TempData["ErrorMessage"] = "Invalid User Name or Password";
                    return RedirectToAction("LoginPage", "UserLogin");
                }
                else
                {
                    dtLogin.Load(objSDR);

                    //Load the retrived data to session through HttpContext.
                    foreach (DataRow dr in dtLogin.Rows)
                    {
                        HttpContext.Session.SetInt32("UserID", Convert.ToInt32(dr["UserID"]));
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        //HttpContext.Session.SetString("MobileNo", dr["MobileNo"].ToString());
                        //HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                    }
                    return RedirectToAction("Index", "Home");
                }
                return View("Register", model);
            }
            return View("Register", model);
        }
    }
}
