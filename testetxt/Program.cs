using System;
using System.IO;
using static System.IO.Stream;
using static System.Console;

string path = @"C:\Code";
Write("Press R for read-only or W for writeable: ");

ConsoleKeyInfo key = ReadKey();
WriteLine();
Stream? s;
if (key.Key == ConsoleKey.R)
{
    s = File.Open(
    Path.Combine(path, "file.txt"),
    FileMode.OpenOrCreate,
    FileAccess.Read);
}
else
{
    s = File.Open(
    Path.Combine(path, "file.txt"),
    FileMode.OpenOrCreate,
    FileAccess.Write);
}
string message;
switch (s)
{
    case FileStream writeableFile when s.CanWrite:
        message = "The stream is a file that I can write to.";
        break;
    case FileStream readOnlyFile:
        message = "The stream is a read-only file.";
        break;
    case MemoryStream ms:
        message = "The stream is a memory address.";
        break;
    default: // always evaluated last despite its current position
        message = "The stream is some other type.";
        break;
    case null:
        message = "The stream is null.";
        break;
}
WriteLine(message);