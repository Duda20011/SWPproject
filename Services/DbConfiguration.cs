using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price)
                   .HasColumnType("money");

            builder.HasOne(s => s.User)
                .WithMany(s => s.Courses)
                .HasForeignKey(fk => fk.UserId);

        }
    }
    public class CourseEnrollConfig : IEntityTypeConfiguration<CourseEnroll>
    {
        public void Configure(EntityTypeBuilder<CourseEnroll> builder)
        {
            builder.Ignore(s => s.Id);

            builder.HasKey(s => new { s.UserId, s.CourseId });

            builder.HasOne(s => s.User)
                   .WithMany(s => s.CourseEnrolls)
                   .HasForeignKey(fk => fk.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Course)
                   .WithMany(s => s.CourseEnrolls)
                   .HasForeignKey(fk => fk.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
    public class FeedbackConfig : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(x => x.Id);

            //builder.HasOne(s => s.CourseEnroll)
            //    .WithMany(s => s.Feedbacks)
            //    .HasForeignKey(fk => fk.CourseEnrollId);

        }
    }
    
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TotalPrice)
                   .HasColumnType("money");
            builder.HasOne(s => s.User)
                   .WithMany(s => s.Orders)
                   .HasForeignKey(fk => fk.UserId)
                   .OnDelete(DeleteBehavior.Restrict); // Set the delete behavior to restrict
        }
    }
    public class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(s => s.Order)
                .WithMany(s => s.OrderDetails)
                .HasForeignKey(fk => fk.OrderId);

            builder.HasOne(s => s.Course)
                .WithMany(s => s.OrderDetails)
                .HasForeignKey(fk => fk.CourseId);

        }
    }
    
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
