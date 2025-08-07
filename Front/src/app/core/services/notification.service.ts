import { Injectable } from '@angular/core';
import { NzNotificationService } from 'ng-zorro-antd/notification';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private notification: NzNotificationService) { }


  showError(title: string, message: string): void {
    console.log('Warning:', title, message);
    this.notification.error(title, message, {
      nzDuration: 6000,
      nzPlacement: 'topRight'
    });
  }

  showWarning(title: string, message: string): void {
    
    this.notification.warning(title, message, {
      nzDuration: 5000,
      nzPlacement: 'topRight'
    });
  }

  showInfo(title: string, message: string): void {
    this.notification.info(title, message, {
      nzDuration: 4000,
      nzPlacement: 'topRight'
    });
  }

  // Métodos específicos para errores de autenticación
  showTokenExpired(): void {
    this.showWarning(
      'Sesión Expirada', 
      'Tu sesión ha expirado. Por favor, inicia sesión nuevamente.'
    );
  }

  showAccessDenied(): void {
    this.showError(
      'Acceso Denegado', 
      'No tienes permisos para realizar esta acción.'
    );
  }

  showNetworkError(): void {
    this.showError(
      'Error de Conexión', 
      'Verifica tu conexión a internet e intenta nuevamente.'
    );
  }

  showServerError(): void {
    this.showError(
      'Error del Servidor', 
      'Ha ocurrido un error interno. Intenta más tarde.'
    );
  }
}
