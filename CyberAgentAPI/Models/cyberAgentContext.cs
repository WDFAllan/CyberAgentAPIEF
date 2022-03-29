using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CyberAgentAPI.Models
{
    public partial class CyberAgentContext : DbContext
    {
        public CyberAgentContext()
        {
        }

        public CyberAgentContext(DbContextOptions<CyberAgentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionCategory> QuestionCategories { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual DbSet<SurveyQuestionCategory> SurveyQuestionCategories { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=cyberAgent");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Answers>(entity =>
            {
                entity.HasKey("AnswerId");
                entity.ToTable("answers");

                entity.Property(e => e.AnswerId).HasColumnName("answerId");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("answer");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.SurveyQuestionId).HasColumnName("surveyQuestionId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                //entity.HasOne(d => d.SurveyQuestion)
                //    .WithMany(p => p.Answers)
                //    .HasForeignKey(d => d.SurveyQuestionId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK__answers__surveyQ__6442E2C9");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__answers__userId__65370702");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("question");

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.AuditType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("auditType");

                entity.Property(e => e.Big).HasColumnName("big");

                entity.Property(e => e.Domain)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("domain");

                entity.Property(e => e.KeyWord)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("keyWord");

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("language");

                entity.Property(e => e.Medium).HasColumnName("medium");

                entity.Property(e => e.QuestionCategoryId).HasColumnName("questionCategoryId");

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("questionText");

                entity.Property(e => e.Small).HasColumnName("small");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");

                //entity.HasOne(d => d.QuestionCategory)
                //    .WithMany(p => p.Questions)
                //    .HasForeignKey(d => d.QuestionCategoryId)
                //    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<QuestionCategory>(entity =>
            {
                entity.ToTable("questionCategory");

                entity.Property(e => e.QuestionCategoryId).HasColumnName("questionCategoryId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__questionC__paren__503BEA1C");
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("survey");

                entity.Property(e => e.SurveyId).HasColumnName("surveyId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SurveyQuestion>(entity =>
            {
                entity.ToTable("survey_question");

                entity.Property(e => e.SurveyQuestionId).HasColumnName("surveyQuestionId");

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.SurveyId).HasColumnName("surveyId");

                entity.Property(e => e.SurveyQuestionCategoryId).HasColumnName("surveyQuestionCategoryId");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.SurveyQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__survey_qu__quest__5F7E2DAC");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyQuestions)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__survey_qu__surve__607251E5");

                entity.HasOne(d => d.SurveyQuestionCategory)
                    .WithMany(p => p.SurveyQuestions)
                    .HasForeignKey(d => d.SurveyQuestionCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__survey_qu__surve__6166761E");
            });

            modelBuilder.Entity<SurveyQuestionCategory>(entity =>
            {
                entity.ToTable("surveyQuestionCategory");

                entity.Property(e => e.SurveyQuestionCategoryId).HasColumnName("surveyQuestionCategoryId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__surveyQue__paren__531856C7");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tags");

                entity.Property(e => e.TagId).HasColumnName("tagId");

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tagName");

                entity.Property(e => e.TagValue)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tagValue");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tags__questionId__5AB9788F");
            });

            modelBuilder.Entity<User>(entity =>
            {

                entity.HasKey("UserId");
                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("password")
                    .IsFixedLength(true);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("passwordSalt")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
