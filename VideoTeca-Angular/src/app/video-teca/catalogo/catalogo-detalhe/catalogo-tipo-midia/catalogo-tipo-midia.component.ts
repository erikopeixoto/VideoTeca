import { Event, Router } from '@angular/router';
import { Component,
         OnInit, Input, Output, EventEmitter , ChangeDetectionStrategy, ViewChild, resolveForwardRef,
} from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Alerta } from '../../../../shared/modelos/alerta';

import { MatDialog } from '@angular/material/dialog';
import { Util } from '../../../../utils/util';
import { MaskManager } from '../../../../utils/mask-manager';
import { CatalogoTipoMidia } from '../../../../modelos/catalogo-tipo-midia';
import { TipoMidiaService } from '../../../../servicos/tipo-midia.service';
import { TipoMidia } from '../../../../modelos/tipo-midia';
import { AlertaComponent } from '../../../../shared/components/alerta/alerta.component';
import { InputSelectComponent } from '../../../../shared/components/campos/input-select/input-select.component';

@Component({
  selector: 'app-catalogo-tipo-midia',
  templateUrl: './catalogo-tipo-midia.component.html',
  styleUrls: ['./catalogo-tipo-midia.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CatalogoTipoMidiaComponent implements OnInit {
  @Input() modalParent: any;
  @Input() item: any;
  @Output() itemDestino = new EventEmitter();

  @ViewChild('DesTipoMidia', {static: true}) desTipoMidia: InputSelectComponent;

  public id: number;
  public formTipoMidia: FormGroup;
  public operacao: string;
  public maskManager: MaskManager;
  public pai: CatalogoTipoMidiaComponent;
  public catalogoTipoMidia: CatalogoTipoMidia;
  public tipoMidia: TipoMidia;
  public tipoMidias: TipoMidia[];

  constructor(
    private tipoMidiaService: TipoMidiaService,
    public fb: FormBuilder,
    public dialog: MatDialog,
  ) {
      this.pai = this;
  }

  ngOnInit(): void {
    this.formTipoMidia = this.fb.group({
      tipoMidia: ['', Validators.required],
      qtdTitulo: [0 , [Validators.required, Validators.min(1)]],
      id: []
    });
    this.catalogoTipoMidia = this.item;
  }

  ngAfterViewInit(): void {
    this.tipoMidiaService.listar().then((lista) => {
      if (lista.length > 0) {
        lista.sort((a, b) => Util.ordenacao(a , b, 'descricao'));
        this.tipoMidias = lista;
      }
    });

    if (! Util.isNullOrEmpty(this.catalogoTipoMidia)) {
      this.operacao = 'Confirma a alteração?';
      this.id = this.catalogoTipoMidia.id;
      this.carregar();
    } else {
      this.operacao = 'Confirma a inclusão?';
    }    
  }

  carregar(): void {
    this.formTipoMidia.setValue(this.catalogoTipoMidia); 
  }

  enviar(): void {
    if (this.formTipoMidia.valid) {
      const config = {
        data: {
          titulo: 'Confirmar',
          descricao: this.operacao,
          btnSucesso: 'Ok',
          corBtnSucesso: 'accent',
          possuirBtnFechar: true
        } as Alerta
      };
      const dialogRef = this.dialog.open(AlertaComponent, config);
      dialogRef.afterClosed().subscribe((opcao: boolean) => {
        if (opcao) {
           if (Util.isNullOrEmpty(this.id)) {
              this.incluir();
           } else {
            this.alterar();
          }
        }
      });
    }
  }

  incluir(): void {
    if (this.formTipoMidia.valid) {
      const data = this.modalParent.dataSource.data;
      data.push({id: 0, idTipoMidia: this.formTipoMidia.controls['tipoMidia'].value,
        desTipoMidia: document.getElementById('tipoMidia').innerText.replace('\t', ''),
        qtdTitulo: this.formTipoMidia.controls['qtdTitulo'].value,
        idCatalogo: this.modalParent.formCatalogo.controls['id'].value});
      this.modalParent.dataSource.data = data;
    }
    this.modalParent.modalCatalogoTipoMidia.closeModal();
  }

  alterar(): void {
    this.tipoMidia = this.formTipoMidia.getRawValue() as TipoMidia;
    this.tipoMidiaService.alterar(this.tipoMidia).then(() => {
      this.modalParent.pesquisar();
      this.modalParent.modalCatalogoTipoMidia.closeModal();
    });
  }

  fechar(): void {
    this.modalParent.modalCatalogoTipoMidia.closeModal();
  }
}
