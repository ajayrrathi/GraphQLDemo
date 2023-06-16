using System;
using GraphQLHotChoclateServer.Shared;
using HotChocolate.Types;
using MasterProject.Core.Entities;

namespace GraphQLHotChoclateServer
{
    [ExtendObjectType(typeof(GraphQLSubcription))]
    public class GraphQLClientSubcription
	{
		[Subscribe]
		public IList<Client> ClientCreated([EventMessage] IList<Client> clients) => clients;

	}
}

