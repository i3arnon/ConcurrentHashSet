# ConcurrentHashSet

[![NuGet](https://img.shields.io/nuget/dt/ConcurrentHashSet.svg)](https://www.nuget.org/packages/ConcurrentHashSet)
[![NuGet](https://img.shields.io/nuget/v/ConcurrentHashSet.svg)](https://www.nuget.org/packages/ConcurrentHashSet)
[![license](https://img.shields.io/github/license/i3arnon/ConcurrentHashSet.svg)](LICENSE)

A ConcurrentHashSet implementation based on .NET's ConcurrentDictionary

This implementation supports basic operations per item without `HashSet`'s set operations as they make less sense in concurrent scenarios IMO:

```csharp
var concurrentHashSet =
    new ConcurrentHashSet<string>(
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
```