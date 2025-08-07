import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Producto } from '../../core/interfaces/producto';
import { RespuestaApi } from '../../core/interfaces/respuesta-api';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  private apiUrl = 'https://localhost:7095/api/Producto';

  constructor(private http: HttpClient) {}

  getAll(): Observable<RespuestaApi<Producto[]>> {
    return this.http.get<RespuestaApi<Producto[]>>(`${this.apiUrl}/ConsultarProductos`);
  }

  create(product: Producto): Observable<Producto> {
    return this.http.post<Producto>(`${this.apiUrl}/GuardarProducto`, product);
  }

  update(product: Producto): Observable<Producto> {
    return this.http.put<Producto>(`${this.apiUrl}/ActualizarProducto`, product);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/EliminarProducto/${id}`);
  }
}
