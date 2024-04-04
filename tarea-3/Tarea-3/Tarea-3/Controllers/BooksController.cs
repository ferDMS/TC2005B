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
    [Route("api/books")]
    public class BooksController : Controller
    {
        // Importar el string de conexión de las appsettings
        // El constructor modificado del BooksController recibe la configuración
        private readonly IConfiguration _configuration;
        private string _connectionString;
        public BooksController(IConfiguration configuration)
        {
            _configuration = configuration;
            // Definir connectionString a partir de configuración recibida
            if (_configuration != null && _configuration.GetConnectionString("db1") != null)
            {
                _connectionString = _configuration.GetConnectionString("db1");
            }
        }


        // GET: api/books
        [HttpGet]
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
                using (MySqlCommand cmd = new MySqlCommand("book_getAllBooks", connection))
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
                            book.Url = reader.GetString("url");

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


        // GET api/books/id
        [HttpGet("{bookID}")]
        public Book GetBookByID(int bookID)
        {
            // Como regresamos la información de un único libro, se inicializa el objeto Book
            Book book = new Book();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("book_getBookByID", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadir parámetro del stored procedure con el nombre dado directamente en la DB
                    cmd.Parameters.AddWithValue("@bookID", bookID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Si se regresó una fila de la DB, entonces encontramos el libro
                        if (reader.Read())
                        {
                            // Añadir información del libro
                            book.Id = reader.GetInt32("id_book");
                            book.Title = reader.GetString("title");
                            book.Author = reader.GetString("author");
                            book.Url = reader.GetString("url");
                        }
                    }
                }
            }
            // Regresar el objeto Book con la información del libro
            // Si no se regresa ni una fila, entonces no encontramos nada y se regresa Book vacío
            return book;
        }


        // GET api/books/id/pages
        [HttpGet("{bookID}/pages")]
        public IEnumerable<Page> GetPagesByBookID(int bookID)
        {
            // Regresamos una lista de objetos tipo Page
            List<Page> pages = new List<Page>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("book_getPagesByBookID", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadimos el único parámetro del stored procedure, que es el id del libro
                    cmd.Parameters.AddWithValue("@bookID", bookID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Leer las filas (páginas) regresadas una por una
                        while (reader.Read())
                        {
                            // Crear el objeto de Page con su información y agregarlo a la lista
                            Page page = new Page();
                            page.Id = reader.GetInt32("id_page");
                            page.BookId = reader.GetInt32("id_book");
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


        // PUT api/books/id
        [HttpPut("{bookID}")]
        public void UpdateBook(int bookID, [FromBody] Book book)
        {
            // Nada se predefine aquí pues no regresamos nada

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("book_updateBook", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadir los 4 parámetros, cuidando añadirlos con los nombres dados en el stored procedure
                    cmd.Parameters.AddWithValue("@bookID", bookID);
                    cmd.Parameters.AddWithValue("@newTitle", book.Title);
                    cmd.Parameters.AddWithValue("@newAuthor", book.Author);
                    cmd.Parameters.AddWithValue("@newUrl", book.Url);

                    // Preprocesamiento del comando para mayor eficiencia
                    cmd.Prepare();

                    // Ejecutar el comando.
                    // Este se usa específicamente para querys que no regresan valores (a contrario de cmd.ExecuteReader para SELECT)
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // PUT api/books/id/page/id
        [HttpPut("{bookID}/page/{pageID}")]
        public void UpdatePage(int bookID, int pageID, [FromBody] Page page)
        {
            // Nada se predefine aquí pues no regresamos nada

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("book_updatePage", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Los parámetros a pasar al stored procedure, que también incluye el bookID
                    cmd.Parameters.AddWithValue("@bookID", bookID);
                    cmd.Parameters.AddWithValue("@pageID", pageID);
                    cmd.Parameters.AddWithValue("@newContent", page.Content);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // POST api/books/id/page
        [HttpPost("{bookID}/page")]
        public void InsertPage(int bookID, [FromBody] Page page)
        {
            // Nada se predefine aquí pues no regresamos nada

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("book_insertPage", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@bookID", bookID);
                    cmd.Parameters.AddWithValue("@pageContent", page.Content);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // DELETE api/books/id
        [HttpDelete("{bookID}/page/{pageID}")]
        public void DeletePage(int bookID, int pageID)
        {
            // Nada se predefine aquí pues no regresamos nada

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand("book_deletePage", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@bookID", bookID);
                    cmd.Parameters.AddWithValue("@pageID", pageID);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

