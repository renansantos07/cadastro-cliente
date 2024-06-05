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

    [HttpGet("ObterTodos")]
    [ProducesResponseType(typeof(SuccessResponseExampleDTO<List<ClienteDTO>>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<ActionResult<ClienteDTO>> ObterTodos()
    {
        List<ClienteEntity> clienteEntities = await _clienteService.ObterTodos();
        List<ClienteDTO> clienteDTO = _mapper.Map<List<ClienteDTO>>(clienteEntities);


        return CustomResponse(clienteDTO);
    }

    [HttpGet("Obter")]
    [ProducesResponseType(typeof(SuccessResponseExampleDTO<ClienteDTO>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<ActionResult<ClienteDTO>> Obter([FromQuery]Guid Id)
    {
        ClienteEntity clienteEntities = await _clienteService.Obter(Id);
        ClienteDTO clienteDTO = _mapper.Map<ClienteDTO>(clienteEntities);


        return CustomResponse(clienteDTO);
    }

    [HttpPost("Query")]
    [ProducesResponseType(typeof(SuccessResponseExampleDTO<List<ClienteDTO>>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<ActionResult<ClienteDTO>> ObterQuery(ClienteQueryDTO cliente)
    {
        ClienteEntity clienteEntity = _mapper.Map<ClienteEntity>(cliente);

        List<ClienteEntity> clienteEntities = await _clienteService.ObterQuery(clienteEntity);

        List<ClienteDTO> clienteDTO = _mapper.Map<List<ClienteDTO>>(clienteEntities);


        return CustomResponse(clienteDTO);
    }

    [HttpPut("AtualizarCliente")]
    [ProducesResponseType(typeof(SuccessResponseExampleDTO<ClienteDTO>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<ActionResult<ClienteDTO>> AtualizarCliente([FromQuery]Guid Id, [FromBody]ClienteDTO cliente)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        if (Guid.Empty == cliente.Id)
        {
            cliente.Id = Id;
        }

        ClienteEntity clienteEntity = _mapper.Map<ClienteEntity>(cliente);

        await _clienteService.Atualizar(clienteEntity);

        return CustomResponse(clienteEntity);
    }

    [HttpDelete("DeletarCliente")]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<ActionResult<ClienteDTO>> DeletarCliente([FromQuery]Guid Id)
    {

        await _clienteService.Remover(Id);

        return CustomResponse();
    }
}
