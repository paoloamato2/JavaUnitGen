using System.Collections.Generic;
using System.Text;

namespace TestingCombinatoriale
{
    public class TestCase
    {
        public int Id;
        public List<JacocoLine> Lines;
        public string Valore;

        public TestCase(int id, string valore, List<JacocoLine> lines)
        {
            Id = id;
            Valore = valore;
            Lines = lines;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Test Case ID: " + Id);
            sb.AppendLine("Sequenza dei valori di input: " + Valore);
            foreach (var line in Lines) sb.AppendLine("Linea n. " + line.LineNumber + " coperta=" + line.Hit);

            return sb.ToString();
        }
    }
}