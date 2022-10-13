using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CaducaRest.sakila
{
    public partial class sakilaContext : DbContext
    {
        public sakilaContext()
        {
        }

        public sakilaContext(DbContextOptions<sakilaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerList> CustomerLists { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<FilmActor> FilmActors { get; set; }
        public virtual DbSet<FilmCategory> FilmCategories { get; set; }
        public virtual DbSet<FilmList> FilmLists { get; set; }
        public virtual DbSet<FilmText> FilmTexts { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }
        public virtual DbSet<SalesByFilmCategory> SalesByFilmCategories { get; set; }
        public virtual DbSet<SalesByStore> SalesByStores { get; set; }
        public virtual DbSet<StaffList> StaffLists { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-BQ4N9A9;Database=sakila;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.ActorId)
                    .HasName("PK__actor__8B2447B544C0D7A2")
                    .IsClustered(false);

                entity.ToTable("actor");

                entity.HasIndex(e => e.LastName, "idx_actor_last_name");

                entity.Property(e => e.ActorId).HasColumnName("actor_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK__address__CAA247C98545627E")
                    .IsClustered(false);

                entity.ToTable("address");

                entity.HasIndex(e => e.CityId, "idx_fk_city_id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address2");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("district");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("postal_code");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_address_city");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__category__D54EE9B563095ECB")
                    .IsClustered(false);

                entity.ToTable("category");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("category_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK__city__031491A9BB9E9BA7")
                    .IsClustered(false);

                entity.ToTable("city");

                entity.HasIndex(e => e.CountryId, "idx_fk_country_id");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.City1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_city_country");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__country__7E8CD054990142DB")
                    .IsClustered(false);

                entity.ToTable("country");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.Country1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__customer__CD65CB84640DB73C")
                    .IsClustered(false);

                entity.ToTable("customer");

                entity.HasIndex(e => e.AddressId, "idx_fk_address_id");

                entity.HasIndex(e => e.StoreId, "idx_fk_store_id");

                entity.HasIndex(e => e.LastName, "idx_last_name");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("active")
                    .HasDefaultValueSql("('Y')")
                    .IsFixedLength(true);

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customer_address");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customer_store");
            });

            modelBuilder.Entity<CustomerList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("customer_list");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(91)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("notes");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Sid).HasColumnName("SID");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("zip_code");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.FilmId)
                    .HasName("PK__film__349764A81815CD28")
                    .IsClustered(false);

                entity.ToTable("film");

                entity.HasIndex(e => e.LanguageId, "idx_fk_language_id");

                entity.HasIndex(e => e.OriginalLanguageId, "idx_fk_original_language_id");

                entity.Property(e => e.FilmId).HasColumnName("film_id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.LanguageId).HasColumnName("language_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.OriginalLanguageId).HasColumnName("original_language_id");

                entity.Property(e => e.Rating)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("rating")
                    .HasDefaultValueSql("('G')");

                entity.Property(e => e.ReleaseYear)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("release_year");

                entity.Property(e => e.RentalDuration)
                    .HasColumnName("rental_duration")
                    .HasDefaultValueSql("((3))");

                entity.Property(e => e.RentalRate)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("rental_rate")
                    .HasDefaultValueSql("((4.99))");

                entity.Property(e => e.ReplacementCost)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("replacement_cost")
                    .HasDefaultValueSql("((19.99))");

                entity.Property(e => e.SpecialFeatures)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("special_features");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.FilmLanguages)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_language");

                entity.HasOne(d => d.OriginalLanguage)
                    .WithMany(p => p.FilmOriginalLanguages)
                    .HasForeignKey(d => d.OriginalLanguageId)
                    .HasConstraintName("fk_film_language_original");
            });

            modelBuilder.Entity<FilmActor>(entity =>
            {
                entity.HasKey(e => new { e.ActorId, e.FilmId })
                    .HasName("PK__film_act__086D31FFC4D04025")
                    .IsClustered(false);

                entity.ToTable("film_actor");

                entity.HasIndex(e => e.ActorId, "idx_fk_film_actor_actor");

                entity.HasIndex(e => e.FilmId, "idx_fk_film_actor_film");

                entity.Property(e => e.ActorId).HasColumnName("actor_id");

                entity.Property(e => e.FilmId).HasColumnName("film_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.FilmActors)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_actor_actor");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmActors)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_actor_film");
            });

            modelBuilder.Entity<FilmCategory>(entity =>
            {
                entity.HasKey(e => new { e.FilmId, e.CategoryId })
                    .HasName("PK__film_cat__69C38A33ECD2A806")
                    .IsClustered(false);

                entity.ToTable("film_category");

                entity.HasIndex(e => e.CategoryId, "idx_fk_film_category_category");

                entity.HasIndex(e => e.FilmId, "idx_fk_film_category_film");

                entity.Property(e => e.FilmId).HasColumnName("film_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FilmCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_category_category");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmCategories)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_category_film");
            });

            modelBuilder.Entity<FilmList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("film_list");

                entity.Property(e => e.Actors)
                    .IsRequired()
                    .HasMaxLength(91)
                    .IsUnicode(false)
                    .HasColumnName("actors");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Fid).HasColumnName("FID");

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Rating)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("rating");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<FilmText>(entity =>
            {
                entity.HasKey(e => e.FilmId)
                    .HasName("PK__film_tex__349764A87AF66AF4")
                    .IsClustered(false);

                entity.ToTable("film_text");

                entity.Property(e => e.FilmId)
                    .ValueGeneratedNever()
                    .HasColumnName("film_id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.InventoryId)
                    .HasName("PK__inventor__B59ACC4830B7D904")
                    .IsClustered(false);

                entity.ToTable("inventory");

                entity.HasIndex(e => e.FilmId, "idx_fk_film_id");

                entity.HasIndex(e => new { e.StoreId, e.FilmId }, "idx_fk_film_id_store_id");

                entity.Property(e => e.InventoryId).HasColumnName("inventory_id");

                entity.Property(e => e.FilmId).HasColumnName("film_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inventory_film");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inventory_store");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.LanguageId)
                    .HasName("PK__language__804CF6B2557080CD")
                    .IsClustered(false);

                entity.ToTable("language");

                entity.Property(e => e.LanguageId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("language_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentId)
                    .HasName("PK__payment__ED1FC9EB4484F99D")
                    .IsClustered(false);

                entity.ToTable("payment");

                entity.HasIndex(e => e.CustomerId, "idx_fk_customer_id");

                entity.HasIndex(e => e.StaffId, "idx_fk_staff_id");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("payment_date");

                entity.Property(e => e.RentalId).HasColumnName("rental_id");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payment_customer");

                entity.HasOne(d => d.Rental)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.RentalId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_payment_rental");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payment_staff");
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(e => e.RentalId)
                    .HasName("PK__rental__67DB611A397B6BE1")
                    .IsClustered(false);

                entity.ToTable("rental");

                entity.HasIndex(e => e.CustomerId, "idx_fk_customer_id");

                entity.HasIndex(e => e.InventoryId, "idx_fk_inventory_id");

                entity.HasIndex(e => e.StaffId, "idx_fk_staff_id");

                entity.HasIndex(e => new { e.RentalDate, e.InventoryId, e.CustomerId }, "idx_uq")
                    .IsUnique();

                entity.Property(e => e.RentalId).HasColumnName("rental_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.InventoryId).HasColumnName("inventory_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RentalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("rental_date");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("datetime")
                    .HasColumnName("return_date");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rental_customer");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rental_inventory");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rental_staff");
            });

            modelBuilder.Entity<SalesByFilmCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("sales_by_film_category");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.TotalSales)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("total_sales");
            });

            modelBuilder.Entity<SalesByStore>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("sales_by_store");

                entity.Property(e => e.Manager)
                    .IsRequired()
                    .HasMaxLength(91)
                    .IsUnicode(false)
                    .HasColumnName("manager");

                entity.Property(e => e.Store)
                    .IsRequired()
                    .HasMaxLength(101)
                    .IsUnicode(false)
                    .HasColumnName("store");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.TotalSales)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("total_sales");
            });

            modelBuilder.Entity<StaffList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("staff_list");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(91)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Sid).HasColumnName("SID");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("zip_code");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("PK__store__A2F2A30DDF1903D1")
                    .IsClustered(false);

                entity.ToTable("store");

                entity.HasIndex(e => e.ManagerStaffId, "idx_fk_address_id")
                    .IsUnique();

                entity.HasIndex(e => e.AddressId, "idx_fk_store_address");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ManagerStaffId).HasColumnName("manager_staff_id");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_store_address");

                entity.HasOne(d => d.ManagerStaff)
                    .WithOne(p => p.Store)
                    .HasForeignKey<Store>(d => d.ManagerStaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_store_staff");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.HasKey(e => e.StaffId)
                    .HasName("PK__staff__1963DD9D04E86B1E")
                    .IsClustered(false);

                entity.HasIndex(e => e.AddressId, "idx_fk_address_id");

                entity.HasIndex(e => e.StoreId, "idx_fk_store_id");

                entity.Property(e => e.StaffId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("staff_id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Picture)
                    .HasColumnType("image")
                    .HasColumnName("picture");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_staff_address");

                entity.HasOne(d => d.StoreNavigation)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_staff_store");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
