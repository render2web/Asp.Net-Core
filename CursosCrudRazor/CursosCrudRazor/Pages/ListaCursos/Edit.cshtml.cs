using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursosCrudRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CursosCrudRazor.Pages.ListaCursos
{
    public class EditModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Curso Curso { get; set; }

        [TempData]
        public string Mensaje { get; set; }

        public async Task OnGet(int id)
        {
            Curso = await _db.Curso.FindAsync(id);
        }


        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var CursoDesdeDb = await _db.Curso.FindAsync(Curso.Id);
                CursoDesdeDb.NombreCurso = Curso.NombreCurso;
                CursoDesdeDb.CantidadClases = Curso.CantidadClases;
                CursoDesdeDb.Precio = Curso.Precio;

                await _db.SaveChangesAsync();
                Mensaje = "Libro editado correctamente";
                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
    }
}