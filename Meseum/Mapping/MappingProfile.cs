using Meseum.ViewModel;
using Meseum.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meseum.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            var config = new MapperConfiguration(cfg =>
            {
                CreateMap<Inventory, InventoryVM>();
                CreateMap<InventoryVM, Inventory>();
                CreateMap<IEnumerable<Files>,IEnumerable<ImageFile>>();
                CreateMap<ImageFile,Files>();

            });
            IMapper iMapper = config.CreateMapper();
           
        }
    }
}
