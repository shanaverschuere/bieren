using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Bieren.CORE.Services
{
    public class Helper
    {
        public static string GetConnectionString()
        {
            return @"Data Source=(local)\SQLEXPRESS;Initial Catalog=praBieren;User Id=bieramateur;Password=komMaarEensBinnen;";
        }
        public static string HandleQuotes(string waarde)
        {
            return waarde.Trim().Replace("'", "''"); //zit er een ' dan moet je dit wegschrrijven. Deze methode altijd gebruiken hiervoor. Zal ' vervangen door 2x ' 
        }
    }
}
