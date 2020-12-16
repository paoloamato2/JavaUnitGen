using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestingCombinatoriale
{
    public static class JavaClassAnalyzer
    {

        public static string GetPackageLine(string sourceCode)
        {
            var retVal = "";
            var arr = sourceCode.Split(new[] {"\n"}, StringSplitOptions.None);
            foreach (var str in arr)
            {
                if (!str.Contains("package")) continue;
                retVal = str;
                break;
            }
            return retVal.Trim().Replace("\r","");

        }

        public static string GetPackageName(string packageLine)
        {
            if (packageLine=="")
            {
                return "";
            }
            var arr = packageLine.Split(' ');
            return arr.ToList().Last().Replace(";","");
        }
        public static string[] GetMethods(string sourceCode)
        {
            //var regex = new Regex(@"(public|protected|private|static|\s) +[\w\<\>\[\],\s]+\s+(\w+) *\([^\)]*\) *(\{?|[^;])");
           // var regex = new Regex(@"((public|private|protected|static|final|native|synchronized|abstract|transient)+\s)+[\$_\w\<\>\w\s\[\]]*\s*[\$_\w]+\([^\)]*\)?\s*");
            var regex = new Regex(@"((public|private|protected|static|final|native|synchronized|abstract|transient)+\s)+[\$_\w\<\>\w\s\[\]]*\s*[\$_\w]+\([^\)]*\)?\s*");

            var matches = regex.Matches(sourceCode).Cast<Match>().Select(m => m.Value).ToArray();
            for (var i = 0; i < matches.Length; i++)
            {
               
                var len = matches[i].Length;
                var arr = matches[i].Split(' ');
                var found = false;
                foreach (var str in arr)
                {
                    if (str != "private") continue;
                    matches[i] = "-.-";
                    found = true;
                }

                if (found)
                {
                    continue;
                }

                matches[i] = matches[i].Replace("\r", "").Replace("\n", "").Replace("\t", "");
            }
            return matches;
        }

        public static string[] GetParameters(string metodSig)
        {
            var str = metodSig.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            var parametri = str[1].Split(',');
            return parametri;
        }

        public static string GetClassName(string sourceCode)
        {
            var regex = new Regex(@"(?<=\n|\A)(?:public\s)?(class|interface|enum)\s([^\n\s]*)");
            string[] temp = sourceCode.Split('\n');
            for (var index = 0; index < temp.Length; index++)
            {
                temp[index] = temp[index].Trim();
            }

            sourceCode = string.Join("\n", temp);

            var matches = regex.Matches(sourceCode).Cast<Match>().Select(m => m.Value).ToArray();
            var arr = matches[0].Split(' ');
            return arr.ToList().Last();

        }

        public static string GetReturnType(string metodSig)
        {
            var str = metodSig.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            var arr = str[0].Split(' ');
            var len = arr.Length;
            return arr[len - 2];
        }

        public static Tuple<int,string,string> AnalizzaParametro(string parametro,int count)
        {
            parametro = parametro.Trim().Replace("final ","");
            var attributi = new List<string>();
            attributi = parametro.Split(' ').ToList();
            var tipo = attributi[0];
            var nome = attributi[1];
            return Tuple.Create(count, tipo, nome);

        }

        public static string GetMethodName(string metodSig)
        {
            var str = metodSig.Split('(');
            var arr = str[0].Split(' ');
            return arr.ToList().Last().Trim();
        }
    }
}
