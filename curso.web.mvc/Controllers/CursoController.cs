using curso.web.mvc.Models.Cursos;
using curso.web.mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace curso.web.mvc.Controllers
{    
    public class CursoController : Controller
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(CadastrarCursoViewModelInput cadastrarCursoViewModelInput)
        {
            try
            {
                var curso = await _cursoService.Cadastrar(cadastrarCursoViewModelInput);
                ModelState.AddModelError("", $"Curso {curso.Nome} cadastrado com sucesso");
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError("", ex.Message);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

            }

            return View();
        }

        public IActionResult Listar()
        {
            return View();
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpGet]
        public async Task<IActionResult> Listar(ListarCursosViewModelOutput listarCursosViewModelOutput)
        {
            var cursos = await _cursoService.Obter();

            return View(cursos);

        }
    }
}
