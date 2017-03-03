using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Seed
{
    class Program
    {
        static BillingContext context = new BillingContext();
        static string sourceRoot = @"C:\NTG\Billing\billing.xls";

        static void Main(string[] args)
        {
            getTowns();
            getAgents();
            Console.ReadKey();
        }

        static void getTowns()
        {
            Console.Write("Towns: ");
            IBillingRepository<Town> towns = new BillingRepository<Town>(context);
            DataTable rawData = OpenExcel(sourceRoot, "Towns");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                Town town = new Town()
                {
                    Zip = getString(row, 0),
                    Name = getString(row, 1),
                    Region = (Region)getInteger(row, 2)
                };
                N++;
                towns.Insert(town);
            }
            towns.Commit();
            Console.WriteLine(N);
        }

        static void getAgents()
        {
            Console.Write("Agents: ");
            IBillingRepository<Agent> agents = new BillingRepository<Agent>(context);
            IBillingRepository<Town> towns = new BillingRepository<Town>(context);
            DataTable rawData = OpenExcel(sourceRoot, "Agents");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                Agent agent = new Agent() 
                {
                    Name = getString(row, 1)
                };
                N++;
                string[] Zone = getString(row, 2).Split(',');
                foreach(string Z in Zone) {
                    Region R = (Region)Convert.ToInt32(Z);
                    var area = towns.Get().Where(x => x.Region == R).ToList();
                    foreach(var city in area)
                    {
                        agent.Towns.Add(city);
                    }
                }
                agents.Insert(agent);
            }
            agents.Commit();
            Console.WriteLine(N);
        }

        static DataTable OpenExcel(string path, string sheet)
        {
            var cs = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0", path);
            OleDbConnection conn = new OleDbConnection(cs);
            conn.Open();

            OleDbCommand cmd = new OleDbCommand(string.Format("SELECT * FROM [{0}$]", sheet), conn);
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = cmd;

            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();

            return dt;
        }

        static string getString(DataRow row, int index)
        {
            return row.ItemArray.GetValue(index).ToString();
        }

        static int getInteger(DataRow row, int index)
        {
            return Convert.ToInt32(row.ItemArray.GetValue(index).ToString());
        }

        static DateTime getDate(DataRow row, int index)
        {
            return Convert.ToDateTime(row.ItemArray.GetValue(index).ToString());
        }

        static double getDouble(DataRow row, int index)
        {
            return Convert.ToDouble(row.ItemArray.GetValue(index).ToString());
        }
    }
}