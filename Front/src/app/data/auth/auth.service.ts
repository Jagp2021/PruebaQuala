import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { RespuestaApi } from '../../core/interfaces/respuesta-api';
import { NotificationService } from '../../core/services/notification.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7095/api/Login/Login';
  private readonly TOKEN_KEY = 'token';
  constructor(private http: HttpClient,
    private router: Router,
    private notificationService: NotificationService
  ) {}

  login(usuario: string, password: string) {
    return this.http.post<RespuestaApi<any>>(this.apiUrl, { usuario: usuario, password : password })
      .pipe(tap(res => 
      {
        if(res.estado){
          localStorage.setItem('token', res.data)
        }else {
          console.log('Error de autenticación:', res);
          this.notificationService.showError('Error de Autenticación', res.mensaje);
        }
      }
      ));
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem(this.TOKEN_KEY);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
