using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AngularDemo.Classes;
using AngularDemo.DAL;
using NLog;

namespace AngularDemo.Web.Controllers
{
    public class ColorController : ApiController
    {
        public Logger Log { get; set; }

        public ColorController()
        {
            Log = LogManager.GetCurrentClassLogger();
        }


        [Route("api/colors/GetColors")]
        public IEnumerable<ColorClass> GetColors()
        {
            List<ColorClass> results = new List<ColorClass>();
            try
            {
                using (ColorDAL cDal = new ColorDAL())
                {
                    results = cDal.GetColors();
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ERROR in {0}, Error Message: {1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message));
            }
            return results;
        }
    }
}
