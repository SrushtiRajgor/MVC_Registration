using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Registration1.Models;
using System.IO;
namespace Registration1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["LoggedInUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }      
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Register(Entities model)
        {
            if (ModelState.IsValid && model != null)
            {
                return Json(new { Success = true, message = "Registration successful!" });
            }

            else

            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();
                return Json(new { Success = false, message = "Invalid data submitted", errors });

            }
        }
        public JsonResult SelectEmployeeList()
        {
            List<Entities> lstEntities = new List<Entities>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("Employee_select", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            Entities objEntities = new Entities();

                            objEntities.Id = rdr["Id"] != DBNull.Value ? Convert.ToInt32(rdr["Id"]) : 0;
                            objEntities.FirstName = rdr["FirstName"] != DBNull.Value ? Convert.ToString(rdr["FirstName"]) : string.Empty;
                            objEntities.LastName = rdr["LastName"] != DBNull.Value ? Convert.ToString(rdr["LastName"]) : string.Empty;
                            objEntities.Email = rdr["Email"] != DBNull.Value ? Convert.ToString(rdr["Email"]) : string.Empty;
                            objEntities.Birthdate = rdr["Birthdate"] != DBNull.Value ? Convert.ToDateTime(rdr["Birthdate"]).ToString("dd-MM-yyyy") : string.Empty;
                            objEntities.PhoneNumber = rdr["PhoneNumber"] != DBNull.Value ? Convert.ToString(rdr["PhoneNumber"]) : string.Empty;
                            objEntities.Address = rdr["Address"] != DBNull.Value ? Convert.ToString(rdr["Address"]) : string.Empty;
                            objEntities.City = rdr["City"] != DBNull.Value ? Convert.ToString(rdr["City"]) : string.Empty;
                            objEntities.State = rdr["State"] != DBNull.Value ? Convert.ToString(rdr["State"]) : string.Empty;
                            objEntities.Password = rdr["Password"] != DBNull.Value ? Convert.ToString(rdr["Password"]) : string.Empty;

                            lstEntities.Add(objEntities);
                        }

                    }
                }
                        return new JsonResult
                        {
                            Data = lstEntities,
                            MaxJsonLength = int.MaxValue,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
       
    catch (Exception ex)
            {
                return new JsonResult
                {
                    Data = new { Success = false, Data = lstEntities, message = ex.Message },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet 
                };
            }
        }
        [HttpPost]
        public ActionResult SaveEmployeeData(Entities model)
        {
            try
            {
            //    if (ModelState.IsValid)
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("Employee_Insert", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Name", model.FirstName);
                            cmd.Parameters.AddWithValue("@LName", model.LastName);
                            cmd.Parameters.AddWithValue("@EmailID", model.Email);
                            cmd.Parameters.AddWithValue("@Birthdate", model.Birthdate);
                            cmd.Parameters.AddWithValue("@Mobileno", model.PhoneNumber);
                            cmd.Parameters.AddWithValue("@Address", model.Address);
                            cmd.Parameters.AddWithValue("@City", model.City);
                            cmd.Parameters.AddWithValue("@State", model.State);
                            cmd.Parameters.AddWithValue("@Password", model.Password);
                            //cmd.Parameters.AddWithValue("@ImagePath", model.ImagePath);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                //else
                //{
                //    var errors = ModelState.Values.SelectMany(v => v.Errors)
                //                          .Select(e => e.ErrorMessage)
                //                          .ToList();
                //    return Json(new { Success = false, message = "Invalid data submitted!!!!!", errors });

                //}
                return View("Index","Home");
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult UpdateEmployee(Entities employee)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("Employee_Update", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", employee.Id);
                        cmd.Parameters.AddWithValue("@Name", employee.FirstName);
                        cmd.Parameters.AddWithValue("@LName", employee.LastName);
                        cmd.Parameters.AddWithValue("@EmailID", employee.Email);
                        cmd.Parameters.AddWithValue("@Birthdate", employee.Birthdate);
                        cmd.Parameters.AddWithValue("@Mobileno", employee.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Address", employee.Address);
                        cmd.Parameters.AddWithValue("@City", employee.City);
                        cmd.Parameters.AddWithValue("@State", employee.State);
                        cmd.Parameters.AddWithValue("@Password", employee.Password);
                        cmd.Parameters.AddWithValue("@ConfirmPassword", employee.ConfirmPassword);
                        con.Open();
                        cmd.ExecuteNonQuery();
                       
                    }
                }
                bool isLoggedIn = Session["LoggedInUser"]  != null;
                return Json(new { Success = true, message = "Employee updated successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteEmployee(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("Employee_Delete", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        return Json(new { Success = true, message = "Employee deleted successfully!" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, message = ex.Message });
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateLogin(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ViewBag.ErrorMessage = "Email and Password are required!";
                return View("Login");
            }

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand("Employee_Validate_Login", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmailID", Email);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        con.Open();
                        object result = cmd.ExecuteScalar();
                        int flag = result != null ? Convert.ToInt32(result) : 0;
                        if (flag == 1)
                        {
                            Session["LoggedInUser"] = Email;
                            Session["Name"] = "ABC";
                            //Session.RemoveAll();
                            return RedirectToAction("Index", "Home");

                        }
                        else
                        {
                            ViewBag.ErrorMessage = ("Invalid login credentials.");
                            return View("Login");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"]  = "Login failed.Error - " + ex.Message;
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();

            return RedirectToAction("Login");

        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    string relativePath = "";

                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fileName = Path.GetFileName(file.FileName);

                        string folderPath = Server.MapPath("~/Images/");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        string filePath = Path.Combine(folderPath, fileName);
                        file.SaveAs(filePath);

                        relativePath = "~/Images/" + fileName;
                    }

                    return Json(new { Success = true, ImagePath = relativePath });
                }
                catch (Exception ex)
                {
                    return Json("Error occured. Error detail: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

    }
}