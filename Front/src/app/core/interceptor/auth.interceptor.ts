import { HttpInterceptorFn, HttpErrorResponse } from "@angular/common/http";
import { inject } from "@angular/core";
import { Router } from "@angular/router";
import { catchError, throwError } from "rxjs";
import { AuthService } from "../../data/auth/auth.service";
import { NotificationService } from "../services/notification.service";

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);
  const router = inject(Router);
  const notificationService = inject(NotificationService);
  const token = auth.getToken();

  if (token) {
    const authReq = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
    
    return next(authReq).pipe(
      catchError((error: HttpErrorResponse) => {
        return handleAuthError(error, auth, router, notificationService);
      })
    );
  }

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      return handleAuthError(error, auth, router, notificationService);
    })
  );
};

function handleAuthError(error: HttpErrorResponse, auth: AuthService, router: Router, notificationService: NotificationService) {
  switch (error.status) {
    case 401:
      // Token expirado o inválido
      console.warn('Token expirado o inválido. Redirigiendo al login...');
      notificationService.showTokenExpired();
      auth.logout();
      router.navigate(['/login']);
      break;
    
    case 403:
      // Acceso prohibido
      console.error('Acceso prohibido. No tienes permisos para realizar esta acción.');
      notificationService.showAccessDenied();
      break;
    
    case 0:
      // Error de red
      console.error('Error de conexión. Verifica tu conexión a internet.');
      notificationService.showNetworkError();
      break;
    
    case 500:
      // Error del servidor
      console.error('Error interno del servidor. Intenta más tarde.');
      notificationService.showServerError();
      break;
    
    default:
      // Otros errores HTTP
      console.error(`Error HTTP ${error.status}: ${error.message}`);
      notificationService.showError(
        `Error ${error.status}`,
        error.message || 'Ha ocurrido un error inesperado.'
      );
      break;
  }
  
  return throwError(() => error);
}