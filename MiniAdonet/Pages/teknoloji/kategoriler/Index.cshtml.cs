using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniAdonet.Pages.teknoloji.kategoriler
{
	public class IndexModel : PageModel
    {
        public List<KategoriList> KategoriList = new List<KategoriList>();

        public void OnGet()
        {
            string connectionString = "Server=localhost;Database=MiniAdonet;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = @"SELECT  k.KategoriID, k.KategoriAdi 
                               FROM Kategoriler k";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        KategoriList k = new KategoriList();
                        k.KategoriID = rdr.GetInt32(0);
                        k.KategoriAdi = rdr.GetString(1);

                        KategoriList.Add(k);
                    }
                }
            }
        }
    }

    public class KategoriList
    {
        public int KategoriID { get; set; }
        public string KategoriAdi { get; set; }
    }
}
