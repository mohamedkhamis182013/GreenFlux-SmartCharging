#Build and Run
- Make sure docker engine is running.
- To build the application "docker compose build".
- To run the application "docker compose up".
- Try the application at http://localhost:8080/swagger/
- Check Database => server name: "localhost,1433", Login: "sa", Password: "Any_password123"
- ---------------------------------------------
- Alternative way to run without docker engine using SQL server in memory database
- Make sure you have .net 8 installed. 
- Navigate to "GreenFlux-SmartCharging.Infrastructure/DependencyInjection.cs"
- Comment line 15 and uncomment line 16.
- Run from your IDE.

#Architecture and Tools
- Clean architecture.
- Repository and UnitOfWork. 
- Fluent Validation.
- Exception filter.
- Entity Framework Core
- SQL Server
- Console logger 
- In memory SQL Lite (for integration test and testing the repositories)
- Xunit 
- Fluent Assertions 
- MOQ library 
- Builder pattern 

#Notes and points to enhance
- expected id values for add are empty Guid values for GUID Ids: "00000000-0000-0000-0000-000000000000" and for integer Id: 0.
- The application doesn't have enough unit tests and integration tests.
