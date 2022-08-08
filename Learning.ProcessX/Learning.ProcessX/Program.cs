// See https://aka.ms/new-console-template for more information

using Cysharp.Diagnostics;
using Zx;
using static Zx.Env;

// 標準出力の切り替え。
// verbose = false にするとコマンド呼び出し時の結果が出力されなくなる。
// デフォルトは true
verbose = false;

Console.WriteLine("Begin ProcessX!");

var gitVersion = await "git --version";
Console.WriteLine($"Git is Ver.{gitVersion}");

// Zx.Env でスリープや文字色の変更などもできる。
// cf. https://github.com/Cysharp/ProcessX/blob/master/src/ProcessX/Zx/Env.cs
await sleep(TimeSpan.FromSeconds(3));

color(ConsoleColor.Green);

var currentBranch = await "git branch --show-current";
Console.WriteLine($"Current branch is {currentBranch}.");

color(ConsoleColor.White);

//　こっちは Task<string[]> で出力結果を受け取るパターン
var l1 = await runl($"dotnet --list-sdks");
Console.WriteLine(string.Join(Environment.NewLine, l1));

// こっちは Task<string> で出力結果を受け取るパターン
var t = await process("dotnet --info");
Console.WriteLine(t);

color(ConsoleColor.Red);

// ignore とすることで ProcessErrorException をスローされないようにする。
// エラー出力を受け取るときには便利。
// また、エラー出力を受け取る場合は verbose = true としないと結果が出力できない。
verbose = true;
await ignore(run($"dotnet noinfo"));
try
{
    // stderror を受け取りたいけどこれだと受け取れない。
    // 書き方が悪い？
    var (stdout, stderror) = await run2($"dotnet --list--sdk");
    Console.WriteLine(string.Join(Environment.NewLine, stderror));
}
catch (ProcessErrorException ex)
{
    Console.WriteLine(string.Join(Environment.NewLine, ex.ErrorOutput));
}

color(ConsoleColor.White);

