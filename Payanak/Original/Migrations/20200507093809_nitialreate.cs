using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Backend.Migrations
{
    public partial class nitialreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "um");

            migrationBuilder.EnsureSchema(
                name: "sms");

            migrationBuilder.CreateTable(
                name: "CalendarEvents99",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateDay = table.Column<int>(nullable: false),
                    MonthId = table.Column<int>(nullable: false),
                    DayName = table.Column<string>(nullable: true),
                    MonthName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsClose = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvents99", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessCard",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    userId = table.Column<long>(nullable: true),
                    createDate = table.Column<DateTime>(nullable: true),
                    groupId = table.Column<long>(nullable: true),
                    isBlocked = table.Column<bool>(nullable: true),
                    status = table.Column<short>(nullable: true),
                    templateId = table.Column<long>(nullable: true),
                    numberId = table.Column<long>(nullable: true),
                    key = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCard", x => x.id);
                },
                comment: "اطلاعات مربوط به کارت ویزیت");

            migrationBuilder.CreateTable(
                name: "PanelInfo",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    userId = table.Column<long>(nullable: true),
                    createDate = table.Column<DateTime>(nullable: true),
                    lastActivity = table.Column<DateTime>(nullable: true),
                    version = table.Column<string>(maxLength: 100, nullable: true),
                    number = table.Column<string>(maxLength: 100, nullable: true),
                    serial = table.Column<string>(maxLength: 200, nullable: true),
                    hashId = table.Column<string>(maxLength: 200, nullable: true),
                    name = table.Column<string>(maxLength: 500, nullable: true),
                    groupId = table.Column<long>(nullable: true),
                    isBlocked = table.Column<bool>(nullable: true),
                    status = table.Column<short>(nullable: true),
                    templateId = table.Column<long>(nullable: true),
                    numberId = table.Column<long>(nullable: true),
                    hasForm = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanelInfo", x => x.id);
                },
                comment: "اطلاعات مربوط به پنل های پایانک");

            migrationBuilder.CreateTable(
                name: "PanelVersionInfo",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    createDate = table.Column<DateTime>(nullable: true),
                    nickName = table.Column<string>(maxLength: 200, nullable: true),
                    path = table.Column<string>(nullable: true),
                    minVersion = table.Column<decimal>(type: "numeric(20,2)", nullable: true),
                    maxVersion = table.Column<decimal>(type: "numeric(20,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanelVersionInfo", x => x.id);
                },
                comment: "اطلاعات ورژن پنل ها");

            migrationBuilder.CreateTable(
                name: "ReceiveInfo",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    sender = table.Column<string>(maxLength: 20, nullable: true),
                    receiver = table.Column<string>(maxLength: 20, nullable: true),
                    date = table.Column<DateTime>(nullable: true),
                    msgId = table.Column<string>(maxLength: 20, nullable: true),
                    body = table.Column<string>(nullable: true),
                    count = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveInfo", x => x.id);
                },
                comment: "لیست پیام های دریافتی");

            migrationBuilder.CreateTable(
                name: "ScheduleSmsDetail",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    parentId = table.Column<long>(nullable: true),
                    userId = table.Column<long>(nullable: true),
                    date = table.Column<DateTime>(nullable: true),
                    updatedDate = table.Column<DateTime>(nullable: true),
                    counter = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleSmsDetail", x => x.id);
                },
                comment: "اطلاعات کاربران یک ارسال زماندار");

            migrationBuilder.CreateTable(
                name: "ScheduleSmsInfo",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(maxLength: 500, nullable: true),
                    code = table.Column<long>(nullable: true),
                    userId = table.Column<long>(nullable: true),
                    addedYear = table.Column<int>(nullable: true),
                    addedMonth = table.Column<int>(nullable: true),
                    addedDay = table.Column<int>(nullable: true),
                    status = table.Column<short>(nullable: true),
                    createDate = table.Column<DateTime>(nullable: true),
                    templateId = table.Column<long>(nullable: true),
                    numberId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleSmsInfo", x => x.id);
                },
                comment: "اطلاعات پیام زمان دار");

            migrationBuilder.CreateTable(
                name: "SentInfo",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    numbers = table.Column<string>(nullable: true),
                    groupIds = table.Column<string>(nullable: true),
                    kind = table.Column<int>(nullable: true, defaultValueSql: "0"),
                    status = table.Column<short>(nullable: true, defaultValueSql: "0"),
                    deliveries = table.Column<string>(nullable: true),
                    sendNumber = table.Column<string>(maxLength: 50, nullable: true),
                    userId = table.Column<long>(nullable: true),
                    header = table.Column<string>(maxLength: 200, nullable: true),
                    body = table.Column<string>(nullable: true),
                    sentDate = table.Column<DateTime>(nullable: true),
                    rectIds = table.Column<string>(nullable: true),
                    count = table.Column<long>(nullable: true),
                    calculatedCount = table.Column<long>(nullable: true),
                    price = table.Column<decimal>(type: "numeric(20,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentInfo", x => x.id);
                },
                comment: "برا مشاهده پیام های ارسال شده");

            migrationBuilder.CreateTable(
                name: "Settings",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    lastRecivedSmsId = table.Column<long>(nullable: true),
                    smsPrice = table.Column<decimal>(type: "numeric(20,2)", nullable: true),
                    smsDiscount = table.Column<short>(nullable: true, defaultValueSql: "0"),
                    formKey = table.Column<string>(maxLength: 50, nullable: true),
                    formMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TaskInfo",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    userId = table.Column<long>(nullable: true),
                    percent = table.Column<int>(nullable: true),
                    message = table.Column<string>(nullable: true),
                    status = table.Column<short>(nullable: true),
                    header = table.Column<string>(nullable: true),
                    guid = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskInfo", x => x.id);
                },
                comment: "اطلاعات تسک های کامل شده در سایت");

            migrationBuilder.CreateTable(
                name: "vwBusinessCard",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: true),
                    createDate = table.Column<DateTime>(nullable: true),
                    isBlocked = table.Column<bool>(nullable: true),
                    status = table.Column<short>(nullable: true),
                    userId = table.Column<long>(nullable: true),
                    username = table.Column<string>(maxLength: 100, nullable: true),
                    email = table.Column<string>(maxLength: 500, nullable: true),
                    mobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    firstName = table.Column<string>(maxLength: 50, nullable: true),
                    lastName = table.Column<string>(maxLength: 100, nullable: true),
                    nickName = table.Column<string>(maxLength: 100, nullable: true),
                    birthday = table.Column<DateTime>(nullable: true),
                    gender = table.Column<short>(nullable: true),
                    discount = table.Column<short>(nullable: true),
                    credit = table.Column<decimal>(type: "numeric(20,2)", nullable: true),
                    groupId = table.Column<long>(nullable: true),
                    groupName = table.Column<string>(maxLength: 100, nullable: true),
                    groupTitle = table.Column<string>(maxLength: 500, nullable: true),
                    groupStatus = table.Column<short>(nullable: true),
                    groupPicture = table.Column<string>(nullable: true),
                    templateId = table.Column<long>(nullable: true),
                    templateName = table.Column<string>(maxLength: 200, nullable: true),
                    templateBody = table.Column<string>(nullable: true),
                    numberId = table.Column<long>(nullable: true),
                    numberSend = table.Column<string>(maxLength: 50, nullable: true),
                    numberIsShared = table.Column<bool>(nullable: true),
                    numberIsBlocked = table.Column<bool>(nullable: true),
                    numberOwner = table.Column<long>(nullable: true),
                    numberType = table.Column<short>(nullable: true),
                    numberUsername = table.Column<string>(maxLength: 100, nullable: true),
                    numberPassword = table.Column<string>(maxLength: 100, nullable: true),
                    key = table.Column<string>(maxLength: 500, nullable: true),
                    formGuid = table.Column<string>(nullable: true),
                    isFormValid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                },
                comment: "اطلاعات کامل کارت ویزیت");

            migrationBuilder.CreateTable(
                name: "vwNumber",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: true),
                    number = table.Column<string>(maxLength: 50, nullable: true),
                    isShared = table.Column<bool>(nullable: true),
                    isBlocked = table.Column<bool>(nullable: true),
                    owner = table.Column<long>(nullable: true),
                    type = table.Column<short>(nullable: true),
                    username = table.Column<string>(maxLength: 100, nullable: true),
                    password = table.Column<string>(maxLength: 100, nullable: true),
                    createDate = table.Column<DateTime>(nullable: true),
                    ownerUsername = table.Column<string>(maxLength: 100, nullable: true),
                    email = table.Column<string>(maxLength: 500, nullable: true),
                    mobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    firstName = table.Column<string>(maxLength: 50, nullable: true),
                    lastName = table.Column<string>(maxLength: 100, nullable: true),
                    gender = table.Column<short>(nullable: true),
                    address = table.Column<string>(maxLength: 500, nullable: true),
                    formGuid = table.Column<string>(nullable: true),
                    isFormValid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                },
                comment: "اطلاعات شماره به همراه مالک");

            migrationBuilder.CreateTable(
                name: "vwPanel",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: true),
                    createDate = table.Column<DateTime>(nullable: true),
                    lastActivity = table.Column<DateTime>(nullable: true),
                    version = table.Column<string>(maxLength: 100, nullable: true),
                    number = table.Column<string>(maxLength: 100, nullable: true),
                    serial = table.Column<string>(maxLength: 200, nullable: true),
                    name = table.Column<string>(maxLength: 500, nullable: true),
                    userId = table.Column<long>(nullable: true),
                    username = table.Column<string>(maxLength: 100, nullable: true),
                    email = table.Column<string>(maxLength: 500, nullable: true),
                    mobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    firstName = table.Column<string>(maxLength: 50, nullable: true),
                    lastName = table.Column<string>(maxLength: 100, nullable: true),
                    gender = table.Column<short>(nullable: true),
                    birthday = table.Column<DateTime>(nullable: true),
                    longitude = table.Column<double>(nullable: true),
                    latitude = table.Column<double>(nullable: true),
                    groupName = table.Column<string>(maxLength: 100, nullable: true),
                    groupTitle = table.Column<string>(maxLength: 500, nullable: true),
                    groupDescription = table.Column<string>(nullable: true),
                    groupId = table.Column<long>(nullable: true),
                    groupPicture = table.Column<string>(nullable: true),
                    hashId = table.Column<string>(maxLength: 200, nullable: true),
                    isBlocked = table.Column<bool>(nullable: true),
                    status = table.Column<short>(nullable: true),
                    templateId = table.Column<long>(nullable: true),
                    templateName = table.Column<string>(maxLength: 200, nullable: true),
                    templateBody = table.Column<string>(nullable: true),
                    numberId = table.Column<long>(nullable: true),
                    sendNumber = table.Column<string>(maxLength: 50, nullable: true),
                    sendIsShared = table.Column<bool>(nullable: true),
                    sendIsBlocked = table.Column<bool>(nullable: true),
                    sendType = table.Column<short>(nullable: true),
                    sendUsername = table.Column<string>(maxLength: 100, nullable: true),
                    sendPassword = table.Column<string>(maxLength: 100, nullable: true),
                    formGuid = table.Column<string>(nullable: true),
                    isFormValid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                },
                comment: "اطلاعات کامل پنل ها");

            migrationBuilder.CreateTable(
                name: "vwScheduleSms",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: true),
                    parentId = table.Column<long>(nullable: true),
                    userId = table.Column<long>(nullable: true),
                    date = table.Column<DateTime>(nullable: true),
                    updatedDate = table.Column<DateTime>(nullable: true),
                    counter = table.Column<long>(nullable: true),
                    parentName = table.Column<string>(maxLength: 500, nullable: true),
                    parentCode = table.Column<long>(nullable: true),
                    addedYear = table.Column<int>(nullable: true),
                    addedMonth = table.Column<int>(nullable: true),
                    addedDay = table.Column<int>(nullable: true),
                    ownerUsername = table.Column<string>(maxLength: 100, nullable: true),
                    ownerEmail = table.Column<string>(maxLength: 500, nullable: true),
                    ownerMobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    ownerFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    ownerLastName = table.Column<string>(maxLength: 100, nullable: true),
                    ownerGender = table.Column<short>(nullable: true),
                    username = table.Column<string>(maxLength: 100, nullable: true),
                    email = table.Column<string>(maxLength: 500, nullable: true),
                    mobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    firstName = table.Column<string>(maxLength: 50, nullable: true),
                    lastName = table.Column<string>(maxLength: 100, nullable: true),
                    nickName = table.Column<string>(maxLength: 100, nullable: true),
                    birthday = table.Column<DateTime>(nullable: true),
                    specialDate = table.Column<DateTime>(nullable: true),
                    gender = table.Column<short>(nullable: true),
                    discount = table.Column<short>(nullable: true),
                    credit = table.Column<decimal>(type: "numeric(20,2)", nullable: true),
                    ownerId = table.Column<long>(nullable: true),
                    templateId = table.Column<long>(nullable: true),
                    numberId = table.Column<long>(nullable: true),
                    sendNumber = table.Column<string>(maxLength: 50, nullable: true),
                    sendIsShared = table.Column<bool>(nullable: true),
                    sendIsBlocked = table.Column<bool>(nullable: true),
                    sendType = table.Column<short>(nullable: true),
                    sendUsername = table.Column<string>(maxLength: 100, nullable: true),
                    sendPassword = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                },
                comment: "اطلاعات کامل ارسال زماندار");

            migrationBuilder.CreateTable(
                name: "vwScheduleSmsInfo",
                schema: "sms",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Code = table.Column<long>(nullable: true),
                    addedYear = table.Column<int>(nullable: true),
                    addedMonth = table.Column<int>(nullable: true),
                    addedDay = table.Column<int>(nullable: true),
                    ownerUsername = table.Column<string>(maxLength: 100, nullable: true),
                    ownerEmail = table.Column<string>(maxLength: 500, nullable: true),
                    ownerMobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    ownerFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    ownerLastName = table.Column<string>(maxLength: 100, nullable: true),
                    discount = table.Column<short>(nullable: true),
                    credit = table.Column<decimal>(type: "numeric(20,2)", nullable: true),
                    id = table.Column<long>(nullable: true),
                    userId = table.Column<long>(nullable: true),
                    ownerGender = table.Column<short>(nullable: true),
                    status = table.Column<short>(nullable: true),
                    templateId = table.Column<long>(nullable: true),
                    templateName = table.Column<string>(maxLength: 200, nullable: true),
                    templateBody = table.Column<string>(nullable: true),
                    numberId = table.Column<long>(nullable: true),
                    sendNumber = table.Column<string>(maxLength: 50, nullable: true),
                    sendIsShared = table.Column<bool>(nullable: true),
                    sendIsBlocked = table.Column<bool>(nullable: true),
                    sendType = table.Column<short>(nullable: true),
                    sendUsername = table.Column<string>(maxLength: 100, nullable: true),
                    sendPassword = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vwTask",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: true),
                    userId = table.Column<long>(nullable: true),
                    percent = table.Column<int>(nullable: true),
                    message = table.Column<string>(nullable: true),
                    status = table.Column<short>(nullable: true),
                    header = table.Column<string>(nullable: true),
                    username = table.Column<string>(maxLength: 100, nullable: true),
                    email = table.Column<string>(maxLength: 500, nullable: true),
                    mobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    firstName = table.Column<string>(maxLength: 50, nullable: true),
                    lastName = table.Column<string>(maxLength: 100, nullable: true),
                    gender = table.Column<short>(nullable: true),
                    birthday = table.Column<DateTime>(nullable: true),
                    longitude = table.Column<double>(nullable: true),
                    latitude = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "AccountInfo",
                schema: "um",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    username = table.Column<string>(maxLength: 100, nullable: false),
                    password = table.Column<string>(maxLength: 500, nullable: false),
                    email = table.Column<string>(maxLength: 500, nullable: true),
                    mobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    homePhone = table.Column<string>(maxLength: 15, nullable: true),
                    businessPhone = table.Column<string>(maxLength: 15, nullable: true),
                    createDate = table.Column<DateTime>(nullable: false),
                    lastLogin = table.Column<DateTime>(nullable: false),
                    picture = table.Column<string>(maxLength: 1000, nullable: true),
                    formGuid = table.Column<string>(nullable: true),
                    formDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfo", x => x.id);
                },
                comment: "Default Table For User Login Info");

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "um",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(maxLength: 100, nullable: true),
                    title = table.Column<string>(maxLength: 100, nullable: true),
                    level = table.Column<short>(nullable: true),
                    parent = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.id);
                },
                comment: "Site Permissions");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "um",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(maxLength: 500, nullable: true),
                    title = table.Column<string>(maxLength: 500, nullable: true),
                    canEdit = table.Column<bool>(nullable: true),
                    canDelete = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                },
                comment: "Site Roles");

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "um",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    createDate = table.Column<DateTime>(nullable: false),
                    userId = table.Column<long>(nullable: false),
                    responder = table.Column<long>(nullable: true),
                    status = table.Column<short>(nullable: false),
                    header = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.id);
                },
                comment: "لیست تیکت های ساخته شده");

            migrationBuilder.CreateTable(
                name: "TicketDetail",
                schema: "um",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    senderId = table.Column<long>(nullable: false),
                    ticketId = table.Column<long>(nullable: false),
                    body = table.Column<string>(nullable: false),
                    sendDate = table.Column<DateTime>(nullable: false),
                    status = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDetail", x => x.id);
                },
                comment: "پیام های ارسال شده در تیکت");

            migrationBuilder.CreateTable(
                name: "vwContact",
                schema: "um",
                columns: table => new
                {
                    id = table.Column<long>(nullable: true),
                    username = table.Column<string>(maxLength: 100, nullable: true),
                    email = table.Column<string>(maxLength: 500, nullable: true),
                    mobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    specialDate = table.Column<DateTime>(nullable: true),
                    instagramLink = table.Column<string>(nullable: true),
                    telegramLink = table.Column<string>(nullable: true),
                    firstName = table.Column<string>(maxLength: 50, nullable: true),
                    lastName = table.Column<string>(maxLength: 100, nullable: true),
                    nickName = table.Column<string>(maxLength: 100, nullable: true),
                    birthday = table.Column<DateTime>(nullable: true),
                    gender = table.Column<short>(nullable: true),
                    latitude = table.Column<double>(nullable: true),
                    longitude = table.Column<double>(nullable: true),
                    address = table.Column<string>(maxLength: 500, nullable: true),
                    discount = table.Column<short>(nullable: true),
                    credit = table.Column<decimal>(type: "numeric(20,2)", nullable: true),
                    formGuid = table.Column<string>(nullable: true),
                    isFormValid = table.Column<int>(nullable: true),
                    picture = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vwContactGroups",
                schema: "um",
                columns: table => new
                {
                    id = table.Column<long>(nullable: true),
                    username = table.Column<string>(maxLength: 100, nullable: true),
                    email = table.Column<string>(maxLength: 500, nullable: true),
                    mobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    specialDate = table.Column<DateTime>(nullable: true),
                    instagramLink = table.Column<string>(nullable: true),
                    telegramLink = table.Column<string>(nullable: true),
                    firstName = table.Column<string>(maxLength: 50, nullable: true),
                    lastName = table.Column<string>(maxLength: 100, nullable: true),
                    nickName = table.Column<string>(maxLength: 100, nullable: true),
                    birthday = table.Column<DateTime>(nullable: true),
                    gender = table.Column<short>(nullable: true),
                    latitude = table.Column<double>(nullable: true),
                    longitude = table.Column<double>(nullable: true),
                    address = table.Column<string>(maxLength: 500, nullable: true),
                    name = table.Column<string>(maxLength: 100, nullable: true),
                    title = table.Column<string>(maxLength: 500, nullable: true),
                    descriptions = table.Column<string>(nullable: true),
                    owner = table.Column<long>(nullable: true),
                    status = table.Column<short>(nullable: true),
                    groupId = table.Column<long>(nullable: true),
                    formGuid = table.Column<string>(nullable: true),
                    isFormValid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vwTicket",
                schema: "um",
                columns: table => new
                {
                    id = table.Column<long>(nullable: true),
                    responder = table.Column<long>(nullable: true),
                    userId = table.Column<long>(nullable: true),
                    createDate = table.Column<DateTime>(nullable: true),
                    status = table.Column<short>(nullable: true),
                    responderUsername = table.Column<string>(maxLength: 100, nullable: true),
                    responderEmail = table.Column<string>(maxLength: 500, nullable: true),
                    responderMobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    responderFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    responderLastName = table.Column<string>(maxLength: 100, nullable: true),
                    responderGender = table.Column<short>(nullable: true),
                    responderPicture = table.Column<string>(maxLength: 1000, nullable: true),
                    ownerUsername = table.Column<string>(maxLength: 100, nullable: true),
                    ownerEmail = table.Column<string>(maxLength: 500, nullable: true),
                    ownerMobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    ownerFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    ownerLastName = table.Column<string>(maxLength: 100, nullable: true),
                    ownerGender = table.Column<short>(nullable: true),
                    ownerPicture = table.Column<string>(maxLength: 1000, nullable: true),
                    header = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                },
                comment: "اطلاعات کامل تیکت");

            migrationBuilder.CreateTable(
                name: "vwTicketDetail",
                schema: "um",
                columns: table => new
                {
                    id = table.Column<long>(nullable: true),
                    senderId = table.Column<long>(nullable: true),
                    ticketId = table.Column<long>(nullable: true),
                    body = table.Column<string>(nullable: true),
                    sendDate = table.Column<DateTime>(nullable: true),
                    status = table.Column<short>(nullable: true),
                    senderUsername = table.Column<string>(maxLength: 100, nullable: true),
                    senderEmail = table.Column<string>(maxLength: 500, nullable: true),
                    senderMobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    senderFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    senderLastName = table.Column<string>(maxLength: 100, nullable: true),
                    senderGender = table.Column<short>(nullable: true),
                    userId = table.Column<long>(nullable: true),
                    ticketStatus = table.Column<short>(nullable: true),
                    ownerUsername = table.Column<string>(maxLength: 100, nullable: true),
                    ownerEmail = table.Column<string>(maxLength: 500, nullable: true),
                    ownerMobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    ownerFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    ownerLastName = table.Column<string>(maxLength: 100, nullable: true),
                    ownerGender = table.Column<short>(nullable: true),
                    ownerPicture = table.Column<string>(maxLength: 1000, nullable: true),
                    header = table.Column<string>(maxLength: 500, nullable: true),
                    senderPicture = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                },
                comment: "اطلاعات کامل پیام تیکت");

            migrationBuilder.CreateTable(
                name: "vwTicketLastMessage",
                schema: "um",
                columns: table => new
                {
                    id = table.Column<long>(nullable: true),
                    senderId = table.Column<long>(nullable: true),
                    ticketId = table.Column<long>(nullable: true),
                    body = table.Column<string>(nullable: true),
                    sendDate = table.Column<DateTime>(nullable: true),
                    messageStatus = table.Column<short>(nullable: true),
                    createDate = table.Column<DateTime>(nullable: true),
                    userId = table.Column<long>(nullable: true),
                    responder = table.Column<long>(nullable: true),
                    status = table.Column<short>(nullable: true),
                    header = table.Column<string>(maxLength: 500, nullable: true),
                    ownerUsername = table.Column<string>(maxLength: 100, nullable: true),
                    ownerEmail = table.Column<string>(maxLength: 500, nullable: true),
                    ownerMobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    ownerFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    ownerLastName = table.Column<string>(maxLength: 100, nullable: true),
                    ownerGender = table.Column<short>(nullable: true),
                    ownerPicture = table.Column<string>(maxLength: 1000, nullable: true),
                    responderUsername = table.Column<string>(maxLength: 100, nullable: true),
                    responderEmail = table.Column<string>(maxLength: 500, nullable: true),
                    responderMobilePhone = table.Column<string>(maxLength: 15, nullable: true),
                    responderFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    responderLastName = table.Column<string>(maxLength: 100, nullable: true),
                    responderGender = table.Column<short>(nullable: true),
                    responderPicture = table.Column<string>(maxLength: 1000, nullable: true),
                    unread = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CreditInfo",
                schema: "sms",
                columns: table => new
                {
                    userId = table.Column<long>(nullable: false),
                    discount = table.Column<short>(nullable: true, defaultValueSql: "0"),
                    credit = table.Column<decimal>(type: "numeric(20,2)", nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("CreditInfo_pkey", x => x.userId);
                    table.ForeignKey(
                        name: "fk_user_credit",
                        column: x => x.userId,
                        principalSchema: "um",
                        principalTable: "AccountInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NumberInfo",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    number = table.Column<string>(maxLength: 50, nullable: true),
                    isShared = table.Column<bool>(nullable: true),
                    isBlocked = table.Column<bool>(nullable: true),
                    owner = table.Column<long>(nullable: true),
                    type = table.Column<short>(nullable: true),
                    username = table.Column<string>(maxLength: 100, nullable: true),
                    password = table.Column<string>(maxLength: 100, nullable: true),
                    createDate = table.Column<DateTime>(nullable: true),
                    lastReceivedId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberInfo", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_number",
                        column: x => x.owner,
                        principalSchema: "um",
                        principalTable: "AccountInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "اطلاعات مربوط به شماره های خدماتی و  پیام انبوه");

            migrationBuilder.CreateTable(
                name: "PersonalTemplate",
                schema: "sms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    userId = table.Column<long>(nullable: true),
                    name = table.Column<string>(maxLength: 200, nullable: true),
                    body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalTemplate", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_sms_template",
                        column: x => x.userId,
                        principalSchema: "um",
                        principalTable: "AccountInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "تمپلیت های ساخته شده توسط کاربر برای ارسال پیام");

            migrationBuilder.CreateTable(
                name: "AdditionalInfo",
                schema: "um",
                columns: table => new
                {
                    userId = table.Column<long>(nullable: false),
                    specialDate = table.Column<DateTime>(nullable: true),
                    SpecialDateCounter = table.Column<short>(nullable: true),
                    instagramLink = table.Column<string>(nullable: true),
                    telegramLink = table.Column<string>(nullable: true),
                    isSpecialDateChanged = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AdditionalInfo_pkey", x => x.userId);
                    table.ForeignKey(
                        name: "USER_ID_FK",
                        column: x => x.userId,
                        principalSchema: "um",
                        principalTable: "AccountInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "User Additional Information");

            migrationBuilder.CreateTable(
                name: "AddressInfo",
                schema: "um",
                columns: table => new
                {
                    userId = table.Column<long>(nullable: false),
                    region = table.Column<string>(maxLength: 50, nullable: true),
                    city = table.Column<string>(maxLength: 50, nullable: true),
                    address = table.Column<string>(maxLength: 500, nullable: true),
                    latitude = table.Column<double>(nullable: true),
                    longitude = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AddressInfo_pkey", x => x.userId);
                    table.ForeignKey(
                        name: "USER_ID_FK1",
                        column: x => x.userId,
                        principalSchema: "um",
                        principalTable: "AccountInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "User Address Info");

            migrationBuilder.CreateTable(
                name: "DeviceInfo",
                schema: "um",
                columns: table => new
                {
                    userId = table.Column<long>(nullable: false),
                    os = table.Column<string>(maxLength: 50, nullable: true),
                    platform = table.Column<string>(maxLength: 50, nullable: true),
                    browser = table.Column<string>(maxLength: 50, nullable: true),
                    lastActivity = table.Column<DateTime>(nullable: true),
                    ipAddress = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DeviceInfo_pkey", x => x.userId);
                    table.ForeignKey(
                        name: "USER_ID_FK2",
                        column: x => x.userId,
                        principalSchema: "um",
                        principalTable: "AccountInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "User Device Info");

            migrationBuilder.CreateTable(
                name: "Group",
                schema: "um",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(maxLength: 100, nullable: true),
                    title = table.Column<string>(maxLength: 500, nullable: true),
                    owner = table.Column<long>(nullable: false),
                    descriptions = table.Column<string>(nullable: true),
                    status = table.Column<short>(nullable: true),
                    picture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.id);
                    table.ForeignKey(
                        name: "Group_owner_fkey",
                        column: x => x.owner,
                        principalSchema: "um",
                        principalTable: "AccountInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "table for users group");

            migrationBuilder.CreateTable(
                name: "PersonalInfo",
                schema: "um",
                columns: table => new
                {
                    userId = table.Column<long>(nullable: false),
                    firstName = table.Column<string>(maxLength: 50, nullable: true),
                    lastName = table.Column<string>(maxLength: 100, nullable: true),
                    nickName = table.Column<string>(maxLength: 100, nullable: true),
                    birthday = table.Column<DateTime>(nullable: true),
                    gender = table.Column<short>(nullable: true),
                    birthdayChangeCounter = table.Column<short>(nullable: true),
                    nationalCode = table.Column<string>(maxLength: 10, nullable: true),
                    isBirthdayChanged = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PersonalInfo_pkey", x => x.userId);
                    table.ForeignKey(
                        name: "USER_ID_FK3",
                        column: x => x.userId,
                        principalSchema: "um",
                        principalTable: "AccountInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "User Personal Information");

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "um",
                columns: table => new
                {
                    roleId = table.Column<long>(nullable: false),
                    permissionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RolePermissions_pkey", x => new { x.roleId, x.permissionId });
                    table.ForeignKey(
                        name: "PERMISSION_ID_FK_ROLEPERMISSION",
                        column: x => x.permissionId,
                        principalSchema: "um",
                        principalTable: "Permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ROLE_ID_FK_ROLEPERMISSION",
                        column: x => x.roleId,
                        principalSchema: "um",
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Role Permissions");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "um",
                columns: table => new
                {
                    userId = table.Column<long>(nullable: false),
                    roleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserRoles_pkey", x => new { x.userId, x.roleId });
                    table.ForeignKey(
                        name: "ROLE_ID_FK_USERROLE",
                        column: x => x.roleId,
                        principalSchema: "um",
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "USER_ID_FK_USERROLE",
                        column: x => x.userId,
                        principalSchema: "um",
                        principalTable: "AccountInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "User Roles");

            migrationBuilder.CreateTable(
                name: "UserGroups",
                schema: "um",
                columns: table => new
                {
                    userId = table.Column<long>(nullable: false),
                    groupId = table.Column<long>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserGroups_pkey", x => new { x.userId, x.groupId });
                    table.ForeignKey(
                        name: "UserGroups_groupId_fkey",
                        column: x => x.groupId,
                        principalSchema: "um",
                        principalTable: "Group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "UserGroups_userId_fkey",
                        column: x => x.userId,
                        principalSchema: "um",
                        principalTable: "AccountInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Assigned groups to user");

            migrationBuilder.CreateIndex(
                name: "IX_NumberInfo_owner",
                schema: "sms",
                table: "NumberInfo",
                column: "owner");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalTemplate_userId",
                schema: "sms",
                table: "PersonalTemplate",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_owner",
                schema: "um",
                table: "Group",
                column: "owner");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_permissionId",
                schema: "um",
                table: "RolePermissions",
                column: "permissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_groupId",
                schema: "um",
                table: "UserGroups",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_roleId",
                schema: "um",
                table: "UserRoles",
                column: "roleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarEvents99");

            migrationBuilder.DropTable(
                name: "BusinessCard",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "CreditInfo",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "NumberInfo",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "PanelInfo",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "PanelVersionInfo",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "PersonalTemplate",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "ReceiveInfo",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "ScheduleSmsDetail",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "ScheduleSmsInfo",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "SentInfo",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "Settings",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "TaskInfo",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "vwBusinessCard",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "vwNumber",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "vwPanel",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "vwScheduleSms",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "vwScheduleSmsInfo",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "vwTask",
                schema: "sms");

            migrationBuilder.DropTable(
                name: "AdditionalInfo",
                schema: "um");

            migrationBuilder.DropTable(
                name: "AddressInfo",
                schema: "um");

            migrationBuilder.DropTable(
                name: "DeviceInfo",
                schema: "um");

            migrationBuilder.DropTable(
                name: "PersonalInfo",
                schema: "um");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "um");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "um");

            migrationBuilder.DropTable(
                name: "TicketDetail",
                schema: "um");

            migrationBuilder.DropTable(
                name: "UserGroups",
                schema: "um");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "um");

            migrationBuilder.DropTable(
                name: "vwContact",
                schema: "um");

            migrationBuilder.DropTable(
                name: "vwContactGroups",
                schema: "um");

            migrationBuilder.DropTable(
                name: "vwTicket",
                schema: "um");

            migrationBuilder.DropTable(
                name: "vwTicketDetail",
                schema: "um");

            migrationBuilder.DropTable(
                name: "vwTicketLastMessage",
                schema: "um");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "um");

            migrationBuilder.DropTable(
                name: "Group",
                schema: "um");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "um");

            migrationBuilder.DropTable(
                name: "AccountInfo",
                schema: "um");
        }
    }
}
