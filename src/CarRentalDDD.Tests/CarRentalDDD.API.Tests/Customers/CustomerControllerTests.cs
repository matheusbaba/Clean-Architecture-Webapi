using CarRentalDDD.API.Customers;
using CarRentalDDD.API.Customers.Commands;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading;
using Xunit;

namespace CarRentalDDD.API.Tests.Customers
{
    public class CustomerControllerTests
    {

        [Fact]
        public void Get_ById_ShouldReturnCustomer()
        {
            CustomerDTO expected = new CustomerDTO();
            var mediator = A.Fake<IMediator>();
            A.CallTo(() => mediator.Send(A<CustomerByIdQuery>._, A<CancellationToken>._)).Returns(expected);

            var customerController = new CustomerController(mediator);

            var response = customerController.Get(expected.Id);

            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var customer = Assert.IsAssignableFrom<CustomerDTO>(okResult.Value);
            Assert.Equal(expected, customer);
        }

        [Fact]
        public void Get_ById_ShouldReturnNotFound()
        {
            CustomerDTO customer = null;
            var mediator = A.Fake<IMediator>();
            A.CallTo(() => mediator.Send(A<CustomerByIdQuery>._, A<CancellationToken>._)).Returns(customer);

            var customerController = new CustomerController(mediator);

            var response = customerController.Get(Guid.NewGuid());
            Assert.IsType<NotFoundResult>(response.Result);
        }


        // more tests
    }
}
