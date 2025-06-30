using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniAdonet.Pages.teknoloji.kategoriler
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public KategoriModel kat { get; set; }

        public void OnGet(int id)
        {
            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT KategoriID, KategoriAdi FROM Kategoriler WHERE KategoriID = @id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            kat = new KategoriModel
                            {
                                KategoriID = rdr.GetInt32(0),
                                KategoriAdi = rdr.GetString(1)
                            };
                        }
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "UPDATE Kategoriler SET KategoriAdi = @KategoriAdi WHERE KategoriID = @KategoriID";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@KategoriID", kat.KategoriID);
                    cmd.Parameters.AddWithValue("@KategoriAdi", kat.KategoriAdi);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToPage("/teknoloji/kategoriler/Index");
        }

        public class KategoriModel
        {
            public int KategoriID { get; set; }
            public string KategoriAdi { get; set; }
        }
    }
}
