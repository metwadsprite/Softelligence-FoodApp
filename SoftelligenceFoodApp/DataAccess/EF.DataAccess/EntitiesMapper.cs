using AutoMapper;
using BusinessLogic;
using EF.DataAccess.DataModel;
using System;
using System.Collections.Generic;
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
                cfg.CreateMap<Store, StoreDO>().PreserveReferences();
                cfg.CreateMap<Session, SessionDO>().PreserveReferences()
            });

            currentMapper = mapperConfig.CreateMapper();
        }

        public DestinationClass MapData<DestinationClass, SourceClass>(SourceClass item) where DestinationClass : new()
        {

            var destination = new DestinationClass();

            destination = currentMapper.Map<SourceClass, DestinationClass>(item);
            
            return destination;
        }

        public void MapSessions(Session session, SessionDO sessionDO)
        {
            currentMapper.Map(session, sessionDO);
        }

    }
}
