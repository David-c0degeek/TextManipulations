using System;
using System.Globalization;
using System.IO;
using System.Text;

const int cutoff = 50308538;

using var sr = File.OpenText(@"C:\all_in_one_w\all_in_one_w");

var stringBuilder = new StringBuilder();
string? line;
var count = 0;
long totalCount = 0;

while((line = sr.ReadLine()) != null){
    if (line.Length is < 8 or > 16)
        continue;
    
    if(HasConsecutiveChars(line, 3))
        continue;

    stringBuilder.AppendLine(line);
    count++;
    if (count == cutoff)
    {
        Console.WriteLine("Cutoff reached, writing file");
        totalCount += count;
        count = 0;
        File.AppendAllText(@"D:\CompiledWordList.txt", stringBuilder.ToString());
        stringBuilder.Clear();
        stringBuilder = new StringBuilder();
    }
}

Console.WriteLine($"A total of {totalCount} possible passwords written");


static bool HasConsecutiveChars(string source, int sequenceLength)
{
    var charEnumerator = StringInfo.GetTextElementEnumerator(source);
    var currentElement = string.Empty;
    var count = 1;
    while (charEnumerator.MoveNext()) 
    {
        if (currentElement == charEnumerator.GetTextElement())
        {
            if (++count >= sequenceLength)
            {
                return true;
            }
        }
        else
        {
            count = 1;
            currentElement = charEnumerator.GetTextElement();
        }
    }
    return false;
}