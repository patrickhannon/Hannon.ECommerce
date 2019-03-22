using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data.Entities.Catalog;
using ECommerce.Data.Entities.Media;

namespace ECommerce.Services.Catalog.Impl
{
    public class PictureService : IPictureService
    {
        public PictureService()
        {
            
        }
        public string GetPictureUrl(Picture picture)
        {
           string url = String.Empty;
            if (picture == null)
            {
                //url = GetDefaultPictureUrl(targetSize, defaultPictureType, storeLocation);
            }
           return url;
        }
    }
}