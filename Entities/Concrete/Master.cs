using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Master:IEntity
    {
        public int Id { get; set; }
        public string SliderDescription { get; set; }
        public string SliderImgUrls { get; set; }
        public string Description { get; set; }
        public string DescriptionPhotoUrl { get; set; }
        public string AboutPhotoUrl { get; set; }
        public string NiceWordDescription { get; set; }
        public string NiceWordImgUrl { get; set; }
        public string FooterPhotoUrl { get; set; }
    }
}
