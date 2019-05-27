using AutoMapper;
using BusinessLogic;
using EF.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.DataAccess
{
    public class EntitiesMapper
    {
        private MapperConfiguration mapperConfig;
        private IMapper currentMapper;

        public void InitializeMapper()
        {
            mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MenuItem, MenuItemDO>();
                cfg.CreateMap<MenuItemDO, MenuItem>();

                cfg.CreateMap<Menu, MenuDO>();
                cfg.CreateMap<MenuDO, Menu>();

                cfg.CreateMap<User, UserDO>();
                cfg.CreateMap<UserDO, User>();


                cfg.CreateMap<StoreDO, Store>();
                cfg.CreateMap<Store, StoreDO>();

                cfg.CreateMap<Session, SessionDO>();
                cfg.CreateMap<SessionDO, Session>();
            });

            currentMapper = mapperConfig.CreateMapper();
        }

        public DestinationClass MapData<DestinationClass, SourceClass>(SourceClass item) where DestinationClass : new()
        {

            var destination = currentMapper.Map<SourceClass, DestinationClass>(item);
            
            return destination;
        }
    }
}
