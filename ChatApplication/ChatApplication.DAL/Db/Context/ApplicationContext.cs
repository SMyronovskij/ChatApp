using ChatApplication.Contracts.Service;
using ChatApplication.DAL.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.DAL.Db.Context;

public class ApplicationContext : DbContext
{
    private const string DbName = "chatApp.db";

    private readonly EncryptionService _encryptionService = new();

    public ApplicationContext()
    {
        Database.EnsureCreated();
        AddRootUser().Wait();
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ConversationEntity> Conversations { get; set; }
    public DbSet<ConversationMessageEntity> ConversationMessages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($@"Data Source={DbName}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().ToTable("Users");
        modelBuilder.Entity<ConversationEntity>().ToTable("Conversations");
        modelBuilder.Entity<ConversationMessageEntity>().ToTable("ConversationMessages");
    }

    private async Task AddRootUser()
    {
        if (Users.Any())
            return;

        var user = new UserEntity
        {
            Login = "admin",
            FirstName = "Admin",
            LastName = "Root",
            Password = _encryptionService.GetHashedString("1234")
        };

        Users.Add(user);
        await SaveChangesAsync();
    }
}