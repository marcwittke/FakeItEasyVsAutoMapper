using System.Linq;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.QueryableExtensions;
using FakeItEasy;
using Xunit;

namespace TestProject1
{
    public class TheDummy
    {
        private readonly Mapper _mapper;
        private readonly IQueryable<Entity> _queryable;

        public TheDummy()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Entity, EntityDto>()));
            _queryable = A.Dummy<IQueryable<Entity>>();
        }

        [Fact]
        public void ProvidesEnumerableQuery()
        {
            Assert.IsType<EnumerableQuery<Entity>>(_queryable);
        }
        
        [Fact]
        public void CanEnumerateQueryable()
        {
            Assert.Empty(_queryable.ToArray());
        }

        [Fact]
        public void CanEnumerateDataSourceQueryable()
        {
            IQueryable<EntityDto> mappedQueryable = _queryable.UseAsDataSource(_mapper.ConfigurationProvider).For<EntityDto>();
            Assert.Empty(mappedQueryable.ToArray());
        }

        [Fact]
        public void CanEnumerateProjectedQueryable()
        {
            IQueryable<EntityDto> mappedQueryable = _queryable.ProjectTo<EntityDto>(_mapper.ConfigurationProvider);
            Assert.Empty(mappedQueryable.ToArray());
        }
    }
}