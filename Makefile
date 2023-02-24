.PHONY: run test

run:
	dotnet run --project NSB.OS

test:
	cd NSB.OS.Tests && dotnet test --no-build
