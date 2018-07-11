using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary;

namespace TestLogDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("starting log");
            LogHelper.LogWriter("ProgramMainc", "哈哈，这是一个测试信息asdfaasdasd", "FirstLogPath");
            LogHelper.LogWriterFolder("ProgramMainc", "哈哈，这是一个测试信息asdasdzzxxcagain", "FirstFolder", "FirstLogPath");
            LogHelper.LogWriterFolder("ProgramMaincd", "ccc哈哈，这是一个测试信息asdasdzzxxcagain", "FirstFolder", "FirstLogPath2");
            Console.ReadKey();
        }
    }
}