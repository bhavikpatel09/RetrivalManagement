using AutoMapper;

namespace RetrivalManagement.GraphQL.Services
{
    public class GraphQlMapper : IGraphQlMapper
    {
        private readonly IMapper mapper;

        public GraphQlMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(mapperConfigurationExpression =>
            {
                //Add custom mappers here
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        public T Map<T>(object source)
            where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return this.mapper.Map<T>(source);
        }
    }

    public interface IGraphQlMapper
    {
        T Map<T>(object source)
            where T : class;
    }
}
