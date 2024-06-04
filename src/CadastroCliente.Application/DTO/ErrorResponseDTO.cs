namespace CadastroCliente.Application.DTO
{
    public class ErrorResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public string Hash { get; set; }
    }
}