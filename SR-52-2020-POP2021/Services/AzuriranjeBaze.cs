using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SR_52_2020_POP2021.Services
{
    public class AzuriranjeBaze<T>
    {
        public static string ucitajConnectionString()
        {
            string putanjaFolderaBaze = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory), @"..\..\Baza\"));
            return  @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + putanjaFolderaBaze + @"FitnesCentri.mdf;Integrated Security=True";

        }

        public static void insertUpdateDelete_Baza(string sql)  
        {

            SqlConnection conn = new SqlConnection(ucitajConnectionString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                conn.Close();
            }


        }

        public virtual ObservableCollection<T> citanjeBaze() { return null; }


    }
}
