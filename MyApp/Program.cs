using System.Net;
using System.Text.Json;
//using System.Linq; -> para usar OrderBy 

int ejercicio;
do
{
    do
    {
        Console.Write("\n=> INGRESE UNA OPCION:");
        System.Console.WriteLine("(1)Ejercicio1 - (2)Ejercicio2 - (3)Salir)");
        ejercicio = Convert.ToInt32(Console.ReadLine());
    } while (ejercicio<1 || ejercicio>3);
    if (ejercicio==1)
    {
        ejercicio1();
    }
    else
    {
        if (ejercicio==2)
        {
            ejercicio2();
        }
    }
} while (ejercicio==1 || ejercicio==2);
Console.WriteLine("***** FIN DEL PROGRAMA *****");

////////////////////////////////

void ejercicio1()
{
    RootCivilizacion Civi = obtenerCivilizaciones();
    if (Civi!=null)
    {
        Civi.Civilizations = Civi.Civilizations.OrderBy(x => x.Name).ToList(); //ordenar la lista por nombres
        mostrarCivilizaciones();
        Console.WriteLine($"\n**** Total de Civilizaciones: {Civi.Civilizations.Count()} ****");
        mostrarCaracteristicas();
    }
    else
    {
        Console.WriteLine("**** ERROR! No se pudieron obtener las Civilizaciones, intente mas tarde ****");
    }
    Console.WriteLine("----- FIN DE EJERCICIO 1 -----");

    RootCivilizacion obtenerCivilizaciones()
    {
        var url = $"https://age-of-empires-2-api.herokuapp.com/api/v1/civilizations";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";
        RootCivilizacion Civi=null;
        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader != null)
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            Civi = JsonSerializer.Deserialize<RootCivilizacion>(responseBody)!;
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            // Handle error;
        }
        return Civi; 
    }

    void mostrarCivilizaciones()
    {
        Console.WriteLine("CIVILIZACION | ID\n");
        foreach (Civilization unaCivilizacion in Civi.Civilizations)
        {
            Console.WriteLine($"{unaCivilizacion.Name} -> ({unaCivilizacion.Id})");
        }
    }

    void mostrarCaracteristicas()
    {
        int id;
        bool aux;
        int op;
        do
        {
            Console.WriteLine("\n=> Ingrese el ID de la Civilizacion para ver sus caracteriticas:");
            id = Convert.ToInt32(Console.ReadLine());
            aux = false;
            foreach (Civilization unaCivilizacion in Civi.Civilizations)
            {
                if (unaCivilizacion.Id==id)
                {
                    Console.WriteLine($"\n# Id: {unaCivilizacion.Id}");
                    Console.WriteLine($"# Name: {unaCivilizacion.Name}");
                    Console.WriteLine($"# Expansion: {unaCivilizacion.Expansion}");
                    Console.WriteLine($"# ArmyType: {unaCivilizacion.ArmyType}");
                    if (unaCivilizacion.UniqueUnit.Count()>0) //ejemplo: la Civ de id 31 no tiene UniqueUnit
                    {
                        Console.WriteLine($"# UniqueUnit: {unaCivilizacion.UniqueUnit[0]}");
                    }
                    Console.WriteLine($"# TeamBonus: {unaCivilizacion.TeamBonus}");
                    Console.WriteLine($"# CivilizationBonus:");
                    foreach (string item in unaCivilizacion.CivilizationBonus)
                    {
                        Console.WriteLine("     - "+item);
                    }
                    aux = true;
                    break;
                }
            }
            if (!aux)
            {
                Console.WriteLine("\n**** Error! el ID ingresado es invalido ****");
            }
            do
            {
                Console.WriteLine("\n[1]CONSULTAR NUEVAMENTE - [0]SALIR");
                op = Convert.ToInt32(Console.ReadLine());
            } while (op<0 || op>1);
        } while (op==1);
    }
}

//////////////////////////////////////////

void ejercicio2()
{
    int id;
    Console.WriteLine("=>Ingrese el 'Id' de la unidad que desea obtener: ");
    id = Convert.ToInt32(Console.ReadLine());
    RootUnidad Uni = obtenerUnidad(id);
    if (Uni!=null)
    {
        Console.WriteLine($"\n# ID: {Uni.Id}");
        Console.WriteLine($"# Name: {Uni.Name}");
        Console.WriteLine($"# Description: {Uni.Description}");
        Console.WriteLine($"# Expansion: {Uni.Expansion}");
        Console.WriteLine($"# Age: {Uni.Age}");
        Console.WriteLine($"# CreatedIn: {Uni.CreatedIn}");
        Console.WriteLine($"# Wood: {Uni.Cost.Wood}");
        Console.WriteLine($"# Gold: {Uni.Cost.Gold}");
        Console.WriteLine($"# BuildTime: {Uni.BuildTime}");
        Console.WriteLine($"# ReloadTime: {Uni.ReloadTime}");
        Console.WriteLine($"# HitPoints: {Uni.HitPoints}");
        Console.WriteLine($"# Range: {Uni.Range}");
        Console.WriteLine($"# Attack: {Uni.Attack}");
        Console.WriteLine($"# Armor: {Uni.Armor}");
        Console.WriteLine($"# Accuracy: {Uni.Accuracy}");
    }
    else
    {
        Console.WriteLine("**** ERROR! No se pudo obtener la Unidad, intente mas tarde ****");
    }
    Console.WriteLine("----- FIN DE EJERCICIO 2 -----");

    RootUnidad obtenerUnidad(int id) //obtiene una unidad segun un ID
    {
        var url = $"https://age-of-empires-2-api.herokuapp.com/api/v1/unit/{id}";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";
        RootUnidad Uni=null;
        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader != null)
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            Uni = JsonSerializer.Deserialize<RootUnidad>(responseBody)!;
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            // Handle error;
        }
        return Uni; 
    }
}