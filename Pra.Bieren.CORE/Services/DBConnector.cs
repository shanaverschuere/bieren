using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Pra.Bieren.CORE.Services
{
    public class DBConnector
    {
        public static DataTable ExecuteSelect(string sqlInstructie) //zal een datatable retourneren. Is een SELECT statement 
                                                                    //deze methode moet je eigenlijk maar 1 keer zetten, dan werkt alles goed
        {
            string constring = Helper.GetConnectionString(); //geef mij de connectie string (standaard) 
            DataSet ds = new DataSet();//geef mij een dataset terug 
            SqlDataAdapter da = new SqlDataAdapter(sqlInstructie, constring); //om een SLQ instructie naar SQL te sturen heb je dit nodig. 
            try
            {
                da.Fill(ds); //instructie verzonden naar SQL, dan stuur je dataset naar je database 
            }
            catch (Exception fout) //indien het niet lukt krijg je foutmelding 
            {
                string foutmelding = fout.Message;
                return null;
            }
            return ds.Tables[0];
        }

        public static bool ExecuteCommand(string sqlInstructie) //INSERT UPDATE DELETE instructies, zal een bool returneren (gelukt of niet gelukt) 
        {
            string constring = Helper.GetConnectionString(); //geef mij de connectie string (standaard) 
            SqlConnection mijnVerbinding = new SqlConnection(constring);
            SqlCommand mijnOpdracht = new SqlCommand(sqlInstructie, mijnVerbinding); //SQL instructie 
            try
            {
                mijnOpdracht.Connection.Open(); //verbinding opendoen
                mijnOpdracht.ExecuteNonQuery(); //uitvoeren SQL instructie 
                return true;
            }
            catch (Exception fout) //hier blijft de connectie openstaan, vandaar via de finally met een close 
            {
                string foutmelding = fout.Message;
                return false;
            }
            finally //zal altijd uitgevoerd worden. Zal eerst de finally eerst doen voordat hij de try return of catch return doet 
            {
                mijnVerbinding.Close();
            }
        }

        public static string ExecuteScalaire(string sqlScalaireInstructie) //Stuur je een SELECT instructie naar. Statistische functies bv select count(*) from boeken. Zal een getal retourneren onder de variabele string 
                                                                           // er komt 1 kolom met 1 waarde
        {
            string constring = Helper.GetConnectionString();
            SqlConnection mijnVerbinding = new SqlConnection(constring);
            SqlCommand mijnOpdracht = new SqlCommand(sqlScalaireInstructie, mijnVerbinding);

            string retour = null;
            mijnVerbinding.Open();
            try
            {
                retour = mijnOpdracht.ExecuteScalar().ToString();
            }
            catch (Exception fout)
            {

                return null;
            }
            finally
            {
                mijnVerbinding.Close();
            }

            return retour;
        }
    }
}
