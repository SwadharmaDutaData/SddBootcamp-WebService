using Microsoft.AspNetCore.Mvc;
using SDD_Bootcamp.RestApi.ApiHelpers;
using SDD_Bootcamp.RestApi.Models;
using SDD_Bootcamp.RestApi.Models.Request;
using SDD_Bootcamp.RestApi.Models.Response;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SDD_Bootcamp.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<List<TblUserModel>>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<List<TblUserModel>>>))]
        public ResponseBodyModel<ResponseContentBodyModel<List<TblUserModel>>> GetAllUsers()
        {
            ResponseBodyModel<ResponseContentBodyModel<List<TblUserModel>>> resultList = new ResponseBodyModel<ResponseContentBodyModel<List<TblUserModel>>>();
            ResponseContentBodyModel<List<TblUserModel>> responseContent = new ResponseContentBodyModel<List<TblUserModel>>();
            try
            {
                List<TblUserModel> listUsers = new List<TblUserModel>();
                SqlConnection connection = DatabaseConfig.GetOpenConnection();
                SqlCommand cmd = DatabaseConfig.GetCommand("SELECT * FROM TblUser", connection, CommandType.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        object? userId = reader["UserId"] == DBNull.Value ? null : reader["UserId"];
                        object? firstname = reader["Firstname"] == DBNull.Value ? null : reader["Firstname"];
                        object? lastname = reader["Lastname"] == DBNull.Value ? null : reader["Lastname"];
                        object? username = reader["Username"] == DBNull.Value ? null : reader["Username"];
                        object? emailAddress = reader["EmailAddress"] == DBNull.Value ? null : reader["EmailAddress"];
                        object? password = reader["Password"] == DBNull.Value ? null : reader["Password"];
                        object? isActive = reader["IsActive"] == DBNull.Value ? null : reader["IsActive"];
                        object? createdDate = reader["CreatedDate"] == DBNull.Value ? null : reader["CreatedDate"];

                        TblUserModel model = new TblUserModel();
                        model.UserId = Convert.ToInt32(userId);
                        model.Firstname = firstname.ToString();
                        model.Lastname = lastname.ToString();
                        model.Username = username.ToString();
                        model.EmailAddress = emailAddress.ToString();
                        model.Password = password.ToString();
                        model.IsActive = Convert.ToBoolean(isActive);
                        model.CreatedDate = Convert.ToDateTime(createdDate);

                        listUsers.Add(model);
                    }
                }

                if (listUsers != null && listUsers.Count > 0)
                {
                    reader.Close();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    responseContent.Content = listUsers;

                    resultList.ResponseType = OperationType.CollectData.ToString();
                    resultList.ResponseMessage = $"Collect All Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                    resultList.ResponseBody = responseContent;
                }
                else
                {
                    responseContent.Content = null;

                    resultList.ErrorCode = 17;
                    resultList.ErrorMessage = "Data Not Found";
                    resultList.ErrorLink = null;
                    resultList.ResponseType = OperationType.CollectData.ToString(); ;
                    resultList.ResponseMessage = $"Failed Collect All Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                    resultList.ResponseBody = responseContent;
                }
            }
            catch (Exception ex)
            {
                responseContent.Content = null;

                resultList.ErrorCode = 999;
                resultList.ErrorMessage = ex.Message + "\r\n" + ex.StackTrace;
                resultList.ErrorLink = ex.HelpLink;
                resultList.ResponseType = OperationType.CollectData.ToString(); ;
                resultList.ResponseMessage = $"Failed Collect All Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                resultList.ResponseBody = responseContent;
            }

            return resultList;
        }

        // GET api/<UserController>/5
        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<TblUserModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<TblUserModel>>))]
        public ResponseBodyModel<ResponseContentBodyModel<TblUserModel>> GetUserById(int id)
        {
            ResponseBodyModel<ResponseContentBodyModel<TblUserModel>> resultList = new ResponseBodyModel<ResponseContentBodyModel<TblUserModel>>();
            ResponseContentBodyModel<TblUserModel> responseContent = new ResponseContentBodyModel<TblUserModel>();
            RequestBodyModel<int> request = new RequestBodyModel<int>();
            request.OperationType = OperationType.RetriveData.ToString();
            request.OperationDesc = $"Retrive Data {nameof(TblUserModel)}";
            request.Request = id;

            try
            {
                TblUserModel jurusan = new TblUserModel();
                SqlConnection connection = DatabaseConfig.GetOpenConnection();
                SqlCommand cmd = DatabaseConfig.GetCommand($"SELECT * FROM TblUser WHERE UserId = {request.Request}", connection, CommandType.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() && reader.HasRows)
                {
                    object? userId = reader["UserId"] == DBNull.Value ? null : reader["UserId"];
                    object? firstname = reader["Firstname"] == DBNull.Value ? null : reader["Firstname"];
                    object? lastname = reader["Lastname"] == DBNull.Value ? null : reader["Lastname"];
                    object? username = reader["Username"] == DBNull.Value ? null : reader["Username"];
                    object? emailAddress = reader["EmailAddress"] == DBNull.Value ? null : reader["EmailAddress"];
                    object? password = reader["Password"] == DBNull.Value ? null : reader["Password"];
                    object? isActive = reader["IsActive"] == DBNull.Value ? null : reader["IsActive"];
                    object? createdDate = reader["CreatedDate"] == DBNull.Value ? null : reader["CreatedDate"];

                    TblUserModel model = new TblUserModel();
                    model.UserId = Convert.ToInt32(userId);
                    model.Firstname = firstname.ToString();
                    model.Lastname = lastname.ToString();
                    model.Username = username.ToString();
                    model.EmailAddress = emailAddress.ToString();
                    model.Password = password.ToString();
                    model.IsActive = Convert.ToBoolean(isActive);
                    model.CreatedDate = Convert.ToDateTime(createdDate);

                    responseContent.Content = model;
                }

                if (responseContent != null && responseContent.Content != null)
                {
                    resultList.ResponseType = OperationType.RetriveData.ToString();
                    resultList.ResponseMessage = $"Success Retrive Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                    resultList.ResponseBody = responseContent;
                }
                else
                {
                    responseContent.Content = null;

                    resultList.ErrorCode = 17;
                    resultList.ErrorMessage = "Data Not Found";
                    resultList.ErrorLink = null;
                    resultList.ResponseType = OperationType.RetriveData.ToString();
                    resultList.ResponseMessage = $"Failed Retrive Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                    resultList.ResponseBody = responseContent;
                }
            }
            catch (Exception ex)
            {
                responseContent.Content = null;

                resultList.ErrorCode = 999;
                resultList.ErrorMessage = ex.Message + "\r\n" + ex.StackTrace;
                resultList.ErrorLink = ex.HelpLink;
                resultList.ResponseType = OperationType.RetriveData.ToString();
                resultList.ResponseMessage = $"Failed Retrive Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                resultList.ResponseBody = responseContent;
            }

            return resultList;
        }

        // POST api/<UserController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        public IActionResult PostUers([FromBody] RequestBodyModel<ResponseContentBodyModel<TblUserModel>> request)
        {
            ResponseBodyModel<ResponseContentBodyModel<string>> resultContent = new ResponseBodyModel<ResponseContentBodyModel<string>>();
            ResponseContentBodyModel<string> responseContent = new ResponseContentBodyModel<string>();

            try
            {
                SqlConnection connection = DatabaseConfig.GetOpenConnection();
                SqlCommand cmd = DatabaseConfig.GetCommand(@$"INSERT INTO [dbo].[TblUser]
                                                                ([Firstname]
                                                                ,[Lastname]
                                                                ,[Username]
                                                                ,[EmailAddress]
                                                                ,[Password]
                                                                ,[IsActive]
                                                                ,[CreatedDate])
                                                            VALUES
                                                                ('{request.Request.Content.Firstname}'
                                                                ,'{request.Request.Content.Lastname}'
                                                                ,'{request.Request.Content.Username}'
                                                                ,'{request.Request.Content.EmailAddress}'
                                                                ,'{request.Request.Content.Password}'
                                                                ,'{(request.Request.Content.IsActive == true ? 1 : 0)}'
                                                                ,'{DateTime.Now.ToString()}')", connection, CommandType.Text);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    responseContent.Content = "Success";

                    resultContent.ResponseType = OperationType.AddData.ToString();
                    resultContent.ResponseMessage = $"Success Adding Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                    resultContent.ResponseBody = responseContent;

                    return StatusCode(201, resultContent);
                }
                else
                {
                    responseContent.Content = "Failed";

                    resultContent.ErrorCode = 18;
                    resultContent.ErrorMessage = "Failed Adding Data User";
                    resultContent.ErrorLink = null;
                    resultContent.ResponseType = OperationType.AddData.ToString();
                    resultContent.ResponseMessage = $"Failed Adding Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                    resultContent.ResponseBody = responseContent;

                    return BadRequest(resultContent);
                }
            }
            catch (Exception ex)
            {
                responseContent.Content = "Failed";

                resultContent.ErrorCode = 999;
                resultContent.ErrorMessage = ex.Message + "\r\n" + ex.StackTrace;
                resultContent.ErrorLink = ex.HelpLink;
                resultContent.ResponseType = OperationType.AddData.ToString();
                resultContent.ResponseMessage = $"Failed Adding Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                resultContent.ResponseBody = responseContent;

                return BadRequest(resultContent);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        public IActionResult PutUser(int id, [FromBody] RequestBodyModel<ResponseContentBodyModel<TblUserModel>> request)
        {
            ResponseBodyModel<ResponseContentBodyModel<string>> resultContent = new ResponseBodyModel<ResponseContentBodyModel<string>>();
            ResponseContentBodyModel<string> responseContent = new ResponseContentBodyModel<string>();

            try
            {
                TblUserModel user = new TblUserModel();
                if (id == 0)
                    throw new ArgumentException($"Id Sama Dengan Nol, Id Tidak boleh Nol {nameof(id)}");
                else
                {
                    SqlConnection connection = DatabaseConfig.GetOpenConnection();
                    SqlCommand cmd = DatabaseConfig.GetCommand($"SELECT * FROM TblUser WHERE UserId = {request.Request}", connection, CommandType.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() && reader.HasRows)
                    {
                        object? userId = reader["UserId"] == DBNull.Value ? null : reader["UserId"];
                        object? firstname = reader["Firstname"] == DBNull.Value ? null : reader["Firstname"];
                        object? lastname = reader["Lastname"] == DBNull.Value ? null : reader["Lastname"];
                        object? username = reader["Username"] == DBNull.Value ? null : reader["Username"];
                        object? emailAddress = reader["EmailAddress"] == DBNull.Value ? null : reader["EmailAddress"];
                        object? password = reader["Password"] == DBNull.Value ? null : reader["Password"];
                        object? isActive = reader["IsActive"] == DBNull.Value ? null : reader["IsActive"];
                        object? createdDate = reader["CreatedDate"] == DBNull.Value ? null : reader["CreatedDate"];

                        TblUserModel model = new TblUserModel();
                        model.UserId = Convert.ToInt32(userId);
                        model.Firstname = firstname.ToString();
                        model.Lastname = lastname.ToString();
                        model.Username = username.ToString();
                        model.EmailAddress = emailAddress.ToString();
                        model.Password = password.ToString();
                        model.IsActive = Convert.ToBoolean(isActive);
                        model.CreatedDate = Convert.ToDateTime(createdDate);
                    }
                }

                if (user != null && user.UserId != 0)
                {
                    SqlConnection connection = DatabaseConfig.GetOpenConnection();
                    SqlCommand cmd = DatabaseConfig.GetCommand(@$"UPDATE [dbo].[TblUser]
                                                                   SET [Firstname] = '{request.Request.Content.Firstname}'
                                                                      ,[Lastname] = '{request.Request.Content.Lastname}'
                                                                      ,[Username] = '{request.Request.Content.Username}'
                                                                      ,[EmailAddress] = '{request.Request.Content.EmailAddress}'
                                                                      ,[Password] = '{request.Request.Content.Password}'
                                                                      ,[IsActive] = '{(request.Request.Content.IsActive == true ? 1 : 0)}'
                                                                      ,[CreatedDate] = '{DateTime.Now.ToString()}'
                                                                 WHERE UserId = {id}", connection, CommandType.Text);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        responseContent.Content = "Success";

                        resultContent.ResponseType = OperationType.UpdateData.ToString();
                        resultContent.ResponseMessage = $"Success Updated Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                        resultContent.ResponseBody = responseContent;

                        return StatusCode(201, resultContent);
                    }
                    else
                    {
                        responseContent.Content = "Failed";

                        resultContent.ErrorCode = 18;
                        resultContent.ErrorMessage = "Failed Updated Data User";
                        resultContent.ErrorLink = null;
                        resultContent.ResponseType = OperationType.UpdateData.ToString();
                        resultContent.ResponseMessage = $"Failed Updated Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                        resultContent.ResponseBody = responseContent;

                        return BadRequest(resultContent);
                    }
                }
                else
                {
                    responseContent.Content = "User Not Found";

                    resultContent.ErrorCode = 17;
                    resultContent.ErrorMessage = "Failed Retrive Data User";
                    resultContent.ErrorLink = null;
                    resultContent.ResponseType = OperationType.RetriveData.ToString();
                    resultContent.ResponseMessage = $"Data User Not Found {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                    resultContent.ResponseBody = responseContent;

                    return BadRequest(resultContent);
                }
            }
            catch (Exception ex)
            {
                responseContent.Content = "Failed";

                resultContent.ErrorCode = 999;
                resultContent.ErrorMessage = ex.Message + "\\r\\n" + ex.StackTrace;
                resultContent.ErrorLink = ex.HelpLink;
                resultContent.ResponseType = OperationType.UpdateData.ToString();
                resultContent.ResponseMessage = $"Failed Updated Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                resultContent.ResponseBody = responseContent;

                return BadRequest(resultContent);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        public IActionResult Delete(int id)
        {
            ResponseBodyModel<ResponseContentBodyModel<string>> resultContent = new ResponseBodyModel<ResponseContentBodyModel<string>>();
            ResponseContentBodyModel<string> responseContent = new ResponseContentBodyModel<string>();

            try
            {
                TblUserModel user = new TblUserModel();
                if (id == 0)
                    throw new ArgumentException($"Id Sama Dengan Nol, Id Tidak boleh Nol {nameof(id)}");
                else
                {
                    SqlConnection connGet = DatabaseConfig.GetOpenConnection();
                    SqlCommand cmdGet = DatabaseConfig.GetCommand($"SELECT * FROM TblJurusan WHERE JurusanId = {id}", connGet, CommandType.Text);
                    SqlDataReader reader = cmdGet.ExecuteReader();
                    if (reader.Read() && reader.HasRows)
                    {
                        object? userId = reader["UserId"] == DBNull.Value ? null : reader["UserId"];
                        object? firstname = reader["Firstname"] == DBNull.Value ? null : reader["Firstname"];
                        object? lastname = reader["Lastname"] == DBNull.Value ? null : reader["Lastname"];
                        object? username = reader["Username"] == DBNull.Value ? null : reader["Username"];
                        object? emailAddress = reader["EmailAddress"] == DBNull.Value ? null : reader["EmailAddress"];
                        object? password = reader["Password"] == DBNull.Value ? null : reader["Password"];
                        object? isActive = reader["IsActive"] == DBNull.Value ? null : reader["IsActive"];
                        object? createdDate = reader["CreatedDate"] == DBNull.Value ? null : reader["CreatedDate"];

                        TblUserModel model = new TblUserModel();
                        model.UserId = Convert.ToInt32(userId);
                        model.Firstname = firstname.ToString();
                        model.Lastname = lastname.ToString();
                        model.Username = username.ToString();
                        model.EmailAddress = emailAddress.ToString();
                        model.Password = password.ToString();
                        model.IsActive = Convert.ToBoolean(isActive);
                        model.CreatedDate = Convert.ToDateTime(createdDate);
                    }
                }

                if (user != null && user.UserId != 0)
                {
                    SqlConnection connection = DatabaseConfig.GetOpenConnection();
                    SqlCommand cmd = DatabaseConfig.GetCommand(@$"DELETE FROM [dbo].[TblUser] WHERE UserId = {id}", connection, CommandType.Text);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        responseContent.Content = "Success";

                        resultContent.ResponseType = OperationType.DeleteData.ToString();
                        resultContent.ResponseMessage = $"Success Deleted Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                        resultContent.ResponseBody = responseContent;

                        return Ok(resultContent);
                    }
                    else
                    {
                        responseContent.Content = "Failed";

                        resultContent.ErrorCode = 19;
                        resultContent.ErrorMessage = "Failed Deleted Data User";
                        resultContent.ErrorLink = null;
                        resultContent.ResponseType = OperationType.DeleteData.ToString();
                        resultContent.ResponseMessage = $"Failed Deleted Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                        resultContent.ResponseBody = responseContent;

                        return BadRequest(resultContent);
                    }
                }
                else
                {
                    responseContent.Content = "User Not Found";

                    resultContent.ErrorCode = 17;
                    resultContent.ErrorMessage = "Failed Retrive Data User";
                    resultContent.ErrorLink = null;
                    resultContent.ResponseType = OperationType.RetriveData.ToString();
                    resultContent.ResponseMessage = $"Data User Not Found {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                    resultContent.ResponseBody = responseContent;

                    return BadRequest(resultContent);
                }
            }
            catch (Exception ex)
            {
                responseContent.Content = "Failed";

                resultContent.ErrorCode = 999;
                resultContent.ErrorMessage = ex.Message + "\\r\\n" + ex.StackTrace;
                resultContent.ErrorLink = ex.HelpLink;
                resultContent.ResponseType = OperationType.DeleteData.ToString();
                resultContent.ResponseMessage = $"Failed Deleted Data {nameof(TblUserModel).Replace("Model", "", StringComparison.OrdinalIgnoreCase)}";
                resultContent.ResponseBody = responseContent;

                return BadRequest(resultContent);
            }
        }
    }
}
