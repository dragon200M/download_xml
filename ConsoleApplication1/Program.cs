using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.Data.SqlClient;
using System.Dynamic;


namespace DatabaseUtils
{
    internal class Program
    {
        public static void Main(string[] args)
        {
           

        Config co = new Config();
        var cc =  co.GetParametry();
        Console.WriteLine(cc.ToString());
            
         Connection con = new Connection(cc);
            var w = con.chooseSqlStatement();
            var e = con.chooseSqlColumn();
            
            Console.WriteLine(w+" | "+e);
            con.getXmlFromDB();
            

        }
    }
    

    class Config
    {
        public Parametry GetParametry()
        {
            Parametry resoult = null;
            
            try
            {
                using (StreamReader sr = new StreamReader(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)+"\\config\\config.yaml"))
                {
                    
                    var input = sr.ReadToEnd();
                    
                    var deserializer = new DeserializerBuilder()
                        .WithNamingConvention(new CamelCaseNamingConvention()).Build();
                        
                    var rr = deserializer.Deserialize<Dokument>(input);

                    resoult = rr.Parametry;

                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Brak pliku config. ->"+ e);
            }

            return resoult;
        }

        
    }
    public class Dokument
    {
        public List<Konfiguracja> Konfiguracja { get; set; }
        public List<XMLType> Xmltype { get; set; }
        public Parametry Parametry { get; set; }
    }

    public class Parametry
    {
        public string Polaczenie { get; set; }
        public int Seria { get; set; }
        public string Rodzajxml { get; set; }
        public string Folderwynikowy { get; set; }
        public string Plikwynikowy { get; set; }

        public override string ToString()
        {
            return string.Format(@"Polaczenie: {0};
Seria: {1};
Rodzaj XML: {2};
Folder wynikowy: {3};
Plik wynikowy: {4};",Polaczenie,Seria,Rodzajxml,Folderwynikowy,Plikwynikowy);
        }
    }

    public class Konfiguracja
    {
        public string Link { get; set; }
    }


    public class XMLType
    {
        public string Item { get; set; }
    }

}