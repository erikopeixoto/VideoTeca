import { Event, Router } from '@angular/router';
import { Component,
         OnInit, Input, Output, EventEmitter , ChangeDetectionStrategy, ViewChild, resolveForwardRef,
} from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

import { TipoMidiaService } from '../../../servicos/tipo-midia.service';
import { Alerta } from '../../../shared/modelos/alerta';
import { MatDialog } from '@angular/material/dialog';
import { AlertaComponent } from '../../../shared/components/alerta/alerta.component';
import { TipoMidia } from '../../../modelos/tipo-midia';
import { Util } from '../../../utils/util';
import { MaskManager } from '../../../utils/mask-manager';
import { InputTextComponent } from '../../../shared/components/campos/input-text/input-text.component';

@Component({
  selector: 'app-tipo-midia-detalhe',
  templateUrl: './tipo-midia-detalhe.component.html',
  styleUrls: ['./tipo-midia-detalhe.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TipoMidiaDetalheComponent implements OnInit {
  @Input() modalParent: any;
  @Input() item: any;
  @Output() itemDestino = new EventEmitter();

  @ViewChild('Descricao', {static: true}) descricao: InputTextComponent;

  public tipoMidia: TipoMidia;
  public id: number;
  public formTipoMidia: FormGroup;
  public operacao: string;
  public maskManager: MaskManager;
  public pai: TipoMidiaDetalheComponent;

  constructor(
    public tipoMidiaService: TipoMidiaService,
    private router: Router,
    public fb: FormBuilder,
    public dialog: MatDialog,
  ) {
      this.pai = this;
  }

  ngOnInit(): void {
    this.formTipoMidia = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(3)]],
      dtcAtualizacao: [''],
      catalogoTipoMidias: [''],
      catalogos: [''],
      id: [],
    });

    if (this.tipoMidiaService.id) {
      this.operacao = 'Confirma a alteração?';
      this.id = this.tipoMidiaService.id;
      this.carregar();
    } else {
      this.operacao = 'Confirma a inclusão?';
    }
  }

  carregar(): Promise<boolean> {
   return new Promise((resolve) => {this.tipoMidiaService.buscar(this.id ).then((tipoMidia) => {
      if (! Util.isNullOrEmpty(tipoMidia)) {
        this.tipoMidia = tipoMidia;
        this.formTipoMidia.setValue(tipoMidia);
        }
      });
    });
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
    this.tipoMidia = this.formTipoMidia.getRawValue() as TipoMidia;
    this.tipoMidia.id = 0;
    this.tipoMidiaService.incluir(this.tipoMidia).then(() => {
      this.modalParent.pesquisar();
      this.modalParent.modalDetalheTipoMidia.closeModal();
    });
  }

  alterar(): void {
    this.tipoMidia = this.formTipoMidia.getRawValue() as TipoMidia;
    this.tipoMidiaService.alterar(this.tipoMidia).then(() => {
      this.modalParent.pesquisar();
      this.modalParent.modalDetalheTipoMidia.closeModal();
    });
  }

  fechar(): void {
    this.modalParent.modalDetalheTipoMidia.closeModal();
  }
}
