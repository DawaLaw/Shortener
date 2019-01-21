# Shortener
Url Shortener Tool to shorten long urls.
Demo site: [Shortener](https://ts.my)

## Local Setup Requirements
* [Visual Studio Code](https://code.visualstudio.com) or [Visual Studio 2017](https://visualstudio.microsoft.com/vs)
* [.NET Core SDK](https://dotnet.microsoft.com/download) - for build and test
* [Azure Storage Emulator](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator) - for local testing and development
* [SQL Server LocalDB or Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) - needed by Azure Storage Emulator
* [Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer) - Optional for verification

## Setup Azure Storage Emulator (Required Once)
1. Install SQL Server and Azure Storage Emulator.
2. Go to `C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator`
3. Run cmd, `AzureStorageEmulator.exe init`
4. Run `AzureStorageEmulator.exe start`

## Setup Source and Run
1. Clone via git or download source.
2. In project directory, open `Shortener.sln` with VS 2017 (or open directory with VS code)
3. Build > Build Solution with VS 2017 (or in command prompt, `dotnet restore` `dotnet build`)
4. Debug > Start Debugging or F5 with VS 2017 (or in command prompt, `dotnet run`)
