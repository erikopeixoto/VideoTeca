import { Event, Router } from '@angular/router';
import { Component,
         OnInit, Input, Output, EventEmitter , ChangeDetectionStrategy, ViewChild, resolveForwardRef,
} from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

import { ClienteService } from '../../../servicos/cliente.service';
import { Alerta } from '../../../shared/modelos/alerta';
import { MatDialog } from '@angular/material/dialog';
import { AlertaComponent } from '../../../shared/components/alerta/alerta.component';
import { ClienteDto } from '../../../dtos/cliente-dto';
import { Cliente } from '../../../modelos/cliente';
import { Util } from '../../../utils/util';
import { MaskManager } from '../../../utils/mask-manager';
import { InputTextComponent } from '../../../shared/components/campos/input-text/input-text.component';
import { CNPJCPFValidator } from '../../../utils/cnpj-cpf-validator';
import { GenericoValidator} from '../../../utils/generico-validator';

@Component({
  selector: 'app-cliente-detalhe',
  templateUrl: './cliente-detalhe.component.html',
  styleUrls: ['./cliente-detalhe.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ClienteDetalheComponent implements OnInit {
  @Input() modalParent: any;
  @Input() item: any;
  @Output() itemDestino = new EventEmitter();

  @ViewChild('NumDoc', {static: true}) numDoc: InputTextComponent;
  @ViewChild('NumCep', {static: true}) numCep: InputTextComponent;
  @ViewChild('NomCliente', {static: true}) nomCliente: InputTextComponent;

  public cliente: Cliente;
  public id: number;
  public clienteDto: ClienteDto;
  public formCliente: FormGroup;
  public operacao: string;
  public maskManager: MaskManager;
  public pai: ClienteDetalheComponent;
  public cargaDados: boolean;
  public readonly tipoPessoas = [
    { id: 1, descricao: 'Física' },
    { id: 2, descricao: 'Jurídica' }
  ];

  constructor(
    public clienteService: ClienteService,
    private router: Router,
    public fb: FormBuilder,
    public dialog: MatDialog,
  ) {
      this.pai = this;
  }

  ngOnInit(): void {
    this.cargaDados = true;
    this.formCliente = this.fb.group({
      tipoPessoa: [1 , Validators.required],
      nomCliente: ['', Validators.required],
      numDocumento: ['', [Validators.required, Validators.minLength(11), CNPJCPFValidator.validaCNPJCPF.bind(this)]],
      numTelefone: ['', [Validators.required, Validators.minLength(11)]],
      id: [],
      desBairro: ['', Validators.required],
      desLogradouro: ['', Validators.required],
      desMunicipio: ['', Validators.required],
      desComplemento: [''],
      numEndereco: ['', Validators.required],
      dtcNascimento: ['', Validators.required],
      dtcAtualizacao: [''],
      clienteCatalogoTipoMidias: [''],
      numCep: ['', Validators.required]
    });

    this.numDoc.ngBlur = () => {this.zeroEsquerda(0, 'numDocumento')};
    this.numDoc.update();

    this.numCep.ngBlur = () => {this.zeroEsquerda(8, 'numCep')};
    this.numCep.update();

    if (this.clienteService.id) {
      this.operacao = 'Confirma a alteração?';
      this.id = this.clienteService.id;
      this.telefoneExistente();
      this.cargaDados = false;
    } else {
      this.operacao = 'Confirma a inclusão?';
    }
  }

  async telefoneExistente(): Promise<boolean> {
    await this.carregar();
    return true;
  }

  carregar(): Promise<boolean> {
   return new Promise((resolve) => {this.clienteService.buscar(this.id ).then((lista) => {
      if (! Util.isNullOrEmpty(lista)) {
        this.formCliente.setValue(lista);
      }
     });
    });
  }

  enviar(): void {
    if (this.formCliente.valid) {
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
    this.cliente = this.formCliente.getRawValue() as Cliente;
    this.cliente.id = 0;
    this.clienteService.incluir(this.cliente).then(() => {
      this.modalParent.pesquisar();
      this.fechar();
    });
  }

  alterar(): void {
    this.cliente = this.formCliente.getRawValue() as Cliente;
    this.clienteService.alterar(this.cliente).then(() => {
      this.modalParent.pesquisar();
      this.fechar();
    });
  }

  fechar(): void {
    this.modalParent.modalDetalheCliente.closeModal();
  }

  alterarMascara(): void {
    let retorno = '000.000.000-00';
    const tipoPessoa = this.formCliente.controls['tipoPessoa'].value;
    const tipoPessoaDto = this.cliente ? this.cliente.tipoPessoa : 0;
    if (tipoPessoa === 1 && this.numDoc.maskTexto === '00.000.000/0000-00') {
      retorno = '000.000.000-00';
      this.formCliente.controls['numDocumento'].setValidators(Validators.compose([
      Validators.required, Validators.minLength(11), CNPJCPFValidator.validaCNPJCPF.bind(this)]));
      if (tipoPessoa !== tipoPessoaDto){
        this.formCliente.controls['numDocumento'].setValue('');
      }
      this.numDoc.maskTexto = retorno;
      this.numDoc.update();
      this.nomCliente.style = "width: 370px";
      this.nomCliente.titulo = "Nome *";
      this.nomCliente.update();
    } else if (tipoPessoa !== 1 && this.numDoc.maskTexto !== '00.000.000/0000-00') {
      this.formCliente.controls['numDocumento'].setValidators(Validators.compose([
        Validators.required, Validators.minLength(14), CNPJCPFValidator.validaCNPJCPF.bind(this)]));
      retorno = '00.000.000/0000-00';
      if (tipoPessoa !== tipoPessoaDto){
        this.formCliente.controls['numDocumento'].setValue('');
      }
      this.numDoc.maskTexto = retorno;
      this.numDoc.update();
      this.nomCliente.style = "width: 630px";
      this.nomCliente.titulo = "Razão Social *";
      this.nomCliente.update();
    }
    this.cargaDados = true;
  }

  zeroEsquerda(tamanho: number, campo: string): void {
    if (! Util.isNullOrEmpty(this.formCliente.controls[campo].value)) {
      if (tamanho === 0) {
        if (this.formCliente.controls['tipoPessoa'].value === 1) {
          tamanho = 11;
        } else {
          tamanho = 14;
        }
      }

      this.formCliente.controls[campo].setValue(Util.completarStringComZero(this.formCliente.controls[campo].value, tamanho));
    }
  }

  ativar(): boolean {
    let retorno = true;
    if (! Util.isNullOrEmpty(this.formCliente.controls['tipoPessoa'].value)) {
      retorno = false;
    }
    return retorno;
  }
}
