using System;
using GraphQLHotChoclateServer.Shared;
using MasterProject.Core.Entities;
using MasterProject.Infrastructure.Data;
using MasterProject.SharedKernel.Interface;
using MasterProject.SharedKernel.Entities;
using MasterProject.SharedKernel.Repository;
using MasterProject.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MasterProject.SharedKernel.Specification;

namespace GraphQLHotChoclateServer
{
	[ExtendObjectType(typeof(GraphQLQuery))]
	public class GraphQLClientQuery
	{
        private readonly AppDBContext _appDBContext;
		private readonly EfRepository _repository;
        public GraphQLClientQuery(AppDBContext appDBContext, EfRepository repository)
		{
			_appDBContext = appDBContext;
			_repository = repository;
        }
		
		[UseDbContext(typeof(AppDBContext))]
		[UsePaging ( IncludeTotalCount = GraphQLQuery.INCLUDE_TOTAL_COUNT, AllowBackwardPagination = GraphQLQuery.ALLOW_BACKWARD_PAGINATION,
            MaxPageSize = GraphQLQuery.MAX_PAGE_SIZE, DefaultPageSize = GraphQLQuery.DEFAULT_PAGE_SIZE)]
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public  async Task<IQueryable<Client>> GetClients([Service] IDbContextFactory<AppDBContext> appDBContext)
		{
			return (await appDBContext.CreateDbContextAsync())
										.Clients
										.Include(cp =>
											cp.ClientProduct)
										.ThenInclude(p => p.Product)
										.ThenInclude(pa => pa.ProductAttributes)
										.ThenInclude(a => a.Attribute)
										.Include(cc => cc.ClientCategories)
										.ThenInclude(ct => ct.Category)
										.Include(ad => ad.Address)
										.ThenInclude(s => s.State)
										.ThenInclude(ct => ct.Country);
        }

        [UsePaging(IncludeTotalCount = GraphQLQuery.INCLUDE_TOTAL_COUNT, AllowBackwardPagination = GraphQLQuery.ALLOW_BACKWARD_PAGINATION,
            MaxPageSize = GraphQLQuery.MAX_PAGE_SIZE, DefaultPageSize = GraphQLQuery.DEFAULT_PAGE_SIZE)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IList<Client>> GetEFClients([Service] EfRepository repository)
		{
            BaseEntityGetAllSpecification<Client> specification = new BaseEntityGetAllSpecification<Client>();
            //specification.AddInclude("Client.ClientProduct");
            specification.AddInclude(c  => c.Address);
            specification.AddInclude("Address.State");
            specification.AddInclude("Address.State.Country");
            specification.AddInclude(c => c.ClientProduct);
            specification.AddInclude("ClientProduct.Product");
            specification.AddInclude("ClientProduct.Product.ProductAttributes");
            specification.AddInclude("ClientProduct.Product.ProductAttributes.Attribute");
            specification.AddInclude(cc => cc.ClientCategories);
            specification.AddInclude("ClientCategories.Category");

            /*
            specification.Includes.Add(c =>
                                        c.Include(cp => cp.ClientProduct)
                                        .ThenInclude(pa => pa.Product)
                                        .ThenInclude(pa => pa.ProductAttributes)
                                        .ThenInclude(a => a.Attribute)
                                        .Include(cc => cc.ClientCategories)
                                        .ThenInclude(ct => ct.Category)
                                        .Include(ad => ad.Address)
                                        .ThenInclude(s => s.State)
                                        .ThenInclude(ct => ct.Country)); 
            */
            return  Result.GetValue<IList<Client>>(await repository.ListAsync<Client>(specification));
		}

        [UseProjection]
		[UseFiltering]
        [UseSorting]
        public async Task<IQueryable<Client>> SearchClients([Service] IDbContextFactory<AppDBContext> appDBContext)
        {
            return (await appDBContext.CreateDbContextAsync())
                                        .Clients
                                        .Include(cp =>
                                            cp.ClientProduct)
                                        .ThenInclude(p => p.Product)
                                        .ThenInclude(pa => pa.ProductAttributes)
                                        .ThenInclude(a => a.Attribute)
                                        .Include(cc => cc.ClientCategories)
                                        .ThenInclude(ct => ct.Category)
                                        .Include(ad => ad.Address)
                                        .ThenInclude(s => s.State)
                                        .ThenInclude(ct => ct.Country);
        }
    }
}

