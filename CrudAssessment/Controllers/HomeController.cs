using CrudAssessment.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CrudAssessment.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        string connString = "Data Source=DESKTOP-QTH4LPK\\SQLEXPRESS;Initial Catalog=store2;Integrated Security=True;";
        public ActionResult Index()
        {
            List<BookModelView> Books = new List<BookModelView>();
            string query = "SELECT * FROM Books";
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Books.Add(new BookModelView
                            {
                                book_id = Convert.ToInt32(sdr["book_id"]),
                                title = Convert.ToString(sdr["title"]),
                                author = Convert.ToString(sdr["author"]),
                                price = Convert.ToString(sdr["price"]),
                                quantity_available = Convert.ToInt32(sdr["quantity_available"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (Books.Count == 0)
            {
                Books.Add(new BookModelView());
            }
            return View(Books);
        }


        public ActionResult BookDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookModelView Book = new BookModelView();
            string query = "SELECT * FROM Books where book_id=" + id;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Book = new BookModelView
                            {
                                book_id = Convert.ToInt32(sdr["book_id"]),
                                title = Convert.ToString(sdr["title"]),
                                author = Convert.ToString(sdr["author"]),
                                price = Convert.ToString(sdr["price"]),
                                quantity_available = Convert.ToInt32(sdr["quantity_available"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (Book == null)
            {
                return HttpNotFound();
            }
            return View(Book);
        }
    }
}