cd NSB.OS
dotnet publish -c Release -o ../Publish
mkdir ../Publish/lib -p
cd ../NSB.OS.Library
gcc -shared -o ../Publish/lib/libNSB.OS.Library.so -fPIC NSB.OS.Library.c
cd ../Publish
./NSB.OS
