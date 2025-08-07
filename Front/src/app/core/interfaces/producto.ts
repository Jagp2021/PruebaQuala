export interface Producto {
    codigoProducto: number;
    nombre: string;
    descripcion?: string;
    referenciaInterna?: string;
    precioUnitario?: number;
    estado?: boolean;
    estadoTexto?: string;
    unidadMedida?: string;
    fechaCreacion?: Date;
}
