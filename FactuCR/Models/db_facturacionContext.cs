using System;
using System.Configuration;
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
        public virtual DbSet<BranchOffice> BranchOffice { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<ConfigCompany> ConfigCompany { get; set; }
        public virtual DbSet<Conversations> Conversations { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Family> Family { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<IdentificationType> IdentificationType { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<MasterAddress> MasterAddress { get; set; }
        public virtual DbSet<MasterCertificate> MasterCertificate { get; set; }
        public virtual DbSet<MasterConsecutive> MasterConsecutive { get; set; }
        public virtual DbSet<MasterDetail> MasterDetail { get; set; }
        public virtual DbSet<MasterInvoiceReference> MasterInvoiceReference { get; set; }
        public virtual DbSet<MasterInvoiceVoucher> MasterInvoiceVoucher { get; set; }
        public virtual DbSet<MasterKey> MasterKey { get; set; }
        public virtual DbSet<MasterLogs> MasterLogs { get; set; }
        public virtual DbSet<MasterMonetaryDetails> MasterMonetaryDetails { get; set; }
        public virtual DbSet<MasterPayment> MasterPayment { get; set; }
        public virtual DbSet<MasterReceiver> MasterReceiver { get; set; }
        public virtual DbSet<MasterSaleCondition> MasterSaleCondition { get; set; }
        public virtual DbSet<MasterSqlupgrade> MasterSqlupgrade { get; set; }
        public virtual DbSet<MasterTransmitter> MasterTransmitter { get; set; }
        public virtual DbSet<MeasuredUnit> MeasuredUnit { get; set; }
        public virtual DbSet<Messages> Mesages { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductHasFamily> ProductHasFamily { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolHasUsers> RolHasUsers { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }
        public virtual DbSet<SituationDocument> SituationDocument { get; set; }
        public virtual DbSet<Tax> Tax { get; set; }
        public virtual DbSet<TelephoneContact> TelephoneContact { get; set; }
        public virtual DbSet<Terminal> Terminal { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VoucherType> VoucherType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.

              
                optionsBuilder.UseMySql(ConfigurationManager.ConnectionStrings["smarterasp_db_dev"].ConnectionString);
                

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => new { e.IdCodificacion, e.IdUser });

                entity.ToTable("address");

                entity.HasIndex(e => e.IdCodificacion)
                    .HasName("fk_ADDRESS_master_Address1_idx");

                entity.Property(e => e.IdCodificacion)
                    .HasColumnName("idCodificacion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.OtherSigns)
                    .HasColumnName("Other_signs")
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.IdCodificacionNavigation)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.IdCodificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ADDRESS_master_Address1");
            });

            modelBuilder.Entity<BranchOffice>(entity =>
            {
                entity.HasKey(e => e.IdOffice);

                entity.ToTable("branch_office");

                entity.Property(e => e.IdOffice)
                    .HasColumnName("Id_Office")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.OfficeNumber)
                    .HasColumnName("Office_Number")
                    .HasColumnType("int(4)");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory);

                entity.ToTable("category");

                entity.Property(e => e.IdCategory)
                    .HasColumnName("Id_Category")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.ToTable("client");

                entity.HasIndex(e => e.IdType)
                    .HasName("fk_CLIENT_IDENTIFICATION_TYPE1_idx");

                entity.Property(e => e.IdentificationNumber)
                    .HasColumnName("Identification_Number")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IdClient)
                    .HasColumnName("Id_Client")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AdmissionDate)
                    .HasColumnName("Admission_Date")
                    .HasColumnType("date");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdType)
                    .HasColumnName("Id_Type")
                    .HasColumnType("int(3)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("Last_Name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Status).HasColumnType("tinyint(4)");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CLIENT_IDENTIFICATION_TYPE1");
            });

            modelBuilder.Entity<ConfigCompany>(entity =>
            {
                entity.HasKey(e => e.IdConfig);

                entity.ToTable("config_company");

                entity.HasIndex(e => e.IdType)
                    .HasName("fk_CONFIG_COMPANY_IDENTIFICATION_CARD1_idx");

                entity.Property(e => e.IdConfig)
                    .HasColumnName("Id_Config")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Canton)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.neighborhood)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.CompannyName)
                    .IsRequired()
                    .HasColumnName("Companny_Name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.CurrencyValue).HasColumnName("Currency_Value");

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Fax).HasColumnType("varchar(10)");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdType)
                    .HasColumnName("Id_Type")
                    .HasColumnType("int(3)");

                entity.Property(e => e.IdUser)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.OtherSigns)
                    .HasColumnName("Other_Signs")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.PasswordTax)
                    .IsRequired()
                    .HasColumnName("Password_Tax")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Telephone).HasColumnType("varchar(10)");

                entity.Property(e => e.UserTax)
                    .IsRequired()
                    .HasColumnName("User_Tax")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Status)
                   .IsRequired()
                   .HasColumnType("varchar(10)");

                entity.Property(e => e.pin)
                    .IsRequired()
                    .HasColumnName("pin")
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.ConfigCompany)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CONFIG_COMPANY_IDENTIFICATION_CARD1");
            });

            modelBuilder.Entity<Conversations>(entity =>
            {
                entity.HasKey(e => e.IdConversation);

                entity.ToTable("conversations");

                entity.Property(e => e.IdConversation).HasColumnName("Id_Conversation");

                entity.Property(e => e.IdRecipient).HasColumnName("idRecipient");

                entity.Property(e => e.IdUser)
                    .HasColumnName("idUser")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.IdCountry);

                entity.ToTable("country");

                entity.Property(e => e.IdCountry)
                    .HasColumnName("Id_Country")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasColumnName("Country_Name")
                    .HasColumnType("varchar(64)");

                entity.Property(e => e.CountryISOCode)
                    .IsRequired()
                    .HasColumnName("Country_ISO_Code")
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.CountryCode)
                    .HasColumnName("Country_Code")
                    .HasColumnType("varchar(7)");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.IdCurrency);

                entity.ToTable("currency");

                entity.Property(e => e.IdCurrency)
                    .HasColumnName("Id_Currency")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(5)");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.CurrentChange).HasColumnName("Current_Change");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.IdDiscount);

                entity.ToTable("discount");

                entity.Property(e => e.IdDiscount)
                    .HasColumnName("Id_Discount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Percentage).HasColumnType("int(11)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.HasKey(e => e.IdFamily);

                entity.ToTable("family");

                entity.Property(e => e.IdFamily)
                    .HasColumnName("Id_Family")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(100)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(25)");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.HasKey(e => e.IdFile);

                entity.ToTable("files");

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
            });

            modelBuilder.Entity<IdentificationType>(entity =>
            {
                entity.HasKey(e => e.IdType);

                entity.ToTable("identification_type");

                entity.Property(e => e.IdType)
                    .HasColumnName("Id_Type")
                    .HasColumnType("int(3)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.IdInventory);

                entity.ToTable("inventory");

                entity.HasIndex(e => e.IdProduct)
                    .HasName("fk_INVENTORY_PRODUCT1_idx");

                entity.Property(e => e.IdInventory)
                    .HasColumnName("Id_Inventory")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdProduct)
                    .HasColumnName("Id_Product")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MovementDate)
                    .HasColumnName("Movement_Date")
                    .HasColumnType("date");

                entity.Property(e => e.MovementType)
                    .IsRequired()
                    .HasColumnName("Movement_Type")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Quantity).HasColumnType("int(10)");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_INVENTORY_PRODUCT1");
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

            modelBuilder.Entity<MasterCertificate>(entity =>
            {
                entity.HasKey(e => e.IdCertificate);

                entity.ToTable("master_certificate");

                entity.Property(e => e.IdCertificate)
                    .HasColumnName("Id_Certificate")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CommpannyName)
                    .IsRequired()
                    .HasColumnName("Commpanny_Name")
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
                    .HasColumnName("Pin_Certificate")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<MasterConsecutive>(entity =>
            {
                entity.HasKey(e => e.IdConsecutive);

                entity.ToTable("master_consecutive");

                entity.HasIndex(e => e.VoucherType)
                    .HasName("fk_MASTER_CONSECUTIVE_VOUCHER_TYPE1_idx");

                entity.Property(e => e.IdConsecutive)
                    .HasColumnName("ID_Consecutive")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchOffice)
                    .IsRequired()
                    .HasColumnName("Branch_Office")
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.NumberConsecutive)
                    .IsRequired()
                    .HasColumnName("Number_Consecutive")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Terminal)
                    .IsRequired()
                    .HasColumnType("varchar(6)");

                entity.Property(e => e.VoucherType)
                    .HasColumnName("Voucher_type")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.VoucherTypeNavigation)
                    .WithMany(p => p.MasterConsecutive)
                    .HasForeignKey(d => d.VoucherType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_CONSECUTIVE_VOUCHER_TYPE1");
            });

            modelBuilder.Entity<MasterDetail>(entity =>
            {
                entity.HasKey(e => e.IdDetail);

                entity.ToTable("master_detail");

                entity.HasIndex(e => e.IdKey)
                    .HasName("fk_MASTER_DETAIL_MASTER_INVOICE_VOUCHER1_idx");

                entity.Property(e => e.IdDetail)
                    .HasColumnName("Id_Detail")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IdKey)
                    .HasColumnName("Id_Key")
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

                entity.HasOne(d => d.IdKeyNavigation)
                    .WithMany(p => p.MasterDetail)
                    .HasForeignKey(d => d.IdKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_DETAIL_MASTER_INVOICE_VOUCHER1");
            });

            modelBuilder.Entity<MasterInvoiceReference>(entity =>
            {
                entity.HasKey(e => e.IdKey);

                entity.ToTable("master_invoice_reference");

                entity.HasIndex(e => e.IdKey)
                    .HasName("fk_MASTER_INVOICE_REFERENCE_MASTER_KEY1_idx");

                entity.Property(e => e.IdKey)
                    .HasColumnName("Id_Key")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Detail)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DocumentNumber)
                    .IsRequired()
                    .HasColumnName("Document_Number")
                    .HasColumnType("varchar(50)");

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

                entity.HasOne(d => d.IdKeyNavigation)
                    .WithOne(p => p.MasterInvoiceReference)
                    .HasForeignKey<MasterInvoiceReference>(d => d.IdKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_INVOICE_REFERENCE_MASTER_KEY1");
            });

            modelBuilder.Entity<MasterInvoiceVoucher>(entity =>
            {
                entity.HasKey(e => e.IdKey);

                entity.ToTable("master_invoice_voucher");

                entity.HasIndex(e => e.IdCondition)
                    .HasName("fk_MASTER_INVOICE_VOUCHER_MASTER_SALE_CONDITION1_idx");

                entity.HasIndex(e => e.IdPayment)
                    .HasName("fk_MASTER_INVOICE_VOUCHER_MASTER_PAYMENT1_idx");

                entity.Property(e => e.IdKey)
                    .HasColumnName("Id_Key")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Env)
                    .IsRequired()
                    .HasColumnName("env")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IdCondition)
                    .HasColumnName("Id_Condition")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdPayment)
                    .HasColumnName("Id_Payment")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RespuestaMhbase64)
                    .HasColumnName("RespuestaMHBase64")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.XmlEnviadoBase64)
                    .IsRequired()
                    .HasColumnName("xmlEnviadoBase64")
                    .HasColumnType("mediumtext");

                entity.HasOne(d => d.IdConditionNavigation)
                    .WithMany(p => p.MasterInvoiceVoucher)
                    .HasForeignKey(d => d.IdCondition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_INVOICE_VOUCHER_MASTER_SALE_CONDITION1");

                entity.HasOne(d => d.IdKeyNavigation)
                    .WithOne(p => p.MasterInvoiceVoucher)
                    .HasForeignKey<MasterInvoiceVoucher>(d => d.IdKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_INVOICE_VOUCHER_MASTER_KEY1");

                entity.HasOne(d => d.IdPaymentNavigation)
                    .WithMany(p => p.MasterInvoiceVoucher)
                    .HasForeignKey(d => d.IdPayment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_INVOICE_VOUCHER_MASTER_PAYMENT1");
            });

            modelBuilder.Entity<MasterKey>(entity =>
            {
                entity.HasKey(e => e.IdKey);

                entity.ToTable("master_key");

                entity.HasIndex(e => e.IdConsecutive)
                    .HasName("fk_MASTER_KEY_MASTER_CONSECUTIVE1_idx");

                entity.HasIndex(e => e.IdSituation)
                    .HasName("fk_MASTER_KEY_SITUATION_DOCUMENT1_idx");

                entity.Property(e => e.IdKey)
                    .HasColumnName("Id_Key")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.IdConsecutive)
                    .HasColumnName("Id_Consecutive")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdSituation)
                    .HasColumnName("Id_Situation")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTrasmitter)
                    .IsRequired()
                    .HasColumnName("Id_Trasmitter")
                    .HasColumnType("varchar(12)");

                entity.Property(e => e.Month)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.NumberKey)
                    .IsRequired()
                    .HasColumnName("Number_Key")
                    .HasColumnType("varchar(8)");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                entity.HasOne(d => d.IdConsecutiveNavigation)
                    .WithMany(p => p.MasterKey)
                    .HasForeignKey(d => d.IdConsecutive)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_KEY_MASTER_CONSECUTIVE1");

                entity.HasOne(d => d.IdSituationNavigation)
                    .WithMany(p => p.MasterKey)
                    .HasForeignKey(d => d.IdSituation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_KEY_SITUATION_DOCUMENT1");
            });

            modelBuilder.Entity<MasterLogs>(entity =>
            {
                entity.HasKey(e => e.IdLog);

                entity.ToTable("master_logs");

                entity.Property(e => e.IdLog)
                    .HasColumnName("Id_Log")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUsers)
                    .HasColumnName("Id_users")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Log)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<MasterMonetaryDetails>(entity =>
            {
                entity.HasKey(e => e.IdKey);

                entity.ToTable("master_monetary_details");

                entity.Property(e => e.IdKey)
                    .HasColumnName("Id_Key")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodeCurrency)
                    .IsRequired()
                    .HasColumnName("Code_Currency")
                    .HasColumnType("varchar(5)");

                entity.Property(e => e.TotalDiscount).HasColumnName("Total_Discount");

                entity.Property(e => e.TotalExempt).HasColumnName("Total_exempt");

                entity.Property(e => e.TotalExemptFreight).HasColumnName("Total_exempt_freight");

                entity.Property(e => e.TotalExemptServices).HasColumnName("Total_Exempt_Services");

                entity.Property(e => e.TotalFinal).HasColumnName("Total_Final");

                entity.Property(e => e.TotalNetSale).HasColumnName("Total_Net_Sale");

                entity.Property(e => e.TotalSale).HasColumnName("Total_sale");

                entity.Property(e => e.TotalTax).HasColumnName("Total_Tax");

                entity.Property(e => e.TotalTaxed).HasColumnName("Total_Taxed");

                entity.Property(e => e.TotalTaxedGoods).HasColumnName("Total_Taxed_Goods");

                entity.Property(e => e.TotalTaxedServices).HasColumnName("Total_Taxed_Services");

                entity.Property(e => e.ValueCurrency).HasColumnName("Value_Currency");

                entity.HasOne(d => d.IdKeyNavigation)
                    .WithOne(p => p.MasterMonetaryDetails)
                    .HasForeignKey<MasterMonetaryDetails>(d => d.IdKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_MONETARY_DETAILS_MASTER_INVOICE_VOUCHER1");
            });

            modelBuilder.Entity<MasterPayment>(entity =>
            {
                entity.HasKey(e => e.IdPayment);

                entity.ToTable("master_payment");

                entity.Property(e => e.IdPayment)
                    .HasColumnName("Id_Payment")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<MasterReceiver>(entity =>
            {
                entity.HasKey(e => e.IdKey);

                entity.ToTable("master_receiver");

                entity.Property(e => e.IdKey)
                    .HasColumnName("Id_Key")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasColumnName("Country_Code")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Fax)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdCanton)
                    .IsRequired()
                    .HasColumnName("Id_Canton")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdCard)
                    .HasColumnName("Id_Card")
                    .HasColumnType("int(15)");

                entity.Property(e => e.IdDistrict)
                    .IsRequired()
                    .HasColumnName("Id_District")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdNeighborhood)
                    .IsRequired()
                    .HasColumnName("Id_Neighborhood")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdProvince)
                    .IsRequired()
                    .HasColumnName("Id_Province")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdType)
                    .IsRequired()
                    .HasColumnName("Id_Type")
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.OthersSigns)
                    .HasColumnName("Others_Signs")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.HasOne(d => d.IdKeyNavigation)
                    .WithOne(p => p.MasterReceiver)
                    .HasForeignKey<MasterReceiver>(d => d.IdKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_RECEIVER_MASTER_INVOICE_VOUCHER1");
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

            modelBuilder.Entity<MasterSqlupgrade>(entity =>
            {
                entity.HasKey(e => e.IdSql);

                entity.ToTable("master_sqlupgrade");

                entity.Property(e => e.IdSql)
                    .HasColumnName("Id_Sql")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Sql)
                    .IsRequired()
                    .HasColumnName("SQL")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.VersionApi).HasColumnName("Version_API");
            });

            modelBuilder.Entity<MasterTransmitter>(entity =>
            {
                entity.HasKey(e => e.IdKey);

                entity.ToTable("master_transmitter");

                entity.Property(e => e.IdKey)
                    .HasColumnName("Id_Key")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasColumnName("Country_Code")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Fax)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdCanton)
                    .IsRequired()
                    .HasColumnName("Id_Canton")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdCard)
                    .HasColumnName("Id_Card")
                    .HasColumnType("int(15)");

                entity.Property(e => e.IdDistrict)
                    .IsRequired()
                    .HasColumnName("Id_District")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdNeighborhood)
                    .IsRequired()
                    .HasColumnName("Id_Neighborhood")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdProvince)
                    .IsRequired()
                    .HasColumnName("Id_Province")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.IdType)
                    .IsRequired()
                    .HasColumnName("Id_Type")
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.OthersSigns)
                    .HasColumnName("Others_Signs")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.HasOne(d => d.IdKeyNavigation)
                    .WithOne(p => p.MasterTransmitter)
                    .HasForeignKey<MasterTransmitter>(d => d.IdKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MASTER_TRANSMITTER_MASTER_INVOICE_VOUCHER1");
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

                entity.Property(e => e.Symboly)
                    .IsRequired()
                    .HasColumnType("varchar(5)");
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(e => e.IdMessage);

                entity.ToTable("mesages");

                entity.HasIndex(e => e.IdConversation)
                    .HasName("fk_MESAGES_CONVERSATIONS1_idx");

                entity.Property(e => e.IdMessage).HasColumnName("Id_Message");

                entity.Property(e => e.Channel).HasColumnType("int(11)");

                entity.Property(e => e.IdConversation).HasColumnName("id_Conversation");

                entity.Property(e => e.IdRecipient).HasColumnName("ID_Recipient");

                entity.Property(e => e.IdSender).HasColumnName("Id_Sender");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.IdConversationNavigation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.IdConversation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_MESAGES_CONVERSATIONS1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.ToTable("product");

                entity.HasIndex(e => e.IdCategory)
                    .HasName("fk_PRODUCT_CATEGORY1_idx");

                entity.HasIndex(e => e.IdCurrency)
                    .HasName("fk_PRODUCT_CURRENCY1_idx");

                entity.HasIndex(e => e.IdDiscount)
                    .HasName("fk_PRODUCT_DISCOUNT1_idx");

                entity.HasIndex(e => e.IdProvider)
                    .HasName("fk_PRODUCT_PROVIDER1_idx");

                entity.HasIndex(e => e.IdTax)
                    .HasName("fk_PRODUCT_TAX1_idx");

                entity.HasIndex(e => e.IdUnit)
                    .HasName("fk_PRODUCT_MEASURED_UNIT1_idx");

                entity.Property(e => e.IdProduct)
                    .HasColumnName("Id_Product")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Barcode).HasColumnType("varchar(100)");

                entity.Property(e => e.CodeProduct)
                    .IsRequired()
                    .HasColumnName("Code_Product")
                    .HasColumnType("varchar(20)");
                
                entity.Property(e => e.CostPrice).HasColumnName("Cost_Price");

                entity.Property(e => e.Description).HasColumnType("varchar(100)");

                entity.Property(e => e.IdCategory)
                    .HasColumnName("Id_Category")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdCurrency)
                    .HasColumnName("Id_Currency")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdDiscount)
                    .HasColumnName("Id_Discount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdProvider)
                    .HasColumnName("Id_Provider")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTax)
                    .HasColumnName("Id_Tax")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUnit)
                    .HasColumnName("Id_Unit")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NameProduct)
                    .IsRequired()
                    .HasColumnName("Name_Product")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ProfitPercentage).HasColumnName("Profit_Percentage");

                entity.Property(e => e.Quantity).HasColumnType("int(10)");

                entity.Property(e => e.SalePrice).HasColumnName("Sale_Price");

                entity.Property(e => e.Status).HasColumnType("tinyint(4)");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("fk_PRODUCT_CATEGORY1");

                entity.HasOne(d => d.IdCurrencyNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdCurrency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_CURRENCY1");

                entity.HasOne(d => d.IdDiscountNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdDiscount)
                    .HasConstraintName("fk_PRODUCT_DISCOUNT1");

                entity.HasOne(d => d.IdProviderNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdProvider)
                    .HasConstraintName("fk_PRODUCT_PROVIDER1");

                entity.HasOne(d => d.IdTaxNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdTax)
                    .HasConstraintName("fk_PRODUCT_TAX1");

                entity.HasOne(d => d.IdUnitNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.IdUnit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_MEASURED_UNIT1");
            });

            modelBuilder.Entity<ProductHasFamily>(entity =>
            {
                entity.HasKey(e => new { e.IdProduct, e.IdFamily });

                entity.ToTable("product_has_family");

                entity.HasIndex(e => e.IdFamily)
                    .HasName("fk_PRODUCT_has_FAMILY_FAMILY1_idx");

                entity.HasIndex(e => e.IdProduct)
                    .HasName("fk_PRODUCT_has_FAMILY_PRODUCT1_idx");

                entity.Property(e => e.IdProduct)
                    .HasColumnName("Id_Product")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdFamily)
                    .HasColumnName("Id_Family")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdFamilyNavigation)
                    .WithMany(p => p.ProductHasFamily)
                    .HasForeignKey(d => d.IdFamily)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_has_FAMILY_FAMILY1");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductHasFamily)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_has_FAMILY_PRODUCT1");
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.HasKey(e => e.IdProvider);

                entity.ToTable("provider");

                entity.HasIndex(e => e.IdType)
                    .HasName("fk_PROVIDER_IDENTIFICATION_CARD1_idx");

                entity.Property(e => e.IdProvider)
                    .HasColumnName("Id_Provider")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdentificationNumber)
                    .HasColumnName("Identification_Number")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Description).HasColumnType("varchar(100)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IdType)
                    .HasColumnName("Id_Type")
                    .HasColumnType("int(3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Provider)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PROVIDER_IDENTIFICATION_CARD1");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.ToTable("rol");

                entity.Property(e => e.IdRol)
                    .HasColumnName("Id_Rol")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(100)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<RolHasUsers>(entity =>
            {
                entity.HasKey(e => new { e.IdRol, e.IdUsers });

                entity.ToTable("rol_has_users");

                entity.HasIndex(e => e.IdRol)
                    .HasName("fk_ROL_has_users_ROL1_idx");

                entity.Property(e => e.IdRol)
                    .HasColumnName("Id_Rol")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUsers).HasColumnType("int(11)");

                entity.HasOne(d => d.RolIdRolNavigation)
                    .WithMany(p => p.RolHasUsers)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ROL_has_users_ROL1");
            });

            modelBuilder.Entity<Sessions>(entity =>
            {
                entity.HasKey(e => e.IdSession);

                entity.ToTable("sessions");

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
            });

            modelBuilder.Entity<SituationDocument>(entity =>
            {
                entity.HasKey(e => e.IdSituation);

                entity.ToTable("situation_document");

                entity.Property(e => e.IdSituation)
                    .HasColumnName("Id_Situation")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.HasKey(e => e.IdTax);

                entity.ToTable("tax");

                entity.Property(e => e.IdTax)
                    .HasColumnName("Id_Tax")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Percentage).HasColumnType("int(11)");
            });

            modelBuilder.Entity<TelephoneContact>(entity =>
            {
                entity.HasKey(e => e.IdContact);

                entity.ToTable("telephone_contact");

                entity.Property(e => e.IdContact)
                    .HasColumnName("Id_Contact")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TelephoneNumber)
                    .IsRequired()
                    .HasColumnName("Telephone_Number")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.CountryCode)
                    .HasColumnName("Country_Code")
                    .HasColumnType("varchar(7)");

                entity.Property(e => e.Description).HasColumnType("varchar(100)");

                entity.Property(e => e.Extension).HasColumnType("int(11)");

                entity.Property(e => e.IdOwner)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Terminal>(entity =>
            {
                entity.HasKey(e => e.IdTerminal);

                entity.ToTable("terminal");

                entity.HasIndex(e => e.IdOffice)
                    .HasName("fk_TERMINAL_BRANCH_OFFICE1_idx");

                entity.Property(e => e.IdTerminal)
                    .HasColumnName("Id_Terminal")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdOffice)
                    .HasColumnName("Id_Office")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.TerminalNumber)
                    .HasColumnName("Terminal_Number")
                    .HasColumnType("int(4)");

                entity.HasOne(d => d.IdOfficeNavigation)
                    .WithMany(p => p.Terminal)
                    .HasForeignKey(d => d.IdOffice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TERMINAL_BRANCH_OFFICE1");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("email")
                    .IsUnique();

                entity.HasIndex(e => e.IdUser)
                    .HasName("idUser");

                entity.HasIndex(e => e.Status)
                    .HasName("status");

                entity.HasIndex(e => e.UserName)
                    .HasName("wire_2")
                    .IsUnique();

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

                entity.Property(e => e.LastAccess).HasColumnName("lastAccess");

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasColumnName("pwd")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Settings)
                    .HasColumnName("settings")
                    .HasColumnType("text");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

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
                    .HasColumnName("Id_Voucher_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });
        }
    }
}
