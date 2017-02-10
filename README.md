# ConcurrentHashSet
A ConcurrentHashSet implementation based on .NET's ConcurrentDictionary

This implementation supports basic operations per item without `HashSet`'s set operations as they make less sense in concurrent scenarios IMO:

    var concurrentHashSet = new ConcurrentHashSet<string>(
        new[]
        {
            "hamster",
            "HAMster",
            "bar",
        },
        StringComparer.OrdinalIgnoreCase);

    concurrentHashSet.TryRemove("foo");

    if (concurrentHashSet.Contains("BAR"))
    {
        Console.WriteLine(concurrentHashSet.Count);
    }

You can get it from NuGet [here](https://www.nuget.org/packages/ConcurrentHashSet/).
