using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data.Entities.Media;

namespace ECommerce.Services.Catalog
{
    public interface IPictureService
    {
        string GetPictureUrl(Picture picture);

    }
}