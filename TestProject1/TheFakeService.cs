using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.QueryableExtensions;
using FakeItEasy;
using Xunit;

namespace TestProject1
{
    public class TheFakeService
    {
        private readonly Mapper _mapper;
        private readonly IEntityService _service;

        public TheFakeService()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Entity, EntityDto>()));
            _service = A.Fake<IEntityService>();
        }
        
        [Fact]
        public void ProvidesEnumerableQuery()
        {
            Assert.IsType<EnumerableQuery<Entity>>(_service.QueryAll());
        }

        [Fact]
        public void CanEnumerateQueryable()
        {
            Assert.Empty(_service.QueryAll().ToArray());
        }

        [Fact]
        public void CanEnumerateDataSourceQueryable()
        {
            IQueryable<EntityDto> mappedQueryable = _service.QueryAll().UseAsDataSource(_mapper.ConfigurationProvider).For<EntityDto>();
            Assert.Empty(mappedQueryable.ToArray());
        }

        [Fact]
        public void CanEnumerateProjectedQueryable()
        {
            IQueryable<EntityDto> mappedQueryable = _service.QueryAll().ProjectTo<EntityDto>(_mapper.ConfigurationProvider);
            Assert.Empty(mappedQueryable.ToArray());
        }
    }
}