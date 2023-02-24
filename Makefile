.PHONY: run test publish

run:
	dotnet run --project NSB.OS

test:
	cd NSB.OS.Tests && dotnet test --no-build

publish:
	cd NSB.OS && dotnet publish -c Release -o ../publish
