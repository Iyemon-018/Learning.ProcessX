using Zx;
using static Zx.Env;

var console = ConsoleApp.Create(args);
console.AddCommands<Runner>();
console.Run();

public sealed class Runner : ConsoleAppBase
{
    // Learning.ProcessX.Runner.exe info --list-name list-sdks
    // <output>
    // 3.1.421 [C:\Program Files\dotnet\sdk]
    // 5.0.408 [C:\Program Files\dotnet\sdk]
    // 5.0.410 [C:\Program Files\dotnet\sdk]
    // 6.0.202 [C:\Program Files\dotnet\sdk]
    // 6.0.203 [C:\Program Files\dotnet\sdk]
    // 6.0.302 [C:\Program Files\dotnet\sdk]
    //
    // こっちのコマンドでもOK。
    // CI で使うのであればこっちのほうが良い。
    // dotnet run Learning.ProcessX.Runner.csproj -- info --list-name list-sdks
    public async Task Info(string listName)
    {
        // こんな感じで CI のアクションに相当するタスクを1メソッドで定義する。
        // CI で実行するタスクをこうやって量産することで、便利なアクションリスト プロジェクトが出来上がる。
        verbose = false;

        color(ConsoleColor.Cyan);

        var list = await runl($"dotnet --{listName}");
        Console.WriteLine(string.Join(Environment.NewLine, list));

        color(ConsoleColor.White);
    }
}