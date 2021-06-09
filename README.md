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