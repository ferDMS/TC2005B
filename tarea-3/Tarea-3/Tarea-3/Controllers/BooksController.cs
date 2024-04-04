using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Policy;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tarea_3.Controllers
{

    [Route("api/books")]
    public class BooksController : Controller
    {
        // Connection string
        private string _connectionString = "Server=127.0.0.1; Port=3306; Database=books; Uid=root; Password=root;";

        // Book class
        public class Book
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Url { get; set; }
        }

        // Page class
        public class Page
        {
            public int Id { get; set; }
            public int BookId { get; set; }
            public string Content { get; set; }
        }

        private List<Book> _books = new List<Book>();

        private List<Page> _pages = new List<Page>();


        // GET: api/books
        [HttpGet]
        public IEnumerable<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("book_getAllBooks", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Book book = new Book
                            {
                                Id = reader.GetInt32("id_book"),
                                Title = reader.GetString("title"),
                                Author = reader.GetString("author"),
                                Url = reader.GetString("url")
                            };

                            books.Add(book);
                        }
                    }
                }
            }

            return books;
        }


        // GET api/books/id
        [HttpGet("{bookID}")]
        public ActionResult<Book> GetBookByID(int bookID)
        {
            Book book = null;

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("book_getBookByID", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookID", bookID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            book = new Book
                            {
                                Id = reader.GetInt32("id_book"),
                                Title = reader.GetString("title"),
                                Author = reader.GetString("author"),
                                Url = reader.GetString("url")
                            };
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }

            return book;
        }

        // GET api/books/id/pages
        [HttpGet("{bookID}/pages")]
        public IEnumerable<Page> GetPagesByBookID(int bookID)
        {
            List<Page> pages = new List<Page>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "book_getPagesByBookID";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@bookID", bookID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Page page = new Page
                            {
                                Id = reader.GetInt32("id_page"),
                                BookId = reader.GetInt32("id_book"),
                                Content = reader.GetString("content")
                            };

                            pages.Add(page);
                        }
                    }
                }
            }

            return pages;
        }

        // PUT api/books/id
        [HttpPut("{bookID}")]
        public IActionResult UpdateBook(int bookID, [FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Invalid book object provided.");
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "book_updateBook";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@bookID", bookID);
                    cmd.Parameters.AddWithValue("@newTitle", book.Title);
                    cmd.Parameters.AddWithValue("@newAuthor", book.Author);
                    cmd.Parameters.AddWithValue("@newUrl", book.Url);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Book updated successfully.");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        // PUT api/books/id/id
        [HttpPut("{bookID}/{pageID}")]
        public IActionResult UpdatePage(int bookID, int pageID, [FromBody] Page page)
        {
            if (page == null)
            {
                return BadRequest("Invalid page object provided.");
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "book_updatePage";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@pageID", pageID);
                    cmd.Parameters.AddWithValue("@newContent", page.Content);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Page updated successfully.");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        // POST api/books/id/pages
        [HttpPost("{bookID}/pages")]
        public IActionResult InsertPage(int bookID, [FromBody] Page page)
        {
            if (page == null)
            {
                return BadRequest("Invalid page object provided.");
            }

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "book_insertPage";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@bookID", bookID);
                    cmd.Parameters.AddWithValue("@pageContent", page.Content);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Page inserted successfully.");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        // DELETE api/books/id
        [HttpDelete("{pageID}")]
        public IActionResult DeletePage(int pageID)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "book_deletePage";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@pageID", pageID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Page deleted successfully.");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
    }
}

