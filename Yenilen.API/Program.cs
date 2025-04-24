using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Yenilen.API.Middlewares;
using Yenilen.API.Shared;
using Yenilen.Application.Common.Mapping;
using Yenilen.Infrastructure;
using Yenilen.Application;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(UserMappingProfile)); // added AutoMapper service
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

//Mediatr service
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationRegistrar).Assembly));
builder.Services.AddApplication();
builder.Services.AddRateLimiter(x =>
    x.AddFixedWindowLimiter("fixed", cfg =>
    {
        cfg.QueueLimit = 100;
        cfg.Window = TimeSpan.FromSeconds(1);
        cfg.PermitLimit = 100;
        cfg.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    }));

//builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseGlobalExceptionHandling();

app.MapControllers().RequireRateLimiting("fixed");

//Seed data uretmek icin
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     //SeedData(context);
// }

app.Run();


//Seed data uretmek icin
// static void SeedData(AppDbContext context)
// {
//     // Eğer veri zaten eklenmişse tekrar seedleme yapmayalım.
//     if (context.Users.Any())
//     {
//         return;
//     }
//
//     // ******************
//     // 1. User ve İlişkili Veriler
//     // ******************
//
//     // İki adet Address (User için)
//     var userAddress1 = new Address
//     {
//         Label = "Home",
//         FullAddress = "123 Home St, Istanbul, Turkiye",
//         latitude = 41.0,
//         longitude = 29.0,
//         CountryCode = "TR",
//         Country = "Turkiye",
//         City = "Istanbul",
//         District = "Kadikoy",
//         Region = "Marmara",
//         PostCode = "34000"
//     };
//
//     var userAddress2 = new Address
//     {
//         Label = "Work",
//         FullAddress = "456 Work Ave, Istanbul, Turkiye",
//         latitude = 41.1,
//         longitude = 29.1,
//         CountryCode = "TR",
//         Country = "Turkiye",
//         City = "Istanbul",
//         District = "Besiktas",
//         Region = "Marmara",
//         PostCode = "34300"
//     };
//
//     // Kullanıcı için Image
//     var userImage = new Image
//     {
//         ImageUrl = "https://example.com/user-image.jpg"
//     };
//
//     
//
//     // ******************
//     // 2. Store ve İlişkili Veriler
//     // ******************
//
//     // Store için tek bir Address (Store.Address)
//     var storeAddress = new Address
//     {
//         Label = "Store Location",
//         FullAddress = "789 Store Rd, Istanbul, Turkiye",
//         latitude = 41.5,
//         longitude = 29.5,
//         CountryCode = "TR",
//         Country = "Turkiye",
//         City = "Istanbul",
//         District = "Sisli",
//         Region = "Marmara",
//         PostCode = "34350"
//     };
//
//     // İki adet Category
//     var category1 = new Category { Name = "Hair" };
//     var category2 = new Category { Name = "Beauty" };
//
//     // İki adet Service (örn. her ikisini de category1'e bağlıyoruz)
//     var service1 = new Service
//     {
//         Name = "Haircut",
//         Price = 30.00m,
//         Duration = 30,
//         Category = category1,
//         CategoryId = category1.Id // (veya uygun şekilde atayın)
//     };
//     var service2 = new Service
//     {
//         Name = "Hair Coloring",
//         Price = 50.00m,
//         Duration = 45,
//         Category = category1,
//         CategoryId = category1.Id
//     };
//
//     // İki adet Staff üyesi
//     var staff1 = new Staff
//     {
//         Name = "Alice",
//         Image = new Image { ImageUrl = "https://example.com/staff1.jpg" }
//     };
//     var staff2 = new Staff
//     {
//         Name = "Bob",
//         Image = new Image { ImageUrl = "https://example.com/staff2.jpg" }
//     };
//
//     // İki adet Opening Time (Date entity’si)
//     // var openingTime1 = new Date
//     // {
//     //     DateTime = DateTime.UtcNow.Date,
//     //     Time = new TimeSpan(9, 0, 0),
//     //     DayOfWeek = DayOfWeek.Monday
//     // };
//     // var openingTime2 = new Date
//     // {
//     //     DateTime = DateTime.UtcNow.Date,
//     //     Time = new TimeSpan(10, 0, 0),
//     //     DayOfWeek = DayOfWeek.Tuesday
//     // };
//
//     // İki adet Store Image
//     var storeImage1 = new Image
//     {
//         ImageUrl = "https://example.com/store1.jpg"
//     };
//     var storeImage2 = new Image
//     {
//         ImageUrl = "https://example.com/store2.jpg"
//     };
//
//     // İki adet Tag
//     var tag1 = new Tag { Name = "Popular" };
//     var tag2 = new Tag { Name = "New" };
//
//     var store = new Store
//     {
//         Name = "Super Store",
//         MobileNumber = "+902345678901",
//         PhoneNumber = "02345678901",
//         About = "A sample store for demonstration purposes.",
//         Address = storeAddress,
//         Categories = new List<Category> { category1, category2 },
//         Services = new List<Service> { service1, service2 },
//         StaffMembers = new List<Staff> { staff1, staff2 },
//         OpeningTimes = new List<Date> { openingTime1, openingTime2 },
//         Images = new List<Image> { storeImage1, storeImage2 },
//         Tags = new List<Tag> { tag1, tag2 }
//     };
//
//     context.Stores.Add(store);
//
//     // ******************
//     // 3. Appointment
//     // ******************
//
//     var appointmentDate = new Date
//     {
//         DateTime = DateTime.UtcNow.AddDays(1),
//         Time = new TimeSpan(14, 0, 0),
//         DayOfWeek = DateTime.UtcNow.AddDays(1).DayOfWeek
//     };
//
//    
//
//     // ******************
//     // 4. Favourite
//     // ******************
//
//     var favouriteDate = new Date
//     {
//         DateTime = DateTime.UtcNow.AddDays(-1),
//         Time = new TimeSpan(12, 0, 0),
//         DayOfWeek = DateTime.UtcNow.AddDays(-1).DayOfWeek
//     };
//
//     var favourite = new Favourite
//     {
//         UserId = 1,
//         Store = store,
//         Date = favouriteDate,
//         ImageUrl = "images.url"
//     };
//
//     context.Favourites.Add(favourite);
//     
//     var user = new User
//     {
//         Id = 1,
//         FirstName = "John",
//         LastName = "Doe",
//         PhoneNumber = "+905555555555",
//         Email = "john.doe@example.com",
//         DateOfBirth = new DateTime(1990, 1, 1),
//         Gender = "Male",
//         Addresses = new List<Address> { userAddress1, userAddress2 },
//         Favourites = new List<Favourite>{favourite},
//         AvatarUrl = userImage
//     };
//
//     context.Users.Add(user);
//     
//     var appointment = new Appointment
//     {
//         User = user,
//         Store = store,
//         Category = category1,
//         Service = service1,
//         Date = appointmentDate,
//         Duration = service1.Duration,
//         Note = "First appointment."
//     };
//
//     context.Appointments.Add(appointment);
//
//     // ******************
//     // 5. Review
//     // ******************
//
//     var reviewDate = new Date
//     {
//         DateTime = DateTime.UtcNow.AddDays(-2),
//         Time = new TimeSpan(16, 0, 0),
//         DayOfWeek = DateTime.UtcNow.AddDays(-2).DayOfWeek
//     };
//
//     var review = new Review
//     {
//         Author = user,
//         Text = "Excellent service!",
//         Rating = 4.5f,
//         Type = reviewDate, // Review entity'sindeki Date property
//         Store = store,
//         Staff = staff1,
//         IsVisible = true,
//         Reply = "Thank you for your feedback!"
//     };
//
//     context.Reviews.Add(review);
//
//     // Tüm eklemeler tamamlandıktan sonra değişiklikleri kaydedelim.
//     context.SaveChanges();
// }