import { Component } from '@angular/core';
import { Producto } from '../../../core/interfaces/producto';
import { ProductoService } from '../../../data/producto/producto-service.service';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { CommonModule } from '@angular/common';
import { FormProductosComponent } from '../form-productos/form-productos.component';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { EstadoPipe } from '../../../core/pipes/estado-pipe';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-productos',
  standalone: true,
  imports: [CommonModule, NzTableModule, NzButtonModule, NzModalModule, NzEmptyModule, FormProductosComponent, NzIconModule, NzToolTipModule,EstadoPipe, NzPopconfirmModule],
  templateUrl: './productos.component.html',
  styleUrl: './productos.component.scss'
})
export class ProductosComponent {
  products: Producto[] = [];
  selectedProduct!: Producto;
  formVisible = false;
  listOfDisplayData: readonly Producto[] = []

  constructor(private service: ProductoService,
    private modal: NzModalService,
    private message: NzMessageService
  ) {}

  ngOnInit(): void {
    this.cargar();
  }

  cargar() {
    this.service.getAll().subscribe(resp => this.products = resp.data);
  }

  nuevo() {
    this.selectedProduct = { codigoProducto: 0, nombre: '' };
    this.formVisible = true;
    this.openModal(true);
  }

  editar(p: Producto) {
    this.selectedProduct = { ...p };
    this.formVisible = true;
    this.openModal(false);
  }

  eliminar(id: number) {
      this.service.delete(id).subscribe(() => {
        this.cargar();
        this.message.success('Producto eliminado correctamente');
      });
  }

  cerrarFormulario(recargar: boolean = false) {
    this.formVisible = false;
    if (recargar) this.cargar();
  }

  openModal(crear: boolean): void {
    const modalRef =this.modal.create({
      nzTitle: crear ? 'Crear producto' : 'Editar producto',
      nzWidth: '50%',
      nzContent: FormProductosComponent,
      nzFooter: null
    });

    modalRef.afterOpen.subscribe(() => {
      const instance = modalRef.getContentComponent();
      if (instance) {
        instance.data = this.selectedProduct;
        instance.ngOnInit();
      }
    });

    modalRef.afterClose.subscribe((estado) => {
      if(estado){
        this.message.success(crear ? 'Producto guardado correctamente' : 'Producto actualizado correctamente');
        this.cargar();
      }

    });
  }

}
