using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Models
{
    public partial class SmsPanelDbContext : DbContext
    {
        public SmsPanelDbContext()
        {
        }

        public SmsPanelDbContext(DbContextOptions<SmsPanelDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountInfo> AccountInfo { get; set; }
        public virtual DbSet<AdditionalInfo> AdditionalInfo { get; set; }
        public virtual DbSet<AddressInfo> AddressInfo { get; set; }
        public virtual DbSet<BusinessCard> BusinessCard { get; set; }
        public virtual DbSet<CreditInfo> CreditInfo { get; set; }
        public virtual DbSet<DeviceInfo> DeviceInfo { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<NumberInfo> NumberInfo { get; set; }
        public virtual DbSet<PanelInfo> PanelInfo { get; set; }
        public virtual DbSet<PanelVersionInfo> PanelVersionInfo { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<PersonalInfo> PersonalInfo { get; set; }
        public virtual DbSet<PersonalTemplate> PersonalTemplate { get; set; }
        public virtual DbSet<ReceiveInfo> ReceiveInfo { get; set; }
        public virtual DbSet<RolePermissions> RolePermissions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<ScheduleSmsDetail> ScheduleSmsDetail { get; set; }

        //Saleh Ebrahimian
        
        public virtual DbSet<CalendarEvents> CalendarEvents99 { get; set; }

        //---------------
        public virtual DbSet<ScheduleSmsInfo> ScheduleSmsInfo { get; set; }
        public virtual DbSet<SentInfo> SentInfo { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<TaskInfo> TaskInfo { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TicketDetail> TicketDetail { get; set; }
        public virtual DbSet<UserGroups> UserGroups { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<VwBusinessCard> VwBusinessCard { get; set; }
        public virtual DbSet<VwContact> VwContact { get; set; }
        public virtual DbSet<VwContactGroups> VwContactGroups { get; set; }
        public virtual DbSet<VwNumber> VwNumber { get; set; }
        public virtual DbSet<VwPanel> VwPanel { get; set; }
        public virtual DbSet<VwScheduleSms> VwScheduleSms { get; set; }
        public virtual DbSet<VwScheduleSmsInfo> VwScheduleSmsInfo { get; set; }
        public virtual DbSet<VwTask> VwTask { get; set; }
        public virtual DbSet<VwTicket> VwTicket { get; set; }
        public virtual DbSet<VwTicketDetail> VwTicketDetail { get; set; }
        public virtual DbSet<VwTicketLastMessage> VwTicketLastMessage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountInfo>(entity =>
            {
                entity.ToTable("AccountInfo", "um");

                entity.HasComment("Default Table For User Login Info");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.BusinessPhone)
                    .HasColumnName("businessPhone")
                    .HasMaxLength(15);

                entity.Property(e => e.CreateDate).HasColumnName("createDate");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(500);

                entity.Property(e => e.FormDate).HasColumnName("formDate");

                entity.Property(e => e.FormGuid).HasColumnName("formGuid");

                entity.Property(e => e.HomePhone)
                    .HasColumnName("homePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.LastLogin).HasColumnName("lastLogin");

                entity.Property(e => e.MobilePhone)
                    .HasColumnName("mobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(500);

                entity.Property(e => e.Picture)
                    .HasColumnName("picture")
                    .HasMaxLength(1000);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<AdditionalInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("AdditionalInfo_pkey");

                entity.ToTable("AdditionalInfo", "um");

                entity.HasComment("User Additional Information");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .ValueGeneratedNever();

                entity.Property(e => e.InstagramLink).HasColumnName("instagramLink");

                entity.Property(e => e.IsSpecialDateChanged).HasColumnName("isSpecialDateChanged");

                entity.Property(e => e.SpecialDate).HasColumnName("specialDate");

                entity.Property(e => e.TelegramLink).HasColumnName("telegramLink");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AdditionalInfo)
                    .HasForeignKey<AdditionalInfo>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("USER_ID_FK");
            });

            modelBuilder.Entity<AddressInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("AddressInfo_pkey");

                entity.ToTable("AddressInfo", "um");

                entity.HasComment("User Address Info");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(500);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.Region)
                    .HasColumnName("region")
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AddressInfo)
                    .HasForeignKey<AddressInfo>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("USER_ID_FK1");
            });

            modelBuilder.Entity<BusinessCard>(entity =>
            {
                entity.ToTable("BusinessCard", "sms");

                entity.HasComment("اطلاعات مربوط به کارت ویزیت");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreateDate).HasColumnName("createDate");

                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.Property(e => e.IsBlocked).HasColumnName("isBlocked");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasMaxLength(500);

                entity.Property(e => e.NumberId).HasColumnName("numberId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TemplateId).HasColumnName("templateId");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<CreditInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("CreditInfo_pkey");

                entity.ToTable("CreditInfo", "sms");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Credit)
                    .HasColumnName("credit")
                    .HasColumnType("numeric(20,2)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.CreditInfo)
                    .HasForeignKey<CreditInfo>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_credit");
            });

            modelBuilder.Entity<DeviceInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("DeviceInfo_pkey");

                entity.ToTable("DeviceInfo", "um");

                entity.HasComment("User Device Info");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Browser)
                    .HasColumnName("browser")
                    .HasMaxLength(50);

                entity.Property(e => e.IpAddress)
                    .HasColumnName("ipAddress")
                    .HasMaxLength(50);

                entity.Property(e => e.LastActivity).HasColumnName("lastActivity");

                entity.Property(e => e.Os)
                    .HasColumnName("os")
                    .HasMaxLength(50);

                entity.Property(e => e.Platform)
                    .HasColumnName("platform")
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.DeviceInfo)
                    .HasForeignKey<DeviceInfo>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("USER_ID_FK2");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group", "um");

                entity.HasComment("table for users group");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Descriptions).HasColumnName("descriptions");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Owner).HasColumnName("owner");

                entity.Property(e => e.Picture).HasColumnName("picture");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(500);

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Group)
                    .HasForeignKey(d => d.Owner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Group_owner_fkey");
            });

            modelBuilder.Entity<NumberInfo>(entity =>
            {
                entity.ToTable("NumberInfo", "sms");

                entity.HasComment("اطلاعات مربوط به شماره های خدماتی و  پیام انبوه");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreateDate).HasColumnName("createDate");

                entity.Property(e => e.IsBlocked).HasColumnName("isBlocked");

                entity.Property(e => e.IsShared).HasColumnName("isShared");

                entity.Property(e => e.LastReceivedId).HasColumnName("lastReceivedId");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasMaxLength(50);

                entity.Property(e => e.Owner).HasColumnName("owner");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100);

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.NumberInfo)
                    .HasForeignKey(d => d.Owner)
                    .HasConstraintName("fk_user_number");
            });

            modelBuilder.Entity<PanelInfo>(entity =>
            {
                entity.ToTable("PanelInfo", "sms");

                entity.HasComment("اطلاعات مربوط به پنل های پایانک");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreateDate).HasColumnName("createDate");

                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.Property(e => e.HasForm).HasColumnName("hasForm");

                entity.Property(e => e.HashId)
                    .HasColumnName("hashId")
                    .HasMaxLength(200);

                entity.Property(e => e.IsBlocked).HasColumnName("isBlocked");

                entity.Property(e => e.LastActivity).HasColumnName("lastActivity");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(500);

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasMaxLength(100);

                entity.Property(e => e.NumberId).HasColumnName("numberId");

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(200);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TemplateId).HasColumnName("templateId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PanelVersionInfo>(entity =>
            {
                entity.ToTable("PanelVersionInfo", "sms");

                entity.HasComment("اطلاعات ورژن پنل ها");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreateDate).HasColumnName("createDate");

                entity.Property(e => e.MaxVersion)
                    .HasColumnName("maxVersion")
                    .HasColumnType("numeric(20,2)");

                entity.Property(e => e.MinVersion)
                    .HasColumnName("minVersion")
                    .HasColumnType("numeric(20,2)");

                entity.Property(e => e.NickName)
                    .HasColumnName("nickName")
                    .HasMaxLength(200);

                entity.Property(e => e.Path).HasColumnName("path");
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.ToTable("Permissions", "um");

                entity.HasComment("Site Permissions");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Parent).HasColumnName("parent");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PersonalInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PersonalInfo_pkey");

                entity.ToTable("PersonalInfo", "um");

                entity.HasComment("User Personal Information");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.BirthdayChangeCounter).HasColumnName("birthdayChangeCounter");

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IsBirthdayChanged).HasColumnName("isBirthdayChanged");

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(100);

                entity.Property(e => e.NationalCode)
                    .HasColumnName("nationalCode")
                    .HasMaxLength(10);

                entity.Property(e => e.NickName)
                    .HasColumnName("nickName")
                    .HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.PersonalInfo)
                    .HasForeignKey<PersonalInfo>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("USER_ID_FK3");
            });

            modelBuilder.Entity<PersonalTemplate>(entity =>
            {
                entity.ToTable("PersonalTemplate", "sms");

                entity.HasComment("تمپلیت های ساخته شده توسط کاربر برای ارسال پیام");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Body).HasColumnName("body");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PersonalTemplate)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_user_sms_template");
            });

            modelBuilder.Entity<ReceiveInfo>(entity =>
            {
                entity.ToTable("ReceiveInfo", "sms");

                entity.HasComment("لیست پیام های دریافتی");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Body).HasColumnName("body");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.MsgId)
                    .HasColumnName("msgId")
                    .HasMaxLength(20);

                entity.Property(e => e.Receiver)
                    .HasColumnName("receiver")
                    .HasMaxLength(20);

                entity.Property(e => e.Sender)
                    .HasColumnName("sender")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<RolePermissions>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PermissionId })
                    .HasName("RolePermissions_pkey");

                entity.ToTable("RolePermissions", "um");

                entity.HasComment("Role Permissions");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.PermissionId).HasColumnName("permissionId");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PERMISSION_ID_FK_ROLEPERMISSION");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ROLE_ID_FK_ROLEPERMISSION");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("Roles", "um");

                entity.HasComment("Site Roles");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CanDelete).HasColumnName("canDelete");

                entity.Property(e => e.CanEdit).HasColumnName("canEdit");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(500);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<ScheduleSmsDetail>(entity =>
            {
                entity.ToTable("ScheduleSmsDetail", "sms");

                entity.HasComment("اطلاعات کاربران یک ارسال زماندار");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Counter).HasColumnName("counter");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.Property(e => e.UpdatedDate).HasColumnName("updatedDate");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<ScheduleSmsInfo>(entity =>
            {
                entity.ToTable("ScheduleSmsInfo", "sms");

                entity.HasComment("اطلاعات پیام زمان دار");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AddedDay).HasColumnName("addedDay");

                entity.Property(e => e.AddedMonth).HasColumnName("addedMonth");

                entity.Property(e => e.AddedYear).HasColumnName("addedYear");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.CreateDate).HasColumnName("createDate");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(500);

                entity.Property(e => e.NumberId).HasColumnName("numberId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TemplateId).HasColumnName("templateId");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<SentInfo>(entity =>
            {
                entity.ToTable("SentInfo", "sms");

                entity.HasComment("برا مشاهده پیام های ارسال شده");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Body).HasColumnName("body");

                entity.Property(e => e.CalculatedCount).HasColumnName("calculatedCount");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Deliveries).HasColumnName("deliveries");

                entity.Property(e => e.GroupIds).HasColumnName("groupIds");

                entity.Property(e => e.Header)
                    .HasColumnName("header")
                    .HasMaxLength(200);

                entity.Property(e => e.Kind)
                    .HasColumnName("kind")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Numbers).HasColumnName("numbers");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(20,2)");

                entity.Property(e => e.RectIds).HasColumnName("rectIds");

                entity.Property(e => e.SendNumber)
                    .HasColumnName("sendNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.SentDate).HasColumnName("sentDate");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.ToTable("Settings", "sms");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.FormKey)
                    .HasColumnName("formKey")
                    .HasMaxLength(50);

                entity.Property(e => e.FormMessage).HasColumnName("formMessage");

                entity.Property(e => e.LastRecivedSmsId).HasColumnName("lastRecivedSmsId");

                entity.Property(e => e.SmsDiscount)
                    .HasColumnName("smsDiscount")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SmsPrice)
                    .HasColumnName("smsPrice")
                    .HasColumnType("numeric(20,2)");
            });

            modelBuilder.Entity<TaskInfo>(entity =>
            {
                entity.ToTable("TaskInfo", "sms");

                entity.HasComment("اطلاعات تسک های کامل شده در سایت");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasMaxLength(50);

                entity.Property(e => e.Header).HasColumnName("header");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.Percent).HasColumnName("percent");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket", "um");

                entity.HasComment("لیست تیکت های ساخته شده");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreateDate).HasColumnName("createDate");

                entity.Property(e => e.Header)
                    .HasColumnName("header")
                    .HasMaxLength(500);

                entity.Property(e => e.Responder).HasColumnName("responder");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<TicketDetail>(entity =>
            {
                entity.ToTable("TicketDetail", "um");

                entity.HasComment("پیام های ارسال شده در تیکت");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body");

                entity.Property(e => e.SendDate).HasColumnName("sendDate");

                entity.Property(e => e.SenderId).HasColumnName("senderId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TicketId).HasColumnName("ticketId");
            });

            modelBuilder.Entity<UserGroups>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.GroupId })
                    .HasName("UserGroups_pkey");

                entity.ToTable("UserGroups", "um");

                entity.HasComment("Assigned groups to user");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserGroups_groupId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserGroups_userId_fkey");
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("UserRoles_pkey");

                entity.ToTable("UserRoles", "um");

                entity.HasComment("User Roles");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ROLE_ID_FK_USERROLE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("USER_ID_FK_USERROLE");
            });

            modelBuilder.Entity<VwBusinessCard>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vwBusinessCard", "sms");

                entity.HasComment("اطلاعات کامل کارت ویزیت");

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.CreateDate).HasColumnName("createDate");

                entity.Property(e => e.Credit)
                    .HasColumnName("credit")
                    .HasColumnType("numeric(20,2)");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(500);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.FormGuid).HasColumnName("formGuid");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.Property(e => e.GroupName)
                    .HasColumnName("groupName")
                    .HasMaxLength(100);

                entity.Property(e => e.GroupPicture).HasColumnName("groupPicture");

                entity.Property(e => e.GroupStatus).HasColumnName("groupStatus");

                entity.Property(e => e.GroupTitle)
                    .HasColumnName("groupTitle")
                    .HasMaxLength(500);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsBlocked).HasColumnName("isBlocked");

                entity.Property(e => e.IsFormValid).HasColumnName("isFormValid");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasMaxLength(500);

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(100);

                entity.Property(e => e.MobilePhone)
                    .HasColumnName("mobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.NickName)
                    .HasColumnName("nickName")
                    .HasMaxLength(100);

                entity.Property(e => e.NumberId).HasColumnName("numberId");

                entity.Property(e => e.NumberIsBlocked).HasColumnName("numberIsBlocked");

                entity.Property(e => e.NumberIsShared).HasColumnName("numberIsShared");

                entity.Property(e => e.NumberOwner).HasColumnName("numberOwner");

                entity.Property(e => e.NumberPassword)
                    .HasColumnName("numberPassword")
                    .HasMaxLength(100);

                entity.Property(e => e.NumberSend)
                    .HasColumnName("numberSend")
                    .HasMaxLength(50);

                entity.Property(e => e.NumberType).HasColumnName("numberType");

                entity.Property(e => e.NumberUsername)
                    .HasColumnName("numberUsername")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TemplateBody).HasColumnName("templateBody");

                entity.Property(e => e.TemplateId).HasColumnName("templateId");

                entity.Property(e => e.TemplateName)
                    .HasColumnName("templateName")
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<VwContact>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vwContact", "um");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(500);

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.Credit)
                    .HasColumnName("credit")
                    .HasColumnType("numeric(20,2)");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(500);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.FormGuid).HasColumnName("formGuid");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InstagramLink).HasColumnName("instagramLink");

                entity.Property(e => e.IsFormValid).HasColumnName("isFormValid");

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(100);

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.MobilePhone)
                    .HasColumnName("mobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.NickName)
                    .HasColumnName("nickName")
                    .HasMaxLength(100);

                entity.Property(e => e.Picture)
                    .HasColumnName("picture")
                    .HasMaxLength(1000);

                entity.Property(e => e.SpecialDate).HasColumnName("specialDate");

                entity.Property(e => e.TelegramLink).HasColumnName("telegramLink");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<VwContactGroups>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vwContactGroups", "um");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(500);

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.Descriptions).HasColumnName("descriptions");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(500);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.FormGuid).HasColumnName("formGuid");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InstagramLink).HasColumnName("instagramLink");

                entity.Property(e => e.IsFormValid).HasColumnName("isFormValid");

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(100);

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.MobilePhone)
                    .HasColumnName("mobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.NickName)
                    .HasColumnName("nickName")
                    .HasMaxLength(100);

                entity.Property(e => e.Owner).HasColumnName("owner");

                entity.Property(e => e.SpecialDate).HasColumnName("specialDate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TelegramLink).HasColumnName("telegramLink");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(500);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<VwNumber>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vwNumber", "sms");

                entity.HasComment("اطلاعات شماره به همراه مالک");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(500);

                entity.Property(e => e.CreateDate).HasColumnName("createDate");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(500);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.FormGuid).HasColumnName("formGuid");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsBlocked).HasColumnName("isBlocked");

                entity.Property(e => e.IsFormValid).HasColumnName("isFormValid");

                entity.Property(e => e.IsShared).HasColumnName("isShared");

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(100);

                entity.Property(e => e.MobilePhone)
                    .HasColumnName("mobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasMaxLength(50);

                entity.Property(e => e.Owner).HasColumnName("owner");

                entity.Property(e => e.OwnerUsername)
                    .HasColumnName("ownerUsername")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<VwPanel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vwPanel", "sms");

                entity.HasComment("اطلاعات کامل پنل ها");

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.CreateDate).HasColumnName("createDate");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(500);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.FormGuid).HasColumnName("formGuid");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.GroupDescription).HasColumnName("groupDescription");

                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.Property(e => e.GroupName)
                    .HasColumnName("groupName")
                    .HasMaxLength(100);

                entity.Property(e => e.GroupPicture).HasColumnName("groupPicture");

                entity.Property(e => e.GroupTitle)
                    .HasColumnName("groupTitle")
                    .HasMaxLength(500);

                entity.Property(e => e.HashId)
                    .HasColumnName("hashId")
                    .HasMaxLength(200);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsBlocked).HasColumnName("isBlocked");

                entity.Property(e => e.IsFormValid).HasColumnName("isFormValid");

                entity.Property(e => e.LastActivity).HasColumnName("lastActivity");

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(100);

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.MobilePhone)
                    .HasColumnName("mobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(500);

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasMaxLength(100);

                entity.Property(e => e.NumberId).HasColumnName("numberId");

                entity.Property(e => e.SendIsBlocked).HasColumnName("sendIsBlocked");

                entity.Property(e => e.SendIsShared).HasColumnName("sendIsShared");

                entity.Property(e => e.SendNumber)
                    .HasColumnName("sendNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.SendPassword)
                    .HasColumnName("sendPassword")
                    .HasMaxLength(100);

                entity.Property(e => e.SendType).HasColumnName("sendType");

                entity.Property(e => e.SendUsername)
                    .HasColumnName("sendUsername")
                    .HasMaxLength(100);

                entity.Property(e => e.Serial)
                    .HasColumnName("serial")
                    .HasMaxLength(200);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TemplateBody).HasColumnName("templateBody");

                entity.Property(e => e.TemplateId).HasColumnName("templateId");

                entity.Property(e => e.TemplateName)
                    .HasColumnName("templateName")
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100);

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<VwScheduleSms>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vwScheduleSms", "sms");

                entity.HasComment("اطلاعات کامل ارسال زماندار");

                entity.Property(e => e.AddedDay).HasColumnName("addedDay");

                entity.Property(e => e.AddedMonth).HasColumnName("addedMonth");

                entity.Property(e => e.AddedYear).HasColumnName("addedYear");

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.Counter).HasColumnName("counter");

                entity.Property(e => e.Credit)
                    .HasColumnName("credit")
                    .HasColumnType("numeric(20,2)");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(500);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(100);

                entity.Property(e => e.MobilePhone)
                    .HasColumnName("mobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.NickName)
                    .HasColumnName("nickName")
                    .HasMaxLength(100);

                entity.Property(e => e.NumberId).HasColumnName("numberId");

                entity.Property(e => e.OwnerEmail)
                    .HasColumnName("ownerEmail")
                    .HasMaxLength(500);

                entity.Property(e => e.OwnerFirstName)
                    .HasColumnName("ownerFirstName")
                    .HasMaxLength(50);

                entity.Property(e => e.OwnerGender).HasColumnName("ownerGender");

                entity.Property(e => e.OwnerId).HasColumnName("ownerId");

                entity.Property(e => e.OwnerLastName)
                    .HasColumnName("ownerLastName")
                    .HasMaxLength(100);

                entity.Property(e => e.OwnerMobilePhone)
                    .HasColumnName("ownerMobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.OwnerUsername)
                    .HasColumnName("ownerUsername")
                    .HasMaxLength(100);

                entity.Property(e => e.ParentCode).HasColumnName("parentCode");

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.Property(e => e.ParentName)
                    .HasColumnName("parentName")
                    .HasMaxLength(500);

                entity.Property(e => e.SendIsBlocked).HasColumnName("sendIsBlocked");

                entity.Property(e => e.SendIsShared).HasColumnName("sendIsShared");

                entity.Property(e => e.SendNumber)
                    .HasColumnName("sendNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.SendPassword)
                    .HasColumnName("sendPassword")
                    .HasMaxLength(100);

                entity.Property(e => e.SendType).HasColumnName("sendType");

                entity.Property(e => e.SendUsername)
                    .HasColumnName("sendUsername")
                    .HasMaxLength(100);

                entity.Property(e => e.SpecialDate).HasColumnName("specialDate");

                entity.Property(e => e.TemplateId).HasColumnName("templateId");

                entity.Property(e => e.UpdatedDate).HasColumnName("updatedDate");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<VwScheduleSmsInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vwScheduleSmsInfo", "sms");

                entity.Property(e => e.AddedDay).HasColumnName("addedDay");

                entity.Property(e => e.AddedMonth).HasColumnName("addedMonth");

                entity.Property(e => e.AddedYear).HasColumnName("addedYear");

                entity.Property(e => e.Credit)
                    .HasColumnName("credit")
                    .HasColumnType("numeric(20,2)");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.NumberId).HasColumnName("numberId");

                entity.Property(e => e.OwnerEmail)
                    .HasColumnName("ownerEmail")
                    .HasMaxLength(500);

                entity.Property(e => e.OwnerFirstName)
                    .HasColumnName("ownerFirstName")
                    .HasMaxLength(50);

                entity.Property(e => e.OwnerGender).HasColumnName("ownerGender");

                entity.Property(e => e.OwnerLastName)
                    .HasColumnName("ownerLastName")
                    .HasMaxLength(100);

                entity.Property(e => e.OwnerMobilePhone)
                    .HasColumnName("ownerMobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.OwnerUsername)
                    .HasColumnName("ownerUsername")
                    .HasMaxLength(100);

                entity.Property(e => e.SendIsBlocked).HasColumnName("sendIsBlocked");

                entity.Property(e => e.SendIsShared).HasColumnName("sendIsShared");

                entity.Property(e => e.SendNumber)
                    .HasColumnName("sendNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.SendPassword)
                    .HasColumnName("sendPassword")
                    .HasMaxLength(100);

                entity.Property(e => e.SendType).HasColumnName("sendType");

                entity.Property(e => e.SendUsername)
                    .HasColumnName("sendUsername")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TemplateBody).HasColumnName("templateBody");

                entity.Property(e => e.TemplateId).HasColumnName("templateId");

                entity.Property(e => e.TemplateName)
                    .HasColumnName("templateName")
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<VwTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vwTask", "sms");

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(500);

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Header).HasColumnName("header");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasMaxLength(100);

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.MobilePhone)
                    .HasColumnName("mobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.Percent).HasColumnName("percent");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<VwTicket>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vwTicket", "um");

                entity.HasComment("اطلاعات کامل تیکت");

                entity.Property(e => e.CreateDate).HasColumnName("createDate");

                entity.Property(e => e.Header)
                    .HasColumnName("header")
                    .HasMaxLength(500);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OwnerEmail)
                    .HasColumnName("ownerEmail")
                    .HasMaxLength(500);

                entity.Property(e => e.OwnerFirstName)
                    .HasColumnName("ownerFirstName")
                    .HasMaxLength(50);

                entity.Property(e => e.OwnerGender).HasColumnName("ownerGender");

                entity.Property(e => e.OwnerLastName)
                    .HasColumnName("ownerLastName")
                    .HasMaxLength(100);

                entity.Property(e => e.OwnerMobilePhone)
                    .HasColumnName("ownerMobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.OwnerPicture)
                    .HasColumnName("ownerPicture")
                    .HasMaxLength(1000);

                entity.Property(e => e.OwnerUsername)
                    .HasColumnName("ownerUsername")
                    .HasMaxLength(100);

                entity.Property(e => e.Responder).HasColumnName("responder");

                entity.Property(e => e.ResponderEmail)
                    .HasColumnName("responderEmail")
                    .HasMaxLength(500);

                entity.Property(e => e.ResponderFirstName)
                    .HasColumnName("responderFirstName")
                    .HasMaxLength(50);

                entity.Property(e => e.ResponderGender).HasColumnName("responderGender");

                entity.Property(e => e.ResponderLastName)
                    .HasColumnName("responderLastName")
                    .HasMaxLength(100);

                entity.Property(e => e.ResponderMobilePhone)
                    .HasColumnName("responderMobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.ResponderPicture)
                    .HasColumnName("responderPicture")
                    .HasMaxLength(1000);

                entity.Property(e => e.ResponderUsername)
                    .HasColumnName("responderUsername")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<VwTicketDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vwTicketDetail", "um");

                entity.HasComment("اطلاعات کامل پیام تیکت");

                entity.Property(e => e.Body).HasColumnName("body");

                entity.Property(e => e.Header)
                    .HasColumnName("header")
                    .HasMaxLength(500);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OwnerEmail)
                    .HasColumnName("ownerEmail")
                    .HasMaxLength(500);

                entity.Property(e => e.OwnerFirstName)
                    .HasColumnName("ownerFirstName")
                    .HasMaxLength(50);

                entity.Property(e => e.OwnerGender).HasColumnName("ownerGender");

                entity.Property(e => e.OwnerLastName)
                    .HasColumnName("ownerLastName")
                    .HasMaxLength(100);

                entity.Property(e => e.OwnerMobilePhone)
                    .HasColumnName("ownerMobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.OwnerPicture)
                    .HasColumnName("ownerPicture")
                    .HasMaxLength(1000);

                entity.Property(e => e.OwnerUsername)
                    .HasColumnName("ownerUsername")
                    .HasMaxLength(100);

                entity.Property(e => e.SendDate).HasColumnName("sendDate");

                entity.Property(e => e.SenderEmail)
                    .HasColumnName("senderEmail")
                    .HasMaxLength(500);

                entity.Property(e => e.SenderFirstName)
                    .HasColumnName("senderFirstName")
                    .HasMaxLength(50);

                entity.Property(e => e.SenderGender).HasColumnName("senderGender");

                entity.Property(e => e.SenderId).HasColumnName("senderId");

                entity.Property(e => e.SenderLastName)
                    .HasColumnName("senderLastName")
                    .HasMaxLength(100);

                entity.Property(e => e.SenderMobilePhone)
                    .HasColumnName("senderMobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.SenderPicture)
                    .HasColumnName("senderPicture")
                    .HasMaxLength(1000);

                entity.Property(e => e.SenderUsername)
                    .HasColumnName("senderUsername")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TicketId).HasColumnName("ticketId");

                entity.Property(e => e.TicketStatus).HasColumnName("ticketStatus");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<VwTicketLastMessage>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vwTicketLastMessage", "um");

                entity.Property(e => e.Body).HasColumnName("body");

                entity.Property(e => e.CreateDate).HasColumnName("createDate");

                entity.Property(e => e.Header)
                    .HasColumnName("header")
                    .HasMaxLength(500);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MessageStatus).HasColumnName("messageStatus");

                entity.Property(e => e.OwnerEmail)
                    .HasColumnName("ownerEmail")
                    .HasMaxLength(500);

                entity.Property(e => e.OwnerFirstName)
                    .HasColumnName("ownerFirstName")
                    .HasMaxLength(50);

                entity.Property(e => e.OwnerGender).HasColumnName("ownerGender");

                entity.Property(e => e.OwnerLastName)
                    .HasColumnName("ownerLastName")
                    .HasMaxLength(100);

                entity.Property(e => e.OwnerMobilePhone)
                    .HasColumnName("ownerMobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.OwnerPicture)
                    .HasColumnName("ownerPicture")
                    .HasMaxLength(1000);

                entity.Property(e => e.OwnerUsername)
                    .HasColumnName("ownerUsername")
                    .HasMaxLength(100);

                entity.Property(e => e.Responder).HasColumnName("responder");

                entity.Property(e => e.ResponderEmail)
                    .HasColumnName("responderEmail")
                    .HasMaxLength(500);

                entity.Property(e => e.ResponderFirstName)
                    .HasColumnName("responderFirstName")
                    .HasMaxLength(50);

                entity.Property(e => e.ResponderGender).HasColumnName("responderGender");

                entity.Property(e => e.ResponderLastName)
                    .HasColumnName("responderLastName")
                    .HasMaxLength(100);

                entity.Property(e => e.ResponderMobilePhone)
                    .HasColumnName("responderMobilePhone")
                    .HasMaxLength(15);

                entity.Property(e => e.ResponderPicture)
                    .HasColumnName("responderPicture")
                    .HasMaxLength(1000);

                entity.Property(e => e.ResponderUsername)
                    .HasColumnName("responderUsername")
                    .HasMaxLength(100);

                entity.Property(e => e.SendDate).HasColumnName("sendDate");

                entity.Property(e => e.SenderId).HasColumnName("senderId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TicketId).HasColumnName("ticketId");

                entity.Property(e => e.Unread).HasColumnName("unread");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
