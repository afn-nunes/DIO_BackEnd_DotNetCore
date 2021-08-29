﻿using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.models.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace curso.api.Controllers
{
    /// <summary>
    /// Classe de controle de cursos
    /// </summary>
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;
        /// <summary>
        /// Construtor da classe CursoController
        /// </summary>
        /// <param name="cursoRepository"></param>
        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;            
        }
        /// <summary>
        /// Este serviço permite cadastrar curso para o usuário autenticado
        /// </summary>
        /// <returns>Retorna status 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar um curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]

        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput) 
        {            
            Curso curso = new Curso();
            curso.Nome = cursoViewModelInput.Nome;
            curso.Descricao = cursoViewModelInput.Descricao;

            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            curso.CodigoUsuario = codigoUsuario;
            _cursoRepository.Adicionar(curso);
            _cursoRepository.Commit();
            return Created("", cursoViewModelInput);
        }

        /// <summary>
        /// Este serviço permite retornar os cursos cadastrados do usuário autenticado
        /// </summary>
        /// <returns>Retorna status 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao obter os cursos")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]

        public async Task<IActionResult> Get()
        {
            
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);            
            var cursos = _cursoRepository.ObterPorUsuario(codigoUsuario).Select(s => new CursoViewModelOutput()
            {
                Nome = s.Nome,
                Descricao = s.Descricao,
                Login = s.Usuario.Login
            });            

            return Ok(cursos);
        }
    }
}
