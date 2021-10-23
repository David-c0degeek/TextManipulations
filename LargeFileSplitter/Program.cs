using System.IO;

var fileCount = 1;
const int maximumLinesPerFile = 15000000 * 5;

StreamWriter? writer = null;
try
{
    using StreamReader inputFile = new StreamReader(@"C:\CompiledWordList.txt");
    var count = 0;
    string? line;
    while ((line = inputFile.ReadLine()) != null)
    {

        if (writer == null || count > maximumLinesPerFile)
        {
            if (writer != null)
            {
                writer.Close();
                writer = null;
                fileCount++;
            }

            writer = new StreamWriter(@$"D:\hashcat\hashcat-3.6.0\CompiledWordList{fileCount}.txt", true);

            count = 0;
        }

        writer.WriteLine(line.ToLower());

        ++count;
    }
}
finally
{
    writer?.Close();
}