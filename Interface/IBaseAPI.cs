using System;
using MasterProject.SharedKernel.Entities;
using MasterProject.SharedKernel.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLHotChoclateServer.Interface
{
	public interface IBaseAPI<T> where T: BaseEntity
	{
        public Task<ActionResult<IMessage>> Create(T Item);
        public Task<ActionResult<IMessage>> Delete(Guid Id);

        public Task<ActionResult<IMessage>> Get();

        public Task<ActionResult<IMessage>> Get(Guid Id);

        public Task<ActionResult<IMessage>> Update(T Item);
    }
}

