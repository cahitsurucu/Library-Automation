using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata;


class Menu
{
    public static List<Book> listOfBooks = new List<Book>();
    public static List<BorrowedBook> borrowedBooks = new List<BorrowedBook>();

    static void Main(string[] args)
    {
        int choose = 0;
        readBookListData();
        readBorrowedBookListData();
        while (choose != 8)
        {
            Console.WriteLine("[1] - Kitap Ekle\n[2] - Kitaplari Listele\n[3] - Kitap Ara\n[4] - Odunc Kitap Al\n[5] - Odunc Alinan Kitaplari Listele\n[6] - Kitabi Iade Et\n[7] - Suresi Gecmis Kitap Goruntule\n[8] - Cikis");
            Console.WriteLine("Seciminiz : ");
            string? s = Console.ReadLine();
            bool result = int.TryParse(s, out choose);
            if (result)
            {
                switch (choose)
                {
                    case 1:
                        clearConsole();
                        addBook();
                        break;
                    case 2:
                        clearConsole();
                        listBooks();
                        break;
                    case 3:
                        clearConsole();
                        searchBook();
                        break;
                    case 4:
                        clearConsole();
                        getBook();
                        break;
                    case 5:
                        clearConsole();
                        listBorrowedBooks();
                        break;
                    case 6:
                        clearConsole();
                        returnBook();
                        break;
                    case 7:
                        clearConsole();
                        listOverdueBooks();
                        break;
                    case 8:
                        writeBookListData();
                        writeBorrowedBookListData();
                        break;
                    default:
                        Console.WriteLine("Yanlis Secim Yaptiniz !");
                        clearConsole();
                        break;
                }
            }
            else
            {
                clearConsole();
            }
        }
    }

