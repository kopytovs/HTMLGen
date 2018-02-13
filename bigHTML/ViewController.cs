using System;
using System.Collections.Generic;

using AppKit;
using Foundation;

using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;

namespace bigHTML
{
    public partial class ViewController : NSViewController
    {

        private Notation3Parser parser = new Notation3Parser();
        private Graph graph = new Graph();



        private HashSet<String> myClasses = new HashSet<string>();
        private HashSet<String> myResources = new HashSet<string>();
        private HashSet<String> myProperties = new HashSet<string>();

        private String str1 = "";
        private String str2 = "";
        private String str3 = "";
        private String str4 = "";

        private string prefix = @"@prefix res: </Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/resources/>.
@prefix prop: </Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/properties/>.
@prefix class: </Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/classes/>.
@prefix xml: <http://www.w3.org/XML/1998/namespace>.
@prefix xsd: <http://www.w3.org/2001/XMLSchema#>.
@prefix rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>.
@prefix rdfs: <http://www.w3.org/2000/01/rdf-schema#>.
@prefix owl: <http://www.w3.org/2002/07/owl#>.";

        private bool[] myBoolean = new bool[3];

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            parser.Load(graph, @"/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/rdf.owl");

            myLabel.StringValue = "Let's do something magical!";
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        partial void doSmthMagic(NSObject sender)
        {
            saveRDF();
        }

        private void saveRDF(){
            string temp = @"";
            temp += prefix;
            temp += "\n";
            magicBar.IncrementBy(5);
            temp += generateRDF();

            System.IO.File.WriteAllText(@"/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/big_old.n3", temp);
            saveBig();
            foreach(string cls in myClasses){
                saveAny(cls, "class");
            }
            magicBar.IncrementBy(10);
            foreach(string prop in myProperties){
                saveAny(prop, "prop");
            }
            magicBar.IncrementBy(10);
            foreach (string res in myResources){
                saveAny(res, "res");
            }
            magicBar.IncrementBy(10);
            saveBigHTML();
            if(magicBar.DoubleValue<100){
                myLabel.StringValue = "Something go wrong, reopen app and try again";
            } else {
                myLabel.StringValue = "MAGIC IS HERE!!!"; 
            }
        }

        private void saveBigHTML(){
            string myBigHTML = "";
            myBigHTML += @"<h1>Triples by KopytovSD</h1><br />
            <table border = ""1"" >
<tr><td>Classes</td><td><ul>";
            foreach (string cls in myClasses)
            {
                myBigHTML += addLink(cls, "class")+"\n";
            }
            magicBar.IncrementBy(5);
            myBigHTML += @"</ul></td></tr>
            <tr> <td>Properties</td><td><ul>";
            foreach (string prop in myProperties)
            {
                myBigHTML += addLink(prop, "prop")+ "\n";
            }
            magicBar.IncrementBy(5);
            myBigHTML += @"</ul></td></tr>
            <tr> <td>Resources</td><td><ul>";
            foreach (string res in myResources)
            {
                myBigHTML += addLink(res, "res")+ "\n";
            }
            magicBar.IncrementBy(5);
            myBigHTML += @"</ul></td></tr>
            </table>";
            System.IO.File.WriteAllText(@"/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/index.html", myBigHTML);
        }

        private string addLink(string smth, string key){
            string temp = "";
            string path = "";
            switch (key)
            {
                case "class":
                    path = "/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/classes/";
                    break;
                case "res":
                    path = "/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/resources/";
                    break;
                case "prop":
                    path = "/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/properties/";
                    break;
                default:
                    break;
            }
            temp += @"<li><a href = """ + path + cleanName(smth) + @".html"">" + cleanName(smth) + ".html</a></li>";
            return temp;
        }

        private void saveBig(){
            string myBig = "";
            myBig += prefix;
            myBig += "\n";
            foreach (string cls in myClasses)
            {
                myBig += takeDataFromN(cls, "class") + "\n";
            }
            magicBar.IncrementBy(5);
            foreach (string prop in myProperties)
            {
                myBig += takeDataFromN(prop, "prop") + "\n";
            }
            magicBar.IncrementBy(5);
            foreach (string res in myResources)
            {
                myBig += takeDataFromN(res, "res") + "\n";
            }
            magicBar.IncrementBy(5);
            string path = "/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/big.n3";
            System.IO.File.WriteAllText(path, myBig);
            magicBar.IncrementBy(15);
        }

        private string takeDataFromN(string fileName, string key){
            string temp = "";
            string path = cleanName(fileName) + ".n3";
            switch(key){
                case "class":
                    path = "/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/classes/" + path;
                    break;
                case "res":
                    path = "/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/resources/" + path;
                    break;
                case "prop":
                    path = "/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/properties/" + path;
                    break;
                default:
                    break;
            }
            //temp = System.IO.File.ReadAllText(path);
            int count = 0;
            foreach(string str in System.IO.File.ReadLines(path)){
                if (count > 7) temp += str + "\n";
                count++;
            }
            return temp;
        }

