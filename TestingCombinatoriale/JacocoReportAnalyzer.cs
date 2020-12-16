using System;
using System.Collections.Generic;
using System.Xml;

namespace TestingCombinatoriale
{
    public static class JacocoReportAnalyzer
    {
        public static List<JacocoLine> ParseXml(string jacocoXml, string className, string methodName)
        {
            var lines = new List<JacocoLine>();
            var sourcefile = GetSourceFileTag(jacocoXml, className);


            var lineaIniziale = GetMethodBound(jacocoXml, className, methodName, out var lineaFinale);

            if (lineaFinale < lineaIniziale) lineaFinale = int.MaxValue;

            if (sourcefile == null) return null;

            var count = 0;
            foreach (XmlNode node in sourcefile)
                if (node.Name == "line")
                {
                    var nr = Convert.ToInt32(node.Attributes[0].Value);

                    if (!(nr >= lineaIniziale && nr < lineaFinale)) continue;

                    var mi = Convert.ToInt32(node.Attributes[1].Value);
                    var ci = Convert.ToInt32(node.Attributes[2].Value);
                    var mb = Convert.ToInt32(node.Attributes[3].Value);
                    var cb = Convert.ToInt32(node.Attributes[4].Value);
                    var jl = new JacocoLine(count, nr, mi, ci, mb, cb);
                    lines.Add(jl);
                    count++;
                }

            return lines;
        }

        private static int GetMethodBound(string jacocoXml, string className, string methodName, out int lineaFinale)
        {
            var doc = new XmlDocument();
            doc.LoadXml(jacocoXml);

            var nodes1 = doc.GetElementsByTagName("class");
            XmlNodeList nodes = null;

            foreach (XmlNode node in nodes1)
            {
                var nome = node.Attributes[0].Value;
                if (nome == className.Replace(".java", "")) nodes = node.ChildNodes;
            }

            // XmlNodeList nodes = doc.GetElementsByTagName("method");

            var lineaIniziale = 0;
            lineaFinale = int.MaxValue;


            for (var index = 0; index < nodes.Count; index++)
            {
                var node = nodes[index];

                var nome = node.Attributes[0].Value; //nome
                lineaIniziale = Convert.ToInt32(node.Attributes[2].Value); //id linea iniziale

                if (nome != methodName) continue;
                if (index + 1 >= nodes.Count) continue;
                var str = nodes[index + 1].Attributes[0].Value;
                var str2 = nodes[index + 1].Name;
                if (str2 == "method") lineaFinale = Convert.ToInt32(nodes[index + 1].Attributes[2].Value);

                break;
            }

            return lineaIniziale;
        }

        private static XmlNode GetSourceFileTag(string jacocoXml, string className)
        {
            var doc = new XmlDocument();
            doc.LoadXml(jacocoXml);
            var nodes = doc.GetElementsByTagName("sourcefile");
            XmlNode sourcefile = null;
            foreach (XmlNode node in nodes)
            {
                var nodeValue = node.Attributes[0].Value;
                if (nodeValue != className) continue;
                sourcefile = node;
                break;
            }

            return sourcefile;
        }
    }
}