Write-Output ‘Step1: Start executing script’

cd ./CustomerTest

Write-Output ‘Step2: Restore nuget packages’

dotnet restore

Write-Output ‘Step3: Build application’

dotnet build --configuration Release

Write-Output ‘Step4: Run application’

dotnet run --configuration Release

Write-Output ‘Press any key to exit’

pasue