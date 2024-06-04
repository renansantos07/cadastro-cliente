using AutoMapper;
using CadastroCliente.Application.DTO;
using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Model.Cliente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadastroCliente.Application.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ClienteController : MainController
{


    private readonly ILogger<ClienteController> _logger;
    private readonly IMapper _mapper;

    private readonly IClienteService _clienteService;

    public ClienteController(
        INotifier notifier,
        ILogger<ClienteController> logger,
        IMapper mapper,
        IClienteService clienteService) : base(notifier)
    {
        _logger = logger;
        _mapper = mapper;
        _clienteService = clienteService;
    }

    [HttpPost("CriarCliente")]
    [ProducesResponseType(typeof(SuccessResponseExampleDTO<ClienteDTO>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<ActionResult<ClienteDTO>> CriarCliente(ClienteDTO cliente)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        ClienteEntity clienteEntity = _mapper.Map<ClienteEntity>(cliente);

        await _clienteService.Adicionar(clienteEntity);

        return CustomResponse(clienteEntity);
    }
}
