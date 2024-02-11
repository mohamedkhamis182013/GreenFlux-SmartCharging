#Build and Run
- Make sure docker engine is running.
- To build the application "docker compose build".
- To run the application "docker compose up".
- Try the application at http://localhost:8080/swagger/
- Check Database => servername: "localhost,1433", Login: "sa", Password: "Any_password123"
- ---------------------------------------------
- Alternative way to run without docker engine using sql server inmemory database
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
- SQL server
- Console logger 
- In memory SQL Lite 
- Xunit 
- Fluent Assertions 
- MOQ library 
- Builder pattern 

#Notes and points to enhance
- expected id values for add are empty Guid value for guid Ids: "00000000-0000-0000-0000-000000000000" and for integar Id: 0.
- Application doesn't have enough unit test and integration test.
