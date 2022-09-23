using CatalogService.Domain.Common;

namespace CatalogService.Domain
{
    public class Catalog:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }


        //Relationship
        public CatalogBrand CatalogBrand { get; set; }
        public CatalogType CatalogType { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }
    }
}
