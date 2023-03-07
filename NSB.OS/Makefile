.PHONY: release

RELEASE_DIR = ../Release

release:
	mkdir -p $(RELEASE_DIR)
	dotnet publish -c Release -o $(RELEASE_DIR)/linux -r linux-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=true
	dotnet publish -c Release -o $(RELEASE_DIR)/windows -r win-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=true
	dotnet publish -c Release -o $(RELEASE_DIR)/macos -r osx-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=true
