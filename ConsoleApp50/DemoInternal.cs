using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ConsoleApp50Tests")]
namespace ConsoleApp50
{
    internal class DemoInternal
    {
        // InternalsVisibleToAttribute class
        // https://docs.microsoft.com/ja-jp/dotnet/api/system.runtime.compilerservices.internalsvisibletoattribute?view=net-5.0

        //見える
        public string GetFromPublic() => "public";

        //見える
        internal string GetFromInternal() => "internal";

        //見える
        protected internal string GetFromProtectedInternal() => "protected-internal";

        // 見えない
        protected string GetFromProtected() => "Protected";

        // 見えない
        private protected string GetFromPrivateProtected() => "private-protected";

        // 見えない
        private string GetFromPrivate() => "private";
    }
}