using System;
using GraphQLHotChoclateServer.Shared;
using MasterProject.Infrastructure;
using MasterProject.Infrastructure.Data;
using MasterProject.SharedKernel.Repository;
using MasterProject.Core.Entities;
using MasterProject.SharedKernel.Entities;
using MasterProject.SharedKernel.Specification;
namespace GraphQLHotChoclateServer
{
    [ExtendObjectType(typeof(GraphQLHotChoclateServer.Shared.GraphQLQuery))]
    public class GraphQLProductQuery
    {
        private readonly AppDBContext _appDBContext;
        private readonly EfRepository _repository;
        public GraphQLProductQuery(AppDBContext appDBContext, EfRepository repository)
        {
            _appDBContext = appDBContext;
            _repository = repository;
        }

        [UsePaging(IncludeTotalCount = GraphQLQuery.INCLUDE_TOTAL_COUNT, AllowBackwardPagination = GraphQLQuery.ALLOW_BACKWARD_PAGINATION,
            MaxPageSize = GraphQLQuery.MAX_PAGE_SIZE, DefaultPageSize = GraphQLQuery.DEFAULT_PAGE_SIZE)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IList<Product>> GetProductsAsync() {
            BaseEntityGetAllSpecification<Product> specification = new BaseEntityGetAllSpecification<Product>();
            specification.AddInclude(c => c.ClientProducts);
            specification.AddInclude("ClientProducts.Client");
            //specification.AddInclude("ClientProducts.Client.Address");
            //specification.AddInclude("ClientProducts.Client.Address.State");
            //specification.AddInclude("ClientProducts.Client.Address.State.country");
            specification.AddInclude(c => c.ProductAttributes);
            specification.AddInclude("ProductAttributes.Attribute");
            return Result.GetValue<IList<Product>>(await _repository.ListAsync<Product>(specification));
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IList<Product>> SearchProductsAsync()
        {
            BaseEntityGetAllSpecification<Product> specification = new BaseEntityGetAllSpecification<Product>();
            specification.AddInclude(c => c.ClientProducts);
            specification.AddInclude("ClientProducts.Client");
            //specification.AddInclude("ClientProducts.Client.Address");
            //specification.AddInclude("ClientProducts.Client.Address.State");
            //specification.AddInclude("ClientProducts.Client.Address.State.country");
            specification.AddInclude(c => c.ProductAttributes);
            specification.AddInclude("ProductAttributes.Attribute");
            return Result.GetValue<IList<Product>>(await _repository.ListAsync<Product>(specification));
        }
    }
}

