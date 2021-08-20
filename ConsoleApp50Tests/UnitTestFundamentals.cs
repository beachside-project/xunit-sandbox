using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using Xunit;
using Xunit.Abstractions;

namespace ConsoleApp50Tests
{
    /// <summary>
    /// XUnit でコードの構成などで知っておきたいこと書いてみる
    /// </summary>
    public class UnitTestFundamentals
    {
        
        private readonly ITestOutputHelper _output;
        
        private static string StaticString = "Hello Static";
        private string _instanceString;



        public UnitTestFundamentals(ITestOutputHelper output)
        {
            _output = output;
            // constructor はテスト毎に毎回呼ばれるので、共通のインスタンスはここで書いておくとよい
            // 各メソッド独自の setup とかをそれぞれのメソッドで書くイメージ


            // つまり
            // static の object はテスト間で共有されるので注意。
            // non-static な object はテスト間で共有されない。

            _instanceString = "Hello from CTOR";


        }

        [Fact]
        public void Test1()
        {
            _output.WriteLine($"StaticString: {StaticString}");
            _output.WriteLine($"_instanceString: {_instanceString}");

            StaticString = "Update by Test1";
            _instanceString = "Update by Test1";

            _output.WriteLine($"StaticString: {StaticString}");
            _output.WriteLine($"_instanceString: {_instanceString}");
        }


        [Fact]
        public async Task Test12()
        {
            await Task.Delay(10000);
            _output.WriteLine($"StaticString: {StaticString}");
            _output.WriteLine($"_instanceString: {_instanceString}");

            StaticString = "Update by Test2";
            _instanceString = "Update by Test2";

            _output.WriteLine($"StaticString: {StaticString}");
            _output.WriteLine($"_instanceString: {_instanceString}");
        }

    }
}
