<b>Instruction to implement custom logging in you ASP Net Core Application</b>

1. Add the ICustomLogger file next to the Startup.cs file.
2. Add the below lines in the Startup.cs file and then you can disable the other logging to avoid any duplication of the data.

            loggerFactory.AddColoredConsoleLogger(c=>
            {
                c.LogLevel = LogLevel.Information;
                c.Color = ConsoleColor.White;
            });
3. You can modify the class as per your requirement.
4. Remember you need to redeploy your code to Azure web app after making these changes.
5. You will the logging in the below format under LogFiles folder on the KUDU site.

7/20/2017 8:10:50 AM - Information - 0 - Microsoft.Extensions.DependencyInjection.DataProtectionServices - Azure Web Sites environment detected. Using 'D:\home\ASP.NET\DataProtection-Keys' as key repository; keys will not be encrypted at rest.
Hosting environment: Production
Content root path: D:\home\site\wwwroot
Now listening on: http://localhost:1726
Application started. Press Ctrl+C to shut down.
7/20/2017 8:10:53 AM - Information - 1 - Microsoft.AspNetCore.Hosting.Internal.WebHost - Request starting HTTP/1.1 GET http://appaspnetcore.azurewebsites.net/  
7/20/2017 8:10:55 AM - Information - 1 - Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker - Executing action method NetCore_Identity.Controllers.HomeController.Index (NetCore-Identity) with arguments ((null)) - ModelState is Valid
7/20/2017 8:11:03 AM - Information - 1 - Microsoft.AspNetCore.Mvc.ViewFeatures.Internal.ViewResultExecutor - Executing ViewResult, running view at path /Views/Home/Index.cshtml.
7/20/2017 8:11:05 AM - Information - 2 - Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker - Executed action NetCore_Identity.Controllers.HomeController.Index (NetCore-Identity) in 10288.5391ms
