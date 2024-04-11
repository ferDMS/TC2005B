using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Security.Policy;

// Para la conexión con la base de datos
using MySql.Data.MySqlClient;

// Para fácil configuración de nuevos comandos `CommandType.StoredProcedure;`
using System.Data;

// Para importar el connectionString de DB desde appsettings
using Microsoft.Extensions.Configuration;

namespace Tarea_3.Controllers
{
    [Route("api")]
    public class BooksController : Controller
    {
        // Importar el string de conexión de las appsettings
        // El constructor modificado del BooksController recibe la configuración
        private readonly IConfiguration _configuration;
        private string _connectionString;
        private string password;
        public BooksController(IConfiguration configuration)
        {
            _configuration = configuration;
            // Definir connectionString a partir de configuración recibida
            if (_configuration != null && _configuration.GetConnectionString("cloud_db") != null)
            {
                _connectionString = _configuration.GetConnectionString("cloud_db");
                // Get password from file
                password = System.IO.File.ReadAllText("password.secret");
                // Add password and SSL part to connection string
                _connectionString = _connectionString + $"Password={password};" + "SslMode=Required;" + "CertificateFile=ca.cer;";
            }
        }

        // GET: api/books
        [HttpGet("books")]
        public IEnumerable<Book> GetAllBooks()
        {
            // Definir lista de libros
            IList<Book> books = new List<Book>();

            // Crear objeto de conexión MySQL con connectionString
            // Debido a que está dentro de `using`, se cerrará la conexión automaticamente
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                // Abrir conexión MySQL
                connection.Open();

                // Crear objeto de comando, estableciendo el stored procedure y conexión donde se llamará
                // Debido a que está dentro de `using`, se cerrará el comando automaticamente
                using (MySqlCommand cmd = new MySqlCommand("getAllBooks", connection))
                {
                    // Establecer explicitamente que el comando llama a un stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Crear objeto de reader, ejecutando el comando
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Leer las filas (libros) regresadas una por una
                        while (reader.Read())
                        {
                            // Crear un nuevo objeto de Book y guardar su información
                            Book book = new Book();
                            book.Id = reader.GetInt32("id_book");
                            book.Title = reader.GetString("title");
                            book.Author = reader.GetString("author");
                            book.Cover = reader.GetString("cover");

                            // Añadir Book a la lista
                            books.Add(book);
                        }
                    }
                    // El comando se cierra automáticamente
                }
                // La conexión a DB se cierra automáticamente
            }

