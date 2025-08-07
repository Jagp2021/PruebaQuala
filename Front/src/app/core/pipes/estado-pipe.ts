import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'estado',
  standalone: true
})
export class EstadoPipe implements PipeTransform {
  transform(value: boolean | undefined): string {
    return value ? 'Activo' : 'Inactivo';
  }
}