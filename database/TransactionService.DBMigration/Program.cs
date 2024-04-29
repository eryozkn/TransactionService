// See https://aka.ms/new-console-template for more information
using TransactionService.DBMigration;

Console.WriteLine("Inits SQL DB for the service");

// Call DbUp migration
var connectionString = "Server=localhost\\SQLEXPRESS;Database=TransactionStore;Trusted_Connection=True;TrustServerCertificate=True"; 
// todo: migrate to an app config 
MsSqlMigrator.Execute(connectionString);