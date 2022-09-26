``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.856/21H2)
Intel Core i5-10400 CPU 2.90GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.304
  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2  [AttachedDebugger]
  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2


```
|                  Method |     Mean |   Error |   StdDev | Ratio |
|------------------------ |---------:|--------:|---------:|------:|
| StringConcatenationTest | 475.1 ns | 9.32 ns | 11.09 ns |  1.00 |
|       StringBuilderTest | 351.1 ns | 5.76 ns |  4.81 ns |  0.74 |
