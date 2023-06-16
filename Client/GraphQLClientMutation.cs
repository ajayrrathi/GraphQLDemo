using System;
using GraphQLHotChoclateServer.DTO;
using GraphQLHotChoclateServer.Shared;
using HotChocolate.Subscriptions;
using MasterProject.Core.Entities;
using MasterProject.Infrastructure;
using MasterProject.SharedKernel.Entities;
using MasterProject.SharedKernel.Specification;

namespace GraphQLHotChoclateServer
{

	[ExtendObjectType(typeof(GraphQLMutation))]
	public class GraphQLClientMutation
	{
		private readonly EfRepository _eFRepository;
		private readonly ITopicEventSender topicEventSender;

        public GraphQLClientMutation(EfRepository eFRepository, ITopicEventSender topicEvent)
		{
			_eFRepository = eFRepository;
			topicEventSender = topicEvent;

        }
        
        /*
        [UseMutationConvention]
        public async Task<Client> UpdateClient(UpdateClient updateDetails)
		{
            //Guid addressID = await GetAddressId(updateDetails);
            Guid addressID = Guid.NewGuid();
            var clients = Result.GetValue<IList<Client>>(await _eFRepository.ListAsync<Client>(new BaseEntityByIdSpecification<Client>(updateDetails.ClientId)));
            var client = clients.First<Client>();
            client.Name = updateDetails.ClientName;
            client.AddressID = addressID;
            await _eFRepository.UpdateAsync<Client>(clients.First());

            clients = Result.GetValue<IList<Client>>(await _eFRepository.ListAsync<Client>(new BaseEntityByIdSpecification<Client>(updateDetails.ClientId)));

            return clients.First<Client>();
        }*/
        private async Task<Address> GetAddressId(AddClient addDetails)
		{
            var addresses = Result.GetValue<IList<Address>>(await _eFRepository.ListAsync<Address>());
            Address address;
            if (addresses.Any(
                            a => a.AddressLine1 == addDetails.AddressLine1 &&
                            a.AddressLine2 == addDetails.AddressLine2 &&
                            a.City == addDetails.City &&
                            a.ZipCode == addDetails.ZipCode))
            {
                address = addresses.FirstOrDefault(
                        a => a.AddressLine1 == addDetails.AddressLine1 &&
                            a.AddressLine2 == addDetails.AddressLine2 &&
                            a.City == addDetails.City &&
                            a.ZipCode == addDetails.ZipCode);

            }
            else
            {
                var ExistingCountry = Result.GetValue<Country>(await _eFRepository.ListAsync(new MasterEntityNameSpecification<Country>(addDetails.Country)));
                if (ExistingCountry == null)
                {
                    ExistingCountry = Result.GetValue<Country>(
                        await _eFRepository.AddAsync<Country>(new Country { Name = addDetails.Country, Code = addDetails.Country.ToUpperInvariant() }));
                }

                var ExistingState = Result.GetValue<State>(await _eFRepository.ListAsync(new MasterEntityNameSpecification<State>(addDetails.State)));
                if (ExistingState == null)
                {
                    ExistingState = Result.GetValue<State>(
                        await _eFRepository.AddAsync(new State { Name = addDetails.State, CountryID = ExistingCountry.Id }));
                }

                address = new Address
                {
                    AddressLine1 = addDetails.AddressLine1,
                    AddressLine2 = addDetails.AddressLine2,
                    City = addDetails.City,
                    StateID = ExistingState.Id,
                    ZipCode = addDetails.ZipCode
                }
                ;
                var message = Result.GetValue<Address>(await _eFRepository.AddAsync<Address>(address));
            }
            return address;
        }
        
        [UseMutationConvention]
        //public async Task<Client> createClient(Client client, ITopicEventSender topicEvent)
        public async Task<Client> createClient(AddClient addDetails)
        {
            Address address = await GetAddressId(addDetails);
            //Guid addressID = Guid.NewGuid();
            var returnValue = Result.GetValue<Client>(await _eFRepository.AddAsync<Client>(new Client { Name = addDetails.ClientName, Address = address,
             }));
			var clients = Result.GetValue<IList<Client>>(await _eFRepository.ListAsync<Client>());

			await topicEventSender.SendAsync(nameof(GraphQLClientSubcription.ClientCreated), clients);

            return returnValue;
        }
    }
}

