using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularDemo.Classes;

namespace AngularDemo.DAL
{
    public class FavColorDAL : DapperBase
    {
        public List<FavoriteColorPair> GetFavorites()
        {
            List<FavoriteColorPair> results = new List<FavoriteColorPair>();
            try
            {
                ConfigureCommand("dbo.usp_DEMO_GetDeveloperColorXRefs");
                results = GetList<FavoriteColorPair>();
            }
            catch (Exception ex)
            {
                WriteErrorToLog("Error getting favorite colors ", System.Reflection.MethodBase.GetCurrentMethod().ToString(), ex);
                results = new List<FavoriteColorPair>();
            }
            finally
            {
                ClearParameters();
            }
            return results;
        }

        public FavoriteColorPair Create(FavoriteColorPair favorite)
        {
            FavoriteColorPair result = null;
            try
            {
                ConfigureCommand("dbo.usp_DEMO_InsertFavorite");
                AddParamWithValue("DevId", favorite.DeveloperId);
                AddParamWithValue("ColorId", favorite.ColorId);
                result = GetSingle<FavoriteColorPair>();

            }
            catch (Exception ex)
            {
                WriteErrorToLog("Error executing usp_DEMO_InsertFavorite", System.Reflection.MethodBase.GetCurrentMethod().ToString(), ex);
            }
            finally
            {
                ClearParameters();
            }
            return result;
        }

        public bool Update(FavoriteColorPair favorite)
        {
            bool result = false;
            try
            {
                ConfigureCommand("dbo.usp_DEMO_UpdateFavorite");
                AddOutputParamWithValue("Succeeded", 0, DbType.Boolean);
                AddParamWithValue("Id", favorite.Id);
                AddParamWithValue("ColorId", favorite.ColorId);
                result = ExecuteNonQueryWithOutputParam<bool>("Succeeded");

            }
            catch (Exception ex)
            {
                WriteErrorToLog("Error executing usp_DEMO_InsertFavorite", System.Reflection.MethodBase.GetCurrentMethod().ToString(), ex);
            }
            finally
            {
                ClearParameters();
            }
            return result;
        }
    }
}
