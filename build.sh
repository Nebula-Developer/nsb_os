cd NSB.OS
dotnet publish -c Release -o ../Publish
mkdir -p ../Publish/lib
# cd ../NSB.OS.Library
# gcc -shared -o ../Publish/lib/libNSB.OS.Library.so -fPIC NSB.OS.Library.c -ltermcap
# cd ../Publish
cd ../Publish
./NSB.OS
