using Microsoft.Web.XmlTransform;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TransformXml
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("args[0] source file name, args[1] transform working directory path, args[2] configuration, defaults web.config executed path Release");
            Console.WriteLine(@"Sample : TransformXml.exe web.config D:\projects\TestProject\WebSite1\Release");
            TransformXmlStandalone transform = new TransformXmlStandalone();
            string source = string.Empty;
            string path = string.Empty;
            string configuration = string.Empty;
            if (args.Length == 0)
            {
                source = "Web.Config";
                path = AppDomain.CurrentDomain.BaseDirectory;
                configuration = "Release";
            }
            if (args.Length == 1)
            {
                source = args[0];
                path = AppDomain.CurrentDomain.BaseDirectory;
                configuration = "Release";
            }
            if (args.Length == 2)
            {
                source = args[0];
                path = args[1];
                configuration = "Release";
            }
            if (args.Length == 3)
            {
                source = args[0];
                path = args[1];
                configuration = args[2];
            }
            var info = new FileInfo(source);
            transform.Source = source;
            transform.Transform = Path.Combine(path, string.Format("{0}.{1}{2}", Path.GetFileNameWithoutExtension(source), configuration, info.Extension));
            transform.Destination = Path.Combine(path, source);
            transform.SourceRootPath = path;
            var result = transform.TransformXml();

        }

    }
}
