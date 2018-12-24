
namespace test.Data
{
    using System.Collections.Generic;
    using System.Linq;

    public class BooksData
    {
        private readonly static List<Book> allBooks = new List<Book>();

        public void Create(string title, int year, string author)
        {
            var id = allBooks.Count + 1;

            allBooks.Add(new Book
            {
                Id = id,
                Title = title,
                Year = year,
                Author = author
            });
        }

        public List<Book> All()
        {
            return allBooks;/*new List<Book>(allBooks);*/
        }

        public Book GetById(int id)
        {
            return allBooks.FirstOrDefault(b => b.Id == id);
        }
    }
}
