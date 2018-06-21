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
            LogHelper.LogWriter("ProgramMain","哈哈，这是一个测试信息","FirstLogPath");
            LogHelper.LogWriterFolder("ProgramMain","哈哈，这是一个测试信息again","FirstFolder", "FirstLogPath");
            Console.ReadKey();
        }
    }
}