        private string generateRDF(){
            createHash();
            string temp = @"";
            magicBar.IncrementBy(5);
            temp += smthTriples(ref myClasses);
            magicBar.IncrementBy(5);
            temp += smthTriples(ref myResources);
            magicBar.IncrementBy(5);
            temp += smthTriples(ref myProperties);
            magicBar.IncrementBy(5);
            return temp;
        }

        private string smthTriples(ref HashSet<string> mas){

            string ret = @"";

            str2 = "?y";
            str3 = "?z";
            str4 = "?y ?z";
            myBoolean[0] = true;
            myBoolean[1] = false;
            myBoolean[2] = false;
            foreach (string res in mas)
            {
                str1 = res;

                string temp = @"
    PREFIX : <http://kopytov.rdf#>
    PREFIX ns0: <http://our-place.spb.ru/today#>
    PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#Resource/>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    
    SELECT " + str4 + @"
    WHERE 
    {
        " + str1 + @" " + str2 + @" " + str3 + @".                                   

    }";

                SparqlResultSet resultSet = graph.ExecuteQuery(temp) as SparqlResultSet;
                if (resultSet != null)
                {
                    for (int i = 0; i < resultSet.Count; i++)
                    {
                        SparqlResult result = resultSet[i];
                        Console.WriteLine(string.Format("{0} {1} {2}\n", createPrefix(myBoolean[0] ? str1 : returnPrefix(GetNodeString(result["x"]))), createPrefix(myBoolean[1] ? str2 : returnPrefix(GetNodeString(result["y"]))), createPrefix(myBoolean[2] ? str3 : returnPrefix(GetNodeString(result["z"])))));
                        ret += string.Format("{0} {1} {2}\n", createPrefix(myBoolean[0] ? str1 : returnPrefix(GetNodeString(result["x"]))), createPrefix(myBoolean[1] ? str2 : returnPrefix(GetNodeString(result["y"]))), createPrefix(myBoolean[2] ? str3 : returnPrefix(GetNodeString(result["z"]))));
                    }
                }

            }
            //ret += "\n";
            return ret;
        }

        private void createHash(){
            createSmth("owl:Class", ref myClasses);
            createSmth("owl:NamedIndividual", ref myResources);
            createSmth("owl:ObjectProperty", ref myProperties);
            createSmth("owl:DatatypeProperty", ref myProperties);
            foreach (string clas in myClasses){
                Console.WriteLine(clas);
            }
        }

        private void createSmth(string str, ref HashSet<string> mas){
            
            str1 = "?x";
            str2 = "rdf:type";
            str3 = str;
            str4 = @"?x";

            myBoolean[0] = false;
            myBoolean[1] = true;
            myBoolean[2] = true;


            string mySparql = @"
    PREFIX : <http://kopytov.rdf#>
    PREFIX ns0: <http://our-place.spb.ru/today#>
    PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#Resource/>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    
    SELECT " + str4 + @"
    WHERE 
    {
        " + str1 + @" " + str2 + @" " + str3 + @".                                   

    }";


            SparqlResultSet resultSet = graph.ExecuteQuery(mySparql) as SparqlResultSet;
            if (resultSet != null)
            {
                for (int i = 0; i < resultSet.Count; i++)
                {
                    SparqlResult result = resultSet[i];
                    mas.Add(myBoolean[0] ? str1 : returnPrefix(GetNodeString(result["x"])));
                }
            }
        }

        private bool saveAny(string obj, string key){
            
            string ret = @"";
            string myHTML = "<h1>"+cleanName(obj)+"</h1><br />\n";
            myHTML += @"<table border = ""1"">";
            ret += prefix;
            ret += "\n";
            str1 = obj;
            str2 = "?y";
            str3 = "?z";
            str4 = "?y ?z";
            myBoolean[0] = true;
            myBoolean[1] = false;
            myBoolean[2] = false;

            string temp = @"
    PREFIX : <http://kopytov.rdf#>
    PREFIX ns0: <http://our-place.spb.ru/today#>
    PREFIX rdfs:<http://www.w3.org/2000/01/rdf-schema#Resource/>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    
    SELECT " + str4 + @"
    WHERE 
    {
        " + str1 + @" " + str2 + @" " + str3 + @".                                   

    }";

            SparqlResultSet resultSet = graph.ExecuteQuery(temp) as SparqlResultSet;
            if (resultSet != null)
            {
                SparqlResult result1 = resultSet[0];
                string t1 = (createPrefix(myBoolean[1] ? str2 : returnPrefix(GetNodeString(result1["y"]))));
                string t2 = (createPrefix(myBoolean[1] ? str2 : returnPrefix(GetNodeString(result1["z"]))));
                ret += string.Format("{0} {1} {2}\n", createPrefix(myBoolean[0] ? str1 : returnPrefix(GetNodeString(result1["x"]))), createPrefix(myBoolean[1] ? str2 : returnPrefix(GetNodeString(result1["y"]))), createPrefix(myBoolean[2] ? str3 : returnPrefix(GetNodeString(result1["z"]))));
                myHTML += "<tr>";
                myHTML += string.Format("{0} {1}\n", addSource(t1), addSource(t2));
                myHTML += "</tr>";
                for (int i = 1; i < resultSet.Count; i++)
                {
                    SparqlResult result = resultSet[i];
                    ret += string.Format("\t\t{0} {1}\n", createPrefix(myBoolean[1] ? str2 : returnPrefix(GetNodeString(result["y"]))), createPrefix(myBoolean[2] ? str3 : returnPrefix(GetNodeString(result["z"]))));
                    myHTML += "<tr>";
                    myHTML += string.Format("{0} {1}\n", addSource(createPrefix(myBoolean[1] ? str2 : returnPrefix(GetNodeString(result["y"])))), addSource(createPrefix(myBoolean[2] ? str3 : returnPrefix(GetNodeString(result["z"])))));
                    myHTML += "</tr>";
                }
            }
            string path = "";
            switch(key){
                case "class":
                    path = @"/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/classes/";
                    break;
                case "prop":
                    path = @"/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/properties/";
                    break;
                case "res":
                    path = @"/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/resources/";
                    break;
                default:
                    return false;
            }
            int count = 0;
            foreach(char ch in obj){
                count++;
                if (ch == ':') break;
            }
            string name = obj;
            //if (count == 0)
            name = name.Remove(0, count);
            path += name;
            System.IO.File.WriteAllText(path + ".n3", ret);
            myHTML += "\n</table>";
            System.IO.File.WriteAllText(path + ".html", myHTML);
            return true;
        }

