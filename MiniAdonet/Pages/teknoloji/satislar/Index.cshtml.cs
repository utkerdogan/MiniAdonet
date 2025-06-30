using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace MiniAdonet.Pages.teknoloji.satislar
{
    public class IndexModel : PageModel
    {
        public List<SatisModel> SatisList = new List<SatisModel>();

        public void OnGet()
        {
            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = @"SELECT s.SatisID, s.MusteriID, u.UrunAdi, s.Adet, s.SatisTarihi
                               FROM Satislar s
                               INNER JOIN Urunler u ON s.UrunID = u.UrunID";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        SatisModel s = new SatisModel();
                        s.SatisID = rdr.GetInt32(0);
                        s.MusteriID = rdr.GetInt32(1);
                        s.UrunAdi = rdr.GetString(2);
                        s.Adet = rdr.GetInt32(3);
                        s.SatisTarihi = rdr.GetDateTime(4);

                        SatisList.Add(s);
                    }
                }
            }
        }
    }

    public class SatisModel
    {
        public int SatisID { get; set; }
        public int MusteriID { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
        public DateTime SatisTarihi { get; set; }
    }
}
