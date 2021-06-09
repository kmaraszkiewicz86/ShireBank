# ShireBank

To build GRPC service and publish release you have to run a script from root folder as below example

.\BuildGrpcService.ps1 -PublishPath C:\Repo\service -Runtime osx-x64

Where:

1. -PublishPath [Mandatory]: It's path where script will generate executable files
2. -Runtime [Optional][DefaultValue = win10-x64] It's runtime envinronent where application will be run e.g win10-x64: windows 10 x64 
