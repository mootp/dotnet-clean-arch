using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    //Registering Application Layer Dependencies
    builder.Services.AddApplication(builder.Configuration);

    //Registering Infrastructure Layer Dependencies
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
