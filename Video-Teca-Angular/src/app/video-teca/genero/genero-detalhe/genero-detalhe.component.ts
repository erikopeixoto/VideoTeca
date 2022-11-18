import { Event, Router } from '@angular/router';
import { Component,
         OnInit, Input, Output, EventEmitter , ChangeDetectionStrategy, ViewChild, resolveForwardRef,
} from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

import { GeneroService } from '../../../servicos/genero.service';
import { Alerta } from '../../../shared/modelos/alerta';
import { MatDialog } from '@angular/material/dialog';
import { AlertaComponent } from '../../..//shared/components/alerta/alerta.component';
import { Genero } from '../../..//modelos/genero';
import { Util } from '../../..//utils/util';
import { MaskManager } from '../../../utils/mask-manager';
import { InputTextComponent } from '../../../shared/components/campos/input-text/input-text.component';

@Component({
  selector: 'app-genero-detalhe',
  templateUrl: './genero-detalhe.component.html',
  styleUrls: ['./genero-detalhe.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GeneroDetalheComponent implements OnInit {
  @Input() modalParent: any;
  @Input() item: any;
  @Output() itemDestino = new EventEmitter();

  @ViewChild('Descricao', {static: true}) descricao: InputTextComponent;

  public genero: Genero;
  public id: number;
  public formGenero: FormGroup;
  public operacao: string;
  public maskManager: MaskManager;
  public pai: GeneroDetalheComponent;

  constructor(
    public generoService: GeneroService,
    private router: Router,
    public fb: FormBuilder,
    public dialog: MatDialog,
  ) {
      this.pai = this;
  }

  ngOnInit(): void {
    this.formGenero = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(3)]],
      dtcAtualizacao: [''],
      catalogos: [''],
      id: [],
    });

    if (this.generoService.id) {
      this.operacao = 'Confirma a alteração?';
      this.id = this.generoService.id;
      this.carregar();
    } else {
      this.operacao = 'Confirma a inclusão?';
      this.formGenero.controls['descricao'].setValue('');
    }
  }

  carregar(): Promise<boolean> {
   return new Promise((resolve) => {this.generoService.buscar(this.id ).then((lista) => {
      if (! Util.isNullOrEmpty(lista)) {
        this.genero = lista;
        this.formGenero.setValue(lista);
        }
      });
    });
  }

  enviar(): void {
    if (this.formGenero.valid) {
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
    this.genero = this.formGenero.getRawValue() as Genero;
    this.genero.id = 0;
    this.generoService.incluir(this.genero).then(() => {
      this.modalParent.pesquisar();
      this.modalParent.modalDetalheGenero.closeModal();
    });
  }

  alterar(): void {
    this.genero = this.formGenero.getRawValue() as Genero;
    this.generoService.alterar(this.genero).then(() => {
      this.modalParent.pesquisar();
      this.modalParent.modalDetalheGenero.closeModal();
    });
  }

  fechar(): void {
    this.modalParent.modalDetalheGenero.closeModal();
  }
}