    static void readBookListData()
    {
        string? line;
        try
        {
            StreamReader sr = new StreamReader("C:\\Velo Games\\Case 1\\Case 1\\BookList.txt");
            line = sr.ReadLine();

            while (line != null)
            {
                string[] array = line.Split("-");
                Book book = new Book();
                book.Title1 = array[0];
                book.Author = array[1];
                book.Isbn1 = Convert.ToInt32(array[2]);
                book.CopyCount = Convert.ToInt32(array[3]);
                book.BorrowedBooks = Convert.ToInt32(array[4]);
                listOfBooks.Add(book);

                line = sr.ReadLine();
            }

            sr.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
        clearConsole();
    }

    static void readBorrowedBookListData()
    {
        string? line;
        try
        {
            StreamReader sr = new StreamReader("C:\\Velo Games\\Case 1\\Case 1\\BorrowedBookList.txt");
            line = sr.ReadLine();

            while (line != null)
            {
                string[] array = line.Split("-");
                BorrowedBook book = new BorrowedBook();
                book.Title = array[0];
                book.Author = array[1];
                book.Isbn = Convert.ToInt32(array[2]);
                book.BorrowCode = Convert.ToInt32(array[3]);
                book.BorrowDate = DateTime.Parse(array[4]);
                book.ExpiredDate = DateTime.Parse(array[5]);
                borrowedBooks.Add(book);

                line = sr.ReadLine();
            }

            sr.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
        clearConsole();
    }

    static void writeBorrowedBookListData()
    {
        try
        {

            StreamWriter sw = new StreamWriter("C:\\Velo Games\\Case 1\\Case 1\\BorrowedBookList.txt");

            foreach (BorrowedBook book in borrowedBooks)
            {
                sw.WriteLine(book.Title + "-" + book.Author + "-" + book.Isbn + "-" + book.BorrowCode + "-" + book.BorrowDate + "-" + book.ExpiredDate);
            }

            sw.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
    }

    static void writeBookListData()
    {
        try
        {

            StreamWriter sw = new StreamWriter("C:\\Velo Games\\Case 1\\Case 1\\BookList.txt");

            foreach (Book book in listOfBooks)
            {
                sw.WriteLine(book.Title1 + "-" + book.Author + "-" + book.Isbn1 + "-" + book.CopyCount + "-" + book.BorrowedBooks);
            }

            sw.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
    }

    static void addBook()
    {
        Book book = new Book();
        string? title;
        string? author;
        int Isbn = 0, copyCount = 0;
        bool isValid = true;
        Console.WriteLine("---- Kitap Ekle ----");
        do
        {
            Console.WriteLine("Kitap Basligini Giriniz : ");
            title = Console.ReadLine();
        }
        while (!(title?.Length > 3));

        do
        {
            Console.WriteLine("Kitap Yazarini Giriniz : ");
            author = Console.ReadLine();
        }
        while (!(author?.Length > 3));

        while (isValid)
        {
            do
            {
                Console.WriteLine("Kitabin ISBN Numarasini Giriniz : ");
                string? isbn = Console.ReadLine();
                bool result = int.TryParse(isbn, out Isbn);
                if (result)
                {
                    isValid = false;
                }
            }
            while (isValid);


            if (listOfBooks.Count != 0)
            {
                foreach (Book books in listOfBooks)
                {
                    if (books.Isbn1 == Isbn)
                    {
                        isValid = true;
                        Console.WriteLine("Girdiginiz ISBN Numarasi Mevcut !");
                        break;
                    }
                }
            }
            else
            {
                break;
            }
        }

        isValid = true;

        do
        {
            Console.WriteLine("Kitabin Kopya Sayisini Giriniz : ");
            string? copy = Console.ReadLine();
            bool result = int.TryParse(copy, out copyCount);
            if (result)
            {
                isValid = false;
            }

        }
        while (isValid);

        book.Title1 = title;
        book.Author = author;
        book.Isbn1 = Isbn;
        book.CopyCount = copyCount;
        book.BorrowedBooks = 0;
        listOfBooks.Add(book);
        clearConsole();
    }

    static void listBooks()
    {
        int index = 0;
        Console.WriteLine("---- Kutuphane Kitap Listesi ----");
        if (listOfBooks.Count > 0)
        {
            foreach (Book book in listOfBooks)
            {
                index++;
                Console.WriteLine(index + ")\nName : " + book.Title1 + "\nAuthor : " + book.Author + "\nISBN : " + book.Isbn1 + "\nCopies : " + book.CopyCount + "\nBorrowed Books : " + book.BorrowedBooks);
            }
        }
        else
        {
            Console.WriteLine("!!! Kutuphanede Kitap Bulunamadi. Lutfen Kitap Ekleyin !!!");
        }

        waitConsole();
    }

    static void searchBook()
    {
        int choose = 0;

        while (choose != 4)
        {
            Console.WriteLine("[1] - Kitap Adina Gore Ara\n[2] - Yazar Adina Gore Ara\n[3] - ISBN Numarasina Gore Ara\n[4] - Cikis\nSeciminiz : ");
            string? s = Console.ReadLine();
            bool result = int.TryParse(s, out choose);
            if (result)
            {
                switch (choose)
                {
                    case 1:
                        clearConsole();
                        searchBookName();
                        break;
                    case 2:
                        clearConsole();
                        searchAuthorName();
                        break;
                    case 3:
                        clearConsole();
                        searchIsbnNumber();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Yanlis Secim !");
                        break;
                }
            }
            else
            {
                clearConsole();
            }
        }
        clearConsole();
    }

    static void searchBookName()
    {
        bool isValid = true;
        int index = 0;
        do
        {
            Console.WriteLine("Aramak Istediginiz Kitabin Ismini Giriniz :");
            string? name = Console.ReadLine();

            if (name?.Length >= 3)
            {
                foreach (Book book in listOfBooks)
                {
                    if (book.Title1.Contains(name))
                    {
                        index++;
                        Console.WriteLine(index + ")" + "\nName : " + book.Title1 + "\nAuthor : " + book.Author + "\nISBN : " + book.Isbn1 + "\nCopies : " + book.CopyCount + "\nBorrowed Books : " + book.BorrowedBooks);
                    }
                }
                isValid = false;
            }
        }
        while (isValid);

        if (index == 0)
        {
            Console.WriteLine("Aradiginiz Kriterde Kitap Bulunamadi !");
        }
        waitConsole();
    }

    static void searchAuthorName()
    {
        bool isValid = true;
        int index = 0;
        do
        {
            Console.WriteLine("Aramak Istediginiz Yazarin Ismini Giriniz :");
            string? name = Console.ReadLine();

            if (name?.Length >= 3)
            {
                foreach (Book book in listOfBooks)
                {
                    if (book.Author.Contains(name))
                    {
                        index++;
                        Console.WriteLine(index + ")" + "\nName : " + book.Title1 + "\nAuthor : " + book.Author + "\nISBN : " + book.Isbn1 + "\nCopies : " + book.CopyCount + "\nBorrowed Books : " + book.BorrowedBooks);
                    }
                }
                isValid = false;
            }
        }
        while (isValid);

        if (index == 0)
        {
            Console.WriteLine("Aradiginiz Kriterde Kitap Bulunamadi !");
        }
        waitConsole();
    }

    static void searchIsbnNumber()
    {
        bool isValid = true;
        int Isbn;
        do
        {
            Console.WriteLine("Aramak Istediginiz Kitabin ISBN Numarasini Giriniz : ");
            string? isbn = Console.ReadLine();
            bool result = int.TryParse(isbn, out Isbn);
            if (result)
            {
                isValid = false;
            }
        }
        while (isValid);

        foreach (Book book in listOfBooks)
        {
            if (book.Isbn1 == Isbn)
            {
                isValid = true;
                Console.WriteLine("\nName : " + book.Title1 + "\nAuthor : " + book.Author + "\nISBN : " + book.Isbn1 + "\nCopies : " + book.CopyCount + "\nBorrowed Books : " + book.BorrowedBooks);
                break;
            }
        }
        if (!isValid)
        {
            Console.WriteLine("Aradiginiz Kriterde Kitap Bulunamadi !");
        }
        waitConsole();
    }

    static void getBook()
    {
        listBooks();
        bool control = true;
        while (control)
        {
            Console.WriteLine("Odunc Almak Istediginiz Kitabin ISBN Numarasini Giriniz : ");
            string? Isbn = Console.ReadLine();
            int isbn;
            bool result = int.TryParse(Isbn, out isbn);
            if (result)
            {
                foreach (Book book in listOfBooks)
                {
                    if (book.Isbn1 == isbn)
                    {
                        control = false;
                        if (book.CopyCount == 0)
                        {
                            Console.WriteLine("Yeterli Sayida Kopya Bulunmuyor !");
                            waitConsole();
                            break;
                        }
                        BorrowedBook borrowedBook = new BorrowedBook();
                        DateTime borrowDate = DateTime.Now;
                        borrowedBook.Title = book.Title1;
                        borrowedBook.Author = book.Author;
                        borrowedBook.Isbn = book.Isbn1;
                        borrowedBook.BorrowDate = borrowDate;
                        borrowedBook.ExpiredDate = new DateTime(borrowDate.Year, borrowDate.Month + 1, borrowDate.Day, borrowDate.Hour, borrowDate.Minute, borrowDate.Second);
                        borrowedBook.BorrowCode = getValidCode();
                        borrowedBooks.Add(borrowedBook);
                        book.CopyCount -= 1;
                        book.BorrowedBooks += 1;
                        break;
                    }
                }

                if (control)
                {
                    int choose = 0;
                    while (choose != 2)
                    {
                        Console.WriteLine("Girdiniz ISBN Numarasina Ait Kitap Bulunamadi !\n[1] - Devam\n[2] - Cikis");
                        string? c = Console.ReadLine();
                        result = int.TryParse(c, out choose);
                        if (result)
                        {
                            switch (choose)
                            {
                                case 1:
                                    control = true;
                                    choose = 2;
                                    break;
                                case 2:
                                    control = false;
                                    break;
                                default:
                                    Console.WriteLine("Yanlis Secim");
                                    break;
                            }
                        }
                    }
                }
            }
        }
        clearConsole();
    }

    static void listBorrowedBooks()
    {
        int index = 0;
        if (borrowedBooks.Count > 0)
        {
            Console.WriteLine("---- Odunc Alinan Kitap Listesi ----");
            foreach (BorrowedBook book in borrowedBooks)
            {
                index++;
                Console.WriteLine(index + ")\nName : " + book.Title + "\nAuthor : " + book.Author + "\nISBN : " + book.Isbn + "\nBorrow Code : " + book.BorrowCode + "\nBorrow Date : " + book.BorrowDate + "\nReturn Date : " + book.ExpiredDate);
            }
        }
        else
        {
            Console.WriteLine("!!! Odunc Alinan Kitap Bulunamadi !!!");
        }
        waitConsole();
    }

    static void returnBook()
    {
        listBorrowedBooks();
        bool control = true, returned = false;
        while (control && borrowedBooks.Count > 0)
        {
            Console.WriteLine("Iade Etmek Istediginiz Kitabin Odunc Numarasini (Borrow Code) Giriniz : ");
            string? borrow = Console.ReadLine();
            int borrowCode;
            bool result = int.TryParse(borrow, out borrowCode);
            if (result)
            {
                foreach (BorrowedBook book in borrowedBooks)
                {
                    if (book.BorrowCode == borrowCode)
                    {
                        Book? book1 = listOfBooks.Find(x => x.Isbn1 == book.Isbn);
                        if (book1 != null)
                        {
                            book1.CopyCount += 1;
                            book1.BorrowedBooks -= 1;
                        }
                        control = false;
                        returned = true;
                        borrowedBooks.Remove(book);
                        break;
                    }
                }

                if (control)
                {
                    int choose = 0;
                    while (choose != 2)
                    {
                        Console.WriteLine("Girdiniz Odunc Numarasina Ait Kitap Bulunamadi !\n[1] - Devam\n[2] - Cikis");
                        string? c = Console.ReadLine();
                        result = int.TryParse(c, out choose);
                        if (result)
                        {
                            switch (choose)
                            {
                                case 1:
                                    control = true;
                                    choose = 2;
                                    break;
                                case 2:
                                    control = false;
                                    break;
                                default:
                                    Console.WriteLine("Yanlis Secim");
                                    break;
                            }
                        }
                    }
                }
            }
        }

        if (returned)
        {
            Console.WriteLine("!!! Kitap Iade Edilmistir !!!");
            waitConsole();
        }
    }

    static void listOverdueBooks()
    {
        int index = 0;
        foreach (BorrowedBook book in borrowedBooks)
        {
            if (book.ExpiredDate < DateTime.Now)
            {
                index++;
                Console.WriteLine(index + ")\nName : " + book.Title + "\nAuthor : " + book.Author + "\nISBN : " + book.Isbn + "\nBorrow Code : " + book.BorrowCode + "\nBorrow Date : " + book.BorrowDate + "\nReturn Date : " + book.ExpiredDate);
            }
        }

        if (index == 0)
        {
            Console.WriteLine("!!! Suresi Gecmis Kitap Bulunamadi !!!");
        }
        waitConsole();
    }

    static int getValidCode()
    {
        int code;
        bool isValid = true;
        Random random = new Random();
        do
        {
            code = random.Next(100000, 1000000);
            foreach (BorrowedBook book in borrowedBooks)
            {
                if (code == book.BorrowCode)
                {
                    isValid = false;
                    break;
                }
                else
                {
                    isValid = true;
                }
            }

        }
        while (!isValid);
        return code;
    }

    static void clearConsole()
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");
    }

    static void waitConsole()
    {
        Console.ReadKey();
        clearConsole();
    }

}