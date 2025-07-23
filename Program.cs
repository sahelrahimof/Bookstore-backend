using System.Data.Common;
using backend.DbContextes;
using backend.Entities;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddDbContext<LibraryDB>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//BOOKS
app.MapGet("api/v1/books/list", ([FromServices] LibraryDB db) =>
{
    return db.Books.ToList();
});
app.MapPost("api/v1/books/create",
([FromServices] LibraryDB db,
[FromBody] Book book) =>
{
    db.Books.Add(book);
    db.SaveChanges();
    return new { message = "book created!!!"};
});
app.MapPut("api/v1/books/update/{id}",
([FromServices] LibraryDB db,
[FromRoute] int id,
[FromBody] Book book) =>
{
    var b = db.Books.Find(id);
    if (b == null)
    {
        return new { message = "book dose not exist ." };
    }
    b.Titel = book.Titel;
    b.Price = book.Price;
    b.Publisher = book.Publisher;
    b.Writer = book.Writer;
    db.SaveChanges();
    return new { message = "book updated" };
}
);

app.MapDelete("api/v1/books/remove/{id}",
([FromServices] LibraryDB db,
[FromRoute] int id) =>
{
    var book = db.Books.Find(id);
    if (book == null)
    {
        return new { message = "book dose not exist ." };
    }
    db.Books.Remove(book);
    db.SaveChanges();
    return new { message = "book removed" };
}
);
//MEMBERS

app.MapGet("api/v1/member/list", ([FromServices] LibraryDB db) =>
{
    return db.Members.ToList();
});
app.MapPost("api/v1/member/create", ([FromServices] LibraryDB db,
[FromBody] Member member) =>
{
    db.Members.Add(member);
    db.SaveChanges();
    return new { message = "book created!!!" };
});
app.MapPut("api/v1/member/update/{id}",
([FromServices] LibraryDB db,
[FromRoute] int id,
[FromBody] Member member) =>
{
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
app.MapDelete("api/v1/member/remove/{id}",
([FromServices] LibraryDB db,
[FromRoute] int id) =>
{
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
