using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

const int cutoff = 10000000;

List<string> allPossibleElements = new()
{
    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W",
    "X", "Y", "Z",
    "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w",
    "x", "y", "z",
    "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
};

static bool NextCombination(IList<int> num, int n, int k)
{
    bool finished;
 
    var changed = finished = false;
 
    if (k <= 0) return false;
 
    for (var i = k - 1; !finished && !changed; i--)
    {
        if (num[i] < n - 1 - (k - 1) + i)
        {
            num[i]++;
 
            if (i < k - 1)
                for (var j = i + 1; j < k; j++)
                    num[j] = num[j - 1] + 1;
            changed = true;
        }
        finished = i == 0;
    }
 
    return changed;
}
 
static IEnumerable Combinations<T>(IEnumerable<T> elements, int k)
{
    var elem = elements.ToArray();
    var size = elem.Length;
 
    if (k > size) yield break;
 
    var numbers = new int[k];
 
    for (var i = 0; i < k; i++)
        numbers[i] = i;
 
    do
    {
        yield return numbers.Select(n => elem[n]);
    } while (NextCombination(numbers, size, k));
}

var sb = new StringBuilder();
var lineCount = 0;

var desiredLengths = new List<int> { 12, 10, 9, 11, 13, 14, 15, 16 };

foreach (var desiredLength in desiredLengths)
{
    foreach (IEnumerable<string> i in Combinations(allPossibleElements, desiredLength))
    {
        var str = string.Join("", i);

        if (HasConsecutiveChars(str, 3)) continue;
    
        sb.AppendLine(str);
        lineCount++;
    
        if (lineCount != cutoff) continue;
            
        Console.WriteLine("Cutoff reached, writing file and resetting StringBuilder");
        File.AppendAllText($@"\\tower\data\Sec\AllCombinationsOfLength{desiredLength}.txt", sb.ToString());
        sb.Clear();
        sb = new StringBuilder();
        lineCount = 0;
    }
}

    
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



// /// <summary>
// /// Method to create lists containing possible combinations of an input list of items. This is 
// /// basically copied from code by user "jaolho" on this thread:
// /// http://stackoverflow.com/questions/7802822/all-possible-combinations-of-a-list-of-values
// /// </summary>
// /// <typeparam name="T">type of the items on the input list</typeparam>
// /// <param name="inputList">list of items</param>
// /// <param name="minimumItems">minimum number of items wanted in the generated combinations, 
// ///                            if zero the empty combination is included,
// ///                            default is eight</param>
// /// <param name="maximumItems">maximum number of items wanted in the generated combinations,
// ///                            default is 16</param>
// /// <returns>list of lists for possible combinations of the input items</returns>
// static void ItemCombinations<T>(List<T> inputList, int minimumItems = 8,
//     int maximumItems = 16, string fileName = "")
// {
//     var nonEmptyCombinations = (int)Math.Pow(2, inputList.Count) - 1;
//
//     var sb = new StringBuilder();
//     var lineCount = 0;
//     
//     // Not-so-simple case, avoid generating the unwanted combinations
//     for (var bitPattern = 1; bitPattern <= nonEmptyCombinations; bitPattern++)
//     {
//         var bitCount = System.Numerics.BitOperations.PopCount((uint)bitPattern);
//         if (bitCount < minimumItems || bitCount > maximumItems) continue;
//         
//         var combination = GenerateCombination(inputList, bitPattern);
//         var combinationString = string.Join(string.Empty, combination);
//         if (!HasConsecutiveChars(combinationString, 3))
//         {
//             sb.AppendLine(combinationString);
//             lineCount++;
//         }
//
//         if (lineCount != cutoff) continue;
//             
//         Console.WriteLine("Cutoff reached, writing file and resetting StringBuilder");
//         File.AppendAllText($@"d:\{fileName}.txt", sb.ToString());
//         sb.Clear();
//         sb = new StringBuilder();
//         lineCount = 0;
//     }
// }
//

//
// /// <summary>
// /// Sub-method of ItemCombinations() method to generate a combination based on a bit pattern.
// /// </summary>
// static List<T> GenerateCombination<T>(List<T> inputList, int bitPattern)
// {
//     List<T> thisCombination = new(inputList.Count);
//     for (var j = 0; j < inputList.Count; j++)
//     {
//         if ((bitPattern >> j & 1) == 1)
//             thisCombination.Add(inputList[j]);
//     }
//
//     return thisCombination;
// }
//
// ItemCombinations(allPossibleElements, 8, 8, "CombinationsOf8");
// ItemCombinations(allPossibleElements, 9, 9, "CombinationsOf9");
// ItemCombinations(allPossibleElements, 10, 10, "CombinationsOf10");
// ItemCombinations(allPossibleElements, 11, 11, "CombinationsOf11");
// ItemCombinations(allPossibleElements, 12, 12, "CombinationsOf12");
// ItemCombinations(allPossibleElements, 13, 13, "CombinationsOf13");
// ItemCombinations(allPossibleElements, 14, 14, "CombinationsOf14");
// ItemCombinations(allPossibleElements, 15, 15, "CombinationsOf15");
// ItemCombinations(allPossibleElements, 16, 16, "CombinationsOf16");