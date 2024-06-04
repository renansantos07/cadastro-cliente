using AutoMapper;
using CadastroCliente.Application.DTO;
using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Model.Cliente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CadastroCliente.Application.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ClienteController : ControllerBase
{


    private readonly ILogger<ClienteController> _logger;
    private readonly IMapper _mapper;

    private readonly IClienteService _clienteService;

    public ClienteController(ILogger<ClienteController> logger, IMapper mapper, IClienteService clienteService)
    {
        _logger = logger;
        _mapper = mapper;
        _clienteService = clienteService;
    }

    [HttpPost("CriarCliente")]
    public async Task<ActionResult<ClienteDTO>> CriarCliente(ClienteDTO cliente)
    {
        if (!ModelState.IsValid) return BadRequest();

        ClienteEntity clienteEntity = _mapper.Map<ClienteEntity>(cliente);

        await _clienteService.Adicionar(clienteEntity);

        return Ok(clienteEntity);
    }
}
