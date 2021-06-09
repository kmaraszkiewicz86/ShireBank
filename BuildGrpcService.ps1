param(
    [Parameter(Mandatory=$True, Position=0, ValueFromPipeline=$false)]
    [System.String]
    $PublishPath,

    [Parameter(Mandatory=$False, Position=1, ValueFromPipeline=$false)]
    [System.String]
    $Runtime = "win10-x64"
)

Write-Output ‘--------------------------------------------------------------------’
Write-Output ‘Step1: Restoring nuget packages’
Write-Output ‘--------------------------------------------------------------------’

cd ./ShireBank

dotnet restore

Write-Output ‘--------------------------------------------------------------------’
Write-Output ‘Step2: Build application’
Write-Output ‘--------------------------------------------------------------------’

dotnet build --configuration Release

Write-Output ‘--------------------------------------------------------------------’
Write-Output ‘Step3: Publishing application to path’
Write-Output ‘--------------------------------------------------------------------’

dotnet publish --configuration Release -o $PublishPath --runtime $Runtime

cd ../Repository.Tests

Write-Output ‘--------------------------------------------------------------------’
Write-Output ‘Step4: Restoring nuget packages for a testing project’
Write-Output ‘--------------------------------------------------------------------’

dotnet restore

Write-Output ‘--------------------------------------------------------------------’
Write-Output ‘Step5: Build unit the test project application’
Write-Output ‘--------------------------------------------------------------------’

dotnet build --configuration Release

Write-Output ‘--------------------------------------------------------------------’
Write-Output ‘Step6: Running unit tests’
Write-Output ‘--------------------------------------------------------------------’

dotnet test -l "console;verbosity=detailed" --configuration Release
cd ..

Write-Output ‘--------------------------------------------------------------------’
Write-Output ‘Script finish job...’
Write-Output ‘--------------------------------------------------------------------’

pause