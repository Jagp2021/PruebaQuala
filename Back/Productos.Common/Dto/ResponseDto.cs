namespace Productos.Common.Dto
{
    public class ResponseDto
    {
        public bool Estado { get; set; }
        public object Data { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
    }
}
