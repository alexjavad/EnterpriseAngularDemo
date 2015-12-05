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
    public class FavoriteColorController : ApiController
    {
        public Logger Log { get; set; }

        public FavoriteColorController()
        {
            Log = LogManager.GetCurrentClassLogger();
        }

        [Route("api/favoritecolors/GetFavorites")]
        public IEnumerable<FavoriteColorPair> GetFavorites()
        {
            List<FavoriteColorPair> results = new List<FavoriteColorPair>();
            try
            {
                using (FavColorDAL fcDal = new FavColorDAL())
                {
                    results = fcDal.GetFavorites();
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ERROR in {0}, Error Message: {1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message));
            }
            return results;
        }

        [Route("api/favoritecolors/PostFavorite")]
        public FavoriteColorPair PostFavorite(FavoriteColorPair favorite)
        {
            FavoriteColorPair result = null;
            try
            {
                using (FavColorDAL fcDal = new FavColorDAL())
                {
                    result = fcDal.Create(favorite);
                }
            }
            catch (Exception e)
            {
                Log.Error(string.Format("ERROR in {0}, Error Message: {1}", System.Reflection.MethodBase.GetCurrentMethod(), e.Message));
            }

            return result;
        }

        [Route("api/favoritecolors/PutFavorite")]
        public void PutFavorite(FavoriteColorPair favorite)
        {
            bool result = false;
            try
            {
                using (FavColorDAL fcDal = new FavColorDAL())
                {
                    result = fcDal.Update(favorite);
                }
            }
            catch (Exception e)
            {
                Log.Error(string.Format("ERROR in {0}, Error Message: {1}", System.Reflection.MethodBase.GetCurrentMethod(), e.Message));
            }

            //return result;
        }

    }
}
