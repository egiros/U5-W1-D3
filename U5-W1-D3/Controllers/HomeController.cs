using Microsoft.Ajax.Utilities;
using U5_W1_D3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace U5_W1_D3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Prodotti> prodotti = new List<Prodotti>();
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Articoli";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Prodotti prodotto = new Prodotti();
                        prodotto.IdArticolo = Convert.ToInt32(reader["IdArticolo"]);
                        prodotto.Nome = reader["Nome"].ToString();
                        prodotto.Descrizione = reader["Descrizione"].ToString();
                        prodotto.Prezzo = Convert.ToDecimal(reader["Prezzo"]);
                        prodotto.imgPath = reader["imgPath"].ToString();
                        prodotto.imgAlt1 = reader["imgAlt1"].ToString();
                        prodotto.imgAlt2 = reader["imgAlt2"].ToString();
                        prodotto.Visibile = Convert.ToBoolean(reader["Visibile"]);
                        prodotti.Add(prodotto);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Error: ");
                    Response.Write(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                return View(prodotti);
            }
        }

        public ActionResult CreateArticoli() { return View(); }
        [HttpPost]
        public ActionResult CreateArticoli(Prodotti prodotto)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "INSERT INTO Articoli (Nome, Descrizione, Prezzo, imgPath, imgAlt1, imgAlt2,  Visibile)" +
                    " VALUES (@Nome, @Descrizione, @Prezzo, @imgPath, @imgAlt1, @ingAlt2, @Visibile)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", prodotto.Nome);
                cmd.Parameters.AddWithValue("@Descrizione", prodotto.Descrizione);
                cmd.Parameters.AddWithValue("@Prezzo", prodotto.Prezzo);
                cmd.Parameters.AddWithValue("@imgPath", prodotto.imgPath);
                cmd.Parameters.AddWithValue("@imgAlt1", prodotto.imgAlt1);
                cmd.Parameters.AddWithValue("@imgAlt2", prodotto.imgAlt2);
                cmd.Parameters.AddWithValue("@Visibile", prodotto.Visibile);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("Error: ");
                Response.Write(ex.Message);

            }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditProduct(int id)
        {
            
        }

        public ActionResult DeleteProduct(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                string query = "UPDATE Articoli SET IsVisible = 0 WHERE IdProdotto = " + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}