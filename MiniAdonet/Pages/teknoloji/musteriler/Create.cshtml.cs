using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MiniAdonet.Pages.teknoloji.musteriler
{
    public class CreateModel : PageModel
    {
        public IActionResult OnPost()
        {
            string adSoyad = Request.Form["AdSoyad"];
            string email = Request.Form["Email"];
            string telefon = Request.Form["Telefon"];

            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "INSERT INTO Musteriler (AdSoyad, Email, Telefon) VALUES (@AdSoyad, @Email, @Telefon)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@AdSoyad", adSoyad);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Telefon", telefon);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToPage("/teknoloji/satislar/Create");
        }
    }
}
