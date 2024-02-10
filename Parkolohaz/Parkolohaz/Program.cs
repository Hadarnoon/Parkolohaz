using Parkolohaz;
using System.Linq.Expressions;

var emeletek = new List<Emelet>();
using var sr = new StreamReader(@"..\..\..\src\parkolohaz.txt");
try
{
	while (!sr.EndOfStream)
	{
		emeletek.Add(new Emelet(sr.ReadLine()));
	}
}
catch
{
    Console.WriteLine("Hiba a fájl beolvasása során");
}

static void Kiiras(List<Emelet> a)
{
    Console.WriteLine("Szint neve 1. szektor 2. szektor 3. szektor 4. szektor 5. szektor 6. szektor\n");
    a.Select((f,i) => $"{i + 1}.szint {f}").ToList().ForEach(x => Console.WriteLine(x));
}

static string LegkevesebbAuto(List<Emelet> a)
{
	var legkevesebbautoemelet = a.OrderBy(e => e.szamadatok.Sum()).First().Szintneve;
	return legkevesebbautoemelet;
}

static List<Tuple<string, int>> Aholnincsauto(List<Emelet> a)
{
    var aholnincsauto = new List<Tuple<string, int>>();
    foreach (var emelet in a)
    {
        for (int i = 0; i < emelet.szamadatok.Count; i++)
        {
            if (emelet.szamadatok[i] == 0)
            {
                aholnincsauto.Add(Tuple.Create(emelet.Szintneve, i + 1));
            }
        }
    }
    return aholnincsauto;
}

static void Atlag(List<Emelet> a)
{
	var atlag = a.SelectMany(e => e.szamadatok).Average();
    Math.Round(atlag, 2);

    var atlagosautok = a.SelectMany(e => e.szamadatok).Count(e => e == atlag);
    var atlagfolottiautok = a.SelectMany(e => e.szamadatok).Count(e => e > atlag);
    var atlagalattiautok = a.SelectMany(e => e.szamadatok).Count(e => e < atlag);

    Console.WriteLine($"Átlagos autók száma: {atlagosautok}");
    Console.WriteLine($"Átlag fölötti autók száma: {atlagfolottiautok}");
    Console.WriteLine($"Átlag alatti autók száma: {atlagalattiautok}");
}

static void EgyAutokiiras(List<Emelet> a)
{
    using var sw = new StreamWriter(@"..\..\..\src\feladat11.txt");
    foreach (var emelet in a)
    {
        var egyAutoSzektorok = emelet.szamadatok
            .Select((szektor, index) => new {Szektor = szektor, Sorszam = index + 1})
            .Where(szektor => szektor.Szektor == 1)
            .Select(szektor => szektor.Sorszam);

        if (egyAutoSzektorok.Any())
        {
            sw.WriteLine($"{emelet.Szintneve} - {string.Join("-", egyAutoSzektorok)}");
        }
    }
    Console.WriteLine("Fájl létrehozva");
}

static void LegFelsoLegtobb(List<Emelet> a)
{
    var legtobbauto = a.Max(e => e.szamadatok.Sum());
    var legtobbautoemelet = a.First(e => e.szamadatok.Sum() == legtobbauto).Szintneve;
    var legfelsoemelet = a.Last().Szintneve;

    if (legtobbautoemelet == legfelsoemelet)
    {
        Console.WriteLine("A legfelső emeleten van a legtöbb autó");
    }
    else
    {
        Console.WriteLine($"A legtöbb autó a(z) {legtobbautoemelet} emeleten van");
    }
}

static void SzabadHelyekSzama(List<Emelet> a)
{
    var maxauto = 15 * 6;
    using (var sw = new StreamWriter(@"..\..\..\src\feladat11.txt", true))
    {
        sw.WriteLine($"\n13.feladat megoldása:");
        for (int i = 0; i < a.Count; i++)
        {
            var autokszama = a[i].szamadatok.Sum();
            var szabadhely = maxauto - autokszama;
            sw.WriteLine($"{i + 1}. {szabadhely}");
            ////sw.Write($"{i + 1}. ");
            ////for (int j = 0; j < a[i].szamadatok.Count; j++)
            ////{
            ////    var autokszama = a[i] 
            ////}
        }
        Console.WriteLine("kiírva");
    }
}

static int SzabadHelyOsszesen(List<Emelet> a)
{
    var maxkapacitas = 6 * 12 * 15;
    var foglalthelyosszesen = a.SelectMany(e => e.szamadatok).Sum();
    var szabadhelyek = maxkapacitas - foglalthelyosszesen;
    return szabadhelyek;
}



Kiiras(emeletek);

Console.WriteLine("\n8.feladat: ");
var legkevesebbautoemelet = LegkevesebbAuto(emeletek);
Console.WriteLine(legkevesebbautoemelet);

Console.WriteLine("\n9.feladat: ");
var aholnincsauto = Aholnincsauto(emeletek);
if (aholnincsauto.Any())
{
    foreach (var item in aholnincsauto)
    {
        Console.WriteLine($"{item.Item2}. {item.Item1}");
    }
}
else
{
    Console.WriteLine("Nincs olyan szektor ahol ne lettek volna");
}
Console.WriteLine("\n10.feladat: ");
Atlag(emeletek);

Console.WriteLine("\n11.feladat: ");
EgyAutokiiras(emeletek);

Console.WriteLine("\n12.feladat: ");
LegFelsoLegtobb(emeletek);

Console.WriteLine("\n13.feladat: ");
SzabadHelyekSzama(emeletek);

Console.WriteLine("\n14.feladat: ");
var szabadhelyek = SzabadHelyOsszesen(emeletek);
Console.WriteLine($"Az maradék szabad helyek száma: {szabadhelyek}");