            // Regresar lista de objetos Book
            return books;
        }


        // GET api/book/id
        [HttpGet("book/{bookID}")]
        public Book GetBook(int bookID)
        {
            // Como regresamos la información de un único libro, se inicializa el objeto Book
            Book book = new Book();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("getBook", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadir parámetro del stored procedure con el nombre dado directamente en la DB
                    cmd.Parameters.AddWithValue("@_id_book", bookID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Si se regresó una fila de la DB, entonces encontramos el libro
                        if (reader.Read())
                        {
                            // Añadir información del libro
                            book.Id = reader.GetInt32("id_book");
                            book.Title = reader.GetString("title");
                            book.Author = reader.GetString("author");
                            book.Cover = reader.GetString("cover");
                        }
                    }
                }
            }
            // Regresar el objeto Book con la información del libro
            // Si no se regresa ni una fila, entonces no encontramos nada y se regresa Book vacío
            return book;
        }


        // PUT api/book/id
        [HttpPut("book/{bookID}")]
        public void UpdateBook(int bookID, [FromBody] Book book)
        {
            // Nada se predefine aquí pues no regresamos nada

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("updateBook", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadir los 4 parámetros, cuidando añadirlos con los nombres dados en el stored procedure
                    cmd.Parameters.AddWithValue("@_id_book", bookID);
                    cmd.Parameters.AddWithValue("@new_title", book.Title);
                    cmd.Parameters.AddWithValue("@new_author", book.Author);
                    cmd.Parameters.AddWithValue("@new_cover", book.Cover);

                    // Preprocesamiento del comando para mayor eficiencia
                    cmd.Prepare();

                    // Ejecutar el comando.
                    // Este se usa específicamente para querys que no regresan valores (a contrario de cmd.ExecuteReader para SELECT)
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // POST api/book
        [HttpPost("book")]
        public void InsertBook([FromBody] Book book)
        {
            // Nada se predefine aquí pues no regresamos nada

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("insertBook", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadir los 4 parámetros, cuidando añadirlos con los nombres dados en el stored procedure
                    cmd.Parameters.AddWithValue("@_title", book.Title);
                    cmd.Parameters.AddWithValue("@_author", book.Author);
                    cmd.Parameters.AddWithValue("@_cover", book.Cover);

                    // Preprocesamiento del comando para mayor eficiencia
                    cmd.Prepare();

                    // Ejecutar el comando.
                    // Este se usa específicamente para querys que no regresan valores (a contrario de cmd.ExecuteReader para SELECT)
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // DELETE api/book/id
        [HttpDelete("book/{bookID}")]
        public void DeleteBook(int bookID)
        {
            // Nada se predefine aquí pues no regresamos nada

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("deleteBook", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@_id_book", bookID);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // GET api/book/id/pages
        [HttpGet("book/{bookID}/pages")]
        public IEnumerable<Page> GetBookAllPages(int bookID)
        {
            // Regresamos una lista de objetos tipo Page
            List<Page> pages = new List<Page>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("getBookAllPages", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadimos el único parámetro del stored procedure, que es el id del libro
                    cmd.Parameters.AddWithValue("@_id_book", bookID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Leer las filas (páginas) regresadas una por una
                        while (reader.Read())
                        {
                            // Crear el objeto de Page con su información y agregarlo a la lista
                            Page page = new Page();
                            page.Id = reader.GetInt32("id_page");
                            page.BookId = reader.GetInt32("id_book");
                            page.PageNum = reader.GetInt32("page_num");
                            page.Content = reader.GetString("content");

                            pages.Add(page);
                        }
                    }
                }
            }
            // Si no encontramos el libro, no regresamos ninguna fila, por lo que regresaríamos lista vacía
            // Si encontramos el libro pero no páginas, igualmente regresamos una lista vacía
            return pages;
        }


        // GET api/book/id/page/num
        [HttpGet("book/{bookID}/page/{pageNum}")]
        public Page GetBookPage(int bookID, int pageNum)
        {
            // Regresamos un objeto tipo Page
            Page page = new Page();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("getBookPage", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadimos el parámetro de libro y número de página a obtener
                    cmd.Parameters.AddWithValue("@_id_book", bookID);
                    cmd.Parameters.AddWithValue("@_page_num", pageNum);

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Si se regresó una fila de la DB, entonces encontramos la página
                        if (reader.Read())
                        {
                            // Colocar la información de la página en el objeto
                            page.Id = reader.GetInt32("id_page");
                            page.BookId = reader.GetInt32("id_book");
                            page.PageNum = reader.GetInt32("page_num");
                            page.Content = reader.GetString("content");
                        }
                    }
                }
            }
            // Si no encontramos el libro, no regresamos ninguna fila, por lo que regresaríamos lista vacía
            // Si encontramos el libro pero no páginas, igualmente regresamos una lista vacía
            return page;
        }


        // PUT api/book/id/page/num
        [HttpPut("book/{bookID}/page/{pageNum}")]
        public void UpdateBookPage(int bookID, int pageNum, [FromBody] Page page)
        {
            // Nada se predefine aquí pues no regresamos nada

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("updatePage", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Los parámetros a pasar al stored procedure, que también incluye el bookID
                    cmd.Parameters.AddWithValue("@_id_book", bookID);
                    cmd.Parameters.AddWithValue("@_page_num", pageNum);
                    cmd.Parameters.AddWithValue("@new_content", page.Content);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // POST api/book/id/page
        [HttpPost("book/{bookID}/page")]
        public void InsertBookPage(int bookID, [FromBody] Page page)
        {
            // Nada se predefine aquí pues no regresamos nada

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("insertBookPage", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@_id_book", bookID);
                    cmd.Parameters.AddWithValue("@_content", page.Content);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // DELETE api/book/id/page/num
        [HttpDelete("book/{bookID}/page/{pageNum}")]
        public void DeleteBookPage(int bookID, int pageNum)
        {
            // Nada se predefine aquí pues no regresamos nada

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("deleteBookPage", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@_id_book", bookID);
                    cmd.Parameters.AddWithValue("@_page_num", pageNum);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

