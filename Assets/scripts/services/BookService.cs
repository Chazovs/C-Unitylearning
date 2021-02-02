using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookService
{
    public void SetMyBooksDropDown()
    {
        BooksRepository repository = new BooksRepository();
        List<Book> books = repository.GetAvilableBooks();
        Dropdown dropDown = MenuObjects.bookDropdown.GetComponent<Dropdown>();

        dropDown.options.Clear();

        foreach(Book book in books)
        {
            dropDown.options.Add(new Dropdown.OptionData() { text = book.name });
        }

        dropDown.captionText.text = books[0].name;

        dropDown.onValueChanged.RemoveAllListeners();
        dropDown.onValueChanged.AddListener(delegate { ChangeCurrentMyBook(dropDown); });
    }

    private void ChangeCurrentMyBook(Dropdown dropDown)
    {
        Menu.currentMyBook = dropDown.value;
        setCurrentMyBook();
    }

    internal List<Book> GetBooks(string bookType)
    {
        List<Book> bookList = new List<Book>();

        if(bookType == Constants.myBooksType)
        {
            BooksRepository repository = new BooksRepository();
            bookList = repository.GetAvilableBooks();
        }

        if (bookType == Constants.newBooksType)
        {
            BooksRepository repository = new BooksRepository();
            bookList = repository.GetNewBooks();
        }

        return bookList;
    }

    public void setCurrentMyBook()
    {
        int myBookIndex = Menu.currentMyBook;

        Book myBook = Menu.myBooks[myBookIndex];

        byte[] imageBytes = Convert.FromBase64String(myBook.qrCode);
        Texture2D tex = new Texture2D(370, 370);

        tex.LoadImage(imageBytes);
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.zero);
        MenuObjects.qrCodeImage.GetComponent<Image>().sprite = sprite;

        string url = String.Concat(Constants.serverUrl, myBook.pdfUri);

        MenuObjects.magicBookUrl.GetComponentInChildren<Text>().text = url;

        //magicBookUrl
        MenuObjects.magicBookUrl.GetComponent<Button>()
            .onClick.AddListener(() => ButtonService.openMagicBookUrl(url));

        //startGameBtn
        MenuObjects.startGameBtn.GetComponent<Button>()
            .onClick.AddListener(() => ButtonService.startGameBtnHandler());
    }
}
