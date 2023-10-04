using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreWebApi.Context;
using StoreWebApi.Models;

namespace StoreWebApi.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public ClientsController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _storeContext.Clients.ToListAsync();

            if (clients != null)
            {
                return Ok(clients);
            }

            return NotFound();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await _storeContext.Clients.
                FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]

        public async Task<IActionResult> Post(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _storeContext.Clients.AddAsync(client);

            await _storeContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, Client clientUpdated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var client = await (_storeContext.Clients.FirstOrDefaultAsync(c => c.Id == id));

            if (client == null)
            {
                return NotFound();
            }

            UpdateClient(clientUpdated, client);

            await _storeContext.SaveChangesAsync();
            return Ok(client);

        }

        private static void UpdateClient(Client clientUpdated, Client client)
        {
            client.Name = clientUpdated.Name;
            client.Phone = clientUpdated.Phone;
            client.ZipCode = clientUpdated.ZipCode;
            client.State = clientUpdated.State;
            client.City = clientUpdated.City;
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var client = await (_storeContext.Clients.FirstOrDefaultAsync(c => c.Id == id));

            if (client == null)
            {
                return NotFound();
            }
            _storeContext.Remove(client);
            await _storeContext.SaveChangesAsync();

            return Ok(client);
        }
    }
}
