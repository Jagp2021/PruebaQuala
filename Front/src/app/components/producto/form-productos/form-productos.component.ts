import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Producto } from '../../../core/interfaces/producto';
import { ProductoService } from '../../../data/producto/producto-service.service';
import { CommonModule } from '@angular/common';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { en_US, NzI18nService } from 'ng-zorro-antd/i18n';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-form-productos',
  standalone: true,
  imports: [ReactiveFormsModule, NzFormModule, NzInputModule, NzButtonModule,NzDatePickerModule,NzSwitchModule  ],
  templateUrl: './form-productos.component.html',
  styleUrl: './form-productos.component.scss'
})
export class FormProductosComponent implements OnInit {
  @Input() data: Producto | null = null;
  private modalRef = inject(NzModalRef<FormProductosComponent>);
  private i18n = inject(NzI18nService);
  form!: FormGroup;

  constructor(private fb: FormBuilder, 
    private service: ProductoService,
    private message: NzMessageService) {
    this.i18n.setLocale(en_US);
  }

  ngOnInit() {
    console.log('Data received in form:', this.data);
    this.form = this.fb.group({
      codigoProducto: [{ value: this.data?.codigoProducto || '', disabled: this.data?.codigoProducto}, [Validators.required, Validators.min(1)]],
      nombre: [this.data?.nombre || '', [Validators.required, Validators.maxLength(250)]],
      descripcion: [this.data?.descripcion || '', [Validators.required, Validators.maxLength(250)]],
      referenciaInterna: [this.data?.referenciaInterna || '', [Validators.required, Validators.maxLength(100)]],
      precioUnitario: [this.data?.precioUnitario || '', [Validators.required, Validators.min(0)]],
      estado: [this.data?.estado || null, [Validators.required]],
      unidadMedida: [this.data?.unidadMedida || '', [Validators.required, Validators.maxLength(50)]],
      fechaCreacion: [this.data?.fechaCreacion || null, [this.dateNotFutureValidator]]
    });
  }

  guardar() {
    Object.values(this.form.controls).forEach(control => {
      control.markAsDirty();
      control.markAsTouched();
      control.updateValueAndValidity({ onlySelf: true });
    });

    const data = { ...this.data, ...this.form.value };

    const request$ = this.data?.codigoProducto === 0
      ? this.service.create(data)
      : this.service.update(data);

    request$.subscribe((resp : any) => {
      if(resp.estado){  
        this.modalRef.close(true)
      } else {
        this.message.error(resp.mensaje);
      }
    });
  }

  cancelar(): void {
    this.modalRef.close(false);
  }

  dateNotFutureValidator(control: any) {
    if (!control.value) {
      return null; 
    }
    
    const selectedDate = new Date(control.value);
    const today = new Date();
    today.setHours(23, 59, 59, 999);
    
    if (selectedDate > today) {
      return { futureDate: true };
    }
    
    return null;
  }

  disabledDate = (current: Date): boolean => {
    const today = new Date();
    today.setHours(23, 59, 59, 999);
    return current && current > today;
  };

}
