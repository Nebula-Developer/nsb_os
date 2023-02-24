.PHONY: run test publish testlib

run:
	bash build.sh

test:
	make testlib
	cd NSB.OS.Tests && dotnet test

testlib:
	mkdir -p ./NSB.OS.Tests/bin/Debug/net7.0/lib/
	gcc -shared -fPIC ./NSB.OS.Library/NSB.OS.Library.c -o ./NSB.OS.Tests/bin/Debug/net7.0/lib/libNSB.OS.Library.so
