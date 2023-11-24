using System.Collections.Concurrent;

class SmsMessage
{
    private string messageText;
    private double price;

    public SmsMessage(string message)
    {
        if (message.Length <= 65)
        {
            price = 1.5;
        }
        else if (message.Length > 65 && message.Length <= 250)
        {
            double extraChars = message.Length - 65;
            price = extraChars * 0.5 + 1.5;
        }
        else
        {
            message = message.Substring(0, 250);
            price = message.Length * 0.5 + 1.5;
        }

        this.messageText = message;
    }

    public double GetPrice()
    {
        return price;
    }

    public string GetMessageText()
    {
        return messageText;
    }
}

class SQLCommand
{
    private string commandText;
    public SQLCommand(string command)
    {
        commandText = command.ToUpper();
    }

    public string GetCommandText()
    {
        return commandText;
    }
}

interface IMyIntList<T>
{
    void Add(T value);
    T Remove(T value);
}

class MyIntList<T> : IMyIntList<T> where T : IComparable<T>
{
    private List<T> list;
    private T maxValue;
    private decimal costPerChar;
    public MyIntList(T initialValue, decimal maxLength, decimal initialCost)
    {
        list = new List<T>();
        maxValue = initialValue;
        costPerChar = initialCost / maxLength;
    }
    public T GetMaxValue()
    {
        return maxValue;
    }
    public decimal GetCostPerChar()
    {
        return costPerChar;
    }
}
class RandomNumberGenerator
{
    private static readonly ConcurrentDictionary<int, double[]> _cache = new ConcurrentDictionary<int, double[]>();
    private readonly Random _random;
    public RandomNumberGenerator(int length)
    {
        _random = new Random(Guid.NewGuid().GetHashCode());
        double[] numbers = GenerateNumbers(length);
        _cache.TryAdd(length, numbers);
    }
    public double[] GetNumbers(int length, double variance)
    {
        double[] result;
        if (!_cache.TryGetValue(length, out result))
        {
            result = GenerateNumbers(length, variance);
            _cache.AddOrUpdate(length, result, (k, v) => result);
        }
        return result;
    }
    private double[] GenerateNumbers(int length) => GenerateNumbers(length, 1);
    private double[] GenerateNumbers(int length, double variance)
    {
        int[] array = Enumerable
            .Range(1, length)
            .Select(n => (int)(_random.NextDouble() * variance))
            .ToArray();
        double sum = array.Sum();
    }
}