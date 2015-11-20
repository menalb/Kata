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
        if (input.Contains("\\"))
        {
            _delimiters += input[2];
            input = input.Substring(4);
        }
        int sum = 0;
        CalculateNumbers(input, result =>
        {
            var negatives = result.Where(integer => integer < 0);
            if (negatives.Count() > 0)
                throw new ArgumentOutOfRangeException(
                    string.Format("Negatives not allowed {0}", string.Join(",", negatives.Select(x => x.ToString()).ToArray())
                    ));
            sum = result.Sum();
        });
        
        return sum;
    }

    private void CalculateNumbers(string input, Action<IEnumerable<int>> processResult)
    {
        var numbers = input.Split(_delimiters.ToCharArray());
        if (numbers.Any(stringNumber => string.IsNullOrEmpty(stringNumber)))
            throw new ArgumentException();
        processResult(numbers.Select(number => int.Parse(number)));
    }
}