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
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<BranchOffice> BranchOffice { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryHasProduct> CategoryHasProduct { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Coin> Coin { get; set; }
        public virtual DbSet<CommerciallBrand> CommerciallBrand { get; set; }
        public virtual DbSet<CompanyInformation> CompanyInformation { get; set; }
        public virtual DbSet<Detail> Detail { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<Entity> Entity { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceReference> InvoiceReference { get; set; }
        public virtual DbSet<MeasuredUnit> MeasuredUnit { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCode> ProductCode { get; set; }
        public virtual DbSet<ProductFamily> ProductFamily { get; set; }
        public virtual DbSet<ProductHasDiscount> ProductHasDiscount { get; set; }
        public virtual DbSet<ProductHasProductFamily> ProductHasProductFamily { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<RecordActivity> RecordActivity { get; set; }
        public virtual DbSet<SaleCondition> SaleCondition { get; set; }
        public virtual DbSet<SalePrice> SalePrice { get; set; }
        public virtual DbSet<TaxExemption> TaxExemption { get; set; }
        public virtual DbSet<TelephoneContact> TelephoneContact { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=localhost;Database=db_facturacion;User=root;Password=;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.IdAddress);

                entity.ToTable("address");

                entity.HasIndex(e => e.EntityIdEntity)
                    .HasName("fk_ADDRESS_ENTITY1_idx");

                entity.Property(e => e.IdAddress)
                    .HasColumnName("Id_Address")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'9'");

                entity.Property(e => e.Canton)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.EntityIdEntity)
                    .IsRequired()
                    .HasColumnName("ENTITY_Id_Entity")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Neighborhood).HasColumnType("varchar(45)");

                entity.Property(e => e.OtherSigns)
                    .IsRequired()
                    .HasColumnName("Other_Signs")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("Postal_Code")
                    .HasColumnType("varchar(6)");

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.EntityIdEntityNavigation)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.EntityIdEntity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ADDRESS_ENTITY1");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceIdInvoice, e.ClientEntityIdEntity, e.CompanyInformationEntityIdEntity });

                entity.ToTable("bill");

                entity.HasIndex(e => e.ClientEntityIdEntity)
                    .HasName("fk_Bill_CLIENT1_idx");

                entity.HasIndex(e => e.CompanyInformationEntityIdEntity)
                    .HasName("fk_Bill_COMPANY_INFORMATION1_idx");

                entity.HasIndex(e => e.InvoiceIdInvoice)
                    .HasName("fk_Bill_INVOICE_idx");

                entity.HasIndex(e => e.PaymentIdPayment)
                    .HasName("fk_Bill_PAYMENT1_idx");

                entity.HasIndex(e => e.SaleConditionIdCondition)
                    .HasName("fk_Bill_SALE_CONDITION1_idx");

                entity.Property(e => e.InvoiceIdInvoice)
                    .HasColumnName("INVOICE_Id_Invoice")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientEntityIdEntity)
                    .HasColumnName("CLIENT_ENTITY_Id_Entity")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.CompanyInformationEntityIdEntity)
                    .HasColumnName("COMPANY_INFORMATION_ENTITY_Id_Entity")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.CreditTimeLimit)
                    .HasColumnName("Credit_Time_Limit")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PaymentIdPayment)
                    .HasColumnName("PAYMENT_id_Payment")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SaleConditionIdCondition)
                    .HasColumnName("SALE_CONDITION_Id_Condition")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ClientEntityIdEntityNavigation)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.ClientEntityIdEntity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Bill_CLIENT1");

                entity.HasOne(d => d.CompanyInformationEntityIdEntityNavigation)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.CompanyInformationEntityIdEntity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Bill_COMPANY_INFORMATION1");

                entity.HasOne(d => d.InvoiceIdInvoiceNavigation)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.InvoiceIdInvoice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Bill_INVOICE1");

                entity.HasOne(d => d.PaymentIdPaymentNavigation)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.PaymentIdPayment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Bill_PAYMENT1");

                entity.HasOne(d => d.SaleConditionIdConditionNavigation)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.SaleConditionIdCondition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Bill_SALE_CONDITION1");
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
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.NumberOffice)
                    .IsRequired()
                    .HasColumnName("Number_Office")
                    .HasColumnType("varchar(45)");
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
                entity.HasKey(e => e.EntityIdEntity);

                entity.ToTable("client");

                entity.Property(e => e.EntityIdEntity)
                    .HasColumnName("ENTITY_Id_Entity")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Birthdate).HasColumnType("datetime");

                entity.Property(e => e.ClientType)
                    .IsRequired()
                    .HasColumnName("Client_Type")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("First_Name")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasColumnName("Second_Name")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.EntityIdEntityNavigation)
                    .WithOne(p => p.Client)
                    .HasForeignKey<Client>(d => d.EntityIdEntity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CLIENT_ENTITY1");
            });

            modelBuilder.Entity<Coin>(entity =>
            {
                entity.HasKey(e => e.IdCoin);

                entity.ToTable("coin");

                entity.Property(e => e.IdCoin)
                    .HasColumnName("Id_Coin")
                    .HasColumnType("int(11)");

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

                entity.HasIndex(e => e.ProviderEntityIdEntity)
                    .HasName("fk_COMMERCIALl_BRAND_PROVIDER1_idx");

                entity.Property(e => e.IdBrand)
                    .HasColumnName("Id_Brand")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.ProviderEntityIdEntity)
                    .IsRequired()
                    .HasColumnName("PROVIDER_ENTITY_Id_Entity")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.ProviderEntityIdEntityNavigation)
                    .WithMany(p => p.CommerciallBrand)
                    .HasForeignKey(d => d.ProviderEntityIdEntity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_COMMERCIALl_BRAND_PROVIDER1");
            });

            modelBuilder.Entity<CompanyInformation>(entity =>
            {
                entity.HasKey(e => e.EntityIdEntity);

                entity.ToTable("company_information");

                entity.HasIndex(e => e.BranchOfficeIdOffice)
                    .HasName("fk_COMPANY_INFORMATION_BRANCH_OFFICE1_idx");

                entity.Property(e => e.EntityIdEntity)
                    .HasColumnName("ENTITY_Id_Entity")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.BoxNumber)
                    .HasColumnName("Box_Number")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchOfficeIdOffice)
                    .HasColumnName("BRANCH_OFFICE_Id_Office")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Tradename)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.BranchOfficeIdOfficeNavigation)
                    .WithMany(p => p.CompanyInformation)
                    .HasForeignKey(d => d.BranchOfficeIdOffice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_COMPANY_INFORMATION_BRANCH_OFFICE1");

                entity.HasOne(d => d.EntityIdEntityNavigation)
                    .WithOne(p => p.CompanyInformation)
                    .HasForeignKey<CompanyInformation>(d => d.EntityIdEntity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_COMPANY_INFORMATION_ENTITY1");
            });

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.HasKey(e => e.IdDetail);

                entity.ToTable("detail");

                entity.HasIndex(e => e.BillInvoiceIdInvoice)
                    .HasName("fk_DETAIL_Bill1_idx");

                entity.HasIndex(e => e.MeasuredUnitIdUnit)
                    .HasName("fk_DETAIL_MEASURED_UNIT1_idx");

                entity.HasIndex(e => e.ProductIdProduct)
                    .HasName("fk_DETAIL_PRODUCT1_idx");

                entity.Property(e => e.IdDetail)
                    .HasColumnName("Id_Detail")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BillInvoiceIdInvoice)
                    .HasColumnName("Bill_INVOICE_Id_Invoice")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.InitialAmount).HasColumnName("Initial_Amount");

                entity.Property(e => e.MeasuredUnitIdUnit)
                    .HasColumnName("MEASURED_UNIT_Id_Unit")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductIdProduct)
                    .HasColumnName("PRODUCT_Id_Product")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Quantity).HasColumnType("int(11)");

                entity.Property(e => e.TotalAmount).HasColumnName("Total_Amount");

                entity.Property(e => e.TypeCode)
                    .IsRequired()
                    .HasColumnName("Type_Code")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.TypeDiscount)
                    .HasColumnName("Type_Discount")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.MeasuredUnitIdUnitNavigation)
                    .WithMany(p => p.Detail)
                    .HasForeignKey(d => d.MeasuredUnitIdUnit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DETAIL_MEASURED_UNIT1");

                entity.HasOne(d => d.ProductIdProductNavigation)
                    .WithMany(p => p.Detail)
                    .HasForeignKey(d => d.ProductIdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_DETAIL_PRODUCT1");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.IdDiscount);

                entity.ToTable("discount");

                entity.Property(e => e.IdDiscount)
                    .HasColumnName("Id_Discount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Percentage).HasColumnType("int(11)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.IdDocument);

                entity.ToTable("document");

                entity.Property(e => e.IdDocument)
                    .HasColumnName("Id_Document")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name).HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.HasKey(e => e.IdEntity);

                entity.ToTable("entity");

                entity.Property(e => e.IdEntity)
                    .HasColumnName("Id_Entity")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.TypeId)
                    .IsRequired()
                    .HasColumnName("Type_Id")
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.IdInventary);

                entity.ToTable("inventory");

                entity.HasIndex(e => e.ProductIdProduct)
                    .HasName("fk_INVENTORY_PRODUCT1_idx");

                entity.Property(e => e.IdInventary)
                    .HasColumnName("Id_Inventary")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cuantity).HasColumnType("int(11)");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.MovementType)
                    .IsRequired()
                    .HasColumnName("Movement_Type")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.ProductIdProduct)
                    .HasColumnName("PRODUCT_Id_Product")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProviderPersonIdPerson)
                    .IsRequired()
                    .HasColumnName("PROVIDER_Person_Id_Person")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.ProductIdProductNavigation)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.ProductIdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_INVENTORY_PRODUCT1");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.IdInvoice);

                entity.ToTable("invoice");

                entity.HasIndex(e => e.DocumentIdDocument)
                    .HasName("fk_INVOICE_Document1_idx");

                entity.Property(e => e.IdInvoice)
                    .HasColumnName("Id_Invoice")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DocumentIdDocument)
                    .HasColumnName("Document_Id_Document")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmissionDate)
                    .HasColumnName("Emission_Date")
                    .HasColumnType("date");

                entity.HasOne(d => d.DocumentIdDocumentNavigation)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.DocumentIdDocument)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_INVOICE_Document1");
            });

            modelBuilder.Entity<InvoiceReference>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceIdInvoice, e.InvoiceDocumentIdDocument });

                entity.ToTable("invoice_reference");

                entity.Property(e => e.InvoiceIdInvoice)
                    .HasColumnName("INVOICE_Id_Invoice")
                    .HasColumnType("int(11)");

                entity.Property(e => e.InvoiceDocumentIdDocument)
                    .HasColumnName("INVOICE_Document_Id_Document")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Detail)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DocumentNumber)
                    .IsRequired()
                    .HasColumnName("Document_Number")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.ReferenceCode)
                    .IsRequired()
                    .HasColumnName("Reference_Code")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.InvoiceIdInvoiceNavigation)
                    .WithMany(p => p.InvoiceReference)
                    .HasForeignKey(d => d.InvoiceIdInvoice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_INVOICE_REFERENCE_INVOICE1");
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
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Symboly)
                    .IsRequired()
                    .HasColumnType("varchar(10)");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.IdPayment);

                entity.ToTable("payment");

                entity.Property(e => e.IdPayment)
                    .HasColumnName("id_Payment")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.ToTable("product");

                entity.HasIndex(e => e.CommercialBrandIdBrand)
                    .HasName("fk_PRODUCT_Commercial_Brand1_idx");

                entity.Property(e => e.IdProduct)
                    .HasColumnName("Id_Product")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CommercialBrandIdBrand)
                    .HasColumnName("Commercial_Brand_Id_Brand")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CostPrice).HasColumnName("Cost_Price");

                entity.Property(e => e.Description).HasColumnType("varchar(100)");

                entity.Property(e => e.IVA).HasColumnName("I.V.A");

                entity.HasOne(d => d.CommercialBrandIdBrandNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CommercialBrandIdBrand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_Commercial_Brand1");
            });

            modelBuilder.Entity<ProductCode>(entity =>
            {
                entity.HasKey(e => new { e.IdCode, e.ProductIdProduct });

                entity.ToTable("product_code");

                entity.HasIndex(e => e.ProductIdProduct)
                    .HasName("fk_PRODUCT_CODE_PRODUCT1_idx");

                entity.Property(e => e.IdCode)
                    .HasColumnName("Id_Code")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.ProductIdProduct)
                    .HasColumnName("PRODUCT_Id_Product")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NameCode)
                    .IsRequired()
                    .HasColumnName("Name_Code")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.ProductIdProductNavigation)
                    .WithMany(p => p.ProductCode)
                    .HasForeignKey(d => d.ProductIdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_CODE_PRODUCT1");
            });

            modelBuilder.Entity<ProductFamily>(entity =>
            {
                entity.HasKey(e => e.IdFamily);

                entity.ToTable("product_family");

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
            });

            modelBuilder.Entity<ProductHasDiscount>(entity =>
            {
                entity.HasKey(e => new { e.ProductIdProduct, e.DiscountIdDiscount });

                entity.ToTable("product_has_discount");

                entity.HasIndex(e => e.DiscountIdDiscount)
                    .HasName("fk_PRODUCT_has_DISCOUNT_DISCOUNT1_idx");

                entity.HasIndex(e => e.ProductIdProduct)
                    .HasName("fk_PRODUCT_has_DISCOUNT_PRODUCT1_idx");

                entity.Property(e => e.ProductIdProduct)
                    .HasColumnName("PRODUCT_Id_Product")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiscountIdDiscount)
                    .HasColumnName("DISCOUNT_Id_Discount")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.DiscountIdDiscountNavigation)
                    .WithMany(p => p.ProductHasDiscount)
                    .HasForeignKey(d => d.DiscountIdDiscount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_has_DISCOUNT_DISCOUNT1");

                entity.HasOne(d => d.ProductIdProductNavigation)
                    .WithMany(p => p.ProductHasDiscount)
                    .HasForeignKey(d => d.ProductIdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRODUCT_has_DISCOUNT_PRODUCT1");
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
                entity.HasKey(e => e.EntityIdEntity);

                entity.ToTable("provider");

                entity.HasIndex(e => e.EntityIdEntity)
                    .HasName("fk_PROVIDER_ENTITY1_idx");

                entity.Property(e => e.EntityIdEntity)
                    .HasColumnName("ENTITY_Id_Entity")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.EntityIdEntityNavigation)
                    .WithOne(p => p.Provider)
                    .HasForeignKey<Provider>(d => d.EntityIdEntity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PROVIDER_ENTITY1");
            });

            modelBuilder.Entity<RecordActivity>(entity =>
            {
                entity.HasKey(e => e.IdRecord);

                entity.ToTable("record_activity");

                entity.HasIndex(e => e.UserEntityIdEntity)
                    .HasName("fk_RECORD_ACTIVITY_USER1_idx");

                entity.Property(e => e.IdRecord)
                    .HasColumnName("Id_Record")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EntryDate)
                    .HasColumnName("Entry_Date")
                    .HasColumnType("date");

                entity.Property(e => e.ExitDate)
                    .HasColumnName("Exit_Date")
                    .HasColumnType("date");

                entity.Property(e => e.UserEntityIdEntity)
                    .IsRequired()
                    .HasColumnName("USER_ENTITY_Id_Entity")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.UserEntityIdEntityNavigation)
                    .WithMany(p => p.RecordActivity)
                    .HasForeignKey(d => d.UserEntityIdEntity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_RECORD_ACTIVITY_USER1");
            });

            modelBuilder.Entity<SaleCondition>(entity =>
            {
                entity.HasKey(e => e.IdCondition);

                entity.ToTable("sale_condition");

                entity.Property(e => e.IdCondition)
                    .HasColumnName("Id_Condition")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name).HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<SalePrice>(entity =>
            {
                entity.HasKey(e => e.IdPrice);

                entity.ToTable("sale_price");

                entity.HasIndex(e => e.CoinIdCoin)
                    .HasName("fk_SALE_PRICE_COIN1_idx");

                entity.HasIndex(e => e.ProductIdProduct)
                    .HasName("fk_SALE_PRICE_PRODUCT1_idx");

                entity.Property(e => e.IdPrice)
                    .HasColumnName("Id_Price")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CoinIdCoin)
                    .HasColumnName("COIN_Id_Coin")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ProductIdProduct)
                    .HasColumnName("PRODUCT_Id_Product")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CoinIdCoinNavigation)
                    .WithMany(p => p.SalePrice)
                    .HasForeignKey(d => d.CoinIdCoin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_SALE_PRICE_COIN1");

                entity.HasOne(d => d.ProductIdProductNavigation)
                    .WithMany(p => p.SalePrice)
                    .HasForeignKey(d => d.ProductIdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_SALE_PRICE_PRODUCT1");
            });

            modelBuilder.Entity<TaxExemption>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("tax/exemption");

                entity.Property(e => e.Code).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(45)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Percentage).HasColumnType("int(11)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<TelephoneContact>(entity =>
            {
                entity.HasKey(e => e.IdContact);

                entity.ToTable("telephone_contact");

                entity.HasIndex(e => e.EntityIdEntity)
                    .HasName("fk_TELEPHONE_CONTACT_ENTITY1_idx");

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

                entity.Property(e => e.EntityIdEntity)
                    .IsRequired()
                    .HasColumnName("ENTITY_Id_Entity")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Extension).HasColumnType("int(11)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.EntityIdEntityNavigation)
                    .WithMany(p => p.TelephoneContact)
                    .HasForeignKey(d => d.EntityIdEntity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TELEPHONE_CONTACT_ENTITY1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.EntityIdEntity);

                entity.ToTable("user");

                entity.HasIndex(e => e.EntityIdEntity)
                    .HasName("fk_USER_ENTITY1_idx");

                entity.Property(e => e.EntityIdEntity)
                    .HasColumnName("ENTITY_Id_Entity")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.EntityIdEntityNavigation)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.EntityIdEntity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_USER_ENTITY1");
            });
        }
    }
}
