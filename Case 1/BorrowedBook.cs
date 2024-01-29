class BorrowedBook
{
    string title = "";
    string author="";
    int isbn;
    int borrowCode;
    DateTime borrowDate;
    DateTime expiredDate;

    public string Title { get => title; set => title = value; }
    public string Author { get => author; set => author = value; }
    public int Isbn { get => isbn; set => isbn = value; }
    public int BorrowCode { get => borrowCode; set => borrowCode = value; }
    public DateTime BorrowDate { get => borrowDate; set => borrowDate = value; }
    public DateTime ExpiredDate { get => expiredDate; set => expiredDate = value; }
}