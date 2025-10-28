using System;
using System.Globalization;
public class Program
{
    public static void Main(string[] args)
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
        Random rand = new Random();
        SeaTransport[] seaTransport = new SeaTransport[3];
        Console.WriteLine("---Морской---");

        for (int i = 0; i < seaTransport.Length; i++)
        {
            int speed = rand.Next(10, 20);
            int costPrice = rand.Next(1, 10); 
            seaTransport[i] = new SeaTransport(speed, costPrice);

            Console.WriteLine(seaTransport[i].GetInfo());
        }

        Array.Sort(seaTransport);

        Console.WriteLine("----сортировка по скорости----");


        foreach (SeaTransport transport in seaTransport)
        {
            Console.WriteLine(transport.GetInfo());
        }

        Console.WriteLine(); 

        //(Наземний транспорт)
        GroundTransport[] groundTransport = new GroundTransport[3];
        Console.WriteLine("---Наземный---");

        for (int i = 0; i < groundTransport.Length; i++)
        {
            int speed = rand.Next(15, 25); 
            int costPrice = rand.Next(3, 10); 
            int roadToll = rand.Next(2, 5); 
            groundTransport[i] = new GroundTransport(speed, costPrice, roadToll);

            Console.WriteLine(groundTransport[i].GetInfo());
        }

        Array.Sort(groundTransport);

        Console.WriteLine("----сортировка по стоимости----");

        foreach (GroundTransport transport in groundTransport)
        {
            Console.WriteLine(transport.GetInfo());
        }
    }
}

public abstract class Transport
{
    public int Speed { get; set; }
    public int CostPrice { get; set; } 
    public double Cost { get; protected set; }

    public abstract double CalculateCost();

    public virtual string GetInfo()
    {
        return $"Скорость={Speed} себестоимость={CostPrice}";
    }
}

public class SeaTransport : Transport, IComparable
{
    public SeaTransport(int speed, int costPrice)
    {
        Speed = speed;
        CostPrice = costPrice;
        CalculateCost();
    }

    public override double CalculateCost()
    {
        Cost = CostPrice / (double)Speed;
        return Cost;
    }

    public override string GetInfo()
    {
        return $"Морской. {base.GetInfo()} стоимость={Cost:F2}";
    }

    public int CompareTo(object o)
    {
        if (o is SeaTransport t)
        {
            
            return this.Speed.CompareTo(t.Speed);
        }
        throw new ArgumentException("Об'єкт не є SeaTransport");
    }
}

public class GroundTransport : Transport, IComparable
{
    public int RoadToll { get; set; } 

    public GroundTransport(int speed, int costPrice, int roadToll)
    {
        Speed = speed;
        CostPrice = costPrice;
        RoadToll = roadToll;
        CalculateCost(); 
    }
    public override double CalculateCost()
    {
        Cost = (CostPrice + RoadToll) / (double)Speed;
        return Cost;
    }

    public override string GetInfo()
    {
        return $"Наземный. Скорость={Speed} себестоимость={CostPrice} дорожный сбор={RoadToll} стоимость={Cost:F2}";
    }

    public int CompareTo(object o)
    {
        if (o is GroundTransport t)
        {
            // Виконуємо сортування за полем Cost
            return this.Cost.CompareTo(t.Cost);
        }
        throw new ArgumentException("Об'єкт не є GroundTransport");
    }
}