using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookService
{
    public void SetMyBooks(ref GameObject dropDownObject)
    {
        BooksRepository repository = new BooksRepository();
        List<Book> books = repository.GetAvilableBooks();
        Dropdown dropDown = dropDownObject.GetComponent<Dropdown>();

        dropDown.options.Clear();

        foreach(Book book in books)
        {
            dropDown.options.Add(new Dropdown.OptionData() { text = book.name });
        }

        dropDown.captionText.text = books[0].name;
    }
}
