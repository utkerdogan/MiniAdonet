using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static MiniAdonet.Pages.teknoloji.urunler.CreateModel;

namespace MiniAdonet.Pages.teknoloji.urunler
{
    public class EditModel : PageModel
    {
        public UrunModel urun = new UrunModel();
        public List<KategoriModel> KategoriList = new List<KategoriModel>();

        public void OnGet()
        {
            string id = Request.Query["id"];

            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT * FROM Urunler WHERE UrunID = @UrunID";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UrunID", id);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        urun.UrunID = rdr.GetInt32(0);
                        urun.UrunAdi = rdr.GetString(1);
                        urun.Fiyat = rdr.GetDecimal(2);
                        urun.Stok = rdr.GetInt32(3);
                        urun.KategoriID = rdr.GetInt32(4);
                    }
                }
            }
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
            int id = int.Parse(Request.Form["urunid"]);
            string urunAdi = Request.Form["urunadi"];
            decimal fiyat = decimal.Parse(Request.Form["fiyat"]);
            int stok = int.Parse(Request.Form["stok"]);
            int kategoriID = int.Parse(Request.Form["kategoriid"]);

            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = @"UPDATE Urunler 
                               SET UrunAdi = @UrunAdi, Fiyat = @Fiyat, Stok = @Stok, KategoriID = @KategoriID 
                               WHERE UrunID = @UrunID";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UrunAdi", urunAdi);
                    cmd.Parameters.AddWithValue("@Fiyat", fiyat);
                    cmd.Parameters.AddWithValue("@Stok", stok);
                    cmd.Parameters.AddWithValue("@KategoriID", kategoriID);
                    cmd.Parameters.AddWithValue("@UrunID", id);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToPage("Index");
        }

        public class UrunModel
        {
            public int UrunID { get; set; }
            public string UrunAdi { get; set; }
            public decimal Fiyat { get; set; }
            public int Stok { get; set; }
            public int KategoriID { get; set; }
        }
    }
}
