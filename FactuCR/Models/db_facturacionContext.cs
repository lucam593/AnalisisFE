using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FactuCR.Models
{
    public partial class db_facturacionContext : DbContext
    {
        public db_facturacionContext()
        {
        }

        public db_facturacionContext(DbContextOptions<db_facturacionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryHasProduct> CategoryHasProduct { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientType> ClientType { get; set; }
        public virtual DbSet<Coin> Coin { get; set; }
        public virtual DbSet<CommerciallBrand> CommerciallBrand { get; set; }
        public virtual DbSet<Conversations> Conversations { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<IdentificationCard> IdentificationCard { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<MarterLogs> MarterLogs { get; set; }
        public virtual DbSet<MasterAddress> MasterAddress { get; set; }
        public virtual DbSet<MasterBranchOffice> MasterBranchOffice { get; set; }
        public virtual DbSet<MasterCertificate> MasterCertificate { get; set; }
        public virtual DbSet<MasterConfigCompanny> MasterConfigCompanny { get; set; }
        public virtual DbSet<MasterConsecutive> MasterConsecutive { get; set; }
        public virtual DbSet<MasterDetail> MasterDetail { get; set; }
        public virtual DbSet<MasterInvoice> MasterInvoice { get; set; }
        public virtual DbSet<MasterInvoiceReference> MasterInvoiceReference { get; set; }
        public virtual DbSet<MasterInvoiceVoucher> MasterInvoiceVoucher { get; set; }
        public virtual DbSet<MasterKey> MasterKey { get; set; }
        public virtual DbSet<MasterLogs> MasterLogs { get; set; }
        public virtual DbSet<MasterMonetaryDetails> MasterMonetaryDetails { get; set; }
        public virtual DbSet<MasterPayment> MasterPayment { get; set; }
        public virtual DbSet<MasterReceiver> MasterReceiver { get; set; }
        public virtual DbSet<MasterRol> MasterRol { get; set; }
        public virtual DbSet<MasterSaleCondition> MasterSaleCondition { get; set; }
        public virtual DbSet<MasterSessions> MasterSessions { get; set; }
        public virtual DbSet<MasterSqlupgrade> MasterSqlupgrade { get; set; }
        public virtual DbSet<MasterTerminal> MasterTerminal { get; set; }
        public virtual DbSet<MasterTransmitter> MasterTransmitter { get; set; }
        public virtual DbSet<MeasuredUnit> MeasuredUnit { get; set; }
        public virtual DbSet<Msgs> Msgs { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductFamily> ProductFamily { get; set; }
        public virtual DbSet<ProductHasProductFamily> ProductHasProductFamily { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<SalePrice> SalePrice { get; set; }
        public virtual DbSet<SituationDocument> SituationDocument { get; set; }
        public virtual DbSet<TaxExemption> TaxExemption { get; set; }
        public virtual DbSet<TelephoneContact> TelephoneContact { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VoucherType> VoucherType { get; set; }

        // Unable to generate entity type for table 'master_permission'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=localhost;Database=db_facturacion;user=root;password=");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.IdAddres);

                entity.ToTable("address");

                entity.HasIndex(e => e.IdAddress)
                    .HasName("fk_Address_master_Address1_idx");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_Address_users1_idx");

                entity.Property(e => e.IdAddres)
                    .HasColumnName("idAddres")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.IdAddress)
                    .HasColumnName("idAddress")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.OtherSigns)
                    .HasColumnName("Other_signs")
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.IdAddressNavigation)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.IdAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Address_master_Address1");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Address_users1");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory);

                entity.ToTable("category");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_CATEGORY_users1_idx");

                entity.Property(e => e.IdCategory)
                    .HasColumnName("Id_Category")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CATEGORY_users1");
            });

            modelBuilder.Entity<CategoryHasProduct>(entity =>
            {
                entity.HasKey(e => new { e.CategoryIdCategory, e.ProductIdProduct });

                entity.ToTable("category_has_product");

                entity.HasIndex(e => e.CategoryIdCategory)
                    .HasName("fk_CATEGORY_has_PRODUCT_CATEGORY1_idx");

                entity.HasIndex(e => e.ProductIdProduct)
                    .HasName("fk_CATEGORY_has_PRODUCT_PRODUCT1_idx");

                entity.Property(e => e.CategoryIdCategory)
                    .HasColumnName("CATEGORY_Id_Category")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductIdProduct)
                    .HasColumnName("PRODUCT_Id_Product")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CategoryIdCategoryNavigation)
                    .WithMany(p => p.CategoryHasProduct)
                    .HasForeignKey(d => d.CategoryIdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CATEGORY_has_PRODUCT_CATEGORY1");

                entity.HasOne(d => d.ProductIdProductNavigation)
                    .WithMany(p => p.CategoryHasProduct)
                    .HasForeignKey(d => d.ProductIdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CATEGORY_has_PRODUCT_PRODUCT1");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.ToTable("client");

                entity.HasIndex(e => e.Email)
                    .HasName("email")
                    .IsUnique();

                entity.HasIndex(e => e.IdClient)
                    .HasName("idUser");

                entity.HasIndex(e => e.IdClientType)
                    .HasName("fk_CLIENT_CLIENT_TYPE1_idx");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_CLIENT_users1_idx");

                entity.HasIndex(e => e.Status)
                    .HasName("status");

                entity.Property(e => e.IdClient).HasColumnName("idClient");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdClientType)
                    .HasColumnName("idClient_Type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.KindClient)
                    .IsRequired()
                    .HasColumnName("Kind_Client")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last_Name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");
            });

            modelBuilder.Entity<ClientType>(entity =>
            {
                entity.HasKey(e => e.IdClientType);

                entity.ToTable("client_type");

                entity.HasIndex(e => e.UsersIdUser)
                    .HasName("fk_CLIENT_TYPE_users1_idx");

                entity.Property(e => e.IdClientType)
                    .HasColumnName("idClient_Type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.UsersIdUser).HasColumnName("users_idUser");

                entity.HasOne(d => d.UsersIdUserNavigation)
                    .WithMany(p => p.ClientType)
                    .HasForeignKey(d => d.UsersIdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CLIENT_TYPE_users1");
            });

            modelBuilder.Entity<Coin>(entity =>
            {
                entity.HasKey(e => e.IdCoin);

                entity.ToTable("coin");

                entity.Property(e => e.IdCoin)
                    .HasColumnName("Id_Coin")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.CurrentChange).HasColumnName("Current_Change");

                entity.Property(e => e.LastModification)
                    .HasColumnName("Last_Modification")
                    .HasColumnType("date");

                entity.Property(e => e.Symbology)
                    .IsRequired()
                    .HasColumnType("varchar(5)");

                entity.Property(e => e.TypeCoin)
                    .IsRequired()
                    .HasColumnName("Type_Coin")
                    .HasColumnType("varchar(10)");
            });

            modelBuilder.Entity<CommerciallBrand>(entity =>
            {
                entity.HasKey(e => e.IdBrand);

                entity.ToTable("commerciall_brand");

                entity.HasIndex(e => e.IdProvider)
                    .HasName("fk_COMMERCIALl_BRAND_PROVIDER1_idx");

                entity.Property(e => e.IdBrand)
                    .HasColumnName("Id_Brand")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdProvider)
                    .IsRequired()
                    .HasColumnName("Id_Provider")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.IdProviderNavigation)
                    .WithMany(p => p.CommerciallBrand)
                    .HasForeignKey(d => d.IdProvider)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_COMMERCIALl_BRAND_PROVIDER1");
            });

            modelBuilder.Entity<Conversations>(entity =>
            {
                entity.HasKey(e => e.IdConversation);

                entity.ToTable("conversations");

                entity.Property(e => e.IdConversation).HasColumnName("idConversation");

                entity.Property(e => e.IdRecipient).HasColumnName("idRecipient");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasColumnName("subject")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.IdDiscount);

                entity.ToTable("discount");

                entity.HasIndex(e => e.IdProduct)
                    .HasName("fk_DISCOUNT_PRODUCT1_idx");

                entity.Property(e => e.IdDiscount)
                    .HasColumnName("Id_Discount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodeDiscount)
                    .HasColumnName("code_discount")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Discountcol)
                    .HasColumnName("DISCOUNTcol")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IdProduct)
                    .HasColumnName("Id_Product")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Percentage).HasColumnType("int(11)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Discount)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DISCOUNT_PRODUCT1");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.HasKey(e => e.IdFile);

                entity.ToTable("files");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_files_master_users1_idx");

                entity.Property(e => e.IdFile).HasColumnName("idFile");

                entity.Property(e => e.DownloadCode)
                    .IsRequired()
                    .HasColumnName("downloadCode")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FileType)
                    .IsRequired()
                    .HasColumnName("fileType")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Md5)
                    .IsRequired()
                    .HasColumnName("md5")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("varchar(15)");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_files_master_users1");
            });

            modelBuilder.Entity<IdentificationCard>(entity =>
            {
                entity.HasKey(e => e.IdCard);

                entity.ToTable("identification_card");

                entity.Property(e => e.IdCard)
                    .HasColumnName("id_Card")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.IdInventary);

                entity.ToTable("inventory");

                entity.HasIndex(e => e.IdProduct)
                    .HasName("fk_INVENTORY_PRODUCT1_idx");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_INVENTORY_users1_idx");

                entity.Property(e => e.IdInventary)
                    .HasColumnName("Id_Inventary")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cuantity).HasColumnType("int(11)");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.IdProduct)
                    .HasColumnName("Id_Product")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.MovementType)
                    .IsRequired()
                    .HasColumnName("Movement_Type")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_INVENTORY_PRODUCT1");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_INVENTORY_users1");
            });

            modelBuilder.Entity<MarterLogs>(entity =>
            {
                entity.HasKey(e => e.IdLog);

                entity.ToTable("marter_logs");

                entity.Property(e => e.IdLog)
                    .HasColumnName("idLog")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<MasterAddress>(entity =>
            {
                entity.HasKey(e => e.IdCodificacion);

                entity.ToTable("master_address");

                entity.Property(e => e.IdCodificacion)
                    .HasColumnName("idCodificacion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdBarrio)
                    .HasColumnName("idBarrio")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdCanton)
                    .HasColumnName("idCanton")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdDistrito)
                    .HasColumnName("idDistrito")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdProvincia)
                    .HasColumnName("idProvincia")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.NombreBarrio)
                    .HasColumnName("nombreBarrio")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NombreCanton)
                    .HasColumnName("nombreCanton")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NombreDistrito)
                    .HasColumnName("nombreDistrito")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NombreProvincia)
                    .HasColumnName("nombreProvincia")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<MasterBranchOffice>(entity =>
            {
                entity.HasKey(e => e.IdBranchOffice);

                entity.ToTable("master_branch_office");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_MASTER_BRANCH_OFFICE_users1_idx");

                entity.HasIndex(e => e.NumberBranchOffice)
                    .HasName("sucursal")
                    .IsUnique();

                entity.Property(e => e.IdBranchOffice)
                    .HasColumnName("id_branch_office")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.NameBranchOffice)
                    .IsRequired()
                    .HasColumnName("name_branch_office")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NumberBranchOffice)
                    .IsRequired()
                    .HasColumnName("Number_branch_office")
                    .HasColumnType("varchar(3)");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.MasterBranchOffice)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_BRANCH_OFFICE_users1");
            });

            modelBuilder.Entity<MasterCertificate>(entity =>
            {
                entity.HasKey(e => e.IdCertificate);

                entity.ToTable("master_certificate");

                entity.Property(e => e.IdCertificate)
                    .HasColumnName("idCertificate")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CompannyName)
                    .IsRequired()
                    .HasColumnName("compannyName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DownloadCode)
                    .IsRequired()
                    .HasColumnName("downloadCode")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Env)
                    .IsRequired()
                    .HasColumnName("ENV")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("idUser")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PinCertificate)
                    .HasColumnName("pinCertificate")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<MasterConfigCompanny>(entity =>
            {
                entity.HasKey(e => e.IdConfig);

                entity.ToTable("master_config_companny");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_master_config_companny_users1_idx");

                entity.Property(e => e.IdConfig)
                    .HasColumnName("idConfig")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Coin)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CompannyName)
                    .IsRequired()
                    .HasColumnName("compannyName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PasswordTax)
                    .IsRequired()
                    .HasColumnName("password_tax")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.UserTax)
                    .IsRequired()
                    .HasColumnName("user_tax")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.MasterConfigCompanny)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_master_config_companny_users1");
            });

            modelBuilder.Entity<MasterConsecutive>(entity =>
            {
                entity.HasKey(e => e.IdConsecutivo);

                entity.ToTable("master_consecutive");

                entity.Property(e => e.IdConsecutivo)
                    .HasColumnName("idConsecutivo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchOffice)
                    .HasColumnName("branch_office")
                    .HasColumnType("int(3)");

                entity.Property(e => e.NumberVoucher)
                    .HasColumnName("Number_voucher")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Terminal).HasColumnType("int(6)");

                entity.Property(e => e.VoucherType)
                    .HasColumnName("voucher_type")
                    .HasColumnType("int(2)");
            });

            modelBuilder.Entity<MasterDetail>(entity =>
            {
                entity.HasKey(e => e.IdDetail);

                entity.ToTable("master_detail");

                entity.HasIndex(e => e.Idvoucher)
                    .HasName("fk_DETAIL_MASTER_VOUCHER1_idx");

                entity.Property(e => e.IdDetail)
                    .HasColumnName("Id_Detail")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Idvoucher)
                    .HasColumnName("idvoucher")
                    .HasColumnType("int(11)");

                entity.Property(e => e.InitialAmount).HasColumnName("Initial_Amount");

                entity.Property(e => e.MeasuredUnit)
                    .IsRequired()
                    .HasColumnName("Measured_Unit")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Quantity).HasColumnType("int(11)");

                entity.Property(e => e.TotalAmount).HasColumnName("Total_Amount");

                entity.Property(e => e.TypeCode)
                    .IsRequired()
                    .HasColumnName("Type_Code")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.TypeDiscount)
                    .HasColumnName("Type_Discount")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.IdvoucherNavigation)
                    .WithMany(p => p.MasterDetail)
                    .HasForeignKey(d => d.Idvoucher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DETAIL_MASTER_VOUCHER1");
            });

            modelBuilder.Entity<MasterInvoice>(entity =>
            {
                entity.HasKey(e => e.IdInvoice);

                entity.ToTable("master_invoice");

                entity.HasIndex(e => e.IdKey)
                    .HasName("fk_MASTER_INVOICE_master_keys1_idx");

                entity.Property(e => e.IdInvoice)
                    .HasColumnName("Id_Invoice")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmissionDate)
                    .HasColumnName("Emission_Date")
                    .HasColumnType("date");

                entity.Property(e => e.IdKey)
                    .HasColumnName("idKey")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdKeyNavigation)
                    .WithMany(p => p.MasterInvoice)
                    .HasForeignKey(d => d.IdKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_INVOICE_master_keys1");
            });

            modelBuilder.Entity<MasterInvoiceReference>(entity =>
            {
                entity.HasKey(e => e.IdReference);

                entity.ToTable("master_invoice_reference");

                entity.HasIndex(e => e.IdInvoice)
                    .HasName("fk_INVOICE_REFERENCE_MASTER_INVOICE1_idx");

                entity.Property(e => e.IdReference)
                    .HasColumnName("Id_reference")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.Detail)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DocumentNumber)
                    .IsRequired()
                    .HasColumnName("Document_Number")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.IdInvoice)
                    .HasColumnName("Id_Invoice")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReferenceCode)
                    .IsRequired()
                    .HasColumnName("Reference_Code")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.RespuestaMhbase64)
                    .HasColumnName("respuestaMHBase64")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.XmlenviadoBase64)
                    .HasColumnName("xmlenviadoBase64")
                    .HasColumnType("mediumtext");

                entity.HasOne(d => d.IdInvoiceNavigation)
                    .WithMany(p => p.MasterInvoiceReference)
                    .HasForeignKey(d => d.IdInvoice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_INVOICE_REFERENCE_MASTER_INVOICE1");
            });

            modelBuilder.Entity<MasterInvoiceVoucher>(entity =>
            {
                entity.HasKey(e => e.Idvoucher);

                entity.ToTable("master_invoice_voucher");

                entity.HasIndex(e => e.IdCondition)
                    .HasName("fk_MASTER_BILL_MASTER_SALE_CONDITION1_idx");

                entity.HasIndex(e => e.IdInvoice)
                    .HasName("fk_MASTER_BILL_MASTER_INVOICE1_idx");

                entity.HasIndex(e => e.IdPayment)
                    .HasName("fk_master_vouchers_MASTER_PAYMENT1_idx");

                entity.HasIndex(e => e.IdReceiver)
                    .HasName("fk_master_vouchers_master_receiver1_idx");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_master_vouchers_master_transmitter1_idx");

                entity.Property(e => e.Idvoucher)
                    .HasColumnName("idvoucher")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Env)
                    .IsRequired()
                    .HasColumnName("env")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IdCondition)
                    .HasColumnName("Id_Condition")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdInvoice)
                    .HasColumnName("Id_Invoice")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdPayment)
                    .HasColumnName("id_Payment")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdReceiver)
                    .HasColumnName("idReceiver")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("idUser")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RespuestaMhbase64)
                    .HasColumnName("respuestaMHBase64")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.XmlEnviadoBase64)
                    .IsRequired()
                    .HasColumnName("xmlEnviadoBase64")
                    .HasColumnType("mediumtext");
            });

            modelBuilder.Entity<MasterKey>(entity =>
            {
                entity.HasKey(e => e.IdClave);

                entity.ToTable("master_key");

                entity.HasIndex(e => e.IdConsecutivo)
                    .HasName("fk_master_keys_MASTER_CONSECUTIVE1_idx");

                entity.Property(e => e.IdClave)
                    .HasColumnName("idClave")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodeKey)
                    .HasColumnName("code_key")
                    .HasColumnType("varchar(8)");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasColumnType("int(3)");

                entity.Property(e => e.Day)
                    .HasColumnName("day")
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.IdConsecutivo)
                    .HasColumnName("idConsecutivo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idtransmitter)
                    .HasColumnName("idtransmitter")
                    .HasColumnType("varchar(12)");

                entity.Property(e => e.Month)
                    .HasColumnName("month")
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.SituationDocument)
                    .HasColumnName("situation_document")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Year).HasColumnType("varchar(2)");
            });

            modelBuilder.Entity<MasterLogs>(entity =>
            {
                entity.HasKey(e => e.IdLog);

                entity.ToTable("master_logs");

                entity.Property(e => e.IdLog)
                    .HasColumnName("idLog")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("idUser")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Log)
                    .IsRequired()
                    .HasColumnName("log")
                    .HasColumnType("text");

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("timeStamp")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");
            });

            modelBuilder.Entity<MasterMonetaryDetails>(entity =>
            {
                entity.HasKey(e => e.IdMonetary);

                entity.ToTable("master_monetary_details");

                entity.HasIndex(e => e.Idvoucher)
                    .HasName("fk_MASTER_MONETARY_DETAILS_MASTER_VOUCHER1_idx");

                entity.Property(e => e.IdMonetary)
                    .HasColumnName("id_monetary")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.CodeCoin)
                    .IsRequired()
                    .HasColumnName("code_Coin")
                    .HasColumnType("varchar(5)");

                entity.Property(e => e.Idvoucher)
                    .HasColumnName("idvoucher")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalDiscount).HasColumnName("Total_Discount");

                entity.Property(e => e.TotalExempt).HasColumnName("Total_exempt");

                entity.Property(e => e.TotalExemptFreight).HasColumnName("Total_exempt_freight");

                entity.Property(e => e.TotalExemptServices).HasColumnName("Total_Exempt_Services");

                entity.Property(e => e.TotalNetSale).HasColumnName("Total_Net_Sale");

                entity.Property(e => e.TotalSale).HasColumnName("Total_sale");

                entity.Property(e => e.TotalTax).HasColumnName("Total_Tax");

                entity.Property(e => e.TotalTaxed).HasColumnName("Total_Taxed");

                entity.Property(e => e.TotalTaxedGoods).HasColumnName("Total_Taxed_Goods");

                entity.Property(e => e.TotalTaxedServices).HasColumnName("Total_Taxed_Services");

                entity.Property(e => e.ValueCoin).HasColumnName("Value_Coin");

                entity.HasOne(d => d.IdvoucherNavigation)
                    .WithMany(p => p.MasterMonetaryDetails)
                    .HasForeignKey(d => d.Idvoucher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_MONETARY_DETAILS_MASTER_VOUCHER1");
            });

            modelBuilder.Entity<MasterPayment>(entity =>
            {
                entity.HasKey(e => e.IdPayment);

                entity.ToTable("master_payment");

                entity.Property(e => e.IdPayment)
                    .HasColumnName("id_Payment")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<MasterReceiver>(entity =>
            {
                entity.HasKey(e => e.Idreceiver);

                entity.ToTable("master_receiver");

                entity.Property(e => e.Idreceiver)
                    .HasColumnName("idreceiver")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ComercialName)
                    .IsRequired()
                    .HasColumnName("Comercial_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasColumnName("Country_code")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("id")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IdCanton)
                    .IsRequired()
                    .HasColumnName("idCanton")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdDistrict)
                    .IsRequired()
                    .HasColumnName("idDistrict")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdProvince)
                    .IsRequired()
                    .HasColumnName("idProvince")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("idUser")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idneighborhood)
                    .IsRequired()
                    .HasColumnName("idneighborhood")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.KindId)
                    .IsRequired()
                    .HasColumnName("kind_id")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(80)");

                entity.Property(e => e.OthersSigns)
                    .IsRequired()
                    .HasColumnName("others_signs")
                    .HasColumnType("varchar(160)");

                entity.Property(e => e.PrincipalEmail)
                    .IsRequired()
                    .HasColumnName("Principal_email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasColumnName("telephone")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.TelephoneFax)
                    .IsRequired()
                    .HasColumnName("telephone_Fax")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<MasterRol>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.ToTable("master_rol");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_master_rol_users1_idx");

                entity.Property(e => e.IdRol)
                    .HasColumnName("idRol")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Kind)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<MasterSaleCondition>(entity =>
            {
                entity.HasKey(e => e.IdCondition);

                entity.ToTable("master_sale_condition");

                entity.Property(e => e.IdCondition)
                    .HasColumnName("Id_Condition")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<MasterSessions>(entity =>
            {
                entity.HasKey(e => e.IdSession);

                entity.ToTable("master_sessions");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_master_sessions_master_users1_idx");

                entity.HasIndex(e => e.SessionKey)
                    .HasName("sessionKey");

                entity.Property(e => e.IdSession).HasColumnName("idSession");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("ip")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.LastAccess).HasColumnName("lastAccess");

                entity.Property(e => e.SessionKey)
                    .IsRequired()
                    .HasColumnName("sessionKey")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.MasterSessions)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_master_sessions_master_users1");
            });

            modelBuilder.Entity<MasterSqlupgrade>(entity =>
            {
                entity.HasKey(e => e.IdSql);

                entity.ToTable("master_sqlupgrade");

                entity.Property(e => e.IdSql)
                    .HasColumnName("idSQL")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Sql)
                    .IsRequired()
                    .HasColumnName("SQL")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.VersionApi).HasColumnName("versionAPI");
            });

            modelBuilder.Entity<MasterTerminal>(entity =>
            {
                entity.HasKey(e => e.IdTerminal);

                entity.ToTable("master_terminal");

                entity.HasIndex(e => e.IdBranchOffice)
                    .HasName("fk_master_terminales_MASTER_BRANCH_OFFICE1_idx");

                entity.HasIndex(e => e.NumberTerminal)
                    .HasName("terminal")
                    .IsUnique();

                entity.Property(e => e.IdTerminal)
                    .HasColumnName("idTerminal")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdBranchOffice)
                    .HasColumnName("id_branch_office")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NameTerminal)
                    .IsRequired()
                    .HasColumnName("name_Terminal")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NumberTerminal)
                    .IsRequired()
                    .HasColumnName("number_terminal")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.IdBranchOfficeNavigation)
                    .WithMany(p => p.MasterTerminal)
                    .HasForeignKey(d => d.IdBranchOffice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_master_terminales_MASTER_BRANCH_OFFICE1");
            });

            modelBuilder.Entity<MasterTransmitter>(entity =>
            {
                entity.HasKey(e => e.IdTransmitter);

                entity.ToTable("master_transmitter");

                entity.Property(e => e.IdTransmitter)
                    .HasColumnName("idTransmitter")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BussinessName)
                    .IsRequired()
                    .HasColumnName("Bussiness_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Canton).HasColumnType("int(11)");

                entity.Property(e => e.District).HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Id).HasColumnType("int(15)");

                entity.Property(e => e.KindId)
                    .HasColumnName("Kind_Id")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Neighborhood).HasColumnType("varchar(20)");

                entity.Property(e => e.OtherSigns)
                    .IsRequired()
                    .HasColumnName("Other_signs")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Province).HasColumnType("int(11)");

                entity.Property(e => e.Telephone).HasColumnType("int(15)");

                entity.Property(e => e.TelephonoFax)
                    .HasColumnName("Telephono_Fax")
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<MeasuredUnit>(entity =>
            {
                entity.HasKey(e => e.IdUnit);

                entity.ToTable("measured_unit");

                entity.Property(e => e.IdUnit)
                    .HasColumnName("Id_Unit")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Simboly)
                    .IsRequired()
                    .HasColumnType("varchar(10)");
            });

            modelBuilder.Entity<Msgs>(entity =>
            {
                entity.HasKey(e => e.IdMsg);

                entity.ToTable("msgs");

                entity.HasIndex(e => e.IdConversation)
                    .HasName("fk_msgs_conversations1_idx");

                entity.Property(e => e.IdMsg).HasColumnName("idMsg");

                entity.Property(e => e.Attachments).HasColumnName("attachments");

                entity.Property(e => e.Channel)
                    .HasColumnName("channel")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdConversation).HasColumnName("idConversation");

                entity.Property(e => e.IdRecipient).HasColumnName("idRecipient");

                entity.Property(e => e.IdSender).HasColumnName("idSender");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("ip")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasColumnType("text");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.HasOne(d => d.IdConversationNavigation)
                    .WithMany(p => p.Msgs)
                    .HasForeignKey(d => d.IdConversation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_msgs_conversations1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.ToTable("product");

                entity.HasIndex(e => e.IdBrand)
                    .HasName("fk_PRODUCT_Commercial_Brand1_idx");

                entity.HasIndex(e => e.IdTax)
                    .HasName("fk_PRODUCT_TAX/EXEMPTION1_idx");

                entity.HasIndex(e => e.IdUnit)
                    .HasName("fk_PRODUCT_MEASURED_UNIT1_idx");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_PRODUCT_users1_idx");

                entity.Property(e => e.IdProduct)
                    .HasColumnName("Id_Product")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Barcode)
                    .HasColumnName("barcode")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CostPrice).HasColumnName("Cost_Price");

                entity.Property(e => e.Description).HasColumnType("varchar(100)");

                entity.Property(e => e.IdBrand)
                    .HasColumnName("Id_Brand")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTax)
                    .HasColumnName("id_Tax")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUnit)
                    .HasColumnName("Id_Unit")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.KindCode)
                    .IsRequired()
                    .HasColumnName("kind_code")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(15)");

                entity.HasOne(d => d.IdBrandNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdBrand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_Commercial_Brand1");

                entity.HasOne(d => d.IdTaxNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdTax)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_TAX/EXEMPTION1");

                entity.HasOne(d => d.IdUnitNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdUnit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_MEASURED_UNIT1");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_users1");
            });

            modelBuilder.Entity<ProductFamily>(entity =>
            {
                entity.HasKey(e => e.IdFamily);

                entity.ToTable("product_family");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_PRODUCT_FAMILY_users1_idx");

                entity.Property(e => e.IdFamily)
                    .HasColumnName("Id_Family")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FamilyType)
                    .IsRequired()
                    .HasColumnName("family_Type")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.ProductFamily)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_FAMILY_users1");
            });

            modelBuilder.Entity<ProductHasProductFamily>(entity =>
            {
                entity.HasKey(e => new { e.ProductIdProduct, e.ProductFamilyIdFamily });

                entity.ToTable("product_has_product_family");

                entity.HasIndex(e => e.ProductFamilyIdFamily)
                    .HasName("fk_PRODUCT_has_PRODUCT_FAMILY_PRODUCT_FAMILY1_idx");

                entity.HasIndex(e => e.ProductIdProduct)
                    .HasName("fk_PRODUCT_has_PRODUCT_FAMILY_PRODUCT1_idx");

                entity.Property(e => e.ProductIdProduct)
                    .HasColumnName("PRODUCT_Id_Product")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductFamilyIdFamily)
                    .HasColumnName("PRODUCT_FAMILY_Id_Family")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ProductFamilyIdFamilyNavigation)
                    .WithMany(p => p.ProductHasProductFamily)
                    .HasForeignKey(d => d.ProductFamilyIdFamily)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_has_PRODUCT_FAMILY_PRODUCT_FAMILY1");

                entity.HasOne(d => d.ProductIdProductNavigation)
                    .WithMany(p => p.ProductHasProductFamily)
                    .HasForeignKey(d => d.ProductIdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_has_PRODUCT_FAMILY_PRODUCT1");
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.HasKey(e => e.IdProvider);

                entity.ToTable("provider");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_PROVIDER_users1_idx");

                entity.Property(e => e.IdProvider)
                    .HasColumnName("Id_Provider")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.KindId)
                    .IsRequired()
                    .HasColumnName("Kind_Id")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.NameProvider)
                    .IsRequired()
                    .HasColumnName("nameProvider")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Provider)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PROVIDER_users1");
            });

            modelBuilder.Entity<SalePrice>(entity =>
            {
                entity.HasKey(e => e.IdPrice);

                entity.ToTable("sale_price");

                entity.HasIndex(e => e.IdCoin)
                    .HasName("fk_SALE_PRICE_COIN1_idx");

                entity.HasIndex(e => e.IdProduct)
                    .HasName("fk_SALE_PRICE_PRODUCT1_idx");

                entity.Property(e => e.IdPrice)
                    .HasColumnName("Id_Price")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdCoin)
                    .HasColumnName("Id_Coin")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdProduct)
                    .HasColumnName("Id_Product")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdCoinNavigation)
                    .WithMany(p => p.SalePrice)
                    .HasForeignKey(d => d.IdCoin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_SALE_PRICE_COIN1");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.SalePrice)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_SALE_PRICE_PRODUCT1");
            });

            modelBuilder.Entity<SituationDocument>(entity =>
            {
                entity.HasKey(e => e.IdSituation);

                entity.ToTable("situation_document");

                entity.Property(e => e.IdSituation).HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<TaxExemption>(entity =>
            {
                entity.HasKey(e => e.IdTax);

                entity.ToTable("tax/exemption");

                entity.Property(e => e.IdTax).HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Percentage).HasColumnType("int(11)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<TelephoneContact>(entity =>
            {
                entity.HasKey(e => e.IdContact);

                entity.ToTable("telephone_contact");

                entity.Property(e => e.IdContact)
                    .HasColumnName("Id_Contact")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.CountryCode)
                    .HasColumnName("Country_Code")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(100)");

                entity.Property(e => e.Extension).HasColumnType("int(11)");

                entity.Property(e => e.IdOwner)
                    .IsRequired()
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("users");

                entity.HasIndex(e => e.IdUser)
                    .HasName("idUser");

                entity.HasIndex(e => e.Status)
                    .HasName("status");

                entity.HasIndex(e => e.UserName)
                    .HasName("wire");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.About)
                    .IsRequired()
                    .HasColumnName("about")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasColumnName("avatar")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("fullName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.LastAccess)
                    .HasColumnName("lastAccess")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasColumnName("pwd")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Settings)
                    .IsRequired()
                    .HasColumnName("settings")
                    .HasColumnType("text");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<VoucherType>(entity =>
            {
                entity.HasKey(e => e.IdVoucherType);

                entity.ToTable("voucher_type");

                entity.Property(e => e.IdVoucherType)
                    .HasColumnName("idVoucher_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });
        }
    }
}
