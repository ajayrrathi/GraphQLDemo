using System;
namespace GraphQLHotChoclateServer.DTO
{
    public class AddClientProduct
    {
        public Guid ClientID { get; set; }
        public Guid ProductID { get; set; }
    }
}

