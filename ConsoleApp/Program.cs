using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConsoleApp.Combinatorics;
using Newtonsoft.Json;
//
// var path1 = @"D:\hashcat\dicts\combinations.txt";
// var path2 = @"D:\hashcat\dicts\combined.txt";
//
// File.AppendAllText(path1, File.ReadAllText(path2));
//
//

const string? path = @"D:\input.txt";

var allText = File.ReadAllText(path);
var lst = new List<string>(
    allText.Split(new[] { "\r\n" },
        StringSplitOptions.RemoveEmptyEntries));

const int cutoff = 10308538;
var totalCombinationsGenerated = 0;

for(var i = 2; i <= 5; i++)
{
    var allPossibleCombinationsOfLengthN = new Combinations<string>(lst, i, GenerateOption.WithRepetition);
    Console.WriteLine($"A total of {allPossibleCombinationsOfLengthN.Count} combinations of length {i} were generated.");
    totalCombinationsGenerated += (int)allPossibleCombinationsOfLengthN.Count;
    
    var sb = new StringBuilder();
    var currentCount = 0;
    
    foreach (var combination in allPossibleCombinationsOfLengthN)
    {
        currentCount++;
        sb.AppendLine(string.Join(string.Empty, combination));
        
        if (currentCount == cutoff)
        {
            Console.WriteLine("Cutoff reached, writing file and resetting StringBuilder");
            File.AppendAllText(@"d:\combinations.txt", sb.ToString());
            sb.Clear();
            sb = new StringBuilder();
            currentCount = 0;
        }
    }
}

Console.WriteLine($"Finished generating all possible combinations");
Console.WriteLine($"A total of {totalCombinationsGenerated} combinations were generated.");