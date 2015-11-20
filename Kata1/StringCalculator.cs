using System;
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
        var numbers = input.Split(_delimiters.ToCharArray());
        if (numbers.Any(stringNumber => string.IsNullOrEmpty(stringNumber)))
            throw new ArgumentException();
        var integers = numbers.Select(number => int.Parse(number));
                
        var negatives = integers.Where(integer => integer < 0);
        if (negatives.Count() > 0)
            throw new ArgumentOutOfRangeException(
                string.Format("Negatives not allowed {0}", string.Join(",", negatives.Select(x => x.ToString()).ToArray())
                ));
        return integers.Sum();
    }
}