using BeerProject;
using System.ComponentModel;

class Program
{
    public static void Main()
    {
        Beer beer1 = new Beer() {BeerName = "Carlsberg", BeerType = BeerType.Pilsner, Brewery = "Carlsberg", Percent = 5, Volume = 33 };
        Beer beer2 = new Beer() { BeerName = "EgonBeer", BeerType = BeerType.Münchener, Brewery = "Heineken", Percent = 8, Volume = 40 };
        Beer beer3 = new Beer() { BeerName = "Tuborg", BeerType = BeerType.Wienerdortmunder, Brewery = "AB Inbev", Percent = 3, Volume = 50 };
        Beer beer4 = new Beer() { BeerName = "JohnBeer", BeerType = BeerType.Porter, Brewery = "Sabmuller", Percent = 6, Volume = 60 };

        List<Beer> beers = new List<Beer>();

        beers.Add(beer1);
        beers.Add(beer2);
        beers.Add(beer3);
        beers.Add(beer4);

        Beer beer5 = beer1.Mix(beer2);

        beer2.Add(beer3);
        
        beer5 = Beer.Mix(beer3, beer4);

        beers.Add(beer5);
        
   

        Console.WriteLine("Øl sorteret på genstande: \n");
        beers.Sort(new SortingBeerBy(SortBy.UNIT));

        foreach (Beer beer in beers)
        {
            Console.WriteLine(beer.ToString());
        }
        Console.WriteLine("\n");
        Console.WriteLine("Øl sorteret på procent: \n");
        beers.Sort(new SortingBeerBy(SortBy.PERCENT));

        foreach (Beer beer in beers)
        {
            Console.WriteLine(beer.ToString());
        }
        Console.WriteLine("\n");
        Console.WriteLine("Øl sorteret på volume: \n");
        beers.Sort(new SortingBeerBy(SortBy.VOLUME));

        foreach (Beer beer in beers)
        {
            Console.WriteLine(beer.ToString());
        }

        Console.ReadLine();

    }
}
enum BeerType
{
    Lager,
    Pilsner,
    Münchener,
    Wienerdortmunder,
    Bock,
    Dobbelbock,
    Porter,
    Mix
}
class Beer : IComparable<Beer>
{
    string brewery;
    string beerName;
    BeerType beerType;
    int volume;
    decimal percent;

    public Beer()
    {
    }

    public Beer(string brewery, string beerName, BeerType beerType, int volume, decimal percent)
    {
        this.Brewery = brewery;
        this.BeerName = beerName;
        this.BeerType = beerType;
        this.Volume = volume;
        this.Percent = percent;
    }

    public string Brewery { get => brewery; set => brewery = value; }
    public string BeerName { get => beerName; set => beerName = value; }
    public int Volume { get => volume; set => volume = value; }
    public decimal Percent { get => percent; set => percent = value; }
    internal BeerType BeerType { get => beerType; set => beerType = value; }

    public decimal GetUnits()
    {
        return volume * percent / 150;
    }

    public override string? ToString()
    {
        return $"Brewery: {Brewery.ToString()} Beer Name: {BeerName.ToString()} Volume: {Volume.ToString()} Percent: {Percent.ToString()} BeerType: {BeerType.ToString()} Units: {Volume*percent/150}";
    }
    public Beer Add(Beer addedBeer)
    {
        return new Beer(Brewery = $"{this.Brewery + addedBeer.Brewery}",
                            BeerName = $"{this.BeerName + addedBeer.BeerName}",
                            BeerType = BeerType.Mix,
                            Volume = this.Volume + addedBeer.Volume,
                            Percent = (this.Percent*this.Volume + addedBeer.Percent * addedBeer.Volume) / (this.Volume + addedBeer.Volume));
    }
    public Beer Mix(Beer mixedBeer)
    {
        return new Beer
        {
            Brewery = $"{this.Brewery + mixedBeer.Brewery}",
            BeerName = $"{this.BeerName + mixedBeer.BeerName}",
            BeerType = BeerType.Mix,
            Volume = this.Volume + mixedBeer.Volume,
            Percent = ((this.Percent * this.Volume + mixedBeer.Percent * mixedBeer.Volume) / (this.Volume + mixedBeer.Volume))
        };
    }
    public static Beer Mix(Beer beer1, Beer beer2)
    {
        return new Beer
        {
            Brewery = $"{beer1.Brewery + beer2.Brewery}",
            BeerName = $"{beer1.BeerName + beer2.BeerName}",
            BeerType = BeerType.Mix,
            Volume = beer1.Volume + beer2.Volume,
            Percent = (beer1.Percent * beer1.Volume + beer2.Percent * beer2.Volume) / (beer1.Volume + beer2.Volume)
        };
    }

    public int CompareTo(Beer? other)
    {
        return (int)(this.Percent * this.Volume-other.Percent * other.Volume);
    }
}