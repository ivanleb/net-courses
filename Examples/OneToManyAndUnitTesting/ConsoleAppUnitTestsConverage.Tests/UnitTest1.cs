using System;
using ConsoleAppUnitTestsCoverage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace ConsoleAppUnitTestsConverage.Tests
{
    [TestClass]
    public class ModuleATests
    {
        IAlphaWebClientRepository alphaWebClientRep;
        IAlphaJsonConvertRepository alphaJsonConvertRep;
        IAlphaConsoleRepository alphaConsoleRep;

        AlphaService alphaService;

        RootObject rootObject;

        [TestInitialize]
        public void TestSetup()
        {
            this.alphaWebClientRep = Substitute.For<IAlphaWebClientRepository>();
            this.alphaJsonConvertRep = Substitute.For<IAlphaJsonConvertRepository>();
            this.alphaConsoleRep = Substitute.For<IAlphaConsoleRepository>();

            this.alphaService = new AlphaService(this.alphaWebClientRep, this.alphaJsonConvertRep, this.alphaConsoleRep);

            this.rootObject = new RootObject()
            {
                RestResponse = new RestResponse()
                {
                    result = new System.Collections.Generic.List<Result>(),
                    messages = new System.Collections.Generic.List<string>()
                }
            };
        }

        [TestMethod]
        public void ShouldCallDownloadString()
        {          
            this.alphaJsonConvertRep.Parse(Arg.Any<string>()).Returns(rootObject);

            this.alphaService.Run();

            this.alphaWebClientRep.Received(1).DownloadString(Arg.Any<string>());
        }

        [TestMethod]
        public void ShouldDownloadStringBeforeParse()
        {
            this.alphaJsonConvertRep.Parse(Arg.Any<string>()).Returns(rootObject);

            this.alphaService.Run();

            Received.InOrder(() => 
            {
                this.alphaWebClientRep.DownloadString(Arg.Any<string>());
                this.alphaJsonConvertRep.Parse(Arg.Any<string>());
            });
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }
    }
}
