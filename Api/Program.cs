using Api.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// настройка взаимодействия с DbContext (метод расширения)
builder.Services.AddPostgreSqlDbContext(builder.Configuration);
builder.Services.AddPostgreSqlIdentityContext();

builder.Services.AddConfigureIdentityOptions();
builder.Services.AddJwtTokenGenerator();

// подключение аутентификации через JWT
builder.Services.AddAuthenticationConfig(builder.Configuration);
// подключение CORS-политики для разрешения/запрета обработки запросов из различных источников
builder.Services.AddCors();

builder.Services.AddScoped<IStorage, PostgreSqlEfStorage>();

// сборка приложения и конвейера обработки запросов
var app = builder.Build();

// Configure the HTTP request pipeline (порядок вызовов в конвейере важен, существуют специальные требования)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// определение CORS-политики: разрешение всех заголовков, методов и источников
app.UseCors(opt =>
    opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("*"));

app.UseAuthentication(); // "промежуточное ПО" для обработки аутентификации
app.UseAuthorization(); // "промежуточное ПО" для определения у аутентифицированного юзера разрешений на доступ к запрашиваемому ресурсу

// middleware (Use...) должны идти до MapControllers (иначе pipeline может работать некорректно)
app.MapControllers();

await app.Services.InitializeRoleAsync();

app.Run();
