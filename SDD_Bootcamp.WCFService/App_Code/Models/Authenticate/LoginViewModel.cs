using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

/// <summary>
/// Summary description for LoginViewModel
/// </summary>
public class LoginViewModel : SoapHeader
{
    private string CONNECTION_STRINGS = System.Configuration.ConfigurationManager.ConnectionStrings["sdd_bootcamp"].ConnectionString;

    public string Email { get; set; }
    public string Npm { get; set; }

    public bool IsValidUser(string email, string npm, out MahasiswaModel model)
    {
        MahasiswaModel mahasiswaModel = null;
        SqlConnection conn = new SqlConnection(CONNECTION_STRINGS);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT * FROM Mahasiswa WHERE email = '" + email + "' AND Npm = '" + npm + "'";
        cmd.CommandTimeout = 10;
        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read() && reader.HasRows)
        {
            mahasiswaModel = new MahasiswaModel();
            mahasiswaModel.ID = Convert.ToInt32(reader["ID"]);
            mahasiswaModel.Npm = reader["Npm"].ToString();
            mahasiswaModel.NamaMahasiswa = reader["NamaMahasiswa"].ToString();
            mahasiswaModel.Email = reader["Email"].ToString();
            mahasiswaModel.Alamat = reader["Alamat"].ToString();
            mahasiswaModel.JenisKelamin = reader["JenisKelamin"].ToString();
            mahasiswaModel.IsActive = Convert.ToBoolean(reader["IsActive"]);
            model = mahasiswaModel;

            if (conn.State == ConnectionState.Open)
                conn.Close();

            return true;
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
            model = mahasiswaModel;

            if (conn.State == ConnectionState.Open)
                conn.Close();

            return false;
        }
    }
}