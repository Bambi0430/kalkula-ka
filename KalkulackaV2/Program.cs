using System.Data; // vyuzivani balicku data

class Program //vytvoreni hlavni tridy
{
    static void Main(string[] args) //vytvoreni metody Main ktera se vola pri startu
    {
        Console.CursorVisible = false; //vypnuti kurzoru v konzoli

        bool run = true; //deklarace a inicializace promene
        string[] menu = { "[Načíst soubor s příklady]", "[Upravit počet desetinných míst]", "[Ukončit]" }; //deklarace a inicializace pole
        string[] menuNacistPriklad = { "Zadejte umístění souboru kde je každý příklad na jednom řádku.\n------INPUT------" }; //deklarace a inicializace pole
        List<string> calculated = new List<string>(); //deklarace a inicializace listu
        int pocetDesMist = 2; //deklarace a inicializace promene

        int MenuCounter = 0; //deklarace a inicializace promene
        int menuPhase = 0; //deklarace a inicializace promene

        while (run) //cyklus while pokud kondice vyhodi True
        {
            if (menuPhase == 0) //podminka 
                DrawMainMenu(menu, ref pocetDesMist, ref MenuCounter); //volani metody (ref. je odkaz na pametove misto respektive na "puvodni promenou")
            else if (menuPhase == 1) //podminka 
                DrawInputFileMenu(menuNacistPriklad, ref calculated, ref menuPhase, ref pocetDesMist, ref MenuCounter); //volani metody (ref. je odkaz na pametove misto respektive na "puvodni promenou")
            else if (menuPhase == 2) //podminka 
                DrawDesetineMista(ref pocetDesMist, ref calculated, ref menuPhase, ref MenuCounter); //volani metody (ref. je odkaz na pametove misto respektive na "puvodni promenou")
            Console.ForegroundColor = ConsoleColor.White; //nastaveni barvy textu na bilou


            foreach (string s in calculated) //iteruje vsechny prvky kolekce s docanou promenou string s
            {
                Console.WriteLine(s); //tisk do konzole
            }

            try //odchyt vyjimek
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey(); //deklarace a inicializace promene


                if (consoleKey.Key == ConsoleKey.DownArrow) //podminka 
                {
                    MenuCounter++; //pricte k MenuCounter 1
                    if (MenuCounter >= menu.Length) //podminka
                    {
                        MenuCounter = 0; //nastavi MenuCounter na 0
                    }
                }
                else if (consoleKey.Key == ConsoleKey.UpArrow) //retezeni podminky
                {
                    MenuCounter--; //snizi MenuCounter o 1
                    if (MenuCounter < 0) //podminka
                    {
                        MenuCounter = menu.Length - 1; //nastavi MenuCOunter na delku pole menu snizenou o jedna
                    }
                }
                else if (consoleKey.Key == ConsoleKey.Enter) //retezeni podminky
                {
                    if (MenuCounter == 0) //podminka
                    {
                        menuPhase = 1; //nastavi MenuPhase na 1
                    }
                    else if (MenuCounter == 1) //retezeni podminky
                    {
                        menuPhase = 2; //nastavi MenuPhase na 2
                    }
                    else if (MenuCounter == 2) //retezeni podminky
                    {
                        Environment.Exit(0); //prerusi beh programu
                        break; //ukonci vsechny akce
                    }
                }
            }
            catch { } //odchyt chyb a nouzovy kod (muze byt prazdny)
        }
    }
    public static void DrawMainMenu(string[] menu, ref int pocetDesMist, ref int MenuCounter) //deklarace Metody
    {
        Console.Clear(); //vymaze vse co je na konzoli
        Console.ForegroundColor = ConsoleColor.White; //nastavi barvu textu na bilou
        for (int i = 0; i < menu.Length; i++) //for pro prochazeni pole(menu)
        {
            if (MenuCounter == i) //podminka
                Console.ForegroundColor = ConsoleColor.Green; //nastaveni barvy textu na zelenou
            else //nouzove reseni
                Console.ForegroundColor = ConsoleColor.White; //nastaveni barvy textu na bilou
            Console.WriteLine(menu[i]); //tisk na konzoli item v poli na indexu i (i vychazi z foru)
        }
        Console.ForegroundColor = ConsoleColor.White; //nastavi barvu textu na bilou
        Console.WriteLine("------INFO------"); //tisk do konzole
        Console.WriteLine($"Počet des. míst: {pocetDesMist}"); //tisk do konzole ($ umozni vkladat promene do stringu)
        Console.WriteLine("------OUTPUT------"); //tisk do konzole

    }
    public static void DrawInputFileMenu(string[] menu, ref List<string> calculated, ref int menuPhase, ref int pocetDesMist, ref int MenuCounter) //deklarace Metody
    {
        Console.Clear(); //vymaze vse co je na konzoli
        Console.ForegroundColor = ConsoleColor.White; //nastavi barvu textu na bilou
        Console.WriteLine(menu[0]); //vytiskne do konzole item co je v menu na indexu 0
        string path = Console.ReadLine(); //deklarace a inicializace promene
        Calculate(path, ref calculated, pocetDesMist); //volani metody na vypocet
        DrawMainMenu(["[Načíst soubor s příklady]", "[Upravit počet desetinných míst]", "[Ukončit]"], ref pocetDesMist, ref MenuCounter); //volani metody s hlavni obrazovkou
        menuPhase = 0; //nastaveni promene
    }
    public static void DrawDesetineMista(ref int pocetDesMist, ref List<string> calculated, ref int menuPhase, ref int MenuCounter) //deklarace Metody
    {
        Console.Clear(); //vymaze vse co je na konzoli
        Console.ForegroundColor = ConsoleColor.White; //nastavi barvu textu na bilou
        Console.WriteLine($"Na kolik desetinných míst se mají provádět výpočty. Momentálně: {pocetDesMist}"); //tisk do konzole
        Console.WriteLine("------INPUT------"); //tisk to konzole
        string temp = Console.ReadLine(); //deklarace a inicializace promene
        calculated.Clear(); //vymaze list
        if (int.TryParse(temp, out int number)) //podminka ktery zjistuje jestli je promena temp ciste cislice
        {
            if (Convert.ToInt32(temp) >= 0) //podminka ktera zjistuje jestli je temp vetsi nebo rovno 0
            {
                pocetDesMist = Convert.ToInt32(temp); //prirazeni hodnoty promene
                calculated.Add($"Počet desetinných míst změněn na {pocetDesMist}"); //vlozeni itemu do listu
            }
            else //nouzove reseni
            {
                calculated.Add("Nesprávný formát desetinného čísla."); //vlozeni itemu do listu
            }
        }
        else //nouzove reseni
        {
            calculated.Add("Nesprávný formát desetinného čísla."); //vlozeni itemu do listu
        }
        DrawMainMenu(["[Načíst soubor s příklady]", "[Upravit počet desetinných míst]", "[Ukončit]"], ref pocetDesMist, ref MenuCounter); //volani Metody
        menuPhase = 0; //nastaveni hodnoty promene
    }
    public static void Calculate(string path, ref List<string> calculated, int PocetDesMist)  //deklarace Metody
    {
        calculated.Clear(); //vyprazdni list
        if (!File.Exists(path)) //podminka ktera zjisti jestli zadana cesta existuje
        {
            calculated.Add("Chybně zadané umístění."); //vlozeni do listu
            return; //preruseni chodu metody
        }

        if (path.Split('.')[path.Split('.').Length - 1] != "txt") //podminka ktera zisti jestli se jedna o .txt soubor
        {
            calculated.Add("Špatný fromát souboru (pouze *.txt)."); //vlozeni do listu
            return; //preruseni chodu metody
        }

        using (StreamReader sr = new StreamReader(path)) //otevreni streamreaderu
        {
            string line; //deklarace promene
            while ((line = sr.ReadLine()) != null) //iterace do te doby dokud line nebude null po precteni radku readerem
            {
                try
                {
                    //tady je problem ze se to nezaokrouhluje
                    DataTable table = new DataTable();
                    object result = table.Compute(line, ""); //vypocitani vysledku ze stringu z jednoho radku
                    float result2 = Convert.ToSingle(result);
                    calculated.Add($"{line} = {Math.Round(result2, PocetDesMist)}"); //pridat do listu
                }
                catch
                {
                    calculated.Add($"{line} --špatný formát nelze vypočítat"); //pridani do listu
                }
                          
            }
        }
    }
}
