using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithTypeAndBrandSpecifications :BaseSpecifications<Product>
    {
        public ProductWithTypeAndBrandSpecifications():base()
        {
            Includes.Add(P=>P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }
        public ProductWithTypeAndBrandSpecifications(int id):base(P=>P.Id==id)
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }
    }
}
