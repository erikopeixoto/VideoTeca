import { Event, Router } from '@angular/router';
import { Component,
         OnInit, Input, Output, EventEmitter , ChangeDetectionStrategy, ViewChild, resolveForwardRef,
} from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

import { CatalogoService } from '../../../servicos/catalogo.service';
import { Alerta } from '../../../shared/modelos/alerta';
import { MatDialog } from '@angular/material/dialog';
import { AlertaComponent } from '../../../shared/components/alerta/alerta.component';
import { CatalogoDto } from '../../../dtos/catalogo-dto';
import { Catalogo } from '../../../modelos/catalogo';
import { Util } from '../../../utils/util';
import { MaskManager } from '../../../utils/mask-manager';
import { InputTextComponent } from '../../../shared/components/campos/input-text/input-text.component';
import { CNPJCPFValidator } from '../../../utils/cnpj-cpf-validator';
import { GenericoValidator} from '../../../utils/generico-validator';

@Component({
  selector: 'app-catalogo-detalhe',
  templateUrl: './catalogo-detalhe.component.html',
  styleUrls: ['./catalogo-detalhe.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CatalogoDetalheComponent implements OnInit {
  @Input() modalParent: any;
  @Input() item: any;
  @Output() itemDestino = new EventEmitter();

  public catalogo: Catalogo;
  public id: number;
  public catalogoDto: CatalogoDto;
  public formCatalogo: FormGroup;
  public operacao: string;
  public maskManager: MaskManager;
  public pai: CatalogoDetalheComponent;
  public cargaDados: boolean;
  public readonly tipoPessoas = [
    { id: 1, descricao: 'FÌsica' },
    { id: 2, descricao: 'JurÌdica' }
  ];

  constructor(
    public catalogoService: CatalogoService,
    private router: Router,
    public fb: FormBuilder,
    public dialog: MatDialog,
  ) {
      this.pai = this;
  }

  ngOnInit(): void {
    this.cargaDados = true;
    this.formCatalogo = this.fb.group({
      tipoPessoa: [1 , Validators.required],
      nomCatalogo: ['', Validators.required],
      numDocumento: ['', [Validators.required, Validators.minLength(11), CNPJCPFValidator.validaCNPJCPF.bind(this)]],
      numTelefone: ['', [Validators.required, Validators.minLength(11)]],
      id: [],
      desBairro: ['', Validators.required],
      desLogradouro: ['', Validators.required],
      desMunicipio: ['', Validators.required],
      desComplemento: [''],
      numEndereco: ['', Validators.required],
      dtcNascimento: ['', Validators.required],
      numCep: ['', GenericoValidator.validarData.bind(this)]
    });

    if (this.catalogoService.id) {
      this.operacao = 'Confirma a altera√ß√£o?';
      this.id = this.catalogoService.id;
      this.telefoneExistente();
      this.cargaDados = false;
    } else {
      this.operacao = 'Confirma a inclus√£o?';
    }
  }

  async telefoneExistente(): Promise<boolean> {
    await this.carregar();
    return true;
  }

  carregar(): Promise<boolean> {
   return new Promise((resolve) => {this.catalogoService.buscar(this.id ).then((lista) => {
      if (! Util.isNullOrEmpty(lista)) {
        this.formCatalogo.setValue(lista); 
        }
      });
    });
  }

  enviar(): void {
    if (this.formCatalogo.valid) {
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
    this.catalogo = this.formCatalogo.getRawValue() as Catalogo;
    this.catalogo.id = 0;
    this.catalogoService.incluir(this.catalogo).then(() => {
      this.modalParent.pesquisar();
      this.modalParent.modalDetalheCatalogo.closeModal();
    });
  }

  alterar(): void {
    this.catalogo = this.formCatalogo.getRawValue() as Catalogo;
    this.catalogoService.alterar(this.catalogo).then(() => {
      this.modalParent.pesquisar();
      this.modalParent.modalDetalheCatalogo.closeModal();
    });
  }

  fechar(): void {
    this.modalParent.modalDetalheCatalogo.closeModal();
  }
}
