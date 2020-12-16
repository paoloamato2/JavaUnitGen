using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace TestingCombinatoriale
{
    public partial class TestingCombinatorio : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public string CTWedgeModel = "";
        public string percorsoBin = "";

        public string percorsoJUnit = "";

        public string percorsoSrc = "";

        private int timeout;

        public TestingCombinatorio()
        {
            InitializeComponent();
        }

        private static DialogResult ShowInputDialog(ref string input, string title)
        {
            var size = new Size(600, 300);
            var inputBox = new Form
            {
                FormBorderStyle = FormBorderStyle.FixedDialog, ClientSize = size, Text = title
            };


            var textBox = new TextBox
            {
                Size = new Size(size.Width - 10, 230),
                Location = new Point(5, 5),
                Text = input,
                Multiline = true
            };
            inputBox.Controls.Add(textBox);

            var okButton = new Button
            {
                DialogResult = DialogResult.OK,
                Name = "okButton",
                Size = new Size(75, 23),
                Text = "&OK",
                Location = new Point(size.Width - 80 - 80, 270)
            };
            inputBox.Controls.Add(okButton);

            var cancelButton = new Button
            {
                DialogResult = DialogResult.Cancel,
                Name = "cancelButton",
                Size = new Size(75, 23),
                Text = "&Cancel",
                Location = new Point(size.Width - 80, 270)
            };
            inputBox.Controls.Add(cancelButton);

            // inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;
            textBox.Focus();

            var result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private void TestingCombinatorio_Load(object sender, EventArgs e)
        {
            LoadParallelismOptions();
            CopyJavaTools();
            ClearCombobox();
        }

        private void LoadParallelismOptions()
        {
            cbParallelismo.Items.Add(new ComboBoxItem(-1, "Seleziona"));
            cbParallelismo.SelectedIndex = 0;
            for (var i = 1; i <= 3; i++) cbParallelismo.Items.Add(new ComboBoxItem(i, i.ToString()));
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void caricaClasseJavaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogClass.ShowDialog() != DialogResult.OK) return;
            ClearModel();
            ClearCombobox();
            var path = openFileDialogClass.FileName;
            var sourceCode = File.ReadAllText(path);
            var methods = JavaClassAnalyzer.GetMethods(sourceCode).ToList();
            var count = 0;
            foreach (var cbItem in from method in methods where method != "-.-" select new ComboBoxItem(count, method))
            {
                cbMetodi.Items.Add(cbItem);
                count++;
            }

            cbMetodi.Enabled = true;
            lblNomeClasse.Text = "Nome classe: " + JavaClassAnalyzer.GetClassName(sourceCode);
            MessageBox.Show("File caricato!");
        }

        private void ClearModel()
        {
            CTWedgeModel = "";
        }

        private void ClearCombobox()
        {
            cbMetodi.Items.Clear();
            var cbItem = new ComboBoxItem(-1, "Seleziona metodo");
            cbMetodi.Items.Add(cbItem);
            cbMetodi.SelectedIndex = 0;
        }

        private void btnProcedi_Click(object sender, EventArgs e)
        {
            if (InputValidation()) return;

            if (cbTimeout.Checked) timeout = Convert.ToInt32(txtTimeout.Text);

            CleanTempFiles();

            var sCode = File.ReadAllText(openFileDialogClass.FileName);
            var packageLine = JavaClassAnalyzer.GetPackageLine(sCode);
            if (packageLine != "")
            {
                var sCodeMod = sCode.Replace(packageLine, " ");
                File.WriteAllText(openFileDialogClass.FileName, sCodeMod);
            }


            var selectedItem = cbMetodi.SelectedItem as ComboBoxItem;
            var metodo = selectedItem.Nome;

            var sb = CreaModello(metodo);


            var modello = sb.ToString();


            var k = Convert.ToInt32(txtK.Text);
            var CTanswer = InviaRichiesta(modello, k.ToString());


            SalvaCsv(CTanswer);
            MessageBox.Show("File data.csv generato con successo!");


            SalvaClasseTest(metodo);
            MessageBox.Show("Classe di test generata con successo!");


            GeneraOutputJacocoNoMin();
            MessageBox.Show("Output Jacoco senza minimizzazione generato con successo!");


            var start = MinimizzaTestSuite(out var testSuite, out var testSuiteRidotta, out var end);

            GeneraOutputJacocoMin();


            File.WriteAllText(openFileDialogClass.FileName, sCode);

            VisualizzaOutput(testSuite, testSuiteRidotta, end, start);

            var dialogResult =
                MessageBox.Show(
                    "Vuoi modificare i risultati attesi per le sequenze di input appartenenti alla test suite minimizzata?",
                    "Attenzione", MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes) return;
            var path = Directory.GetCurrentDirectory() + @"\dataMin.csv";
            var data = File.ReadAllText(path);

            ShowInputDialog(ref data, "Modifica di \"dataMin.csv\"");

            File.WriteAllText(path, data);
            MessageBox.Show("\"dataMin.csv\" aggiornato con successo!");
        }

        private static void VisualizzaOutput(IReadOnlyCollection<TestCase> testSuite,
            IReadOnlyCollection<TestCase> testSuiteRidotta, DateTime end, DateTime start)
        {
            MessageBox.Show("Output Jacoco con minimizzazione generato con successo!");

            var message = new StringBuilder();
            message.AppendLine("--------------------");
            message.AppendLine("La test suite non minimizzata ha " + testSuite.Count + " casi di test");
            message.AppendLine("--------------------");
            message.AppendLine("La test suite minimizzata ha " + testSuiteRidotta.Count + " casi di test");
            message.AppendLine("--------------------");
            message.AppendLine("Tempo totale impiegato per la minimizzazione: " + (end - start).TotalSeconds +
                               " secondi");
            message.AppendLine("--------------------");
            message.AppendLine(
                "\"coperturaNoMin\" è la cartella contenente il report di copertura prodotto da Jacoco prima della minimizzazione.");
            message.AppendLine(
                "\"dataNoMin.csv\" è il file contenente l'insieme degli input prima della minimizzazione.");
            message.AppendLine("--------------------");
            message.AppendLine(
                "\"coperturaConMin\" è la cartella contenente il report di copertura prodotto da Jacoco dopo la minimizzazione.");
            message.AppendLine("\"dataMin.csv\" è il file contenente l'insieme degli input dopo la minimizzazione.");
            message.AppendLine("--------------------");

            MessageBox.Show(message.ToString());
        }

        private DateTime MinimizzaTestSuite(out List<TestCase> testSuite, out List<TestCase> testSuiteRidotta,
            out DateTime end)
        {
            var start = DateTime.Now;

            testSuite = CreaMatrice();

            //foreach (var tc in testSuite)
            //{
            //    MessageBox.Show(tc.ToString());
            //}

            var numRighe = testSuite.Count;
            var numColonne = testSuite[0].Lines.Count;

            var matrice = new int[numRighe, numColonne];

            // StringBuilder ts = new StringBuilder();

            for (var i = 0; i < testSuite.Count; i++)
            {
                var tc = testSuite[i];
                for (var j = 0; j < tc.Lines.Count; j++)
                {
                    var line = tc.Lines[j];
                    if (line.Hit) matrice[i, j] = 1;
                }
            }

            print(matrice);
            var temp = Minimizzazione.Minimizza(matrice);
            testSuiteRidotta = new List<TestCase>();

            foreach (var t in temp)
            {
                //var elem = testSuite.Single(x => x.id == t);
                var elem = testSuite[t];
                testSuiteRidotta.Add(elem);
            }

            var tsR = new StringBuilder();

            foreach (var tc in testSuiteRidotta) tsR.AppendLine(tc.Valore);

            File.WriteAllText(percorsoBin + @"\data.csv", tsR.ToString());
            File.WriteAllText(Directory.GetCurrentDirectory() + @"\dataMin.csv", tsR.ToString());

            end = DateTime.Now;
            return start;
        }

        private bool InputValidation()
        {
            if (!CheckPaths()) return true;

            if (!CheckKSelected()) return true;

            if (!CheckSelectedMethod())
            {
                MessageBox.Show("Devi selezionare un metodo dalla lista!");
                return true;
            }

            if (!CheckSelectedParallellism())
            {
                MessageBox.Show("Devi selezionare il numero di threads!");
                return true;
            }

            return false;
        }

        private bool CheckPaths()
        {
            try
            {
                if (!Directory.Exists(txtSrc.Text) || !Directory.Exists(txtBin.Text))
                {
                    MessageBox.Show(
                        "Controllare che il percorso dei file sorgenti e il percorso in cui inserire i file .class siano effettivamente esistenti.");
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Controllare che il percorso dei file sorgenti e il percorso in cui inserire i file .class siano effettivamente esistenti.");
                return false;
            }

            return true;
        }

        private bool CheckKSelected()
        {
            try
            {
                Convert.ToInt32(txtK.Text);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Il parametro K non è valido");
                return false;
            }
        }

        private bool CheckSelectedParallellism()
        {
            var cbItem = cbParallelismo.SelectedItem as ComboBoxItem;
            return cbItem.Id != -1;
        }

        public static void DeleteDirectory(string target_dir)
        {
            var files = Directory.GetFiles(target_dir);
            var dirs = Directory.GetDirectories(target_dir);

            foreach (var file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (var dir in dirs) DeleteDirectory(dir);

            Directory.Delete(target_dir, false);
        }

        private static void CleanTempFiles()
        {
            try
            {
                var path = Directory.GetCurrentDirectory();
                File.Delete(path + @"\data.csv");
                File.Delete(path + @"\jacoco.exec");
                DeleteDirectory(path + @"\temp\");
                DeleteDirectory(path + @"\coperturaNoMin\");
                DeleteDirectory(path + @"\coperturaConMin\");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void GeneraOutputJacocoMin()
        {
            var className = lblNomeClasse.Text.Substring(13);
            var testClassName = "Test" + className;


            var srcPath = percorsoSrc;
            var binPath = percorsoBin;

            File.Delete(Directory.GetCurrentDirectory() + @"\jacoco.exec");
            File.Delete(binPath + @"\jacoco.exec");

            var currentUser = Environment.UserName;


            var command = "";


            command =
                @"/C java -javaagent:C:\javatools\jacocoagent.jar -jar ""C:\javatools\junit.jar"" --classpath=""C:\javatools\jacocoagent.jar;" +
                binPath + @""" --scan-classpath";

            // System.Diagnostics.Process.Start("CMD.exe", command).WaitForExit();

            new ShellCommand(command).ExecuteCommand();

            var jacocoPath = Directory.GetCurrentDirectory();

            File.Copy(jacocoPath + @"\jacoco.exec", binPath + @"\jacoco.exec", true);

            //Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\coperturaNoMin");

            command = @"/C java -jar ""C:\javatools\jacococli.jar"" report """ + binPath + @"\jacoco.exec" +
                      @""" --classfiles """ + binPath + @""" --sourcefiles """ + srcPath +
                      @""" --html """ + Directory.GetCurrentDirectory() + @"\coperturaConMin" + @"""";
            // System.Diagnostics.Process.Start("CMD.exe", command).WaitForExit();

            new ShellCommand(command).ExecuteCommand();


            command = @"/C java -jar ""C:\javatools\jacococli.jar"" report """ + binPath + @"\jacoco.exec" +
                      @""" --classfiles """ + binPath + @""" --sourcefiles """ + srcPath +
                      @""" --xml """ + Directory.GetCurrentDirectory() + @"\coperturaConMin\jacoco.xml" + @"""";
            //  System.Diagnostics.Process.Start("CMD.exe", command).WaitForExit();

            new ShellCommand(command).ExecuteCommand();
        }

        public void print(int[,] arr)
        {
            var rowLength = arr.GetLength(0);
            var colLength = arr.GetLength(1);

            for (var i = 0; i < rowLength; i++)
            {
                for (var j = 0; j < colLength; j++) Console.Write($"{arr[i, j]} ");
                Console.Write(Environment.NewLine + Environment.NewLine);
            }

            Console.ReadLine();
        }

        private List<TestCase> CreaMatrice()
        {
            var testSuite = new List<TestCase>();

            var className = lblNomeClasse.Text.Substring(13) + ".java";
            var methodName = JavaClassAnalyzer.GetMethodName(cbMetodi.SelectedItem.ToString());

            var csvlines = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\data.csv");
            File.WriteAllLines(Directory.GetCurrentDirectory() + @"\dataNoMin.csv", csvlines);

            var count = 0;

            //foreach (var csvline in csvlines)
            //{
            //    int id = count;
            //    string valore = csvline;
            //    GeneraCoverageTestCase(csvline,id);
            //    string jacocoXML = File.ReadAllText(Directory.GetCurrentDirectory() + @"\temp\"+id+@"\jacoco.xml");
            //    List<JacocoLine> lines = JacocoReportAnalyzer.parseXML(jacocoXML, className, methodName);
            //    TestCase tc = new TestCase(id, valore, lines);
            //    testSuite.Add(tc);
            //    count++;
            //}


            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = Convert.ToInt32(cbParallelismo.SelectedItem.ToString())
            };

            Parallel.For(0, csvlines.Length, options, index =>
            {
                var csvline = csvlines[index];
                var id = index;
                var valore = csvline;
                GeneraCoverageTestCase(csvline, id);
                var jacocoXML = File.ReadAllText(Directory.GetCurrentDirectory() + @"\temp\" + id + @"\jacoco.xml");
                var lines = JacocoReportAnalyzer.ParseXml(jacocoXML, className, methodName);
                var tc = new TestCase(id, valore, lines);
                testSuite.Add(tc);
            });

            //for (var index = 0; index < csvlines.Length; index++)
            //{
            //    var csvline = csvlines[index];
            //    int id = count;
            //    string valore = csvline;
            //    GeneraCoverageTestCase(csvline, id);
            //    string jacocoXML = File.ReadAllText(Directory.GetCurrentDirectory() + @"\temp\" + id + @"\jacoco.xml");
            //    List<JacocoLine> lines = JacocoReportAnalyzer.parseXML(jacocoXML, className, methodName);
            //    TestCase tc = new TestCase(id, valore, lines);
            //    testSuite.Add(tc);
            //    count++;
            //}

            return testSuite;
        }

        private void GeneraCoverageTestCase(string csvline, int id)
        {
            var className = lblNomeClasse.Text.Substring(13);
            var testClassName = "Test" + className;

            var srcPath = percorsoSrc;


            var binPath = percorsoBin;

            // File.WriteAllText(binPath+@"\data.csv", csvline);


            var currentUser = Environment.UserName;

            var tempPath = Directory.GetCurrentDirectory() + @"\temp\" + id;

            Copy(binPath, tempPath);
            File.WriteAllText(tempPath + @"\data.csv", csvline);


            File.Delete(Directory.GetCurrentDirectory() + @"\jacoco.exec");
            File.Delete(binPath + @"\jacoco.exec");
            File.Delete(tempPath + @"\jacoco.exec");


            var command =
                @"-javaagent:C:\javatools\jacocoagent.jar=output=file,destfile=" + tempPath + @"\jacoco.exec" +
                @" -jar ""C:\javatools\junit.jar"" --classpath=""C:\javatools\jacocoagent.jar;" +
                tempPath + @""" --scan-classpath";

            // System.Diagnostics.Process.Start("CMD.exe", command).WaitForExit();

            var sc = new ShellCommand(command) {Exe = "java"};
            sc.ExecuteCommand();

            var jacocoPath = Directory.GetCurrentDirectory();


            //  File.Copy(tempPath + @"\jacoco.exec", binPath + @"\jacoco.exec", true);


            //  Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\temp\"+id);

            command = @"-jar ""C:\javatools\jacococli.jar"" report """ + tempPath + @"\jacoco.exec" +
                      @""" --classfiles """ + tempPath + @""" --sourcefiles """ + srcPath +
                      @""" --xml """ + Directory.GetCurrentDirectory() + @"\temp\" + id + @"\jacoco.xml" + @"""";

            // System.Diagnostics.Process.Start("CMD.exe", command).WaitForExit();

            sc.Input = command;
            sc.ExecuteCommand();
        }

        private void GeneraOutputJacocoNoMin()
        {
            var className = lblNomeClasse.Text.Substring(13);
            var testClassName = "Test" + className;

            var srcPath = txtSrc.Text;
            var binPath = txtBin.Text;


            percorsoSrc = srcPath;
            percorsoBin = binPath;

            File.Delete(Directory.GetCurrentDirectory() + @"\jacoco.exec");
            File.Delete(binPath + @"\jacoco.exec");

            var currentUser = Environment.UserName;

            File.Copy(Directory.GetCurrentDirectory() + "\\" + testClassName + ".java",
                srcPath + "\\" + testClassName + ".java", true);
            File.Copy(Directory.GetCurrentDirectory() + "\\data.csv", binPath + "\\data.csv", true);


            //var command = @"/C javac -classpath C:\Users\" + currentUser + @"\.p2\pool\plugins\*;" + srcPath + "" +
            //              @" " +
            //              @" " + srcPath + @"\*; " +srcPath + @"\*.java" + "-d " + binPath;
            //attenzione
            //var command = @"/C javac -classpath C:\Users\" + currentUser + @"\.p2\pool\plugins\*;" + srcPath + "" +
            //              @" " +
            //              @" " + srcPath + @"\*.java -d " + binPath;

            // var command = @"/C javac -classpath C:\javatools\*;" + srcPath + @"\*; -d " + binPath;

            //command.Replace(@"*\", "*");

            var cartelle = GetClassPath(srcPath);

            var command = @"/C javac -classpath C:\javatools\*;";
            command += srcPath + @"\*;";
            foreach (var cart in cartelle) command += cart;

            command += " " + srcPath + @"\*.java ";
            command += "-d " + binPath;

            // System.Diagnostics.Process.Start("CMD.exe", command).WaitForExit();

            new ShellCommand(command).ExecuteCommand();


            command =
                @"/C java -javaagent:C:\javatools\jacocoagent.jar -jar ""C:\javatools\junit.jar"" --classpath=""C:\javatools\jacocoagent.jar;" +
                binPath + @""" --scan-classpath";

            // System.Diagnostics.Process.Start("CMD.exe", command).WaitForExit();

            new ShellCommand(command).ExecuteCommand();

            var jacocoPath = Directory.GetCurrentDirectory();

            File.Copy(jacocoPath + @"\jacoco.exec", binPath + @"\jacoco.exec", true);

            //Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\coperturaNoMin");

            command = @"/C java -jar ""C:\javatools\jacococli.jar"" report """ + binPath + @"\jacoco.exec" +
                      @""" --classfiles """ + binPath + @""" --sourcefiles """ + srcPath +
                      @""" --html """ + Directory.GetCurrentDirectory() + @"\coperturaNoMin" + @"""";
            // System.Diagnostics.Process.Start("CMD.exe", command).WaitForExit();

            new ShellCommand(command).ExecuteCommand();


            command = @"/C java -jar ""C:\javatools\jacococli.jar"" report """ + binPath + @"\jacoco.exec" +
                      @""" --classfiles """ + binPath + @""" --sourcefiles """ + srcPath +
                      @""" --xml """ + Directory.GetCurrentDirectory() + @"\coperturaNoMin\jacoco.xml" + @"""";
            //  System.Diagnostics.Process.Start("CMD.exe", command).WaitForExit();

            new ShellCommand(command).ExecuteCommand();
        }

        private string[] GetClassPath(string srcPath)
        {
            var cartelle = Directory.GetDirectories(srcPath, "*", SearchOption.AllDirectories);
            for (var i = 0; i < cartelle.Length; i++) cartelle[i] += @"\*;";

            return cartelle;
        }

        private void SalvaClasseTest(string metodo)
        {
            var parametri = JavaClassAnalyzer.GetParameters(metodo);
            var methodName = JavaClassAnalyzer.GetMethodName(metodo);
            var returnType = JavaClassAnalyzer.GetReturnType(metodo);
            var className = lblNomeClasse.Text.Substring(13);
            var classObjName = "obj" + className;
            var sb = new StringBuilder();
            sb.AppendLine(@"import static org.junit.Assert.*;");
            sb.AppendLine(@"import org.junit.jupiter.api.*;");
            sb.AppendLine(@"import org.junit.jupiter.api.Test;");
            sb.AppendLine(@"import org.junit.jupiter.params.ParameterizedTest;");
            sb.AppendLine(@"import org.junit.jupiter.params.provider.CsvFileSource;");
            sb.AppendLine(@"import org.junit.jupiter.params.provider.CsvSource;");
            sb.AppendLine(@"import java.time.Duration;");
            sb.AppendLine("");
            sb.AppendLine("class Test" + className + " {");

            sb.AppendLine("\t" + className + " " + classObjName + ";");
            sb.AppendLine("\t@BeforeEach");
            sb.AppendLine("\tvoid init() {");

            var inizializzazione = "\t\t" + classObjName + " = new " + className + "();";
            //  sb.AppendLine("\t\t" + classObjName + " = new " + className + "();");

            var dialogResult =
                MessageBox.Show(
                    "Vuoi modificare lo scheletro di invocazione del costruttore relativo alla classe considerata? (Necessario nel caso in cui il costruttore preveda dei parametri)",
                    "Attenzione", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes) ShowInputDialog(ref inizializzazione, "Modifica costruttore");

            sb.AppendLine(inizializzazione);
            sb.AppendLine("\t\tassertNotNull(" + classObjName + ");");
            sb.AppendLine("\t}");
            sb.AppendLine("");
            sb.AppendLine("\t@ParameterizedTest");
            sb.AppendLine("\t" + @"@CsvFileSource(resources = ""data.csv"")");
            sb.Append("\tvoid TestMethod(");
            foreach (var t in parametri) sb.Append(t + ",");

            sb.AppendLine(returnType + " risultato_atteso) {");

            if (cbTimeout.Checked)
                sb.AppendFormat(
                    "\t\torg.junit.jupiter.api.Assertions.assertTimeoutPreemptively(Duration.ofSeconds({0}), () -> {{",
                    timeout); //timeout


            sb.AppendLine("");
            sb.Append("\t\t" + returnType + " risultato_effettivo = " + classObjName + "." + methodName + "(");

            for (var i = 0; i < parametri.Length; i++)
            {
                var attributi = JavaClassAnalyzer.AnalizzaParametro(parametri[i], 0);
                if (i == parametri.Length - 1)
                    sb.Append(attributi.Item3);
                else
                    sb.Append(attributi.Item3 + ",");
            }


            sb.AppendLine(");");


            //sb.AppendLine("\t\t});"); //timeout
            if (returnType.Equals("float", StringComparison.OrdinalIgnoreCase) ||
                returnType.Equals("double", StringComparison.OrdinalIgnoreCase))
            {
                var precision =
                    Interaction.InputBox(
                        "Il tipo di ritorno è " + returnType +
                        ". Inserisci l'errore assoluto massimo accettato per il confronto tra risultato atteso e risultato ottenuto:",
                        "Attenzione");
                sb.AppendLine("\t\tassertTrue(\"Errore\",Math.abs(risultato_atteso-risultato_effettivo)<" + precision +
                              ");");
            }
            else
            {
                sb.AppendLine("\t\tassertEquals(risultato_atteso,risultato_effettivo);");
            }

            if (cbTimeout.Checked) sb.AppendLine("\t\t});"); //timeout


            sb.AppendLine("\t}");
            sb.AppendLine("}");

            var path = Directory.GetCurrentDirectory() + "/Test" + className + ".java";
            var sourceCode = sb.ToString();
            File.WriteAllText(path, sourceCode);
        }

        private static void SalvaCsv(string CTanswer)
        {
            var path = Directory.GetCurrentDirectory() + @"\data.csv";

            dynamic array = JsonConvert.DeserializeObject(CTanswer);

            if (CTanswer.Contains(".csv"))
            {
                using (var client = new WebClient())
                {
                    string name = array.result;

                    var url = "https://foselab.unibg.it/ctwedge/results/?name=" + name;

                    var answer = client.DownloadString(url);
                    while (answer == "" || answer.Contains("Please")) answer = client.DownloadString(url);
                    client.DownloadFile(url, Directory.GetCurrentDirectory() + @"\data.csv");
                    var lines1 = File.ReadAllLines(path);
                    File.WriteAllLines(path, lines1.Skip(1).ToArray());
                }

                return;
            }


            string csv = array.result.ToString();
            csv = csv.Replace(';', ',');
            //  string path = Directory.GetCurrentDirectory() + "/data.csv";
            File.WriteAllText(path, csv);
            var lines = File.ReadAllLines(path);
            File.WriteAllLines(path, lines.Skip(1).ToArray());
        }

        private static string InviaRichiesta(string modello, string k)
        {
            var request = (HttpWebRequest) WebRequest.Create("https://foselab.unibg.it/ctwedge/generator/");

            var postData = "model=" + Uri.EscapeDataString(modello);
            postData += "&strength=" + Uri.EscapeDataString(k);
            postData += "&generator=" + Uri.EscapeDataString("ACTS");
            postData += "&ignConstr" + Uri.EscapeDataString("false");
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse) request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }

        private StringBuilder CreaModello(string metodo)
        {
            var parametri = JavaClassAnalyzer.GetParameters(metodo).ToList();
            var count = 1;
            var sb = new StringBuilder();
            sb.AppendLine("Model " + Path.GetFileNameWithoutExtension(openFileDialogClass.FileName));
            sb.AppendLine("Parameters:");
            foreach (var parametro in parametri)
            {
                var t = JavaClassAnalyzer.AnalizzaParametro(parametro, count);
                var nome = t.Item3;
                var tipo = t.Item2;
                var title = "Parametro n. " + count;
                var prompt = @"Inserisci l'insieme dei possibili valori per il parametro """ + nome +
                             @""" di tipo """ + tipo + @"""";
                var dizionario = Interaction.InputBox(prompt, title);
                sb.AppendLine(nome + " : " + dizionario);
                count++;
            }

            var returnType = JavaClassAnalyzer.GetReturnType(metodo);

            var n = "risultato";
            var tl = "Valore di ritorno";
            var p = @"Inserisci un dizionario dei dati per il valore di ritorno di tipo """ + returnType +
                    @"""";
            var d = Interaction.InputBox(p, tl);
            sb.AppendLine(n + " : " + d);

            sb.AppendLine("Constraints:");
            var vincoli = "";
            ShowInputDialog(ref vincoli, "Inserisci qui i vincoli");
            sb.AppendLine(vincoli);
            return sb;
        }

        private bool CheckSelectedMethod()
        {
            var cbItem = cbMetodi.SelectedItem as ComboBoxItem;
            return cbItem.Id != -1;
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);


            foreach (var fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }


            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  File.WriteAllText(@"C:\Users\amato\source\repos\TestingCombinatoriale\TestingCombinatoriale\bin\Debug\data.csv", "ciao");
            Process.Start("java");
        }

        private static void CopyJavaTools()
        {
            const string dest = @"C:/javatools";
            if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);

            Copy(Directory.GetCurrentDirectory() + "/javatools/", dest);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var rand = new Random(DateTime.Now.Millisecond);
            var matrice = new int[1000, 10];
            var numRighe = matrice.GetLength(0);
            var numColonne = matrice.GetLength(1);

            for (var i = 0; i < numRighe; i++)
            for (var j = 0; j < numColonne; j++)
                matrice[i, j] = 1;
            //matrice[0, 1] = matrice[0, 2] = matrice[0, 3] = 1;
            //matrice[1, 0] = matrice[1, 2] = matrice[1, 3] = 1;
            //matrice[2, 0] = matrice[2, 1] = matrice[2, 2] = 1;
            //matrice[3, 5] = 1;
            //matrice[4, 3] = matrice[4, 5] = 1;
            //matrice[5, 4] = 1;

            // print(matrice);
            Minimizzazione.Minimizza(matrice);
        }

        private void txtSrc_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogSrc.ShowDialog() == DialogResult.OK)
                txtSrc.Text = folderBrowserDialogSrc.SelectedPath;
        }

        private void txtBin_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogBin.ShowDialog() == DialogResult.OK)
                txtBin.Text = folderBrowserDialogBin.SelectedPath;
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Software realizzato per l'esame di Software Testing, anno 2020.\r\nAutore: Paolo Amato");
        }

        private void cbMetodi_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(cbMetodi, "Da qui puoi selezionare il metodo su cui effettuare testing di unità.");
        }

        private void txtK_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(txtK,
                "Esercita tutte le k-ple.\nA meno di vincoli, il numero di test generati è pari al prodotto delle cardinalità dei k input aventi più classi di equivalenza.\r");
        }

        private void cbParallelismo_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(cbParallelismo,
                "Dopo aver generato la test suite e il relativo report di copertura, comincia la minimizzazione.\nIn questa fase si valuta la copertura di codice ottenuta con ciascuna sequenza di input e si individua poi la test suite minima che garantisce la stessa copertura in termini di LOC.\nL'utilizzo di più threads potrebbe velocizzare questa fase.");
        }

        private void btnProcedi_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(btnProcedi, "Avvia l'intera procedura.");
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void cbParallelismo_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lblParallelismo_Click(object sender, EventArgs e)
        {
        }

        private void txtBin_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblBin_Click(object sender, EventArgs e)
        {
        }

        private void txtSrc_TextChanged(object sender, EventArgs e)
        {
        }

        private void selezionaPathJUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var cartelle = Directory.GetDirectories(@"C:\Users\amato\OneDrive\Desktop\ClassiDiTest\BlackJack", "*",
                SearchOption.AllDirectories);
            foreach (var str in cartelle) MessageBox.Show(str);
        }

        private void txtTimeout_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtTimeout.Text, "[^0-9]"))
            {
                MessageBox.Show("Il timeout deve essere un valore numerico!");
                txtTimeout.Text = txtTimeout.Text.Remove(txtTimeout.Text.Length - 1);
                txtTimeout.SelectionStart = txtTimeout.Text.Length;
                txtTimeout.SelectionLength = 0;
            }
        }

        private void cbTimeout_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTimeout.Checked)
                txtTimeout.Enabled = true;
            else
                txtTimeout.Enabled = false;
        }
    }
}