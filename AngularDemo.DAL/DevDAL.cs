using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularDemo.Classes;

namespace AngularDemo.DAL
{
    public class DevDAL : DapperBase
    {
        public List<Developer> GetDevelopers()
        {
            List<Developer> results = new List<Developer>();
            try
            {
                ConfigureCommand("dbo.usp_DEMO_GetDevelopers");
                results = GetList<Developer>();
            }
            catch (Exception ex)
            {
                WriteErrorToLog("Error getting developers ", System.Reflection.MethodBase.GetCurrentMethod().ToString(), ex);
                results = new List<Developer>();
            }
            finally
            {
                ClearParameters();
            }
            return results;
        }
    }
}
