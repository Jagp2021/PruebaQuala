namespace Productos.Common.Util
{
    public static class Functions
    {
        public static DateTime? ConvertirZonaHoraria(DateTime? fechaHora)
        {
            if (fechaHora is null) return fechaHora;


            DateTime fechaHoraAjustada = fechaHora.Value.AddHours(-5);


            return fechaHoraAjustada;
        }
    }


}
