using System.Linq;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.QueryableExtensions;
using FakeItEasy;
using Xunit;

namespace TestProject1
{
    public class FakeItEasyTest
    {
        private readonly Mapper _mapper;
        private readonly IQueryable<Entity> _queryable;

        public FakeItEasyTest()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Entity, EntityDto>()));
            _queryable = A.Fake<IQueryable<Entity>>();
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

            // never returns!
            Assert.Empty(mappedQueryable.ToArray());
        }

        [Fact]
        public void CanEnumerateProjectedQueryable()
        {
            // never returns!
            IQueryable<EntityDto> mappedQueryable = _queryable.ProjectTo<EntityDto>(_mapper.ConfigurationProvider);
            Assert.Empty(mappedQueryable.ToArray());
        }
    }
}