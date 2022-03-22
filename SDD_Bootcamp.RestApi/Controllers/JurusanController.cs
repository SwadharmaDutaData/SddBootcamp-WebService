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
    [Produces("application/json")]
    public class JurusanController : ControllerBase
    {
        // GET: api/<JurusanController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<List<TblJurusanModel>>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<List<TblJurusanModel>>>))]
        public ResponseBodyModel<ResponseContentBodyModel<List<TblJurusanModel>>> GetAllJurusan()
        {
            ResponseBodyModel<ResponseContentBodyModel<List<TblJurusanModel>>> resultList = new ResponseBodyModel<ResponseContentBodyModel<List<TblJurusanModel>>>();
            ResponseContentBodyModel<List<TblJurusanModel>> responseContent = new ResponseContentBodyModel<List<TblJurusanModel>>();

            try
            {
                List<TblJurusanModel> listJurusan = new List<TblJurusanModel>();
                SqlConnection connection = DatabaseConfig.GetOpenConnection();
                SqlCommand cmd = DatabaseConfig.GetCommand("SELECT * FROM TblJurusan", connection, CommandType.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        object? jurusanId = reader["JurusanId"] == DBNull.Value ? null : reader["JurusanId"];
                        object? namaJurusan = reader["NamaJurusan"] == DBNull.Value ? null : reader["NamaJurusan"];
                        object? deskripsiJurusan = reader["DeskripsiJurusan"] == DBNull.Value ? null : reader["DeskripsiJurusan"];
                        object? createTime = reader["CreatedTime"] == DBNull.Value ? null : reader["CreatedTime"];
                        object? isActive = reader["IsActive"] == DBNull.Value ? null : reader["IsActive"];

                        TblJurusanModel model = new TblJurusanModel();
                        model.JurusanId = Convert.ToInt32(jurusanId);
                        model.NamaJurusan = namaJurusan.ToString();
                        model.DeskripsiJurusan = deskripsiJurusan.ToString();
                        model.CreatedTime = Convert.ToDateTime(createTime);
                        model.IsActive = Convert.ToBoolean(isActive);

                        listJurusan.Add(model);
                    }
                }

                if (listJurusan != null && listJurusan.Count > 0)
                {
                    reader.Close();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    responseContent.Content = listJurusan;

                    resultList.ResponseType = OperationType.CollectData.ToString();
                    resultList.ResponseMessage = $"Collect All Data {nameof(TblJurusanModel)}";
                    resultList.ResponseBody = responseContent;
                }
                else
                {
                    responseContent.Content = null;

                    resultList.ErrorCode = 17;
                    resultList.ErrorMessage = "Data Not Found";
                    resultList.ErrorLink = null;
                    resultList.ResponseType = OperationType.CollectData.ToString(); ;
                    resultList.ResponseMessage = $"Failed Collect All Data {nameof(TblJurusanModel)}";
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
                resultList.ResponseMessage = $"Failed Collect All Data {nameof(TblJurusanModel)}";
                resultList.ResponseBody = responseContent;
            }

            return resultList;
        }

        // GET api/<JurusanController>/5
        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<TblJurusanModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<TblJurusanModel>>))]
        public ResponseBodyModel<ResponseContentBodyModel<TblJurusanModel>> GetJurusanById(int id)
        {
            ResponseBodyModel<ResponseContentBodyModel<TblJurusanModel>> resultList = new ResponseBodyModel<ResponseContentBodyModel<TblJurusanModel>>();
            ResponseContentBodyModel<TblJurusanModel> responseContent = new ResponseContentBodyModel<TblJurusanModel>();
            RequestBodyModel<int> request = new RequestBodyModel<int>();
            request.OperationType = OperationType.RetriveData.ToString();
            request.OperationDesc = $"Retrive Data {nameof(TblJurusanModel)}";
            request.Request = id;

            try
            {
                TblJurusanModel listMahasiswa = new TblJurusanModel();
                SqlConnection connection = DatabaseConfig.GetOpenConnection();
                SqlCommand cmd = DatabaseConfig.GetCommand($"SELECT * FROM TblJurusan WHERE JurusanId = {request.Request}", connection, CommandType.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() && reader.HasRows)
                {
                    object? jurusanId = reader["JurusanId"] == DBNull.Value ? null : reader["JurusanId"];
                    object? namaJurusan = reader["NamaJurusan"] == DBNull.Value ? null : reader["NamaJurusan"];
                    object? deskripsiJurusan = reader["DeskripsiJurusan"] == DBNull.Value ? null : reader["DeskripsiJurusan"];
                    object? createTime = reader["CreatedTime"] == DBNull.Value ? null : reader["CreatedTime"];
                    object? isActive = reader["IsActive"] == DBNull.Value ? null : reader["IsActive"];

                    TblJurusanModel model = new TblJurusanModel();
                    model.JurusanId = Convert.ToInt32(jurusanId);
                    model.NamaJurusan = namaJurusan.ToString();
                    model.DeskripsiJurusan = deskripsiJurusan.ToString();
                    model.CreatedTime = Convert.ToDateTime(createTime);
                    model.IsActive = Convert.ToBoolean(isActive);

                    responseContent.Content = model;
                }

                if (responseContent != null && responseContent.Content != null)
                {
                    resultList.ResponseType = OperationType.RetriveData.ToString();
                    resultList.ResponseMessage = $"Success Retrive Data {nameof(TblJurusanModel)}";
                    resultList.ResponseBody = responseContent;
                }
                else
                {
                    responseContent.Content = null;

                    resultList.ErrorCode = 17;
                    resultList.ErrorMessage = "Data Not Found";
                    resultList.ErrorLink = null;
                    resultList.ResponseType = OperationType.RetriveData.ToString();
                    resultList.ResponseMessage = $"Failed Retrive Data {nameof(TblJurusanModel)}";
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
                resultList.ResponseMessage = $"Failed Retrive Data {nameof(TblJurusanModel)}";
                resultList.ResponseBody = responseContent;
            }

            return resultList;
        }

        // POST api/<JurusanController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        public IActionResult PostJurusan([FromBody] RequestBodyModel<ResponseContentBodyModel<TblJurusanModel>> request)
        {
            ResponseBodyModel<ResponseContentBodyModel<string>> resultContent = new ResponseBodyModel<ResponseContentBodyModel<string>>();
            ResponseContentBodyModel<string> responseContent = new ResponseContentBodyModel<string>();

            try
            {
                SqlConnection connection = DatabaseConfig.GetOpenConnection();
                SqlCommand cmd = DatabaseConfig.GetCommand(@$"INSERT INTO [dbo].[TblJurusan]
                                                               ([NamaJurusan]
                                                               ,[DeskripsiJurusan]
                                                               ,[CreatedTime]
                                                               ,[IsActive])
                                                         VALUES
                                                               ('{request.Request.Content.NamaJurusan}'
                                                               ,'{request.Request.Content.DeskripsiJurusan}'
                                                               ,'{DateTime.Now.ToString()}'
                                                               ,'{(request.Request.Content.IsActive == true ? 1 : 0)}')", connection, CommandType.Text);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    responseContent.Content = "Success";

                    resultContent.ResponseType = OperationType.AddData.ToString();
                    resultContent.ResponseMessage = $"Success Adding Data {nameof(TblJurusanModel)}";
                    resultContent.ResponseBody = responseContent;

                    return Created("api/jurusan", resultContent);
                }
                else
                {
                    responseContent.Content = "Failed";

                    resultContent.ErrorCode = 18;
                    resultContent.ErrorMessage = "Failed Adding Data Jurusan";
                    resultContent.ErrorLink = null;
                    resultContent.ResponseType = OperationType.AddData.ToString();
                    resultContent.ResponseMessage = $"Failed Adding Data {nameof(TblJurusanModel)}";
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
                resultContent.ResponseMessage = $"Failed Adding Data {nameof(TblJurusanModel)}";
                resultContent.ResponseBody = responseContent;

                return BadRequest(resultContent);
            }
        }

        // PUT api/<JurusanController>/5
        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        public IActionResult PutJurusan(int id, [FromBody] RequestBodyModel<ResponseContentBodyModel<TblJurusanModel>> request)
        {
            ResponseBodyModel<ResponseContentBodyModel<string>> resultContent = new ResponseBodyModel<ResponseContentBodyModel<string>>();
            ResponseContentBodyModel<string> responseContent = new ResponseContentBodyModel<string>();

            try
            {
                TblJurusanModel jurusan = new TblJurusanModel();
                if (id == 0)
                    throw new ArgumentException($"Id Sama Dengan Nol, Id Tidak boleh Nol {nameof(id)}");
                else
                {
                    SqlConnection connGet = DatabaseConfig.GetOpenConnection();
                    SqlCommand cmdGet = DatabaseConfig.GetCommand($"SELECT * FROM TblJurusan WHERE JurusanId = {id}", connGet, CommandType.Text);
                    SqlDataReader reader = cmdGet.ExecuteReader();
                    if (reader.Read() && reader.HasRows)
                    {
                        object? jurusanId = reader["JurusanId"] == DBNull.Value ? null : reader["JurusanId"];
                        object? namaJurusan = reader["NamaJurusan"] == DBNull.Value ? null : reader["NamaJurusan"];
                        object? deskripsiJurusan = reader["DeskripsiJurusan"] == DBNull.Value ? null : reader["DeskripsiJurusan"];
                        object? createTime = reader["CreatedTime"] == DBNull.Value ? null : reader["CreatedTime"];
                        object? isActive = reader["IsActive"] == DBNull.Value ? null : reader["IsActive"];

                        jurusan.JurusanId = Convert.ToInt32(jurusanId);
                        jurusan.NamaJurusan = namaJurusan.ToString();
                        jurusan.DeskripsiJurusan = deskripsiJurusan.ToString();
                        jurusan.CreatedTime = Convert.ToDateTime(createTime);
                        jurusan.IsActive = Convert.ToBoolean(isActive);
                    }
                }

                if (jurusan != null && jurusan.JurusanId != 0)
                {
                    SqlConnection connection = DatabaseConfig.GetOpenConnection();
                    SqlCommand cmd = DatabaseConfig.GetCommand(@$"UPDATE [dbo].[TblJurusan]
                                                                   SET [NamaJurusan] = '{request.Request.Content.NamaJurusan}'
                                                                      ,[DeskripsiJurusan] = '{request.Request.Content.DeskripsiJurusan}'
                                                                      ,[CreatedTime] = '{DateTime.Now.ToString()}'
                                                                      ,[IsActive] = '{(request.Request.Content.IsActive == true ? 1 : 0)}'
                                                                 WHERE JurusanId = {id}", connection, CommandType.Text);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        responseContent.Content = "Success";

                        resultContent.ResponseType = OperationType.UpdateData.ToString();
                        resultContent.ResponseMessage = $"Success Updated Data {nameof(TblJurusanModel)}";
                        resultContent.ResponseBody = responseContent;

                        return Created($"api/jurusan/update/{id}", resultContent);
                    }
                    else
                    {
                        responseContent.Content = "Failed";

                        resultContent.ErrorCode = 18;
                        resultContent.ErrorMessage = "Failed Updated Data Mahasiswa";
                        resultContent.ErrorLink = null;
                        resultContent.ResponseType = OperationType.UpdateData.ToString();
                        resultContent.ResponseMessage = $"Failed Updated Data {nameof(TblJurusanModel)}";
                        resultContent.ResponseBody = responseContent;

                        return BadRequest(resultContent);
                    }
                }
                else
                {
                    responseContent.Content = "Mahasiswa Not Found";

                    resultContent.ErrorCode = 17;
                    resultContent.ErrorMessage = "Failed Retrive Data Jurusan";
                    resultContent.ErrorLink = null;
                    resultContent.ResponseType = OperationType.RetriveData.ToString();
                    resultContent.ResponseMessage = $"Data Jurusan Not Found {nameof(TblJurusanModel)}";
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
                resultContent.ResponseMessage = $"Failed Updated Data {nameof(TblJurusanModel)}";
                resultContent.ResponseBody = responseContent;

                return BadRequest(resultContent);
            }
        }

        // DELETE api/<JurusanController>/5
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBodyModel<ResponseContentBodyModel<string>>))]
        public IActionResult Delete(int id)
        {
            ResponseBodyModel<ResponseContentBodyModel<string>> resultContent = new ResponseBodyModel<ResponseContentBodyModel<string>>();
            ResponseContentBodyModel<string> responseContent = new ResponseContentBodyModel<string>();

            try
            {
                TblJurusanModel jurusan = new TblJurusanModel();
                if (id == 0)
                    throw new ArgumentException($"Id Sama Dengan Nol, Id Tidak boleh Nol {nameof(id)}");
                else
                {
                    SqlConnection connGet = DatabaseConfig.GetOpenConnection();
                    SqlCommand cmdGet = DatabaseConfig.GetCommand($"SELECT * FROM TblJurusan WHERE JurusanId = {id}", connGet, CommandType.Text);
                    SqlDataReader reader = cmdGet.ExecuteReader();
                    if (reader.Read() && reader.HasRows)
                    {
                        object? jurusanId = reader["JurusanId"] == DBNull.Value ? null : reader["JurusanId"];
                        object? namaJurusan = reader["NamaJurusan"] == DBNull.Value ? null : reader["NamaJurusan"];
                        object? deskripsiJurusan = reader["DeskripsiJurusan"] == DBNull.Value ? null : reader["DeskripsiJurusan"];
                        object? createTime = reader["CreatedTime"] == DBNull.Value ? null : reader["CreatedTime"];
                        object? isActive = reader["IsActive"] == DBNull.Value ? null : reader["IsActive"];

                        jurusan.JurusanId = Convert.ToInt32(jurusanId);
                        jurusan.NamaJurusan = namaJurusan.ToString();
                        jurusan.DeskripsiJurusan = deskripsiJurusan.ToString();
                        jurusan.CreatedTime = Convert.ToDateTime(createTime);
                        jurusan.IsActive = Convert.ToBoolean(isActive);
                    }
                }

                if (jurusan != null && jurusan.JurusanId != 0)
                {
                    SqlConnection connection = DatabaseConfig.GetOpenConnection();
                    SqlCommand cmd = DatabaseConfig.GetCommand(@$"DELETE FROM [dbo].[TblJurusan] WHERE JurusanId = {id}", connection, CommandType.Text);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        responseContent.Content = "Success";

                        resultContent.ResponseType = OperationType.DeleteData.ToString();
                        resultContent.ResponseMessage = $"Success Deleted Data {nameof(TblJurusanModel)}";
                        resultContent.ResponseBody = responseContent;

                        return Created($"api/jurusan/delete/{id}", resultContent);
                    }
                    else
                    {
                        responseContent.Content = "Failed";

                        resultContent.ErrorCode = 19;
                        resultContent.ErrorMessage = "Failed Deleted Data Jurusan";
                        resultContent.ErrorLink = null;
                        resultContent.ResponseType = OperationType.DeleteData.ToString();
                        resultContent.ResponseMessage = $"Failed Deleted Data {nameof(TblJurusanModel)}";
                        resultContent.ResponseBody = responseContent;

                        return BadRequest(resultContent);
                    }
                }
                else
                {
                    responseContent.Content = "Jurusan Not Found";

                    resultContent.ErrorCode = 17;
                    resultContent.ErrorMessage = "Failed Retrive Data Jurusan";
                    resultContent.ErrorLink = null;
                    resultContent.ResponseType = OperationType.RetriveData.ToString();
                    resultContent.ResponseMessage = $"Data Jurusan Not Found {nameof(TblJurusanModel)}";
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
                resultContent.ResponseMessage = $"Failed Deleted Data {nameof(TblJurusanModel)}";
                resultContent.ResponseBody = responseContent;

                return BadRequest(resultContent);
            }
        }
    }
}
