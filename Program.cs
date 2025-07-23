using System.Data.Common;
using backend.DbContextes;
using backend.Entities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//BOOKS
app.MapGet("api/v1/books/list", () =>
{
    using var db = new LibraryDB();
    return db.Books.ToList();
});
app.MapPost("api/v1/books/create", (Book book) =>
{
    using var db = new LibraryDB();
    db.Books.Add(book);
    db.SaveChanges();
    return "book created!!!";
});
app.MapPut("api/v1/books/update/{id}", (int id, Book book) =>
{
    using var db = new LibraryDB();
    var b = db.Books.Find(id);
    if (b == null)
    {
        return "book dose not exist .";
    }
    b.Titel = book.Titel;
    b.Price = book.Price;
    b.Publisher = book.Publisher;
    b.Writer = book.Writer;
    db.SaveChanges();
    return "book updated";
}
);
app.MapDelete("api/v1/books/remove/{id}", (int id) =>
{
    using var db = new LibraryDB();
    var book = db.Books.Find(id);
    if (book == null)
    {
        return "book dose not exist .";
    }
    db.Books.Remove(book);
    db.SaveChanges();
    return "book removed";
}
);
//MEMBERS

app.MapGet("api/v1/member/list", () =>
{
    using var db = new LibraryDB();
    return db.Members.ToList();
});
app.MapPost("api/v1/member/create", (Member member) =>
{
    using var db = new LibraryDB();
    db.Members.Add(member);
    db.SaveChanges();
    return "book created!!!";
});
app.MapPut("api/v1/member/update/{id}", (int id, Member member) =>
{
    using var db = new LibraryDB();
    var m = db.Members.Find(id);
    if (m == null)
    {
        return "this member dose not exist .";
    }
    m.Firstname = member.Firstname;
    m.Lastname = member.Lastname;
    m.Gender = member.Gender;
    db.SaveChanges();
    return "book updated";
}
);
app.MapDelete("api/v1/member/remove/{id}", (int id) =>
{
    using var db = new LibraryDB();
    var member = db.Members.Find(id);
    if (member == null)
    {
        return " member dose not exist .";
    }
    db.Members.Remove(member);
    db.SaveChanges();
    return "member removed";
}
);





app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
