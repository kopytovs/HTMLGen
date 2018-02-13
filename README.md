# HTMLGen

task1:
create folders ”classes”, “resources”, “properties”, “KnowledgeBase”.
create *.n3-file for each property, for each class and for each individual (you can copy from big *.n3). Each small *.n3-file should contain only one, the same subject for all triples, for ex:

:cat 	rdf:type 	:Animal;
	:eats 		:mouse,
			:bird.


examples 
for class: http://ontology.tom.ru/classes/AlgebraicTask.n3
for property: http://ontology.tom.ru/properties/requiresHaving.n3  
for resource ( == individual):http://ontology.tom.ru/resources/checkIfPointBelongsToGraph.n3  


use real namespaces like:
@prefix class: <H:/classes/>
@prefix prop: <H:/properties/>
@prefix res: <H:/resources/>

All objects should be found by they URIs, so URIs should be REAL! Use local addresses

create *Info.html-file for each property, for each class and for each individual. Initially they can contain only name and short description like here: 
http://ontology.tom.ru/resources/checkIfPointBelongsToGraphInfo.html 
generate new *.html-files based on *Info.html and *.n3 files, this file should look like http://ontology.tom.ru/resources/checkIfPointBelongsToGraph.html (generated from files with the same name: http://ontology.tom.ru/resources/checkIfPointBelongsToGraph.n3 and http://ontology.tom.ru/resources/checkIfPointBelongsToGraphInfo.html  )
read files using StreamReader instance, “.ReadLine()” method
just copy all lines from *Info.html file
decide if you are going to use dotNetRDF library or manual pascing:
first case: use, for example, “GetNodeString(triple.Subject)” for getting subject string (look at your code of SPARQL when you list your triples;
second case: while reading lines from *.n3 file, implement deleting double gaps using .Replace(“  “,” “) method, repeat this until nothing changes. Then split line by parts using .Split(‘ ‘) method and think, what should be property, what is object.
use this for cases of comma:

if (nk[nk.Length - 1].EndsWith(",")) //last section after splitting for nk[]
{
      while (true) //only objects
      {
               line = fileToRead.ReadLine();
               line = deleteGaps(line);
               if (line.Contains("\""))
               {
                      fileToWrite.WriteLine("<br>" + line);
               }
              else
               {                                
                      fileToWrite.WriteLine ... //but with hyperlink
               }
               if (line.EndsWith(".") || line.EndsWith(";"))
                           break;
     }
}



task2:
generate big *.n3 file using small *.n3 files. 
Use construction:
DirectoryInfo info = new DirectoryInfo(pathToDirectory);
FileInfo[] files = info.getFiles(“.n3”);
foreach (FileInfo file in files)
{
	file.Name;
}

This “file.Name” will contain in this cycle all the names of *.n3 files (one after another).
task3:
using the same construction, create Index.html-file like this: http://ontology.tom.ru/ 
