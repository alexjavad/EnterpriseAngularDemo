using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularDemo.Classes;

namespace AngularDemo.DAL
{
    public class ColorDAL : DapperBase
    {
        public List<ColorClass> GetColors()
        {
            List<ColorClass> results = new List<ColorClass>();
            try
            {
                ConfigureCommand("dbo.usp_DEMO_GetColors");
                results = GetList<ColorClass>();
            }
            catch (Exception ex)
            {
                WriteErrorToLog("Error getting colors ", System.Reflection.MethodBase.GetCurrentMethod().ToString(), ex);
                results = new List<ColorClass>();
            }
            finally
            {
                ClearParameters();
            }
            return results;
        }
    }
}
