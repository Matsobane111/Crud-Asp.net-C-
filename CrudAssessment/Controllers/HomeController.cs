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

        public ActionResult CreateBook()
        {
            return View();
        }

        // POST: Home/CreateBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook([Bind(Include = "book_id,title,author,price,quantity_available")] BookModelView bookModelView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        
                        string query = "insert into Books values (@title, @author,@price,@quantity_available)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@title", bookModelView.title);
                            cmd.Parameters.AddWithValue("@author", bookModelView.author);
                            cmd.Parameters.AddWithValue("@price", bookModelView.price);
                            cmd.Parameters.AddWithValue("@quantity_available", bookModelView.quantity_available);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            catch
            {
            }
            return View(bookModelView);
        }


        public ActionResult UpdateBook(int? id)
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBook([Bind(Include = "book_id,title,author,price,quantity_available")] BookModelView bookModelView)
        {
            if (ModelState.IsValid)
            {
                string query = "UPDATE Books SET title = @title, author = @author,price=@price,quantity_available=@quantity_available Where book_id =@book_id";
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@title", bookModelView.title);
                        cmd.Parameters.AddWithValue("@author", bookModelView.author);
                        cmd.Parameters.AddWithValue("@price", bookModelView.price);
                        cmd.Parameters.AddWithValue("@quantity_available", bookModelView.quantity_available);
                        cmd.Parameters.AddWithValue("@book_id", bookModelView.book_id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return RedirectToAction("Index");
            }
            return View(bookModelView);
        }


        public ActionResult DeleteBook(int? id)
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
            return View(Book);
        }

  
        [HttpPost, ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBook(int id)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "Delete FROM Books where book_id='" + id + "'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return RedirectToAction("Index");
        }


    }
}