using InsuranceBackend.DTOs;
using InsuranceBackend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("devsetting",
   policy =>
   {
       policy.SetIsOriginAllowed(host=>true)
           .AllowAnyHeader()
           .AllowAnyMethod();
   });
});
//builder.Services.AddDbContext<InsuranceDB>(opt => opt.UseInMemoryDatabase("InsuranceDB"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
///
/// setting up data for application
/// 
var ratingList = new List<Rating> {
    new Rating { RatingID = 1, RatingName = "Professional", RatingWeight = 1.0f },
    new Rating { RatingID = 2, RatingName = "White Collar", RatingWeight = 1.25f },
    new Rating { RatingID = 3, RatingName = "Light Manual", RatingWeight = 1.5f },
    new Rating { RatingID = 4, RatingName = "Heavy Manual", RatingWeight = 1.75f }
};
var occupationList = new List<Occupation> {
    new Occupation { OccupationID =1 , OccupationName = "Cleaner" , RatingID= 3} ,
    new Occupation { OccupationID =2, OccupationName="Doctor", RatingID =1},
    new Occupation { OccupationID =3, OccupationName="Author", RatingID =2},
    new Occupation { OccupationID =4, OccupationName ="Farmer", RatingID=4 },
    new Occupation { OccupationID =5, OccupationName ="Mechanic", RatingID=4 },
    new Occupation { OccupationID =5, OccupationName ="Florist", RatingID=3 }

};

app.MapGet("getOccupations", () =>
{
    return Results.Ok(occupationList);
});
app.MapPost("/calculateInsurance", ([FromBody] InsuranceCalcDTO insuranceDTO) =>
{
    //(Death Cover amount *Occupation Rating Factor *Age) / 1000 * 12

    //var ratingFactor = ratingList.Where(x => x.RatingID == occupationList.Where(o => o.OccupationID == insuranceDTO.OccupationID).FirstOrDefault().RatingID).First().RatingWeight;
    var calcSum = 0f;
    try
    {
        float ratingID = occupationList.FirstOrDefault(o => o.OccupationID == insuranceDTO.OccupationID).RatingID;
        if (ratingID.Equals(null))
        {
            throw new ArgumentException("Rating ID is null");
        }
        float ratingFactor = ratingList.FirstOrDefault(x => x.RatingID == ratingID).RatingWeight;
        if (ratingFactor.Equals(null))
        {
            throw new ArgumentException("Rating not found for the Occupant");
        }
        calcSum = (float)((insuranceDTO.DeathSumInsured * ratingFactor * insuranceDTO.Age) / 1000 * 12);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, ex.StackTrace);
        return Results.Problem("Something went wrong!");
    }

    return Results.Json(calcSum);
});


app.UseCors("devsetting");
app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}