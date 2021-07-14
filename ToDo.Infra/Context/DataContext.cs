using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDo.Domain.Auth;
using ToDo.Domain.Entities;

namespace ToDo.Infra.Context
{
    public class DataContext  : DbContext
    {
        public IConfiguration Configuration { get; }
    
        public DataContext()
        {
            
        }
        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<TodoItem> Todos { get; set; }
        public virtual DbSet<User> Users { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {   
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings"));
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().ToTable("Todo");
            modelBuilder.Entity<TodoItem>().HasKey(c => new { c.Id });
            modelBuilder.Entity<TodoItem>().Property(x => x.Id);
            modelBuilder.Entity<TodoItem>().Property(x => x.User).HasMaxLength(120).HasColumnType("varchar(120)");
            modelBuilder.Entity<TodoItem>().Property(x => x.Title).HasMaxLength(160).HasColumnType("varchar(160)");
            modelBuilder.Entity<TodoItem>().Property(x => x.Done).HasColumnType("bit");
            modelBuilder.Entity<TodoItem>().Property(x => x.Date);
            modelBuilder.Entity<TodoItem>().HasIndex(b => b.User);
            
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().HasKey(c => new { c.Id });
            modelBuilder.Entity<User>().Property(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Username).HasMaxLength(120).HasColumnType("varchar(120)");
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(10).HasColumnType("varchar(10)");

            base.OnModelCreating(modelBuilder);
        }
    }
}