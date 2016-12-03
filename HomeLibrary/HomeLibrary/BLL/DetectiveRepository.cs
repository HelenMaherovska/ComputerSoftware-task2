using HomeLibrary.Containers;
using HomeLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HomeLibrary.Helpers.Enums;

namespace HomeLibrary.BLL
{
    public class DetectiveRepository : AbstractBookRepository<Detective>
    {
        private const string GetAllDetectivesQuery = "SELECT * FROM Detective";
        private const string GetDetectiveByIdQuery = "SELECT * FROM Detective WHERE id = @id";
        private const string InsertIntoDetectivesQuery = "INSERT INTO Detective VALUES(@id, @heroes_number);";
        private const string UpdateDetectiveQuery = "UPDATE Detective SET heroes_number = @heroes_number WHERE id = @id;";
        private const string DeleteDetectiveQuery = "DELETE FROM Detective WHERE id = @id;";

        public override int Add(Detective book)
        {
            var book_id = base.Add(book);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(InsertIntoDetectivesQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", book_id);
                    command.Parameters.AddWithValue("@heroes_number", book.HeroesNumber);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        return book_id;
                    }
                }
            }
        }

        public override void Delete(int id, int estimate_id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(DeleteDetectiveQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        base.Delete(id, estimate_id);
                    }
                }
            }
        }

        public override int Edit(Detective book)
        {
            var book_id = base.Edit(book);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(UpdateDetectiveQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", book_id);
                    command.Parameters.AddWithValue("@heroes_number", book.HeroesNumber);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        return book_id;
                    }
                }
            }
        }

        public override BookContainer<Detective> GetAll(string type = null, BookContainer<Detective> section = null, int? sectionId = null)
        {
            DetectiveSection books = new DetectiveSection();
            base.GetAll("Detective", books, 1);

            return GetDetectives(books, type, section, sectionId);
        }

        public override Detective GetById(int id, string type = null)
        {
            var book = base.GetById(id, "Detective");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(GetDetectiveByIdQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            book.HeroesNumber = (int)reader["heroes_number"];
                        }

                        return book;
                    }
                }
            }
        }

        public override BookContainer<Detective> Search(string text, BookFilter filter, string type = null, BookContainer<Detective> section = null, int? sectionId = null)
        {
            DetectiveSection books = new DetectiveSection();
            base.Search(text, filter, "Detective", books, 1);

            return GetDetectives(books, type, section, sectionId);
        }

        private BookContainer<Detective> GetDetectives(DetectiveSection books, string type, BookContainer<Detective> section, int? sectionId, string query = GetAllDetectivesQuery)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var bookId = (int)reader["id"];

                            if (books.Books.FirstOrDefault(b => b.Id == bookId) != null)
                            {
                                books.Books.First(b => b.Id == bookId).HeroesNumber = (int)reader["heroes_number"];
                            }                            
                        }
                        return books;
                    }
                }
            }
        }
    }
}
