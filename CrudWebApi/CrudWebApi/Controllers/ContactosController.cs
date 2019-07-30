using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWebApi.Data;
using CrudWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly ContactoContexto _context;

        public ContactosController(ContactoContexto contexto)
        {
            _context = contexto;
        }

        // Petición tipo GET: api/contactos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contacto>>> GetContactoItems()
        {
            return await _context.ContactoItems.ToListAsync();
        }


        //Petición tipo get: un solo registros: api/contactos/4
        [HttpGet("{id}")]
        public async Task<ActionResult<Contacto>> GetContactoItem(int id)
        {
            var contactoItem = await _context.ContactoItems.FindAsync(id);

            if(contactoItem == null)
            {
                return NotFound();
            }

            return contactoItem;

        }


        ///Petición de tipo POST: api/contactos
        [HttpPost]
        public async Task<ActionResult<Contacto>> PostContactoItem(Contacto item)
        {
            _context.ContactoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContactoItem), new { id = item.Id }, item);
        }
        

        ///Petición de tipo PUT: api/contactos/2
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactoItem(int id, Contacto item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //peticion delete para borrar: api/contactos/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactoItem(int id)
        {
            var contactoItem = await _context.ContactoItems.FindAsync(id);

            if (contactoItem == null)
            {
                return NotFound();
            }

            _context.ContactoItems.Remove(contactoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}