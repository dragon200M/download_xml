using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DatabaseUtils
{
    public class Connection
    {
        private static string sqlstatement_1 = "select seria,Optymalizacja,XMLciecie from Optymalizacja where Seria = @ID";
        private static string sqlstatement_2 = "select seria,Optymalizacja,XMLciecie from Optymalizacja where Seria = @ID";
        private static string xmlCiec = "XMLciecie";
        private static string xmlOptymalizacji = "Optymalizacja";
        
        private static string noneStatement = "NONE";
        
        private Parametry _parametry;

        public Connection(Parametry p)
        {
            _parametry = p;
        }

        public string chooseSqlStatement()
        {
            string resoult = "NONE";
            
            if (_parametry != null)
            {
                switch (_parametry.Rodzajxml)
                {
                    case "XmlCiecie":
                        resoult = sqlstatement_1;
                        break;
                    case "XMLSeria":
                        resoult = sqlstatement_2;
                        break;
                    default:
                        resoult = noneStatement;
                        break;
                }
               
            }
            
            return resoult;
        }
        
        public string chooseSqlColumn()
        {
            string resoult = "NONE";
            
            if (_parametry != null)
            {
                switch (_parametry.Rodzajxml)
                {
                    case "XmlCiecie":
                        resoult = xmlCiec;
                        break;
                    case "XMLSeria":
                        resoult = xmlOptymalizacji;
                        break;
                    default:
                        resoult = noneStatement;
                        break;
                }
               
            }
            
            return resoult;
        }


        public void getXmlFromDB()
        {
            if (_parametry != null)
            {
                string connectionString = _parametry.Polaczenie;
                string sqlStatemant = chooseSqlStatement();
                string column = chooseSqlColumn();

                try
                {
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(sqlStatemant, sqlConnection);
                    cmd.Parameters.Add("@ID", SqlDbType.Int);
                    cmd.Parameters["@ID"].Value = _parametry.Seria;

                    var reader = cmd.ExecuteReader();
                    reader.Read();

                    var xmlBuffer = reader[column];

                    using (StreamWriter writer =
                        new StreamWriter("C:\\Users\\maciej\\Desktop\\" + _parametry.Plikwynikowy))
                    {
                        writer.WriteLine(xmlBuffer);
                    }

                    sqlConnection.Close();

                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Podany argument jest nie poprawny. " + e);
                }
                catch (SqlException s)
                {
                    Console.WriteLine("Bład query sql -> XmlCiecie lub XMLSeria");
                }





            }


            
        }




    }
}