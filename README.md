Jasily.EverythingSDK
===

a nice C# wrapper for Everything binary SDK.

how to use
---

``` cs
var search = new EverythingSearch();
var result = search.Search("abc", 100, 4);
foreach (var path in result)
{
    Console.WriteLine(path);
}
```

ref dll
---

#### Everything32.dll

downloaded from http://www.voidtools.com/support/everything/sdk/ on 2015-11-15.