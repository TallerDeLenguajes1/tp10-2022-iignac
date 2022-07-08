using System.Net;
using System.Text.Json;
//using System.Linq; -> para usar OrderBy 

Civilizacion Civi = obtenerCivilizaciones();
if (Civi!=null)
{
    Civi.Civilizations = Civi.Civilizations.OrderBy(x => x.Name).ToList(); //ordenar la lista por nombres
    mostrarCivilizaciones();
    Console.WriteLine($"\n**** Total de Civilizaciones: {Civi.Civilizations.Count()} ****");
    mostrarCaracteristicas();
}
else
{
    Console.WriteLine("**** No se pudieron obtener las Civilizaciones, intente mas tarde ****");
}

//--------- Funciones ------------

Civilizacion obtenerCivilizaciones()
{
    var url = $"https://age-of-empires-2-api.herokuapp.com/api/v1/civilizations";
    var request = (HttpWebRequest)WebRequest.Create(url);
    request.Method = "GET";
    request.ContentType = "application/json";
    request.Accept = "application/json";
    Civilizacion Civi=null;
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
                        Civi = JsonSerializer.Deserialize<Civilizacion>(responseBody)!;
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
    Console.WriteLine("#### CIVILIZACIONES ####");
    foreach (Civilization unaCivilizacion in Civi.Civilizations)
    {
        Console.WriteLine(unaCivilizacion.Name);
    }
}

void mostrarCaracteristicas()
{
    string aux;
    bool aux2;
    int op;
    do
    {
        Console.WriteLine("\n=> Ingrese el nombre de la Civilizacion para ver sus caracteriticas (1er letra en mayusc):");
        aux = Console.ReadLine()!;
        aux2 = false;
        foreach (Civilization unaCivilizacion in Civi.Civilizations)
        {
            if (unaCivilizacion.Name==aux)
            {
                Console.WriteLine($"\n# Id: {unaCivilizacion.Id}");
                Console.WriteLine($"# Name: {unaCivilizacion.Name}");
                Console.WriteLine($"# Expansion: {unaCivilizacion.Expansion}");
                Console.WriteLine($"# ArmyType: {unaCivilizacion.ArmyType}");
                Console.WriteLine($"# TeamBonus: {unaCivilizacion.TeamBonus}");
                Console.WriteLine($"# CivilizationBonus:");
                foreach (string item in unaCivilizacion.CivilizationBonus)
                {
                    Console.WriteLine("     - "+item);
                }
                aux2 = true;
                break;
            }
        }
        if (!aux2)
        {
            Console.WriteLine("\n**** Error! el nombre ingresado es invalido ****");
        }
        do
        {
            Console.WriteLine("\n[1]CONSULTAR NUEVAMENTE - [0]SALIR");
            op = Convert.ToInt32(Console.ReadLine());
        } while (op<0 || op>1);
    } while (op==1);
}