using System;
using GraphQLHotChoclateServer.Shared;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using MasterProject.Core.Entities;
using MasterProject.Infrastructure;
using MasterProject.SharedKernel.Specification;
using MasterProject.SharedKernel.Entities;
using System.Linq;
namespace GraphQLHotChoclateServer
{
    [ExtendObjectType(typeof(GraphQLSubcription))]
    public class GraphQLProductSubscription
    {
        private ITopicEventReceiver _topicEventReceiver;
        public GraphQLProductSubscription([Service] ITopicEventReceiver topicEventReceiver, EfRepository eFRepository) {
            _topicEventReceiver = topicEventReceiver;
            var specification = new BaseEntityGetAllSpecification<Product>();
            var products =  Result.GetValue<IList<Product>>(eFRepository.ListAsync<Product>(specification).Result);
            foreach (Product pro in products)
            {
                _topicEventReceiver.SubscribeAsync<string, IList<ClientProduct>>($"{pro.Id}_SubcribeClientsWithProduct");
            }
        }
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<IList<ClientProduct>>> SubcribeClientsWithProduct
                (Guid ProductID, [Service] ITopicEventReceiver topicEventReceiver)
        {
            string subscriptionType = $"{ProductID}_SubcribeClientsWithProduct";
            return await topicEventReceiver.SubscribeAsync<string, IList<ClientProduct>>(
                subscriptionType);
        }
    }
}

