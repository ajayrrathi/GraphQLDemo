using System;
namespace GraphQLHotChoclateServer.DTO
{
    public class UpdateClient  : AddClient
    {
        public Guid ClientId { get; set; }
    }
}

