#include "stdio.h"
#include "iostream"
#include "string.h"
// #include "PersList1.h"

using namespace std;

// Als Anwendung von Pointern werden noch kurz
// die basics der LinkedList besprochen

// ToDo ToDo ToDo ToDo ToDo ToDo ToDo ToDo
//
// Demo muß noch lauffähig gemacht werden
//

// Struktur zur Verwaltung eines Eintrages in
// einer MPersList
typedef struct MPersonS
{
	char m_Name[20];
	int  m_CatNr;
	struct MPersonS* m_Next;
} MPerson;

MPerson* PlCreatePerson1();
MPerson* PlCreatePerson2(char* aName, int aCatNr);

void LinkedListDemo1();

void main()
{
  LinkedListDemo1();
}

void LinkedListDemo1()
{
	// Zeigt die prinzipielle Funktionsweise und den Aufbau einer Linked-List
	
	MPerson* root=0; // Zeiger auf den Anfang der Liste
	MPerson* pers=0; // Hilfsvariable
	
	
	// Liste erzeugen ( Befüllen ) Liste erzeugen ( Befüllen )Liste erzeugen ( Befüllen )
	
	pers = PlCreatePerson2("aaa",10); // Ein Listenelement erzeugen
	
	root = pers; // 1'stes Listenelement in die Liste einhängen

	pers = PlCreatePerson2("bbb",20); // 2'tes Listenelement erzeugen
	
	// 2'tes Listenelement einhängen
	pers->m_Next = root;   root = pers;
	
	pers = PlCreatePerson2("ccc",30); // 3'tes Listenelement erzeugen
	
	// 3'tes Listenelement einhängen
	pers->m_Next = root;   root = pers;
	
	
	// Alle Listenelemente besuchen um Sie z.B. auszugeben
	
	// Einen Iterator zum Besuchen aller Listenelemente erzeugen
	// und auf die Wurzel ( root ) der Liste setzen
	MPerson* iter=root;
	while( iter!=0 ) // Das Next des letzen Listenelements ist 0 => die Abbruchbedingung der Iteration
	{
		printf("%s %d\n", iter->m_Name, iter->m_CatNr);
		iter = iter->m_Next;
	}
	
	
	// Listenelemente entfernen Listenelemente entfernen Listenelemente entfernen
	
	// 1'tes Listenelement aus der Liste ausketten
	pers = root;
	root = root->m_Next;
	
	// nächstes Listenelement aus der Liste ausketten
	pers = root;
	root = root->m_Next;
}


MPerson* PlCreatePerson2(char* aName, int aCatNr)
{
	MPerson* pers = PlCreatePerson1();
	pers->m_CatNr=aCatNr; strcpy(pers->m_Name,aName);
	return pers;
}

MPerson* PlCreatePerson1()
{
	MPerson* pers = new MPerson;
	// alle Datenelemente auf 0 initialisieren
	pers->m_CatNr=0; strcpy(pers->m_Name,"");
	pers->m_Next=0;
	return pers;
}




