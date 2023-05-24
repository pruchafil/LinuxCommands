using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

const string name1 = "[name]";
const string name2 = "[name2]";
const string lvl = "[lvl]";

Dictionary<string, string> commands = new() {
    { $"su {name1}",                    $"Změňte uživatele na {name1}" },
    { $"sudo {name1}",                  $"Proveďte umožnění využít příkazů nadřazeného uživatele {name1}" },
    { $"groupadd {name1}",              $"Vytvořte skupinu {name1}" },
    { $"groupdel {name1}",              $"Smažte skupinu {name1}" },
    { $"adduser {name1}",               $"Přidejte uživatele {name1} pomocí vysokoúrovňového příkazu pro zjednodušené přidání uživatele" },
    { $"deluser {name1}",               $"Smažte uživatele {name1} pomocí vysokoúrovňového příkazu pro zjednodušené smazání uživatele" },
    { $"useradd {name1}",               $"Přidejte uživatele {name1} pomocí nízkoúrovňového příkazu pro manuální přidání uživatele" },
    { $"userdel {name1}",               $"Smažte uživatele {name1} pomocí nízkoúrovňového příkazu pro manuální smazání uživatele" },
    { $"passwd {name1}",                $"Změňte heslo uživatele {name1}" },
    { $"usermod -g {name1} {name2}",    $"Vložte uživatele {name2} do skupiny {name1}" },
    { $"ls",                            $"Vypište obsah aktuální složky" },
    { $"cd {name1}",                    $"Změňte adresář na {name1}" },
    { $"mkdir {name1}",                 $"Vytvořte složku {name1}" },
    { $"touch {name1}",                 $"Vytvořte jednoduchý soubor {name1}" },
    { $"chmod {lvl} {name1}",           $"Změňte práva na {lvl} pro soubor {name1}" },
    { $"nano {name1}",                  $"Vytvořte / otevřete a editujte soubor {name1}" },
    { $"cat {name1}",                   $"Zobrazte obsah souboru {name1}" }
};

string[] names = { "abc", "car", "flower", "myfile", "name", "file", "lfile1", "texts", "data", "mydata" };
const int rounds = 10;

Console.WriteLine("Tento program vyzkouší vaše porozumění základním linuxovým příkazům.");
Console.WriteLine();
Console.WriteLine("Stiskněte cokoliv.");

Console.ReadKey();

do
{
    Console.Clear();

    byte correct = 0;

    for (int i = 0; i < rounds; ++i)
    {
        KeyValuePair<string, string> kvp = Generate(commands.ElementAt(Random.Shared.Next(0, commands.Count)));
        Console.WriteLine("{0}) Proveďte tuto akci: {1}", i + 1, kvp.Value);
        Console.WriteLine();

        if (Console.ReadLine().ToLower() == kvp.Key.ToLower())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Správně");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;

            ++correct;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("Špatně");
            Console.WriteLine();
            Console.WriteLine("Správná odpověď byla: {0}", kvp.Key);
            Console.ForegroundColor = ConsoleColor.White;
        }

        Console.WriteLine();
    }

    Console.WriteLine("Správně {0}/{1}", correct, 10);
    Console.WriteLine();
    Console.WriteLine("Opakovat? Y");
} while (char.ToUpper(Console.ReadKey().KeyChar) == 'Y');

KeyValuePair<string, string> Generate(KeyValuePair<string, string> pair)
{
    string key = pair.Key,
           value = pair.Value;

    int nameIndex = -1, nameIndex2 = -1;

    if (key.Contains($"{name1}"))
    {
        nameIndex = Random.Shared.Next(0, names.Length);
        key = key.Replace($"{name1}", names[nameIndex]);
        value = value.Replace($"{name1}", names[nameIndex]);
    }

    if (key.Contains($"{name2}"))
    {
        do
        {
            nameIndex2 = Random.Shared.Next(0, names.Length);
        } while (nameIndex == nameIndex2);
        key = key.Replace($"{name2}", names[nameIndex]);
        value = value.Replace($"{name2}", names[nameIndex]);
    }

    if (key.Contains($"{lvl}"))
    {
        StringBuilder sb = new();
        for (int i = 0; i < 3; ++i)
        {
            int sum = 0;
            if (Random.Shared.Next(0, 2) == 1)
                sum += 1;
            if (Random.Shared.Next(0, 2) == 1)
                sum += 2;
            if (Random.Shared.Next(0, 2) == 1)
                sum += 4;
            sb.Append(sum);
        }
        string str = sb.ToString();
        key = key.Replace($"{lvl}", str);
        value = value.Replace($"{lvl}", str);
    }

    return new(key, value);
}