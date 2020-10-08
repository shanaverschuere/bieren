using System;
using System.Collections.Generic;
using System.Text;
using Pra.Bieren.CORE.Services;

namespace Pra.Bieren.CORE.Entities
{
    public class Bier
    #region Properties
    {
        public int Id { get; set; }

        public double Alcohol { get; set; }

        private int bierSoortID;

        private BierSoorten soort;

        public int BierSoortID
        {
            get { return BierSoortID; }
            set
            {
                bierSoortID = value;
                soort = BierSoortService.GetByID(value); //hier roep je de statische methode op, onmiddelijk gekoppeld met biersoort
            }
        }

        public BierSoorten Soort
        {
            get { return soort; }
        }

        private string naam;
        public string Naam
        {
            get { return naam; }
            set
            {
                value = value.Trim();
                if (value.Length <= 0)
                {
                    throw new Exception("Gelieve een geldige naam met minstens 1 character mee te geven");
                }
                if (value.Length >= 50)
                {
                    throw new Exception("Max aantal characters gebruikt, je naam mag max 50 tekens lang zijn");
                }
                naam = value;
            }
        }

        private decimal prijs;
        public decimal Prijs
        {
            get { return prijs; }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Gelieve een geldige getal in te vullen, gaande van 0 tot en met 18");
                }
                if (value > 18)
                {
                    throw new Exception("Gelieve een geldige getal in te vullen, gaande van 0 tot en met 18");
                }
                prijs = value;
            }
        }
        private int score;

        public int Score
        {
            get { return score; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Gelieve een geldige getal in te vullen");
                }
                score = value;
            }
        }
        #endregion

        #region Constructor 
        public Bier()
        {

        }

        public Bier(int id, string naam, int biersoortId, double alcohol, int score)
        {
            this.Id = id;
            Naam = naam; //hier pak je de publieke propertie aangezien er een restrictie op zit
            BierSoortID = biersoortId; //hier prop gebruiken om de setter te laten uitvoeren
            this.Alcohol = alcohol;
            this.score = score;
        }
        #endregion
        public override string ToString()
        {
            return $"{naam} - {Alcohol.ToString("0.00")}%";
        }
    }
}
