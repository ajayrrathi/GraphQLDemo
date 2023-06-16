using System;
using MasterProject.Infrastructure;
using MasterProject.SharedKernel.Interface;
using MasterProject.SharedKernel.Repository;
using MasterProject.SharedKernel.Specification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GraphQLHotChoclateServer.Interface;
using MasterProject.SharedKernel.Entities;

namespace GraphQLHotChoclateServer.DataService
{
	public class BaseEntityController<T> : ControllerBase
	{
        protected readonly IRepository _repository;
        public BaseEntityController(IRepository repository)
        {
            _repository = repository;
        }
    }
}

