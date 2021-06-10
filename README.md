# ShireBank

To build GRPC service and publish release you have to run a script from root folder as below example

.\BuildGrpcService.ps1 -PublishPath C:\Repo\service -Runtime osx-x64

Where:

1. -PublishPath [Mandatory]: It's path where script will generate executable files
2. -Runtime [Optional][DefaultValue = win10-x64] It's runtime envinronent where application will be run e.g win10-x64: windows 10 x64 

-------------------------------------------------------------------------------------------------------------

Project summary:

I did small modification to the SharedInterface project. I moved CustomerInterface to path \SharedInterface\Interfaces\CustomInterface

Project has 3 new project:
1. Model to store enitity models and others project shared models
2. Repository to store services working with the database
3. Service has services that are proxies between db services and Grpc services
4. I did some modification in a class \CustomerTest\Program.cs to work well with the Grpc service
5. I added a proxy service to join grpc services with an old \CustomerTest\Program.cs style
6. I had to comment line from 164 to 168 on the CustomerTest.Program class. I haven't got more time to resolve that issue.
7. I added in the Repository.Core.ShireBankDbContext on the line 23, a functionality to delete database. It was added to help user running the CustomerTest tests, 
	without delete sqlite database each time when user runs new tests from the CustomerTests project.
8. I added Thread.Sleep in the CustomerTest.Program class on the line 172 (before running the inspector logic) to wait small amount of time to check if everyting works 
	fine with a bigger amount of request to grpc server
9. I added Thread.Sleep in the CustomerTest.Program class on the line 13 to help me debugging solution when I run solution with two running project.


P.S. I will grateful for any tips how to improve this project and what things I can do better the I did in this project. Mainly I'd like to hear a feedback from inspector logic,
I wonder if my idea is good, maybe I could do it in better way.