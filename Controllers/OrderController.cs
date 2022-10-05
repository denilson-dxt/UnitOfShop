using Microsoft.AspNetCore.Mvc;
using UnitOfShop.Data;
using UnitOfShop.Models;
using UnitOfShop.Repositories;

namespace UnitOfShop.Controllers
{
    [ApiController]
    [Route("v1/orders")]
    public class OrderController : ControllerBase
    {
        [HttpPost("")]
        public Order Post(
            [FromServices] ICustomerRepository customerRepository,
            [FromServices] IOrderRepository orderRepository,
            [FromServices] IUnitOfWork unitOfWork
        )
        {
            try
            {
                var customer = new Customer() { Name = "Denilson Tivane" };
                var order = new Order()
                {
                    Number = "123",
                    Customer = customer
                };

                customerRepository.Save(customer);
                orderRepository.Save(order);

                unitOfWork.Commit();

                return order;
            }
            catch
            {
                unitOfWork.Rollback();
                return null;
            }

        }
    }
}