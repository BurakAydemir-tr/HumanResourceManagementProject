using Business.Abstract;
using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using Core.Exception;
using Core.Sevices.AuthorizeService;
using Core.Sevices.MailService;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Security.Claims;
using WebAPI.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));

builder.Services.AddScoped<ICandidateDal, EfCandidateDal>();
builder.Services.AddScoped<ICandidateService, CandidateManager>();

builder.Services.AddScoped<IEmployerDal, EfEmployerDal>();
builder.Services.AddScoped<IEmployerService, EmployerManager>();

builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IUserService, UserManager>();

builder.Services.AddScoped<IJobPositionDal, EfJobPositionDal>();
builder.Services.AddScoped<IJobPositionService, JobPositionManager>();

builder.Services.AddScoped<IJobAdvertisementDal, EfJobAdvertisementDal>();
builder.Services.AddScoped<IJobAdvertisementService, JobAdvertisementManager>();

builder.Services.AddScoped<IUniversityDal, EfUniversityDal>();
builder.Services.AddScoped<IUniversityService, UniversityManager>();

builder.Services.AddScoped<IJobExperienceDal, EfJobExperienceDal>();
builder.Services.AddScoped<IJobExperienceService, JobExperienceManager>();

builder.Services.AddScoped<ITechnologyDal, EfTechnologyDal>();
builder.Services.AddScoped<ITechnologyService, TechnologyManager>();

builder.Services.AddScoped<ICVDal, EfCVDal>();
builder.Services.AddScoped<ICVService, CVManager>();

builder.Services.AddScoped<IOperationClaimDal, EfOperationClaimDal>();
builder.Services.AddScoped<IOperationClaimService, OperationClaimManager>();

builder.Services.AddScoped<IUserOperationClaimDal, EfUserOperationClaimDal>();
builder.Services.AddScoped<IUserOperationClaimService, UserOperationClaimManager>();

builder.Services.AddScoped<IEndpointDal, EfEndpointDal>();
builder.Services.AddScoped<IEndpointService, EndpointManager>();

builder.Services.AddScoped<IEndpointOperationClaimDal, EfEndpointOperationClaimDal>();
builder.Services.AddScoped<IEndpointOperationClaimService, EndpointOperationClaimManager>();

builder.Services.AddScoped<ITokenHelper, JwtHelper>();
builder.Services.AddScoped<IAuthService, AuthManager>();

builder.Services.AddScoped<IMailService, MailManager>();
builder.Services.AddScoped<IApplicationService, ApplicationManager>();

//Assembly'deki tüm Validator'larý çalýþtýrýyor.
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

/*SeriLog Yapýlandýrýlmasý*/
var columnOpts = new ColumnOptions(); //log için kolon yapýlandýrmasý
columnOpts.Store.Remove(StandardColumn.Properties); //log dan kolon kaldýrabiliyoruz
columnOpts.Store.Add(StandardColumn.LogEvent); //log a kolan ekliyoruz
columnOpts.LogEvent.DataLength = 2048;
columnOpts.PrimaryKey = columnOpts.TimeStamp;
columnOpts.TimeStamp.NonClusteredIndex = true;
columnOpts.AdditionalColumns = new Collection<SqlColumn>() //Log a tanýmlý olmayan kolon eklenebiliyor.
{
    new SqlColumn(){ColumnName = "EnvironmentUserName", PropertyName = "UserName", DataType = SqlDbType.NVarChar, DataLength = 64, AllowNull=true}
};

Logger log = new LoggerConfiguration() // Log konfigurasyonlaru yapýlýyor
    //.WriteTo.Console()
    .WriteTo.File("Logs/log.txt") //Dosyaya yazdýrýlýyor.
    //Veritabanýna yazdýrýlýyor.
    .WriteTo.MSSqlServer(connectionString: "Server=localhost;Database=HrmsNew;Trusted_Connection=True;TrustServerCertificate=True",
        new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true },
        columnOptions: columnOpts
        )
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

//UseSerilog kullanarak build in içindeki loglamayý iptal etmiiþ oluyoruz.
builder.Host.UseSerilog(log);


builder.Services.AddControllers(options =>
{
    options.Filters.Add<RolePermissionFilter>();
})
    .AddNewtonsoftJson(options=>
    options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


var tokenOptions=builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience=tokenOptions.Audience,
        ValidIssuer=tokenOptions.Issuer,
        ClockSkew=TimeSpan.Zero,
        IssuerSigningKey=SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
        NameClaimType=ClaimTypes.Email, //JWT üzerinde Name claimne karþýlýk gelen deeri User.Identity.Name propertysinden elde edebiliriz.
    };
});

/*Swagger Ayarý*/
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345.54321\""
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            new string[] { }
        }
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware(); //Kendi oluþturduðumuz genel hata yakalama middleware i

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
