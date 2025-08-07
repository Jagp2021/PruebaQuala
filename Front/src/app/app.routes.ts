import { Routes } from '@angular/router';
import { ProductosComponent } from './components/producto/productos/productos.component';
import { LoginComponent } from './components/login/login.component';
import { authGuard } from './core/guard/auth.guard';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
  { path: '', component: ProductosComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: '' }
];
