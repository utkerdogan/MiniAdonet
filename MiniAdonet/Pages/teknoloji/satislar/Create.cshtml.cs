using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MiniAdonet.Pages.teknoloji.satislar
{
    public class CreateModel : PageModel
    {
        public List<UrunModel> UrunList = new List<UrunModel>();
        public List<MusteriModel> MusteriList = new List<MusteriModel>();

        public void OnGet()
        {
            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string urunSql = "SELECT UrunID, UrunAdi FROM Urunler";
                using (SqlCommand cmd = new SqlCommand(urunSql, con))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        UrunList.Add(new UrunModel
                        {
                            UrunID = rdr.GetInt32(0),
                            UrunAdi = rdr.GetString(1)
                        });
                    }
                    rdr.Close();
                }

                string musteriSql = "SELECT MusteriID, AdSoyad FROM Musteriler";
                using (SqlCommand cmd = new SqlCommand(musteriSql, con))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        MusteriList.Add(new MusteriModel
                        {
                            MusteriID = rdr.GetInt32(0),
                            AdSoyad = rdr.GetString(1)
                        });
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            int musteriId = Convert.ToInt32(Request.Form["musteriId"]);
            int urunId = Convert.ToInt32(Request.Form["urunId"]);
            int adet = Convert.ToInt32(Request.Form["adet"]);
            DateTime satisTarihi = DateTime.Now;

            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "INSERT INTO Satislar (MusteriID, UrunID, Adet, SatisTarihi) VALUES (@MusteriID, @UrunID, @Adet, @SatisTarihi)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@MusteriID", musteriId);
                    cmd.Parameters.AddWithValue("@UrunID", urunId);
                    cmd.Parameters.AddWithValue("@Adet", adet);
                    cmd.Parameters.AddWithValue("@SatisTarihi", satisTarihi);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToPage("/teknoloji/satislar/Index");
        }

        public class UrunModel
        {
            public int UrunID { get; set; }
            public string UrunAdi { get; set; }
        }

        public class MusteriModel
        {
            public int MusteriID { get; set; }
            public string AdSoyad { get; set; }
        }
    }
}
