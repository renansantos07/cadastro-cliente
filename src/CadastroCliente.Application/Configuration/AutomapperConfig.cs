using AutoMapper;
using CadastroCliente.Application.DTO;
using CadastroCliente.Domain.Model.Cliente;

namespace CadastroCliente.Application.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<ClienteEntity, ClienteDTO>().ReverseMap();
        }
    }
}