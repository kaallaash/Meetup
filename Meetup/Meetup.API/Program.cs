using AuthorizationService.API.DI;
using FluentValidation;
using Meetup.API.Validators;
using Meetup.API.ViewModels;
using Meetup.BLL.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMeetupBll(builder.Configuration);
builder.Services
    .AddAutoMapper(typeof(Meetup.API.AutoMapper.MappingProfile), typeof(Meetup.BLL.AutoMapper.MappingProfile));
builder.Services.AddScoped<IValidator<ChangeEventViewModel>, ChangeEventViewModelValidator>();
builder.Services.AddScoped<IValidator<ChangeSpeakerViewModel>, ChangeSpeakerViewModelValidator>();
builder.Services.AddAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
