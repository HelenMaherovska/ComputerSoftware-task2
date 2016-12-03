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
    public class SpecialLiteratureRepository : AbstractBookRepository<SpecialLiterature>
    {
        private const string GetAllSpecialLiteratureQuery = "SELECT * FROM SpecialLiterature";
        private const string GetSpecialLiteratureByIdQuery = "SELECT * FROM SpecialLiterature WHERE id = @id";
        private const string InsertIntoSpecialLiteratureQuery = "INSERT INTO SpecialLiterature VALUES(@id, @field);";
        private const string UpdateSpecialLiteratureQuery = "UPDATE SpecialLiterature SET field = @field WHERE id = @id;";
        private const string DeleteSpecialLiteratureQuery = "DELETE FROM SpecialLiterature WHERE id = @id;";

        public override int Add(SpecialLiterature book)
        {
            var book_id = base.Add(book);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(InsertIntoSpecialLiteratureQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", book_id);
                    command.Parameters.AddWithValue("@field", book.Field);

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
                using (SqlCommand command = new SqlCommand(DeleteSpecialLiteratureQuery, connection))
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

        public override int Edit(SpecialLiterature book)
        {
            var book_id = base.Edit(book);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(UpdateSpecialLiteratureQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", book_id);
                    command.Parameters.AddWithValue("@field", book.Field);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        return book_id;
                    }
                }
            }
        }

        public override BookContainer<SpecialLiterature> GetAll(string type = null, BookContainer<SpecialLiterature> section = null, int? sectionId = null)
        {
            SpecialLiteratureSection books = new SpecialLiteratureSection();
            base.GetAll("SpecialLiterature", books, 2);

            return GetSpecialLiterature(books, type, section, sectionId);
        }

        public override SpecialLiterature GetById(int id, string type = null)
        {
            var book = base.GetById(id, "SpecialLiterature");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(GetSpecialLiteratureByIdQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            book.Field = (string)reader["field"];
                        }

                        return book;
                    }
                }
            }
        }

        public override BookContainer<SpecialLiterature> Search(string text, BookFilter filter, string type = null, BookContainer<SpecialLiterature> section = null, int? sectionId = null)
        { 
            SpecialLiteratureSection books = new SpecialLiteratureSection();
            base.Search(text, filter, "SpecialLiterature", books, 2);

            return GetSpecialLiterature(books, type, section, sectionId);
        }

        private BookContainer<SpecialLiterature> GetSpecialLiterature(SpecialLiteratureSection books, string type, BookContainer<SpecialLiterature> section, int? sectionId, string query = GetAllSpecialLiteratureQuery)
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
                                books.Books.First(b => b.Id == bookId).Field = (string)reader["field"];
                            }
                        }
                        return books;
                    }
                }
            }
        }
    }
}
