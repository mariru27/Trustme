﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trustme.Data;

namespace Trustme.Migrations
{
    [DbContext(typeof(AppContext))]
    partial class AppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Trustme.Models.Key", b =>
                {
                    b.Property<int>("KeyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CertificateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KeySize")
                        .HasColumnType("int");

                    b.Property<string>("PublicKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SignedDocumentId")
                        .HasColumnType("int");

                    b.HasKey("KeyId");

                    b.HasIndex("SignedDocumentId");

                    b.ToTable("Key");
                });

            modelBuilder.Entity("Trustme.Models.Role", b =>
                {
                    b.Property<int>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRole");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Trustme.Models.SignedDocument", b =>
                {
                    b.Property<int>("IdSignedDocument")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Document")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Signature")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSignedDocument");

                    b.ToTable("SignedDocuments");
                });

            modelBuilder.Entity("Trustme.Models.UnsignedDocument", b =>
                {
                    b.Property<int>("IdUnsignedDocument")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Document")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("KeyPreference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUnsignedDocument");

                    b.ToTable("UnsignedDocuments");
                });

            modelBuilder.Entity("Trustme.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Trustme.Models.UserKey", b =>
                {
                    b.Property<int>("IdUserKey")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("IdUserKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserKey");
                });

            modelBuilder.Entity("Trustme.Models.UserSignedDocument", b =>
                {
                    b.Property<int>("IdUserSignedDocument")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SignedDocumentId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("IdUserSignedDocument");

                    b.HasIndex("SignedDocumentId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSignedDocuments");
                });

            modelBuilder.Entity("Trustme.Models.UserUnsignedDocument", b =>
                {
                    b.Property<int>("IdUserUnsignedDocument")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UnsignedDocumentId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("IdUserUnsignedDocument");

                    b.HasIndex("UnsignedDocumentId");

                    b.HasIndex("UserId");

                    b.ToTable("UserUnsignedDocuments");
                });

            modelBuilder.Entity("Trustme.Models.Key", b =>
                {
                    b.HasOne("Trustme.Models.SignedDocument", "Signed")
                        .WithMany()
                        .HasForeignKey("SignedDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trustme.Models.User", b =>
                {
                    b.HasOne("Trustme.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trustme.Models.UserKey", b =>
                {
                    b.HasOne("Trustme.Models.Key", "Key")
                        .WithOne("UserKey")
                        .HasForeignKey("Trustme.Models.UserKey", "IdUserKey")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Trustme.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trustme.Models.UserSignedDocument", b =>
                {
                    b.HasOne("Trustme.Models.SignedDocument", "SignedDocument")
                        .WithMany()
                        .HasForeignKey("SignedDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trustme.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trustme.Models.UserUnsignedDocument", b =>
                {
                    b.HasOne("Trustme.Models.UnsignedDocument", "UnsignedDocument")
                        .WithMany()
                        .HasForeignKey("UnsignedDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trustme.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
