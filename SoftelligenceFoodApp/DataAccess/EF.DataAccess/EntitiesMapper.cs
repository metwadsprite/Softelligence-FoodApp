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
                cfg.CreateMap<User, UserDO>().PreserveReferences();
                cfg.CreateMap<UserDO, User>().PreserveReferences();


                cfg.CreateMap<StoreDO, Store>().PreserveReferences()
                    .ForMember(dest => dest.menuItems,
                               opt => opt.MapFrom(source => source.Menu.MenuItems));
                cfg.CreateMap<Store, StoreDO>().PreserveReferences()
                    .ForMember(dest => dest.Menu.MenuItems,
                               opt => opt.MapFrom(source => source.menuItems));


                cfg.CreateMap<Session, SessionDO>().PreserveReferences()
                    .ForMember(dest => dest.SessionStore,
                                source => source.Ignore());
                cfg.CreateMap<SessionDO, Session>().PreserveReferences()
                    .ForMember(dest => dest.Stores,
                                source => source.Ignore());
            });

            currentMapper = mapperConfig.CreateMapper();
        }

        public DestinationClass MapData<DestinationClass, SourceClass>(SourceClass item) where DestinationClass : new()
        {

            var destination = new DestinationClass();

            destination = currentMapper.Map<SourceClass, DestinationClass>(item);
            
            return destination;
        }

        public void MapToSessionsDO(Session sessionSource, SessionDO sessionDODestination)
        {
            currentMapper.Map(sessionSource, sessionDODestination);

            currentMapper.Map(sessionSource.Orders, sessionDODestination.Orders);

            for (int i = 0; i < sessionDODestination.Orders.Count(); i++)
            {
                currentMapper.Map(sessionSource.Stores.ElementAt(i), sessionDODestination.SessionStore.ElementAt(i).Store);
            }
        }

        public void MapToSession(SessionDO sessionDOSource, Session sessionDestination)
        {
            currentMapper.Map(sessionDOSource, sessionDestination);
            currentMapper.Map(sessionDOSource.Orders, sessionDestination.Orders);

            for (int i = 0; i < sessionDestination.Orders.Count(); i++)
            {
                currentMapper.Map(sessionDOSource.SessionStore.ElementAt(i).Store, sessionDestination.Stores.ElementAt(i));
            }
        }

    }
}
