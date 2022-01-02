using Microsoft.Extensions.DependencyInjection;
using RealState.Common.Entities;
using RealState.Common.Interfaces.Services;
using RealState.Common.Models;
using RealState.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RealState.Test.Sets
{
    [TestCaseOrderer("RealState.Test.AlphabeticalOrderer", "RealState.Test")]
    public class OwnerTest : IClassFixture<ServiceFixture>
    {
        private readonly ServiceProvider _serviceProvider;

        private readonly GenericService<Owner> _service;

        public OwnerTest(ServiceFixture serviceFixture)
        {
            _serviceProvider = serviceFixture.ServiceProvider;
            _service = (GenericService<Owner>)_serviceProvider.GetService(
                typeof(IGenericService<Owner>));
        }

        [Fact]
        public async Task _1Add()
        {
            Response result =
                (Response)await _service.Insert(new Owner ()
                {
                    Name = "Test Owner",
                    Address = "Test Address",
                    BirthDate = DateTime.Now,
                    Photo = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }                   
                });
            Assert.True(result.Status);
        }

        [Fact]
        public async Task _2Get()
        {
            Response result = (Response)await _service.GetAll(1, 10);
            Assert.True(result.Status);
            IEnumerable<Owner> owners = result.Content;
            Assert.Contains(owners, x => x.Name.Equals("Test Owner"));
            Guid addedId = owners.First(x => x.Name.Equals("Test Owner")).Id;
            result = (Response)await _service.GetById(addedId);
            Assert.True(result.Status);
            Assert.Equal(((IEnumerable<Owner>)result.Content).First().Id, addedId);
        }

        [Fact]
        public async Task _3Update()
        {
            Response result = (Response)await _service.GetAll(1, 10);
            Assert.True(result.Status);
            Owner owner =
                ((IEnumerable<Owner>)(result.Content)).FirstOrDefault(x => x.Name.Equals("Test Owner"));
            Assert.NotNull(owner);
            owner.Name = "Test Owner Update";
            result = (Response)await _service.Update(owner);
            Assert.True(result.Status);
        }

        [Fact]
        public async Task _4Delete()
        {
            Response result = (Response)await _service.GetAll(1, 10);
            Assert.True(result.Status);
            IEnumerable<Owner> owners = result.Content;
            Guid addedId = owners.First(x => x.Name.Equals("Test Owner Update")).Id;
            result = (Response)await _service.Delete(addedId);
            Assert.True(result.Status);
        }
    }
}
