
using System.Collections.Generic;

public class BooksRepository : AbstactRepository
{

    public List<Book> getAvilableBooks()
    {
        List<Book> books = new List<Book>();

        books.Add(Constants.defaultBook);

        return books;
    }
    
}
