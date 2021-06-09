Write-Output ‘Step1: Building application’

cd .\Repository.Tests\

dotnet restore
dotnet build --configuration Debug

Write-Output ‘Step2: Running tests’
dotnet test -l "console;verbosity=detailed"

Write-Output ‘Press any key to exit’

pause