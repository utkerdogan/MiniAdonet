using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static MiniAdonet.Pages.teknoloji.urunler.CreateModel;

namespace MiniAdonet.Pages.teknoloji.kategoriler
{
	public class CreateModel : PageModel
    {
        public IActionResult OnPost()
        {
            string kategoriAdi = Request.Form["KategoriAdi"];

            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "INSERT INTO Kategoriler (KategoriAdi) VALUES (@KategoriAdi)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@KategoriAdi", kategoriAdi);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToPage("/teknoloji/kategoriler/Index");
        }
    }
}
