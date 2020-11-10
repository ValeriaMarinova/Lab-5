using Microsoft.Extensions.Configuration;

namespace AutoRepair
{
    class Program
    {
        static IConfiguration configuration;

        static void Main(string[] args)
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var connectionString = configuration.GetConnectionString("PostgresAutorepairDB");

            //Scenarios.FillDatabase(connectionString);
            Scenarios.ExampleOfOrderWorkflow(connectionString);
            //Scenarios.MeasureTime(connectionString);
        }
    }
}
