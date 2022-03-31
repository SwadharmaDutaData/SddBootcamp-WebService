using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using SDD_Bootcamp.RestApi.ApiHelpers;
using SDD_Bootcamp.RestApi.Models;
using SDD_Bootcamp.RestApi.Models.Request;
using SDD_Bootcamp.RestApi.Models.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SDD_Bootcamp.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class MahasiswaController : ControllerBase
    {
        // GET: api/<MahasiswaController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<List<MahasiswaModel>>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<List<MahasiswaModel>>>))]
        public ResponseBodyModel<ResponseContentBodyModel<List<MahasiswaModel>>> GetAllMahasiswa()
        {
            ResponseBodyModel<ResponseContentBodyModel<List<MahasiswaModel>>> resultList = new ResponseBodyModel<ResponseContentBodyModel<List<MahasiswaModel>>>();
            ResponseContentBodyModel<List<MahasiswaModel>> responseContent = new ResponseContentBodyModel<List<MahasiswaModel>>();

            try
            {
                List<MahasiswaModel> listMahasiswa = new List<MahasiswaModel>();
                SqlConnection connection = DatabaseConfig.GetOpenConnection();
                SqlCommand cmd = DatabaseConfig.GetCommand("SELECT * FROM Mahasiswa", connection, CommandType.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        MahasiswaModel model = new MahasiswaModel();
                        model.Id = Convert.ToInt32(reader["ID"]);
                        model.Npm = reader["Npm"].ToString();
                        model.NamaMahasiswa = reader["NamaMahasiswa"].ToString();
                        model.Email = reader["Email"].ToString();
                        model.Alamat = reader["Alamat"].ToString();
                        model.JenisKelamin = reader["JenisKelamin"].ToString();
                        model.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        model.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);

                        listMahasiswa.Add(model);
                    }
                }

                if (listMahasiswa != null && listMahasiswa.Count > 0)
                {
                    reader.Close();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    responseContent.Content = listMahasiswa;

                    resultList.ResponseType = OperationType.CollectData.ToString();
                    resultList.ResponseMessage = $"Collect All Data {nameof(MahasiswaModel)}";
                    resultList.ResponseBody = responseContent;
                }
                else
                {
                    responseContent.Content = null;

                    resultList.ErrorCode = 17;
                    resultList.ErrorMessage = "Data Not Found";
                    resultList.ErrorLink = null;
                    resultList.ResponseType = OperationType.CollectData.ToString(); ;
                    resultList.ResponseMessage = $"Failed Collect All Data {nameof(MahasiswaModel)}";
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
                resultList.ResponseMessage = $"Failed Collect All Data {nameof(MahasiswaModel)}";
                resultList.ResponseBody = responseContent;
            }

            return resultList;
        }

        // GET api/<MahasiswaController>/5
        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<MahasiswaModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<MahasiswaModel>>))]
        public ResponseBodyModel<ResponseContentBodyModel<MahasiswaModel>> GetById(int id)
        {
            ResponseBodyModel<ResponseContentBodyModel<MahasiswaModel>> resultList = new ResponseBodyModel<ResponseContentBodyModel<MahasiswaModel>>();
            ResponseContentBodyModel<MahasiswaModel> responseContent = new ResponseContentBodyModel<MahasiswaModel>();
            RequestBodyModel<int> request = new RequestBodyModel<int>();
            request.OperationType = OperationType.RetriveData.ToString();
            request.OperationDesc = $"Retrive Data {nameof(MahasiswaModel)}";
            request.Request = id;

            try
            {
                MahasiswaModel listMahasiswa = new MahasiswaModel();
                SqlConnection connection = DatabaseConfig.GetOpenConnection();
                SqlCommand cmd = DatabaseConfig.GetCommand($"SELECT * FROM Mahasiswa WHERE Id = {request.Request}", connection, CommandType.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() && reader.HasRows)
                {
                    MahasiswaModel model = new MahasiswaModel();
                    model.Id = Convert.ToInt32(reader["ID"]);
                    model.Npm = reader["Npm"].ToString();
                    model.NamaMahasiswa = reader["NamaMahasiswa"].ToString();
                    model.Email = reader["Email"].ToString();
                    model.Alamat = reader["Alamat"].ToString();
                    model.JenisKelamin = reader["JenisKelamin"].ToString();
                    model.IsActive = Convert.ToBoolean(reader["IsActive"]);
                    model.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);

                    responseContent.Content = model;
                }

                if (responseContent != null && responseContent.Content != null)
                {
                    resultList.ResponseType = OperationType.RetriveData.ToString();
                    resultList.ResponseMessage = $"Success Retrive Data {nameof(MahasiswaModel)}";
                    resultList.ResponseBody = responseContent;
                }
                else
                {
                    responseContent.Content = null;

                    resultList.ErrorCode = 17;
                    resultList.ErrorMessage = "Data Not Found";
                    resultList.ErrorLink = null;
                    resultList.ResponseType = OperationType.RetriveData.ToString();
                    resultList.ResponseMessage = $"Failed Retrive Data {nameof(MahasiswaModel)}";
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
                resultList.ResponseMessage = $"Failed Retrive Data {nameof(MahasiswaModel)}";
                resultList.ResponseBody = responseContent;
            }

            return resultList;
        }

        // POST api/<MahasiswaController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        public IActionResult PostMahasiswa([FromBody] RequestBodyModel<ResponseContentBodyModel<MahasiswaModel>> request)
        {
            ResponseBodyModel<ResponseContentBodyModel<string>> resultContent = new ResponseBodyModel<ResponseContentBodyModel<string>>();
            ResponseContentBodyModel<string> responseContent = new ResponseContentBodyModel<string>();

            try
            {
                SqlConnection connection = DatabaseConfig.GetOpenConnection();
                SqlCommand cmd = DatabaseConfig.GetCommand(@$"INSERT INTO [dbo].[Mahasiswa]
                                                                   ([NPM]
                                                                   ,[NamaMahasiswa]
                                                                   ,[Email]
                                                                   ,[Alamat]
                                                                   ,[JenisKelamin]
                                                                   ,[IsActive]
                                                                   ,[CreatedDate])
                                                             VALUES
                                                                   ('{request.Request.Content.Npm}'
                                                                   ,'{request.Request.Content.NamaMahasiswa}'
                                                                   ,'{request.Request.Content.Email}'
                                                                   ,'{request.Request.Content.Alamat}'
                                                                   ,'{request.Request.Content.JenisKelamin}'
                                                                   ,{(request.Request.Content.IsActive == true ? 1 : 0)}
                                                                   ,'{DateTime.Now.ToString()}')", connection, CommandType.Text);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    responseContent.Content = "Success";

                    resultContent.ResponseType = OperationType.AddData.ToString();
                    resultContent.ResponseMessage = $"Success Adding Data {nameof(MahasiswaModel)}";
                    resultContent.ResponseBody = responseContent;

                    return StatusCode(201, resultContent);
                }
                else
                {
                    responseContent.Content = "Failed";

                    resultContent.ErrorCode = 18;
                    resultContent.ErrorMessage = "Failed Adding Data Mahasiswa";
                    resultContent.ErrorLink = null;
                    resultContent.ResponseType = OperationType.AddData.ToString();
                    resultContent.ResponseMessage = $"Failed Adding Data {nameof(MahasiswaModel)}";
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
                resultContent.ResponseMessage = $"Failed Adding Data {nameof(MahasiswaModel)}";
                resultContent.ResponseBody = responseContent;

                return BadRequest(resultContent);
            }
        }

        // PUT api/<MahasiswaController>/5
        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        public IActionResult PutMahasiswa(int id, [FromBody] RequestBodyModel<ResponseContentBodyModel<MahasiswaModel>> request)
        {
            ResponseBodyModel<ResponseContentBodyModel<string>> resultContent = new ResponseBodyModel<ResponseContentBodyModel<string>>();
            ResponseContentBodyModel<string> responseContent = new ResponseContentBodyModel<string>();

            try
            {
                MahasiswaModel mahasiswa = new MahasiswaModel();
                if (id == 0)
                    throw new ArgumentException($"Id Sama Dengan Nol, Id Tidak boleh Nol {nameof(id)}");
                else
                {
                    SqlConnection connGet = DatabaseConfig.GetOpenConnection();
                    SqlCommand cmdGet = DatabaseConfig.GetCommand($"SELECT * FROM Mahasiswa WHERE Id = {id}", connGet, CommandType.Text);
                    SqlDataReader reader = cmdGet.ExecuteReader();
                    if (reader.Read() && reader.HasRows)
                    {
                        mahasiswa.Id = Convert.ToInt32(reader["ID"]);
                        mahasiswa.Npm = reader["Npm"].ToString();
                        mahasiswa.NamaMahasiswa = reader["NamaMahasiswa"].ToString();
                        mahasiswa.Email = reader["Email"].ToString();
                        mahasiswa.Alamat = reader["Alamat"].ToString();
                        mahasiswa.JenisKelamin = reader["JenisKelamin"].ToString();
                        mahasiswa.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        mahasiswa.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    }
                }

                if (mahasiswa != null && mahasiswa.Id != 0)
                {
                    SqlConnection connection = DatabaseConfig.GetOpenConnection();
                    SqlCommand cmd = DatabaseConfig.GetCommand(@$"UPDATE [dbo].[Mahasiswa]
                                                               SET [NPM] = '{request.Request.Content.Npm}'
                                                                  ,[NamaMahasiswa] = '{request.Request.Content.NamaMahasiswa}'
                                                                  ,[Email] = '{request.Request.Content.Email}'
                                                                  ,[Alamat] = '{request.Request.Content.Alamat}'
                                                                  ,[JenisKelamin] = '{request.Request.Content.JenisKelamin}'
                                                                  ,[IsActive] = '{(request.Request.Content.IsActive == true ? 1 : 0)}'
                                                                  ,[CreatedDate] = '{DateTime.Now.ToString()}'
                                                             WHERE ID = {id}", connection, CommandType.Text);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        responseContent.Content = "Success";

                        resultContent.ResponseType = OperationType.UpdateData.ToString();
                        resultContent.ResponseMessage = $"Success Updated Data {nameof(MahasiswaModel)}";
                        resultContent.ResponseBody = responseContent;

                        return StatusCode(201, resultContent);
                    }
                    else
                    {
                        responseContent.Content = "Failed";

                        resultContent.ErrorCode = 18;
                        resultContent.ErrorMessage = "Failed Updated Data Mahasiswa";
                        resultContent.ErrorLink = null;
                        resultContent.ResponseType = OperationType.UpdateData.ToString();
                        resultContent.ResponseMessage = $"Failed Updated Data {nameof(MahasiswaModel)}";
                        resultContent.ResponseBody = responseContent;

                        return BadRequest(resultContent);
                    }
                }
                else
                {
                    responseContent.Content = "Mahasiswa Not Found";

                    resultContent.ErrorCode = 17;
                    resultContent.ErrorMessage = "Failed Retrive Data Mahasiswa";
                    resultContent.ErrorLink = null;
                    resultContent.ResponseType = OperationType.RetriveData.ToString();
                    resultContent.ResponseMessage = $"Data Mahasiswa Not Found {nameof(MahasiswaModel)}";
                    resultContent.ResponseBody = responseContent;

                    return BadRequest(resultContent);
                }
            }
            catch (Exception ex)
            {
                responseContent.Content = "Failed";

                resultContent.ErrorCode = 999;
                resultContent.ErrorMessage = ex.Message +"\\r\\n"+ ex.StackTrace;
                resultContent.ErrorLink = ex.HelpLink;
                resultContent.ResponseType = OperationType.UpdateData.ToString();
                resultContent.ResponseMessage = $"Failed Updated Data {nameof(MahasiswaModel)}";
                resultContent.ResponseBody = responseContent;

                return BadRequest(resultContent);
            }
        }

        // DELETE api/<MahasiswaController>/5
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        public IActionResult DeleteMahasiswa(int id)
        {
            ResponseBodyModel<ResponseContentBodyModel<string>> resultContent = new ResponseBodyModel<ResponseContentBodyModel<string>>();
            ResponseContentBodyModel<string> responseContent = new ResponseContentBodyModel<string>();

            try
            {
                MahasiswaModel mahasiswa = new MahasiswaModel();
                if (id == 0)
                    throw new ArgumentException($"Id Sama Dengan Nol, Id Tidak boleh Nol {nameof(id)}");
                else
                {
                    SqlConnection connGet = DatabaseConfig.GetOpenConnection();
                    SqlCommand cmdGet = DatabaseConfig.GetCommand($"SELECT * FROM Mahasiswa WHERE Id = {id}", connGet, CommandType.Text);
                    SqlDataReader reader = cmdGet.ExecuteReader();
                    if (reader.Read() && reader.HasRows)
                    {
                        mahasiswa.Id = Convert.ToInt32(reader["ID"]);
                        mahasiswa.Npm = reader["Npm"].ToString();
                        mahasiswa.NamaMahasiswa = reader["NamaMahasiswa"].ToString();
                        mahasiswa.Email = reader["Email"].ToString();
                        mahasiswa.Alamat = reader["Alamat"].ToString();
                        mahasiswa.JenisKelamin = reader["JenisKelamin"].ToString();
                        mahasiswa.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        mahasiswa.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    }
                }

                if (mahasiswa != null && mahasiswa.Id != 0)
                {
                    SqlConnection connection = DatabaseConfig.GetOpenConnection();
                    SqlCommand cmd = DatabaseConfig.GetCommand(@$"DELETE FROM [dbo].[Mahasiswa] WHERE ID = {id}", connection, CommandType.Text);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        responseContent.Content = "Success";

                        resultContent.ResponseType = OperationType.DeleteData.ToString();
                        resultContent.ResponseMessage = $"Success Deleted Data {nameof(MahasiswaModel)}";
                        resultContent.ResponseBody = responseContent;

                        return Ok(resultContent);
                    }
                    else
                    {
                        responseContent.Content = "Failed";

                        resultContent.ErrorCode = 19;
                        resultContent.ErrorMessage = "Failed Deleted Data Mahasiswa";
                        resultContent.ErrorLink = null;
                        resultContent.ResponseType = OperationType.DeleteData.ToString();
                        resultContent.ResponseMessage = $"Failed Deleted Data {nameof(MahasiswaModel)}";
                        resultContent.ResponseBody = responseContent;

                        return BadRequest(resultContent);
                    }
                }
                else
                {
                    responseContent.Content = "Mahasiswa Not Found";

                    resultContent.ErrorCode = 17;
                    resultContent.ErrorMessage = "Failed Retrive Data Mahasiswa";
                    resultContent.ErrorLink = null;
                    resultContent.ResponseType = OperationType.RetriveData.ToString();
                    resultContent.ResponseMessage = $"Data Mahasiswa Not Found {nameof(MahasiswaModel)}";
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
                resultContent.ResponseMessage = $"Failed Deleted Data {nameof(MahasiswaModel)}";
                resultContent.ResponseBody = responseContent;

                return BadRequest(resultContent);
            }
        }
    }
}
