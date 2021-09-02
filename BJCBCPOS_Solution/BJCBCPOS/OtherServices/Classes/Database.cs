using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace BJCBCPOS.OtherServices.Classes {
  class Database
  {
    // Development Server
    private string ConnectionString = "Data Source = localhost; Password=Snaps@BGC.THx#1WMS>18Dev;Persist Security Info=True; User ID=SnapsMe;Initial Catalog=STDBENGINE;";
    public Database()
    {
    }

    public DataSet GetDataset(string SQL)
    {
      try
      {
        DataSet DS = new DataSet();
        using (var CN = new SqlConnection(this.ConnectionString))
        {
          try
          {
            CN.Open();
            using (var DA = new SqlDataAdapter(SQL, CN))
            {
              DA.Fill(DS);
            }

            CN.Close();
          }
          catch (Exception ex)
          {
            CN.Close();
            throw ex;
          }
        }

        return DS;
      }
      catch (Exception exc)
      {

        throw exc;
      }

    }
    public bool ExecuteSQL(string SQL)
    {
      bool Exc = false;
      using (var CN = new SqlConnection(this.ConnectionString))
      {
        try
        {
          CN.Open();
          using (var CM = new SqlCommand(SQL, CN))
          {
            CM.ExecuteNonQuery();
            Exc = true;
          }

          CN.Close();
        }
        catch (Exception ex)
        {
          CN.Close();
          throw ex;
        }
      }

      return Exc;
    }
    public string GetString(string SQL)
    {
      string SC = "";
      using (var CN = new SqlConnection(this.ConnectionString))
      {
        try
        {
          CN.Open();
          using (var CM = new SqlCommand(SQL, CN))
          {
            SC = CM.ExecuteScalar().ToString();
          }

          CN.Close();
        }
        catch (Exception ex)
        {
          CN.Close();
          throw ex;
        }
      }

      return SC;
    }
  }
}
