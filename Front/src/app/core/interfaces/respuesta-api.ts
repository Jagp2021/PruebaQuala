export interface RespuestaApi<T> {
    estado: boolean;
    mensaje: string;
    data: T;
}
