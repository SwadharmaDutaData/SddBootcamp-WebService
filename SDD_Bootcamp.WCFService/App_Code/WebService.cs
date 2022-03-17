using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    public LoginViewModel auth;
    private string CONNECTION_STRINGS = System.Configuration.ConfigurationManager.ConnectionStrings["sdd_bootcamp"].ConnectionString;
    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    [SoapHeader("auth", Required = true, Direction = SoapHeaderDirection.InOut)]
    [SoapDocumentMethod(Action = "http://www.sandev.com/", RequestNamespace = "http://www.sandev.com/Request", RequestElementName = "GetDataMahasiswaRequest", ResponseNamespace = "http://www.sandev.com/Response", ResponseElementName = "GetDataMahasiswaResponse")]
    public SoapResponseBody<ObjectModel<List<MahasiswaModel>>> GetDataMahasiswa()
    {

        SoapResponseBody<ObjectModel<List<MahasiswaModel>>> responBody = new SoapResponseBody<ObjectModel<List<MahasiswaModel>>>();
        ObjectModel<List<MahasiswaModel>> obj = new ObjectModel<List<MahasiswaModel>>();
        List<MahasiswaModel> listMahasiswa = new List<MahasiswaModel>();
        SqlConnection conn = new SqlConnection(CONNECTION_STRINGS);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT * FROM Mahasiswa";
        cmd.CommandTimeout = 10;
        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            if (reader.HasRows)
            {
                MahasiswaModel mahasiswaModel = new MahasiswaModel();
                mahasiswaModel.ID = Convert.ToInt32(reader["ID"]);
                mahasiswaModel.Npm = reader["Npm"].ToString();
                mahasiswaModel.NamaMahasiswa = reader["NamaMahasiswa"].ToString();
                mahasiswaModel.Email = reader["Email"].ToString();
                mahasiswaModel.Alamat = reader["Alamat"].ToString();
                mahasiswaModel.JenisKelamin = reader["JenisKelamin"].ToString();
                mahasiswaModel.IsActive = Convert.ToBoolean(reader["IsActive"]);

                listMahasiswa.Add(mahasiswaModel);
            }
            else { listMahasiswa = new List<MahasiswaModel>(); }
        }

        obj.Data = listMahasiswa;
        if (conn.State == ConnectionState.Open)
            conn.Close();

        if (listMahasiswa != null)
        {
            responBody.ResponCode = "200";
            responBody.ResponMessage = "Success";
            responBody.ResponBody = obj;
        }
        else
        {
            responBody.ResponCode = "403";
            responBody.ResponMessage = "Failed";
            responBody.ResponBody = obj;
        }

        return responBody;
    }

    [WebMethod]
    [SoapHeader("auth", Required = true, Direction = SoapHeaderDirection.InOut)]
    [SoapDocumentMethod(Action = "http://www.sandev.com/", RequestNamespace = "http://www.sandev.com/Request", RequestElementName = "GetDataMahasiswaByIdRequest", ResponseNamespace = "http://www.sandev.com/Response", ResponseElementName = "GetDataMahasiswaByIdResponse")]
    public SoapResponseBody<ObjectModel<MahasiswaModel>> GetDataMahasiswaById(int id)
    {
        SoapResponseBody<ObjectModel<MahasiswaModel>> responseBody = new SoapResponseBody<ObjectModel<MahasiswaModel>>();
        MahasiswaModel mahasiswaModel = new MahasiswaModel();
        ObjectModel<MahasiswaModel> obj = new ObjectModel<MahasiswaModel>();
        SqlConnection conn = new SqlConnection(CONNECTION_STRINGS);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT * FROM Mahasiswa WHERE ID = " + id;
        cmd.CommandTimeout = 10;
        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            if (reader.HasRows)
            {
                mahasiswaModel.ID = Convert.ToInt32(reader["ID"]);
                mahasiswaModel.Npm = reader["Npm"].ToString();
                mahasiswaModel.NamaMahasiswa = reader["NamaMahasiswa"].ToString();
                mahasiswaModel.Email = reader["Email"].ToString();
                mahasiswaModel.Alamat = reader["Alamat"].ToString();
                mahasiswaModel.JenisKelamin = reader["JenisKelamin"].ToString();
                mahasiswaModel.IsActive = Convert.ToBoolean(reader["IsActive"]);

                obj.Data = mahasiswaModel;
            } 
            else
            {
                mahasiswaModel = new MahasiswaModel();
                obj.Data = mahasiswaModel;
            }
        }

        if (conn.State == ConnectionState.Open)
            conn.Close();

        if (mahasiswaModel != null && mahasiswaModel.ID != 0 && mahasiswaModel.IsActive != false)
        {
            
            responseBody.ResponCode = "200";
            responseBody.ResponMessage = "Success";
            responseBody.ResponBody = obj;
        }
        else
        {
            responseBody.ResponCode = "403";
            responseBody.ResponMessage = "Failed";
            responseBody.ResponBody = obj;
        }

        return responseBody;
    }

    [WebMethod]
    [SoapHeader("auth", Required = true, Direction = SoapHeaderDirection.InOut)]
    [SoapDocumentMethod(Action = "http://www.sandev.com/", RequestNamespace = "http://www.sandev.com/Request", RequestElementName = "LoginUserRequest", ResponseNamespace = "http://www.sandev.com/Response", ResponseElementName = "LoginUserResponse")]
    public MahasiswaModel LoginUser(string email, string npm)
    {
        MahasiswaModel mahasiswaModel = null;
        LoginViewModel loginViewModel = new LoginViewModel();
        if (loginViewModel.IsValidUser(email, npm, out mahasiswaModel))
        {
            return mahasiswaModel;
        }
        else
        {
            return new MahasiswaModel();
        }
    }

    [WebMethod]
    [SoapHeader("auth", Required = true, Direction = SoapHeaderDirection.InOut)]
    [SoapDocumentMethod(Action = "http://www.sandev.com/", RequestNamespace = "http://www.sandev.com/Request", RequestElementName = "AddMahasiswaRequest", ResponseNamespace = "http://www.sandev.com/Response", ResponseElementName = "AddMahasiswaResponse")]
    public SoapResponseBody<MahasiswaModel> AddMahasiswa(SoapRequestBody<MahasiswaModel> model)
    {
        SoapResponseBody<MahasiswaModel> responBody = new SoapResponseBody<MahasiswaModel>();
        MahasiswaModel mahasiswaModel = new MahasiswaModel();
        SqlConnection conn = new SqlConnection(CONNECTION_STRINGS);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "INSERT INTO Mahasiswa " +
                            "(Npm, NamaMahasiswa, Email, Alamat, JenisKelamin, IsActive)" +
                            "VALUES" +
                            "('"+ model.RequstBody.Npm + "', '"+ model.RequstBody.NamaMahasiswa + "', '"+ model.RequstBody.Email + "', '"+ model.RequstBody.Alamat + "', '"+ model.RequstBody.JenisKelamin + "', "+ (model.RequstBody.IsActive == true ? 1 : 0) + ");";
        cmd.CommandTimeout = 10;
        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            if (reader.HasRows)
            {
                mahasiswaModel.ID = Convert.ToInt32(reader["ID"]);
                mahasiswaModel.Npm = reader["Npm"].ToString();
                mahasiswaModel.NamaMahasiswa = reader["NamaMahasiswa"].ToString();
                mahasiswaModel.Email = reader["Email"].ToString();
                mahasiswaModel.Alamat = reader["Alamat"].ToString();
                mahasiswaModel.JenisKelamin = reader["JenisKelamin"].ToString();
                mahasiswaModel.IsActive = Convert.ToBoolean(reader["IsActive"]);
            }
            else
            {
                mahasiswaModel = new MahasiswaModel();
                mahasiswaModel.ID = 0;
                mahasiswaModel.Npm = "";
                mahasiswaModel.NamaMahasiswa = "";
                mahasiswaModel.Email = "";
                mahasiswaModel.Alamat = "";
                mahasiswaModel.JenisKelamin = "";
                mahasiswaModel.IsActive = false;
            }
        }

        responBody.ResponCode = "200";
        responBody.ResponMessage = "Success";
        responBody.ResponBody = mahasiswaModel;

        if (conn.State == ConnectionState.Open)
            conn.Close();

        return responBody;
    }

    [WebMethod]
    [SoapHeader("auth", Required = true, Direction = SoapHeaderDirection.InOut)]
    [SoapDocumentMethod(Action = "http://www.sandev.com/", RequestNamespace = "http://www.sandev.com/Request", RequestElementName = "UpdateMahasiswaRequest", ResponseNamespace = "http://www.sandev.com/Response", ResponseElementName = "UpdateMahasiswaResponse")]
    public SoapResponseBody<ObjectModel<MahasiswaModel>> UpdateMahasiswa(int id, SoapRequestBody<MahasiswaModel> model)
    {
        SoapResponseBody<ObjectModel<MahasiswaModel>> responBody = new SoapResponseBody<ObjectModel<MahasiswaModel>>();
        ObjectModel<MahasiswaModel> obj = new ObjectModel<MahasiswaModel>();

        if (id != 0)
        {
            MahasiswaModel mahasiswaModel = new MahasiswaModel();
            SqlConnection conn = new SqlConnection(CONNECTION_STRINGS);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Mahasiswa " +
                                "SET Npm = " + model.RequstBody.Npm +
                                " NamaMahasiswa = " + model.RequstBody.NamaMahasiswa +
                                " Email = " + model.RequstBody.Email +
                                " Alamat = " + model.RequstBody.Alamat +
                                " JenisKelamin = " + model.RequstBody.JenisKelamin +
                                " IsActive = " + model.RequstBody.IsActive +
                                " WHERE ID = " + model.RequstBody.ID;
            cmd.CommandTimeout = 10;
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM Mahasiswa WHERE ID = " + id;
                cmd.CommandTimeout = 10;

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() && reader.HasRows)
                {
                    mahasiswaModel.ID = Convert.ToInt32(reader["ID"]);
                    mahasiswaModel.Npm = reader["Npm"].ToString();
                    mahasiswaModel.NamaMahasiswa = reader["NamaMahasiswa"].ToString();
                    mahasiswaModel.Email = reader["Email"].ToString();
                    mahasiswaModel.Alamat = reader["Alamat"].ToString();
                    mahasiswaModel.JenisKelamin = reader["JenisKelamin"].ToString();
                    mahasiswaModel.IsActive = Convert.ToBoolean(reader["IsActive"]);

                    obj.Data = mahasiswaModel;
                }
                else
                {
                    mahasiswaModel = new MahasiswaModel();
                    mahasiswaModel.ID = 0;
                    mahasiswaModel.Npm = "";
                    mahasiswaModel.NamaMahasiswa = "";
                    mahasiswaModel.Email = "";
                    mahasiswaModel.Alamat = "";
                    mahasiswaModel.JenisKelamin = "";
                    mahasiswaModel.IsActive = false;

                    obj.Data = null;
                }

                responBody.ResponCode = "200";
                responBody.ResponMessage = "Success";
                responBody.ResponBody = obj;
            }
            else
            {
                responBody.ResponCode = "403";
                responBody.ResponMessage = "No Data Found after update";
                responBody.ResponBody = obj;
            }

            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        else
        {
            responBody.ResponCode = "400";
            responBody.ResponMessage = "Data Not Found";
            responBody.ResponBody = obj;
        }

        return responBody;
    }
}
