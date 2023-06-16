using System;
using GraphQLHotChoclateServer.Shared;
using HotChocolate.Subscriptions;
using MasterProject.Infrastructure;
using MasterProject.Core.Entities;
using MasterProject.SharedKernel.Entities;
using MasterProject.Core.Specification;
using GraphQLHotChoclateServer.DTO;

namespace GraphQLHotChoclateServer
{
    [ExtendObjectType(typeof(GraphQLMutation))]
    public class GraphQLProductMutation
    {
        private readonly EfRepository _eFRepository;
        private readonly ITopicEventSender topicEventSender;
        private ITopicEventReceiver _topicEventReceiver;
        public GraphQLProductMutation(EfRepository eFRepository, ITopicEventSender topicEvent, [Service] ITopicEventReceiver topicEventReceiver)
        {
            _eFRepository = eFRepository;
            topicEventSender = topicEvent;
            _topicEventReceiver = topicEventReceiver;
        }

        [UseMutationConvention]
        public async Task<bool> CreateClientProduct(AddClientProduct addClientProduct)
        {
            var client = Result.GetValue<IList<Client>>(
                await _eFRepository.GetByID<Client>(addClientProduct.ClientID));

            var product = Result.GetValue<IList<Product>>(
                await _eFRepository.GetByID<Product>(addClientProduct.ProductID));

            var specification = new SpecificationGetClientProduct(addClientProduct.ClientID, addClientProduct.ProductID);

            var clientproduct = Result.GetValue<IList<ClientProduct>>(
                await _eFRepository.ListAsync<ClientProduct>(specification));

            if (client.Any() && product.Any() && !clientproduct.Any()) {
                var newClientProduct = new ClientProduct { Client = client.FirstOrDefault(), Product = product.FirstOrDefault() };
                var message = await _eFRepository.AddAsync<ClientProduct>(newClientProduct);

                if (message.IsSuccessful)
                {
                    var specClientWithProduct = new SpecificationGetClientsWithProduct(addClientProduct.ProductID);
                    specClientWithProduct.AddInclude(cp => cp.Client);
                    var clientProduct = Result.GetValue<IList<ClientProduct>>(await _eFRepository.ListAsync<ClientProduct>(specClientWithProduct));
                    string subscriptionType = $"{addClientProduct.ProductID}_SubcribeClientsWithProduct";
                     await _topicEventReceiver.SubscribeAsync<string, IList<ClientProduct>>(
               subscriptionType);
                    await topicEventSender.SendAsync(subscriptionType, clientProduct);

                }
                return message.IsSuccessful;
            }

            return false;
        }
    }
}

