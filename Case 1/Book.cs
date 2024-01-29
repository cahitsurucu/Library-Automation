class Book
{
    string Title = "";
    string author = "";
    int Isbn;
    int copyCount;
    int borrowedBooks;

    public string Title1 { get => Title; set => Title = value; }
    public string Author { get => author; set => author = value; }
    public int Isbn1 { get => Isbn; set => Isbn = value; }
    public int CopyCount { get => copyCount; set => copyCount = value; }
    public int BorrowedBooks { get => borrowedBooks; set => borrowedBooks = value; }
}