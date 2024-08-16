using Spectre.Console;

namespace Showyfx.Core;

/// <summary>
/// 控制台输出工具，对 Spectre.Console 的封装
/// </summary>
public static class ShowyCsr
{
    public interface IShowyConsole
    {
        Color Color { get; set; }
        void Write();
    }

    public static class Table
    {
        //readonly Spectre.Console.Table tableWriter = new() { ShowRowSeparators = true };

        public static void Write()
        {
            // tableWriter.BorderColor(Color.Yellow3_1);
            // AnsiConsole.Write(tableWriter);
        }
    }
}