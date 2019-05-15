using System;
using AutoMapper;

namespace DataMapper
{
    public class EntitiesMapper
    {
        //initialize/configure
        //getmapperobject -> imapper , instantiez si apelat in unittest
        //
        public DestinationClass MapData<DestinationClass, SourceClass>(SourceClass item) where DestinationClass : new()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DestinationClass, SourceClass>();
            });

            IMapper iMapper = config.CreateMapper();

            var destination = new DestinationClass();

            destination = iMapper.Map<SourceClass, DestinationClass>(item);

            return destination;
        }
        

    }
}