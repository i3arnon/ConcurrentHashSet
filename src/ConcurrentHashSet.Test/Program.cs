// See https://aka.ms/new-console-template for more information
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

Console.WriteLine("ConcurrentHashSet test now!");

try
{
    var watch1 = new Stopwatch();
    watch1.Start();

    int total1 = 0;
    ConcurrentHashSet<string> hashSet1 = new ConcurrentHashSet<string>();
    using (var sr = new StreamReader("stopwords_en_nltk.txt", Encoding.UTF8))
    {
        string? line = null;
        while ((line = sr.ReadLine()) != null)
        {
            if(!hashSet1.Contains(line))
                hashSet1.Add(line);
            System.Threading.Interlocked.Add(ref total1, 1);
        }
    }
    watch1.Stop();
    Console.WriteLine($"ConcurrentHashSet: stopwords_en_nltk.txt load finished, Count:{hashSet1.Count}, total:{total1}. time elapsed {watch1.ElapsedMilliseconds} ms");

    var watch2 = new Stopwatch();
    watch2.Start();
    int total2 = 0;
    ConcurrentHashSet<string> hashSet2 = new ConcurrentHashSet<string>();
    System.Threading.Tasks.Parallel.ForEach(File.ReadLines("stopwords_en_nltk.txt", Encoding.UTF8), (line, _, lineNumber) =>
    {
        if (!hashSet2.Contains(line))
            hashSet2.Add(line);
        System.Threading.Interlocked.Add(ref total2, 1);
    });

    watch2.Stop();
    Console.WriteLine($"ConcurrentHashSet: stopwords_en_nltk.txt load as parallel finished, Count:{hashSet2.Count}, total:{total2}. time elapsed {watch2.ElapsedMilliseconds} ms");

    var watch3 = new Stopwatch();
    watch3.Start();
    int total3 = 0;
    ConcurrentBag<string> bag = new ConcurrentBag<string>();
    System.Threading.Tasks.Parallel.ForEach(File.ReadLines("stopwords_en_nltk.txt", Encoding.UTF8), (line, _, lineNumber) =>
    {
        if (!bag.Contains(line))
            bag.Add(line);
        System.Threading.Interlocked.Add(ref total3, 1);
    });

    watch3.Stop();

    Console.WriteLine($"ConcurrentBag: stopwords_en_nltk.txt load as parallel finished, Count:{bag.Count}, total:{total3}. time elapsed {watch3.ElapsedMilliseconds} ms");

    Console.WriteLine("Press any key to exit.");
}
catch (IOException e)
{
    Console.WriteLine(string.Format("{0} load failure, reason: {1}", "stopwords_en_nltk.txt", e.Message));
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

Console.ReadKey();
