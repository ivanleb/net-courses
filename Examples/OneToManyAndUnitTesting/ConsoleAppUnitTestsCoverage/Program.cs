using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppUnitTestsCoverage
{
    public interface IAlphaWebClientRepository
    {
        string DownloadString(string uri);
    }

    public class AlphaWebClientRepository : IAlphaWebClientRepository
    {
        public string DownloadString(string uri)
        {
            using (var wc = new WebClient())
            {
                var json = wc.DownloadString(uri);

                return json;
            }
        }
    }

    public interface IAlphaJsonConvertRepository
    {
        RootObject Parse(string json);
    }

    public class AlphaJsonConvertRepository : IAlphaJsonConvertRepository
    {
        public RootObject Parse(string json)
        {
            return JsonConvert.DeserializeObject<RootObject>(json);
        }
    }

    public interface IAlphaConsoleRepository
    {
        void WriteLine(string text);

        void WriteLine(int number);

        int Read();
    }

    public class AlphaConsoleRepository : IAlphaConsoleRepository
    {
        public int Read()
        {
           return Console.Read();
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void WriteLine(int number)
        {
            Console.WriteLine(number);
        }
    }

    public class AlphaService
    {
        private readonly IAlphaWebClientRepository alphaWebClientRepository;
        private readonly IAlphaJsonConvertRepository alphaJsonConvertRep;
        private readonly IAlphaConsoleRepository alphaConsoleRepository;

        public AlphaService(
            IAlphaWebClientRepository alphaWebClientRepository,
            IAlphaJsonConvertRepository alphaJsonConvertRep,
            IAlphaConsoleRepository alphaConsoleRepository
            )
        {
            this.alphaWebClientRepository = alphaWebClientRepository;
            this.alphaJsonConvertRep = alphaJsonConvertRep;
            this.alphaConsoleRepository = alphaConsoleRepository;
        }

        public void Run()
        {
            // +++
            var uri = "http://get/all";

            var json = string.Empty;

            

            var obj = alphaJsonConvertRep.Parse(json);

            json = alphaWebClientRepository.DownloadString(uri);

            alphaConsoleRepository.WriteLine(obj.RestResponse.result.Count);
            alphaConsoleRepository.WriteLine(string.Empty);
            alphaConsoleRepository.WriteLine("Press any key");

            alphaConsoleRepository.Read();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var alphaWebClientRep = new AlphaWebClientRepository();
            var alphaJsonConvertRep = new AlphaJsonConvertRepository();
            var alphaConsoleRep = new AlphaConsoleRepository();

            var alphaService = new AlphaService(alphaWebClientRep, alphaJsonConvertRep, alphaConsoleRep);

            alphaService.Run();

            //using (var wc = new WebClient())
            //{
            //    var json = wc.DownloadString(uri);
            //    var obj = JsonConvert.DeserializeObject<RootObject>(json);

            //    Console.WriteLine(obj.RestResponse.result.Count);

            //    Console.WriteLine(string.Empty);
            //    Console.WriteLine("Press any key");

            //    Console.Read();
            //}
        }
    }

    public class Result
    {
        public string name { get; set; }
        public string alpha2_code { get; set; }
        public string alpha3_code { get; set; }
    }

    public class RestResponse
    {
        public List<string> messages { get; set; }
        public List<Result> result { get; set; }
    }

    public class RootObject
    {
        public RestResponse RestResponse { get; set; }
    }

}
