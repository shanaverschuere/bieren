using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Bieren.CORE.Entities
{
  public class BierSoorten
    {
        #region Properties
        public int Id { get; set; }

        private string soort;

        public string Soort
        {
            get { return soort; }
            set 
            {
                value = value.Trim();
                if (value.Length == 0)
                {
                    throw new Exception("Waarde biersoorten kan niet leeg zijn"); 
                }
                if (value.Length > 50)
                {
                    value = value.Substring(0, 50);  //is de waarde langer dan 50, dan kappen we het af 
                }
                soort = value; }
        }
        #endregion

        #region Constructor

        public BierSoorten()
        {

        }

        public BierSoorten(int id, string soort)
        {
            this.Id = id;
            Soort = soort; //je neemt de publieke propertie vast (het heeft een restrictie)  
        }
        #endregion

        public override string ToString()
        {
            return $"{soort}"; 
        }
    }
}
