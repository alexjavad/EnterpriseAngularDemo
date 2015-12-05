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
    public class DeveloperController : ApiController
    {
        public Logger Log { get; set; }

        public DeveloperController()
        {
            Log = LogManager.GetCurrentClassLogger();
        }

        [Route("api/developers/GetDevelopers")]
        public IEnumerable<Developer> GetDevelopers()
        {
            List<Developer> results = new List<Developer>();
            try
            {
                using (DevDAL devDal = new DevDAL())
                {
                    results = devDal.GetDevelopers();
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
