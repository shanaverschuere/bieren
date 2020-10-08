using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Pra.Bieren.CORE.Entities;

namespace Pra.Bieren.CORE.Services
{
    public class BierService
    {
        #region Properties

        private List<Bier> bieren;

        public List<Bier> Bieren
        {
            get { return bieren; } //is om de list op te halen, een set is niet nodig omdat je niets hoeft te wijzigen
        }
        #endregion

        #region Constructor 
        public BierService()
        {
            ReadAllRecords();
        }
        #endregion

        #region Methoden

        private void ReadAllRecords()
        {
            bieren = new List<Bier>(); // instantieren van de list. bieren = de private propertie

            string sql = "select * from Bieren order by naam";
            DataTable dtBieren = DBConnector.ExecuteSelect(sql);
            foreach (DataRow row in dtBieren.Rows)
            {
                //Lange notatie
                //bieren.Add(
                //    new Bier
                //    {
                //        Id = int.Parse(row[0].ToString()),
                //    });

                bieren.Add(new Bier(int.Parse(row[0].ToString()), row[1].ToString(), int.Parse(row[2].ToString()), double.Parse(row[3].ToString()), int.Parse(row[4].ToString())));
            }
        }

        public bool AddBier(Bier bier)
        {
            // oppassen met alcohol: hier zit bv 5,3 in
            // SQL aanvaardt geen komma in gebroken getallen

            string strAlcohol = bier.Alcohol.ToString();
            strAlcohol = strAlcohol.Replace(",", ".");  //je vervangt hier de komma door een puntje 

            string sql;
            sql = "insert into Bieren(naam, bierSoortId, alcohol, score) values (";
            sql += "'" + Helper.HandleQuotes(bier.Naam) + ",";
            sql += bier.BierSoortID + ",";
            sql += bier.Alcohol + ",";
            sql += bier.Score + " ) ";

            if (DBConnector.ExecuteCommand(sql)) // als de sql commando lukt , dan doen we...
            {
                //onderstaande instructie nodig voor autonummering
                sql = "select max(id) from bieren";
                bier.Id = int.Parse(DBConnector.ExecuteScalaire(sql)); //bier id vullen met nieuw gevormd ID

                bieren.Add(bier);
                bieren = bieren.OrderBy(p => p.Naam).ToList(); //lambda notatie 

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditBier(Bier bier)
        {
            // oppassen met alcohol: hier zit bv 5,3 in
            // SQL aanvaardt geen komma in gebroken getallen

            string strAlcohol = bier.Alcohol.ToString();
            strAlcohol = strAlcohol.Replace(",", ".");  //je vervangt hier de komma door een puntje 


            string sql;
            sql = "update Bieren ";
            sql += " set naam = '" + Helper.HandleQuotes(bier.Naam) + ",";
            sql += " biersoortid = " + bier.BierSoortID + ", ";
            sql += " alcohol = " + strAlcohol + ",";
            sql += "score = " + bier.Score + " , ";
            sql += " where id = " + bier.Id;

            if (DBConnector.ExecuteCommand(sql))
            {
                //sorteer de list op naam
                bieren = bieren.OrderBy(p => p.Naam).ToList();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteBier(Bier bier)
        {
            string sql = "delete from bieren where id = " + bier.Id;
            if (DBConnector.ExecuteCommand(sql))
            {
                // bier ook uit de list verwijderen
                bieren.Remove(bier);
                //list opnieuw sorteren hoeft hier niet
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
