using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MiniAdonet.Pages.teknoloji.musteriler
{
    public class IndexModel : PageModel
    {
        public List<MusteriModel> MusteriList = new List<MusteriModel>();

        public void OnGet()
        {
            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT MusteriID, AdSoyad, Email, Telefon FROM Musteriler";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        MusteriList.Add(new MusteriModel
                        {
                            MusteriID = rdr.GetInt32(0),
                            AdSoyad = rdr.GetString(1),
                            Email = rdr.GetString(2),
                            Telefon = rdr.GetString(3)
                        });
                    }
                }
            }
        }
    }

    public class MusteriModel
    {
        public int MusteriID { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
    }
}
