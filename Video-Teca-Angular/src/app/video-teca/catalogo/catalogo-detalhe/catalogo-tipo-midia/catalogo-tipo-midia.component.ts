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
import { CatalogoTipoMidiaDto } from '../../../../dtos/catalogo-tipo-midia-dto';
import { CatalogoService } from '../../../../servicos/catalogo.service';
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
  @Output() itemDestino = new EventEmitter();

  @ViewChild('IdTipoMidia', {static: true}) idTipoMidia: InputSelectComponent;

  public id: number;
  public formTipoMidia: FormGroup;
  public operacao: string;
  public maskManager: MaskManager;
  public pai: CatalogoTipoMidiaComponent;
  public catalogoTipoMidiaDto: CatalogoTipoMidiaDto;
  public qtdTitulo: number;
  public tipoMidias: TipoMidia[];

  constructor(
    private catalogoService: CatalogoService,
    private tipoMidiaService: TipoMidiaService,
    public fb: FormBuilder,
    public dialog: MatDialog,
  ) {
      this.pai = this;
  }

  ngOnInit(): void {
    this.formTipoMidia = this.fb.group({
      idTipoMidia: ['', Validators.required],
      qtdTitulo: [null, [Validators.required, Validators.min(1)]],
      qtdDisponivel: [null],
      idCatalogo: [],
      id: [],
      descricao: []
    });
    this.catalogoTipoMidiaDto = this.catalogoService.catalogoTipoMidiaDto;
    this.idTipoMidia.isDisabled = false;
  }

  ngAfterViewInit(): void {
    if (! Util.isNullOrEmpty(this.catalogoTipoMidiaDto)) {
      this.operacao = 'Confirma a alteração?';
      this.id = this.catalogoTipoMidiaDto.id;
      this.carregarCombos().then(() => {
        this.carregar();
      })
    } else {
      this.carregarCombos();
      this.operacao = 'Confirma a inclusão?';
    }
  }

  async carregarCombos(): Promise<boolean> {
    return new Promise((resolve) => {
      this.tipoMidiaService.listar().then((lista) => {
        if (lista.length > 0) {
          lista.sort((a, b) => Util.ordenacao(a , b, 'descricao'));
          this.tipoMidias = lista;
          resolve(true);
        }
      });
    });
  }
  carregar(): void {
      this.idTipoMidia.isDisabled = true;
      this.formTipoMidia.setValue(this.catalogoTipoMidiaDto);
      this.idTipoMidia.valor = this.catalogoTipoMidiaDto.idTipoMidia;
      this.idTipoMidia.update();
      this.formTipoMidia.updateValueAndValidity();

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
          this.modalParent.modalCatalogoTipoMidia.closeModal();
        }
      });
    }
  }

  incluir(): void {
    if (this.formTipoMidia.valid) {
      const data = this.modalParent.dataSource.data;
      data.push({id: 0, idTipoMidia: this.formTipoMidia.controls['idTipoMidia'].value,
        descricao: document.getElementById('idTipoMidia').innerText.replace('\t', ''),
        qtdTitulo: this.formTipoMidia.controls['qtdTitulo'].value,
        idCatalogo: this.modalParent.formCatalogo.controls['id'].value});
      this.modalParent.dataSource.data = data;
    }
  }

  alterar(): void {
    const data = this.modalParent.dataSource.data;
    let itemNovo = {id: this.catalogoService.catalogoTipoMidiaDto.id,
                    idTipoMidia: this.catalogoService.catalogoTipoMidiaDto.idTipoMidia,
                    descricao: document.getElementById('idTipoMidia').innerText.replace('\t', ''),
                    qtdTitulo: this.formTipoMidia.controls['qtdTitulo'].value,
                    idCatalogo: this.catalogoService.catalogoTipoMidiaDto.idCatalogo};

    let itemIndex = data.findIndex(item => item.id == this.catalogoService.catalogoTipoMidiaDto.id);
    data[itemIndex] = itemNovo;
    this.modalParent.dataSource.data = data;
  }

  fechar(): void {
    this.modalParent.modalCatalogoTipoMidia.closeModal();
  }
}
