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
    Console.WriteLine("**** ERROR! No se pudieron obtener las Civilizaciones, intente mas tarde ****");
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