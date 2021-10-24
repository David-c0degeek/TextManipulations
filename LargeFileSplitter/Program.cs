using System.IO;

// 1 gigabyte = 1073741824 bytes
// char = 2 bytes
// max 16 characters per line
// (32 * 1073741824) / (sizeof(char) * 16) 
const int desiredFileSizeInGigabyte = 32;

// 1024bytes = 1kb
// 1024kb = 1mb
// 1024mb = 1gb
const long maximumFileSizeInBytes = desiredFileSizeInGigabyte * 1024L * 1024L * 1024L;

var fileCount = 1;

StreamWriter? writer = null;
try
{
    using StreamReader inputFile = new(@"C:\CompiledWordList.txt");
    var currentFileSizeInBytes = 0L;
    string? line;
    while ((line = inputFile.ReadLine()) != null)
    {
        currentFileSizeInBytes += line.Length * sizeof(char);
        
        if (writer == null || currentFileSizeInBytes > maximumFileSizeInBytes)
        {
            if (writer != null)
            {
                writer.Close();
                writer = null;
                fileCount++;
            }

            writer = new StreamWriter(@$"D:\hashcat\hashcat-3.6.0\CompiledWordList{fileCount}.txt", true);

            currentFileSizeInBytes = 0;
        }

        writer.WriteLine(line);
    }
}
finally
{
    writer?.Close();
}