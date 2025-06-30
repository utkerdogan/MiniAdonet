using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace MiniAdonet.Pages.teknoloji.urunler
{
    public class IndexModel : PageModel
    {
        public List<UrunModel> UrunList = new List<UrunModel>();

        public void OnGet()
        {
            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = @"SELECT u.UrunID, u.UrunAdi, u.Fiyat, u.Stok, k.KategoriAdi 
                               FROM Urunler u 
                               INNER JOIN Kategoriler k ON u.KategoriID = k.KategoriID";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        UrunModel u = new UrunModel();
                        u.UrunID = rdr.GetInt32(0);
                        u.UrunAdi = rdr.GetString(1);
                        u.Fiyat = rdr.GetDecimal(2);
                        u.Stok = rdr.GetInt32(3);
                        u.KategoriAdi = rdr.GetString(4);

                        UrunList.Add(u);
                    }
                }
            }
        }
    }

    public class UrunModel
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public decimal Fiyat { get; set; }
        public int Stok { get; set; }
        public string KategoriAdi { get; set; }
    }
}
