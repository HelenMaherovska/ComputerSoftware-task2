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
    public abstract class AbstractBookRepository<T> : AbstractRepository, IBookRepository<T>
        where T : Book
    {
        private const string GetAllBooksQuery = @"SELECT b.id, b.author, b.title, b.edition, b.[year], b.pages, b.section_id, b.estimate_id, s.name, e.origin, e.[availability], e.worth, e.recommendation
                                                FROM Book b INNER JOIN Section s ON b.section_id = s.id 
                                                INNER JOIN Estimate e ON b.estimate_id = e.id 
                                                WHERE section_id = @id";

        private const string GetBookByIdQuery = @"SELECT b.id, b.author, b.title, b.edition, b.[year], b.pages, b.section_id, b.estimate_id, s.name, e.origin, e.[availability], e.worth, e.recommendation
                                                FROM Book b INNER JOIN Section s ON b.section_id = s.id 
                                                INNER JOIN Estimate e ON b.estimate_id = e.id 
                                                WHERE b.id = @id";

        private const string InsertIntoEstimateQuery = @"INSERT INTO Estimate 
                                                        OUTPUT Inserted.id AS ""estimate_id""
                                                        VALUES(@origin, @availability, @worth, @recommendation);";

        private const string InsertIntoBookQuery = @"INSERT INTO Book 
                                                    OUTPUT Inserted.id AS ""book_id""
                                                    VALUES(@author, @title, @edition, @year, @pages, @section_id, @estimate_id);";

        private const string UpdateBookQuery = @"UPDATE Book 
                                                 SET author = @author, title = @title, edition = @edition, year = @year, pages = @pages, section_id = @section_id
                                                 OUTPUT Inserted.id AS ""book_id""
                                                 WHERE id = @id";

        private const string UpdateEstimateQuery = @"UPDATE Estimate 
                                                   SET origin = @origin, availability = @availability, worth = @worth, recommendation = @recommendation
                                                   OUTPUT Inserted.id AS ""estimate_id""
                                                   WHERE id = @id";

        private const string DeleteBookQuery = @"DELETE FROM Book WHERE id = @id";

        private const string DeleteEstimateQuery = @"DELETE FROM Estimate WHERE id = @id";

        private const string SearchBookQuery = @"SELECT b.id, b.author, b.title, b.edition, b.[year], b.pages, b.section_id, b.estimate_id, s.name, e.origin, e.[availability], e.worth, e.recommendation
                                           FROM Book b INNER JOIN Section s ON b.section_id = s.id 
                                           INNER JOIN Estimate e ON b.estimate_id = e.id 
                                           WHERE section_id = @id AND ";

        public virtual int Add(T book)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(InsertIntoBookQuery, connection))
                {
                    var estimate_id = AddEstimate(book.Estimate);

                    command.Parameters.AddWithValue("@author", book.Author);
                    command.Parameters.AddWithValue("@title", book.Title);
                    command.Parameters.AddWithValue("@edition", book.Edition);
                    command.Parameters.AddWithValue("@year", book.Year);
                    command.Parameters.AddWithValue("@pages", book.Pages);
                    command.Parameters.AddWithValue("@section_id", book.Section.Id);
                    command.Parameters.AddWithValue("@estimate_id", estimate_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int book_id = -1;
                        if (reader.Read())
                        {
                            book_id = (int)reader["book_id"];
                        }

                        return book_id;
                    }
                }
            }
        }

        public virtual void Delete(int id, int estimate_id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(DeleteBookQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        DeleteEstimate(estimate_id);
                    }
                }
            }
        }

        public virtual int Edit(T book)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(UpdateBookQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", book.Id);
                    command.Parameters.AddWithValue("@author", book.Author);
                    command.Parameters.AddWithValue("@title", book.Title);
                    command.Parameters.AddWithValue("@edition", book.Edition);
                    command.Parameters.AddWithValue("@year", book.Year);
                    command.Parameters.AddWithValue("@pages", book.Pages);
                    command.Parameters.AddWithValue("@section_id", book.Section.Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int book_id = -1;
                        if (reader.Read())
                        {
                            book_id = (int)reader["book_id"];
                            EditEstimate(book.Estimate);
                        }

                        return book_id;
                    }
                }
            }
        }

        public virtual BookContainer<T> GetAll(string type, BookContainer<T> section, int? sectionId)
        {
            return GetBooks(type, section, sectionId);
        }

        public virtual T GetById(int id, string type = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(GetBookByIdQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        T book = (T)Activator.CreateInstance(Type.GetType("HomeLibrary.Entities." + type));

                        if (reader.Read())
                        {                           

                            book.Id = (int)reader["id"];
                            book.Author = (string)reader["author"];
                            book.Title = (string)reader["title"];
                            book.Edition = (string)reader["edition"];
                            book.Year = (int)reader["year"];
                            book.Pages = (int)reader["pages"];
                            book.Section.Name = (string)reader["name"];
                            book.Estimate.Origin = (string)reader["origin"];
                            book.Estimate.Availability = (bool)reader["availability"];
                            book.Estimate.Worth = (string)reader["worth"];
                            book.Estimate.Recommendation = (string)reader["recommendation"];

                        }

                        return book;
                    }
                }
            }
        }

        public virtual BookContainer<T> Search(string text, BookFilter filter, string type, BookContainer<T> section, int? sectionId)
        {
            string SearchQuery = "";
            switch (filter)
            {
                case BookFilter.Author:
                    SearchQuery = string.Concat(SearchBookQuery, "author LIKE '%", text, "%'");
                    break;
                case BookFilter.Title:
                    SearchQuery = string.Concat(SearchBookQuery, "title LIKE '%", text, "%'");
                    break;
                case BookFilter.Edition:
                    SearchQuery = string.Concat(SearchBookQuery, "edition LIKE '%", text, "%'");
                    break;
                default:
                    break;
            }
            
            return GetBooks(type, section, sectionId, SearchQuery);
        }

        private BookContainer<T> GetBooks(string type, BookContainer<T> section, int? sectionId, string query = GetAllBooksQuery)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", sectionId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        BookContainer<T> books = section;

                        while (reader.Read())
                        {
                            T book = (T)Activator.CreateInstance(Type.GetType("HomeLibrary.Entities." + type));

                            book.Id = (int)reader["id"];
                            book.Author = (string)reader["author"];
                            book.Title = (string)reader["title"];
                            book.Edition = (string)reader["edition"];
                            book.Year = (int)reader["year"];
                            book.Pages = (int)reader["pages"];
                            book.Section.Name = (string)reader["name"];
                            book.Estimate.Origin = (string)reader["origin"];
                            book.Estimate.Availability = (bool)reader["availability"];
                            book.Estimate.Worth = (string)reader["worth"];
                            book.Estimate.Recommendation = (string)reader["recommendation"];

                            books.Books.Add(book);

                        }

                        books.BooksCount = books.Books.Count;

                        return books;
                    }
                }
            }
        }

        private int AddEstimate(Estimate estimate)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(InsertIntoEstimateQuery, connection))
                {
                    command.Parameters.AddWithValue("@origin", estimate.Origin);
                    command.Parameters.AddWithValue("@availability", estimate.Availability);
                    command.Parameters.AddWithValue("@worth", estimate.Worth);
                    command.Parameters.AddWithValue("@recommendation", estimate.Recommendation);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int estimate_id = -1;
                        if (reader.Read())
                        {
                            estimate_id = (int)reader["estimate_id"];
                        }

                        return estimate_id;
                    }
                }
            }
        }

        private void EditEstimate(Estimate estimate)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(UpdateEstimateQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", estimate.Id);
                    command.Parameters.AddWithValue("@origin", estimate.Origin);
                    command.Parameters.AddWithValue("@availability", estimate.Availability);
                    command.Parameters.AddWithValue("@worth", estimate.Worth);
                    command.Parameters.AddWithValue("@recommendation", estimate.Recommendation);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                    }
                }
            }
        }

        private void DeleteEstimate(int estimate_id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(DeleteEstimateQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", estimate_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                    }
                }
            }
        }

    }
}
