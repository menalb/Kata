using System;
using System.Collections.Generic;
using System.Linq;
public class StringCalculator
{
    private string _delimiters = ",\n";

    public int Add(string input)
    {
        if (string.IsNullOrEmpty(input))
            return 0;

        input = UpdateDelimitersAndGetTheCleanInput(input);

        int sum = 0;
        CalculateNumbers(input, _delimiters, result =>
         {
             ProcessPossibleNegativeValues(result);
             sum = result.Sum();
         });

        return sum;
    }

    private string UpdateDelimitersAndGetTheCleanInput(string input)
    {
        if (input.Contains("\\"))
        {
            _delimiters += input[2];
            input = input.Substring(4);
        }
        return input;
    }

    private static void ProcessPossibleNegativeValues(IEnumerable<int> numbers)
    {
        var negatives = numbers.Where(integer => integer < 0);
        if (negatives.Count() > 0)
            throw new ArgumentOutOfRangeException
            (
                string.Format("Negatives not allowed {0}", string.Join(",", negatives.Select(x => x.ToString()).ToArray()))
             );
    }

    private static void CalculateNumbers(string input, string delimiters, Action<IEnumerable<int>> processResult)
    {
        var numbers = input.Split(delimiters.ToCharArray());
        if (numbers.Any(stringNumber => string.IsNullOrEmpty(stringNumber)))
            throw new ArgumentException();
        processResult(numbers.Select(number => int.Parse(number)));
    }
}