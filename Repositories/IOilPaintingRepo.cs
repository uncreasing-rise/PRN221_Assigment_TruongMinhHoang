using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IOilPaintingRepo
        
    {
        Task<List<OilPaintingArt>> GetList();
        Task<OilPaintingArt> GetOilPaintingArtById(int id);
        Task AddOilPaintingArt(OilPaintingArt oilPaintingArt);
        Task UpdateOilPaintingArt(OilPaintingArt oilPaintingArt);
        Task DeleteOilPaintingArtById(int id);
    }
}
