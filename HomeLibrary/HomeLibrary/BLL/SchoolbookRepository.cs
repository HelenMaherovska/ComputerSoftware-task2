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
    public class SchoolbookRepository : AbstractBookRepository<Schoolbook>
    {
        private const string GetAllSchoolbooksQuery = "SELECT * FROM Schoolbook";
        private const string GetSchoolbookByIdQuery = "SELECT * FROM Schoolbook WHERE id = @id";
        private const string InsertIntoSchoolbooksQuery = "INSERT INTO Schoolbook VALUES(@id, @form, @subject);";
        private const string UpdateSchoolbookQuery = "UPDATE Schoolbook SET form = @form, subject = @subject WHERE id = @id;";
        private const string DeleteSchoolbookQuery = "DELETE FROM Schoolbook WHERE id = @id;";

        public override int Add(Schoolbook book)
        {
            var book_id = base.Add(book);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(InsertIntoSchoolbooksQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", book_id);
                    command.Parameters.AddWithValue("@form", book.Form);
                    command.Parameters.AddWithValue("@subject", book.Subject);

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
                using (SqlCommand command = new SqlCommand(DeleteSchoolbookQuery, connection))
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

        public override int Edit(Schoolbook book)
        {
            var book_id = base.Edit(book);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(UpdateSchoolbookQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", book_id);
                    command.Parameters.AddWithValue("@form", book.Form);
                    command.Parameters.AddWithValue("@subject", book.Subject);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        return book_id;
                    }
                }
            }
        }

        public override BookContainer<Schoolbook> GetAll(string type = null, BookContainer<Schoolbook> section = null, int? sectionId = null)
        {
            SchoolbookSection books = new SchoolbookSection();
            base.GetAll("Schoolbook", books, 3);

            return GetSchoolbooks(books, type, section, sectionId);
        }

        public override Schoolbook GetById(int id, string type = null)
        {
            var book = base.GetById(id, "Schoolbook");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(GetSchoolbookByIdQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            book.Form = (int)reader["form"]; 
                            book.Subject = (string)reader["subject"];
                        }

                        return book;
                    }
                }
            }
        }

        public override BookContainer<Schoolbook> Search(string text, BookFilter filter, string type = null, BookContainer<Schoolbook> section = null, int? sectionId = null)
        {
            SchoolbookSection books = new SchoolbookSection();
            base.Search(text, filter, "Schoolbook", books, 3);

            return GetSchoolbooks(books, type, section, sectionId);
        }

        private BookContainer<Schoolbook> GetSchoolbooks(SchoolbookSection books, string type, BookContainer<Schoolbook> section, int? sectionId, string query = GetAllSchoolbooksQuery)
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
                                books.Books.First(b => b.Id == bookId).Form = (int)reader["form"]; 
                                books.Books.First(b => b.Id == bookId).Subject = (string)reader["subject"];
                            }
                        }
                        return books;
                    }
                }
            }
        }
    }
}
