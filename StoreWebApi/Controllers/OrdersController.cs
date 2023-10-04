using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreWebApi.Context;
using StoreWebApi.Models;
using StoreWebApi.ViewModel;
using System.Linq.Expressions;

namespace StoreWebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public OrdersController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _storeContext.Orders.ToListAsync();

            if (orders != null)
            {
                return Ok(orders);
            }

            return NotFound();

        }

        [HttpGet("api/orders-and-items")]
        public async Task<IActionResult> GetAllOrdersAndItems()
        {
            var ordersAndItems = await _storeContext
                .Orders
                .Include(i => i.Items)
                .ToListAsync();
            
            if (ordersAndItems != null)
            {
                return Ok(ordersAndItems);
            }
            return NotFound(); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _storeContext
                .Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("get-order-items/{id}")]
        public async Task<IActionResult> GetOrderAndItems(int id)
        {

            var order = await _storeContext
                .Orders
                .Include(o => o.Items)
                .ThenInclude(o => o.Product)
                .Include(o => o.Client)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var list = new List<ItemsReadDto>();
            foreach (var item in order.Items)
            {
                var model = new ItemsReadDto
                {
                    Description = item.Product.Description,
                    Barcode = item.Product.BarCode,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    Total = item.Total,
                };

                list.Add(model);
            }

            var viewModel = new
            {
                ClientName = order.Client.Name,
                order.OpenedIn,
                Items = list,
                Status = order.OrderStatus
            };

            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Order order)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _storeContext.Orders.Add(order);

            await _storeContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPost("order-items")]
        public async Task<IActionResult> Items(CreateOrderItems createOrderItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var client = await _storeContext.Clients.FirstOrDefaultAsync(c => c.Id == createOrderItems.ClientId);

            if (client == null)
            {
                return NotFound();
            }
            
            var listOrder = createOrderItems.Items.Select(o => new OrderItem
            {
                ProductId = o.ProductId,
                Quantity = o.Quantity,
                Total =  o.Quantity * _storeContext.Products
                    .Where(p => p.Id == o.ProductId)
                    .Select(p => p.Price)
                    .FirstOrDefault(),
            }).ToList();
            
            var order = new Order
            {
                ClientId = createOrderItems.ClientId,
                Freight = createOrderItems.Freight,
                Comments = createOrderItems.Comments,
                Items = listOrder,
                OpenedIn = DateTime.Now,
                ClosedOut = DateTime.Now.AddMinutes(10),
                OrderStatus = EnumsObjects.OrderStatus.Analysis,
            };

            await _storeContext.Orders.AddAsync(order);

            await _storeContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, Order orderUpdated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var order = await (_storeContext.Orders.FirstOrDefaultAsync(c => c.Id == id));

            if (order == null)
            {
                return NotFound();
            }

            UpdateOrder(orderUpdated, order);

            await _storeContext.SaveChangesAsync();
            return Ok(order);

        }

        private static void UpdateOrder(Order orderUpdated, Order order)
        {
            order.OrderStatus = orderUpdated.OrderStatus;
            order.Freight = orderUpdated.Freight;
            order.OpenedIn = orderUpdated.OpenedIn;
            order.ClosedOut = orderUpdated.ClosedOut;
            order.Client = orderUpdated.Client;
            order.Comments = orderUpdated.Comments;
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var order = await (_storeContext.Orders.FirstOrDefaultAsync(o => o.Id == id));

            if (order == null)
            {
                return NotFound();
            }
            _storeContext.Remove(order);
            await _storeContext.SaveChangesAsync();

            return Ok(order);
        }
    }

}

