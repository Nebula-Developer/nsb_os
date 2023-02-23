.PHONY: run test

run:
	dotnet run --project NSB.OS

test:
	dotnet run --project NSB.OS.Tests
