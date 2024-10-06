using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OilPaintingRepo : IOilPaintingRepo
    {
        public Task AddOilPaintingArt(OilPaintingArt oilPaintingArt)
        {
                return OilPaintingArtDAO.Instance.AddOilPaintingArt(oilPaintingArt);
        }

        public Task DeleteOilPaintingArtById(int id)
        {
            return OilPaintingArtDAO.Instance.DeleteOilPaintingArt(id);
        }

        public Task<List<OilPaintingArt>> GetList()
        {
        return OilPaintingArtDAO.Instance.GetList();
        }

        public Task<OilPaintingArt> GetOilPaintingArtById(int id)
        {
            return OilPaintingArtDAO.Instance.GetOilPaintingArtById(id);
        }

        public Task UpdateOilPaintingArt(OilPaintingArt oilPaintingArt)
        {
        return OilPaintingArtDAO.Instance.UpdateOilPaintingArt(oilPaintingArt);
        }
    }
}
