using System;
using Beijer.Thesaurus.Domain;
using Microsoft.EntityFrameworkCore;

namespace Beijer.Thesaurus.DataContext {

    public class ApplicationDbContext : DbContext {

        public DbSet<Word> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            optionsBuilder.UseSqlite("Filename=./App_Data/Database.db", options => {
                options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                optionsBuilder.LogTo(Console.WriteLine);
            });

            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Word>().ToTable("Words", "dbo");
            modelBuilder.Entity<Word>().HasKey(x => x.Id);
            modelBuilder.Entity<Word>().Property(s => s.Content).IsRequired();
            modelBuilder.Entity<Word>().Property(s => s.Language).IsRequired();
            modelBuilder.Entity<Word>().HasIndex(e => e.Content);

            modelBuilder.Entity<Synonym>().ToTable("Synonyms", "dbo");
            modelBuilder.Entity<Synonym>().HasKey(x => new { x.WordId, x.SynonymWordId });
            modelBuilder.Entity<Synonym>()
                 .HasOne(pt => pt.SynonymWord)
                 .WithMany(p => p.SynonymOf)
                 .HasForeignKey(pt => pt.SynonymWordId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Synonym>()
                .HasOne(pt => pt.Word)
                .WithMany(t => t.Synonyms)
                .HasForeignKey(pt => pt.WordId);

            base.OnModelCreating(modelBuilder);
        }

    }

}