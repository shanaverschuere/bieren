using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Text;
using Pra.Bieren.CORE.Entities;

namespace Pra.Bieren.CORE.Services
{
    #region propertie
    public class BierSoortService
    {
        private List<BierSoorten> biersoorten;

        public List<BierSoorten> Biersoorten
        {
            get { return biersoorten; }
        }
        #endregion

        #region Constructor

        public BierSoortService()
        {
            ReadAllRecords();
        }

        #endregion

        #region Methoden
        private void ReadAllRecords()
        {
            biersoorten = new List<BierSoorten>();

            string sql;
            sql = "select * from biersoorten order by soort";
            DataTable dtBierSoorten = DBConnector.ExecuteSelect(sql);
            foreach (DataRow row in dtBierSoorten.Rows)
            {
                biersoorten.Add(new BierSoorten(int.Parse(row[0].ToString()), row[1].ToString()));
            }
        }
        public bool AddBierSoort(BierSoorten biersoort)
        {
            string sql;
            sql = "insert into BierSoorten(soort) values (";
            sql += "'" + Helper.HandleQuotes(biersoort.Soort) + " ' ) ";

            if (DBConnector.ExecuteCommand(sql)) // als de sql commando lukt , dan doen we...
            {
                //onderstaande instructie nodig voor autonummering
                sql = "select max(id) from BierSoorten";
                biersoort.Id = int.Parse(DBConnector.ExecuteScalaire(sql)); //bier id vullen met nieuw gevormd ID

                biersoorten.Add(biersoort);
                biersoorten = biersoorten.OrderBy(p => p.Soort).ToList(); //lambda notatie 

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditBierSoort(BierSoorten biersoort)
        {
            string sql;
            sql = "update BierSoorten ";
            sql += " set soort = '" + Helper.HandleQuotes(biersoort.Soort) + " '";
            sql += " where id = " + biersoort.Id;

            if (DBConnector.ExecuteCommand(sql))
            {
                //sorteer de list op naam
                biersoorten = biersoorten.OrderBy(p => p.Soort).ToList();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteBierSoorten(BierSoorten biersoort)
        {
            string sql = "select count(*) from bieren where biersoortid = " + biersoort.Id;
            if (int.Parse(DBConnector.ExecuteScalaire(sql)) != 0)
            {
                return false;
                            }
            sql = " delete from biersoorten where id  = " + biersoort.Id;
            if (DBConnector.ExecuteCommand(sql))
            {
                // bier uit de list verwijderen
                biersoorten.Remove(biersoort);

                return true; 
            }
            else
            {
                return false; 
            }
        }

        public static BierSoorten GetByID(int id)
        {
            BierSoorten biersoort;
            string sql;

            sql = "select * from biersoorten where id" + id;
            DataTable dtBierSoorten = DBConnector.ExecuteSelect(sql);
            if (dtBierSoorten.Rows.Count == 0)
            {
                return null; 
            }
            else
            {
                DataRow row = dtBierSoorten.Rows[0];
                biersoort = new BierSoorten(int.Parse(row[0].ToString()), row[1].ToString()); 
            }
            return biersoort; 
        }
        #endregion
    }
}