        private string addSource(string obj){
            string pref = readPrefix(obj);
            string res = "";
            switch (pref){
                case "res:":
                    res = @"<a href=""/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/resources/"+ cleanName(obj) +@".html"">" + obj + ".html</a>";
                    break;
                case "prop:":
                    res = @"<a href=""/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/properties/"+ cleanName(obj) + @".html"">" + obj + ".html</a>";
                    break;
                case "class:":
                    res = @"<a href=""/Users/Kopytov/Documents/Programming/bigHTML/bigHTML/Resources/classes/"+ cleanName(obj) + @".html"">" + obj + ".html</a>";
                    break;
                case "xml:":
                    res = @"<a href=""http://www.w3.org/XML/1998/namespace"+ cleanName(obj) +@".html"+ cleanName(obj) + @".html"">" + obj + ".html</a>";
                    break;
                case "xsd:":
                    res = @"<a href=""http://www.w3.org/2001/XMLSchema#"+ cleanName(obj) + @".html"">" + obj + ".html</a>";
                    break;
                case "rdf:":
                    res = @"<a href=""http://www.w3.org/1999/02/22-rdf-syntax-ns#"+ cleanName(obj) + @".html"">" + obj + ".html</a>";
                    break;
                case "rdfs:":
                    res = @"<a href=""http://www.w3.org/2000/01/rdf-schema#"+ cleanName(obj) + @".html"">" + obj + ".html</a>";
                    break;
                case "owl:":
                    res = @"<a href=""http://www.w3.org/2002/07/owl#"+ cleanName(obj) + @".html"">" + obj + ".html</a>";
                    break;
                default:
                    break;
            }
            res = "<td>" + res + "</td>";
            return res;
        }

        private string readPrefix(string obj){
            string temp = "";
            foreach(char ch in obj){
                temp += ch;
                if (ch == ':') break;
            }
            return temp;
        }

        private string cleanName(string obj){
            string temp = obj.Remove(0, readPrefix(obj).Length);
            return temp;
        }

        private string createPrefix(string str){
            //bool find = false;
            //if (!find){
            foreach(string clas in myClasses){
                if (str == clas) return "class"+str;
            }
            foreach (string prop in myProperties)
            {
                if (str == prop) return "prop"+str;
            }
            foreach (string res in myResources)
            {
                if (str == res) return "res"+str;
            }
            return str;
            //}
        }

        private string returnPrefix(string str)
        {
            string smth = "";
            string owlPrefix = "owl:";
            string rdfPrefix = "rdf:";
            string rdfsPrefix = "rdfs:";
            string myPrefix = ":";
            switch (str)
            {
                case "ObjectProperty":
                    smth = owlPrefix + str;
                    break;
                case "DatatypeProperty":
                    smth = owlPrefix + str;
                    break;
                case "Class":
                    smth = owlPrefix + str;
                    break;
                case "":
                    smth = owlPrefix + str;
                    break;
                case "NamedIndividual":
                    smth = owlPrefix + str;
                    break;
                case "type":
                    smth = rdfPrefix + str;
                    break;
                case "subClassOf":
                    smth = rdfsPrefix + str;
                    break;
                default:
                    smth = myPrefix + str;
                    break;
            }
            return smth;
        }

        string GetNodeString(INode node)
        {
            string s = node.ToString();
            switch (node.NodeType)
            {
                case NodeType.Uri:
                    int lio = s.LastIndexOf('#');
                    int lik = s.LastIndexOf('/');
                    if (lio == -1)
                    {
                        if (lik == -1)
                        {
                            return s;
                        }
                        else
                            return s.Substring(lik + 1);
                    }
                    else
                        return s.Substring(lio + 1);
                case NodeType.Literal:
                    return string.Format("\"{0}\"", s);
                default:
                    return s;
            }
        }
    }
}
