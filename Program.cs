using System.Data.Common;
using backend.DbContextes;
using backend.DTOs;
using backend.DTOs.Books;
using backend.DTOs.Commen;
using backend.DTOs.Members;
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
    return db.Books.Select(b => new BookListDto
    {
        Id = b.Guid,
        Titel = b.Titel,
        Price = b.Price,
        Publisher = b.Publisher,
        Writer = b.Writer

    }).ToList();
});


app.MapPost("api/v1/books/create",
([FromServices] LibraryDB db,
[FromBody] BookAddDto bookAddDto) =>
{
    var book = new Book
    {
        Titel = !string.IsNullOrEmpty(bookAddDto.Titel.Trim()) ? bookAddDto.Titel : "no title",
        Writer = bookAddDto.Writer,
        Publisher = bookAddDto.Publisher,
        Price = bookAddDto.Price
    };
    db.Books.Add(book);
    db.SaveChanges();
    return new CommandResultDto
    {
        Succesfull = true,
        Message = "book created!!!"
    };
});


app.MapPut("api/v1/books/update/{guid}",
([FromServices] LibraryDB db,
[FromRoute] string guid,
[FromBody] BookUpdateDto bookUpdateDto) =>
{
    var b = db.Books.FirstOrDefault(m => m.Guid == guid);
    if (b == null)
    {
        return new CommandResultDto
        {
            Succesfull = false,
            Message = "Not Found!"
        };
    }
    b.Titel = !string.IsNullOrEmpty(bookUpdateDto.Titel) ? bookUpdateDto.Titel : "no title";
    b.Price = bookUpdateDto.Price;
    b.Publisher = bookUpdateDto.Publisher;
    b.Writer = bookUpdateDto.Writer;
    db.SaveChanges();
    return new CommandResultDto
    {
        Succesfull = true,
        Message = "book updated"
    };
}
);

app.MapDelete("api/v1/books/remove/{guid}",
([FromServices] LibraryDB db,
[FromRoute] string guid) =>
{
    var book = db.Books.FirstOrDefault(m => m.Guid == guid);
    if (book == null)
    {
        return new CommandResultDto
        {
            Succesfull = false,
            Message = "Not Found!"
        };
    }
    db.Books.Remove(book);
    db.SaveChanges();
    return new CommandResultDto
    {
        Succesfull = true,
        Message = "book updated"
    };
}
);


//MEMBERS

app.MapGet("api/v1/member/list", ([FromServices] LibraryDB db) =>
{
    return db.Members.ToList();
});


app.MapPost("api/v1/member/create", ([FromServices] LibraryDB db,
[FromBody] MemberAddDto memberAddDto) =>
{
    var member = new Member
    {
        Firstname = !string.IsNullOrEmpty(memberAddDto.Firstname) ? memberAddDto.Firstname : "no name",
        Lastname = memberAddDto.Lastname,
        Gender = memberAddDto.Gender
    };
    db.Members.Add(member);
    db.SaveChanges();
    return new CommandResultDto
    {
        Succesfull = true,
        Message = "member created!!!"
    };
});



app.MapPut("api/v1/member/update/{guid}",
([FromServices] LibraryDB db,
[FromRoute] string guid,
[FromBody] MemberUpdateDto memberUpateDo) =>
{
    var m = db.Members.FirstOrDefault(s => s.Guid == guid);
    if (m == null)
    {
        return new CommandResultDto
        {
            Succesfull = false,
            Message = "Not Found!"
        };
    }
    m.Firstname = memberUpateDo.Firstname;
    m.Lastname = memberUpateDo.Lastname;
    m.Gender = memberUpateDo.Gender;
    db.SaveChanges();
    return new CommandResultDto
    {
        Succesfull = true,
        Message = "member updated"
    };
}
);


app.MapDelete("api/v1/member/remove/{guid}",
([FromServices] LibraryDB db,
[FromRoute] string guid) =>
{
    var member = db.Members.FirstOrDefault(s => s.Guid == guid);
    if (member == null)
    {
        return new CommandResultDto
        {
            Succesfull = false,
            Message = "Not Found!"
        };
    }
    db.Members.Remove(member);
    db.SaveChanges();
    return new CommandResultDto
    {
        Succesfull = true,
        Message = "member updated"
    };
}
);





app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
