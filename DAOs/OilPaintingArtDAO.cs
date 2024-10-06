using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class OilPaintingArtDAO
    {
        private readonly OilPaintingArt2024DbContext _context;
        private static OilPaintingArtDAO instance = null;
        private OilPaintingArtDAO()
        {
            _context = new OilPaintingArt2024DbContext();
        }

        public static OilPaintingArtDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    return new OilPaintingArtDAO();
                }
                return instance;
            }
        }

        public async Task<List<OilPaintingArt>> GetList()
        {

           return await _context.OilPaintingArts.Include(x => x.Supplier).ToListAsync();
        }
        public async Task<OilPaintingArt> GetOilPaintingArtById(int id)
        {

           return await _context.OilPaintingArts.Include(x => x.Supplier).FirstOrDefaultAsync(m => m.OilPaintingArtId == id);
        }

        public async Task AddOilPaintingArt(OilPaintingArt oilPaintingArt)
        {
            try
            {
               var existOilPaintingArt = await GetOilPaintingArtById(oilPaintingArt.OilPaintingArtId);
                if (existOilPaintingArt != null)
                {
                    throw new Exception ("Already exist");
                }
            }catch (Exception e)
            {
                throw e;
            }
            _context.OilPaintingArts.Add(oilPaintingArt);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOilPaintingArt(OilPaintingArt oilPaintingArt)
        {
            try
            {
                var existOilPaintingArt = await GetOilPaintingArtById(oilPaintingArt.OilPaintingArtId);
                if(existOilPaintingArt == null)
                {
                    throw new Exception("Not found");
                }
                existOilPaintingArt.ArtTitle = oilPaintingArt.ArtTitle;
                existOilPaintingArt.Artist = oilPaintingArt.Artist;
                existOilPaintingArt.NotablFeatures = oilPaintingArt.NotablFeatures; 
                existOilPaintingArt.PriceOfOilPaintingArt = oilPaintingArt.PriceOfOilPaintingArt;
                existOilPaintingArt.OilPaintingArtLocation = oilPaintingArt.OilPaintingArtLocation;
                existOilPaintingArt.OilPaintingArtStyle = oilPaintingArt.OilPaintingArtStyle;
                existOilPaintingArt.CreatedDate = oilPaintingArt.CreatedDate;
                existOilPaintingArt.StoreQuantity = oilPaintingArt.StoreQuantity;

                var supplier = await _context.SupplierCompanies.FirstOrDefaultAsync(x => x.SupplierId == oilPaintingArt.SupplierId);    
                if(supplier == null)
                {
                    throw new Exception("Supplier not found");
                }
                existOilPaintingArt.SupplierId = oilPaintingArt.SupplierId;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteOilPaintingArt(int id)
        {
            try
            {
                var existOilPaintingArt = await GetOilPaintingArtById(id);
                if (existOilPaintingArt == null)
                {
                    throw new Exception("Not found");
                }
               _context.OilPaintingArts.Remove(existOilPaintingArt);
                 await _context.SaveChangesAsync();
            }
            catch (Exception e)
                {
                throw e;
            }
        }

      

    }
}
