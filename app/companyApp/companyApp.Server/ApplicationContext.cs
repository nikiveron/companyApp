using companyApplication.Server.DB_Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace companyApplication.Server
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CompanyEntity> Companies { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyEntity>().HasData(
                new CompanyEntity 
                { 
                    CompanyId = 1,
                    ShortName = "Компания 1",
                    FullName = "Полное название компании 1",
                    Inn = 1234567890,
                    Kpp = 123456789,
                    Ogrn = 123456789012345,
                    OgrnDateOfIssue = DateTime.Now,
                    RepLastName = "Иванов",
                    RepFirstName = "Иван",
                    RepPatronymic = "Иванович",
                    RepEmail = "ivanov@company1.ru",
                    RepPhone = "+7(999)123-45-67",
                    DeletedAt = DateTimeOffset.MinValue
                },
                new CompanyEntity 
                { 
                    CompanyId = 2,
                    ShortName = "Компания 2",
                    FullName = "Полное название компании 2",
                    Inn = 0987654321,
                    Kpp = 987654321,
                    Ogrn = 543210987654321,
                    OgrnDateOfIssue = DateTime.Now,
                    RepLastName = "Петров",
                    RepFirstName = "Петр",
                    RepPatronymic = "Петрович",
                    RepEmail = "petrov@company2.ru",
                    RepPhone = "+7(999)765-43-21",
                    DeletedAt = DateTimeOffset.MinValue
                },
                new CompanyEntity 
                { 
                    CompanyId = 3,
                    ShortName = "Компания 3",
                    FullName = "Полное название компании 3",
                    Inn = 1122334455,
                    Kpp = 112233445,
                    Ogrn = 112233445566778,
                    OgrnDateOfIssue = DateTime.Now,
                    RepLastName = "Сидоров",
                    RepFirstName = "Сидор",
                    RepPatronymic = "Сидорович",
                    RepEmail = "sidorov@company3.ru",
                    RepPhone = "+7(999)111-22-33",
                    DeletedAt = DateTimeOffset.MinValue
                }
            );
        }
    }
}
