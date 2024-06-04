namespace CadastroCliente.Application.DTO
{
    public class SuccessResponseExampleDTO<T>
    {
        public bool Success { get;}
        public T Data { get; }
    }
}