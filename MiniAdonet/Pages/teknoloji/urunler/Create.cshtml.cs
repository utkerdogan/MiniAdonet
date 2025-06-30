using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniAdonet.Pages.teknoloji.urunler
{
    public class CreateModel : PageModel
    {
        public List<KategoriModel> KategoriList = new List<KategoriModel>();

        public void OnGet()
        {
            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT KategoriID, KategoriAdi FROM Kategoriler";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        KategoriList.Add(new KategoriModel
                        {
                            KategoriID = rdr.GetInt32(0),
                            KategoriAdi = rdr.GetString(1)
                        });
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            string urunAdi = Request.Form["UrunAdi"];
            decimal fiyat = decimal.Parse(Request.Form["Fiyat"]);
            int stok = int.Parse(Request.Form["Stok"]);
            int kategoriID = int.Parse(Request.Form["KategoriID"]);

            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "INSERT INTO Urunler (UrunAdi, Fiyat, Stok, KategoriID) VALUES (@UrunAdi, @Fiyat, @Stok, @KategoriID)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UrunAdi", urunAdi);
                    cmd.Parameters.AddWithValue("@Fiyat", fiyat);
                    cmd.Parameters.AddWithValue("@Stok", stok);
                    cmd.Parameters.AddWithValue("@KategoriID", kategoriID);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToPage("Index");
        }

        public class KategoriModel
        {
            public int KategoriID { get; set; }
            public string KategoriAdi { get; set; }
        }
    }
}
