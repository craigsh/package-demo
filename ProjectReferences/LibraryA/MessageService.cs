using NuGetDemo.LibraryB;

namespace NuGetDemo.LibraryA;

public static class MessageService
{
    public static void PrintMessage() => MessagePrinter.Print();
}
