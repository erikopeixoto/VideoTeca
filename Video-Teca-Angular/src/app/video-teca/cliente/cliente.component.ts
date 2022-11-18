import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Cliente } from '../../modelos/cliente';
import { ClienteDto } from '../../dtos/cliente-dto';
import { FiltroClienteDto } from '../../dtos/filtro-cliente-dto';
import { ModalComponent } from '../../shared/modal/modal.component';
import { ClienteService } from '../../servicos/cliente.service';
import { Util } from '../../utils/util';
import { Alerta } from '../../shared/modelos/alerta';
import { MatDialog } from '@angular/material/dialog';
import { AlertaComponent } from '../../shared/components/alerta/alerta.component';
import { InputTextComponent } from '../../shared/components/campos/input-text/input-text.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { CNPJCPFValidator } from '../../utils/cnpj-cpf-validator';
import { MessagingService } from '../../servicos/messaging.service';
import { ModalMessage } from '../../shared/modelos/modal-message';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit, AfterViewInit {

  @ViewChild('modalDetalheCliente', {static: true} ) modalDetalheCliente: ModalComponent;
  @ViewChild('modalLocacaoCliente', {static: true} ) modalLocacaoCliente: ModalComponent;

  displayedColumns: string[] = [ 'nome', 'municipio', 'telefone', 'cpfcnpj', 'acoes' , 'id'];
  dataSource: MatTableDataSource<ClienteDto>;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  get getVisibleClienteDetalhe(): boolean {
    return this.modalDetalheCliente.getVisible();
  }

  get getVisibleClienteLocacao(): boolean {
    return this.modalLocacaoCliente.getVisible();
  }

  @ViewChild('NomeCliente', {static: true}) nomCliente: InputTextComponent;
  @ViewChild('NumDoc', {static: true}) numDoc: InputTextComponent;

  public clienteDto: ClienteDto;
  public parente: ClienteComponent;
  public stsCliente = false;
  public cliente: Cliente;
  public clienteFiltrados: ClienteDto [];
  public filtroLista: Cliente;
  public filtroListagem: FormGroup;
  public readonly tipoPessoas = [
    { id: 0, descricao: '' },
    { id: 1, descricao: 'Física' },
    { id: 2, descricao: 'Jurídica' }
  ];

  constructor(
    public clienteService: ClienteService,
    private readonly messagingService: MessagingService,
    public fb: FormBuilder,
    public dialog: MatDialog
  ) {
    this.parente = this;
  }

  ngOnInit(): void {
    this.cliente = new Cliente();
    this.filtroListagem = this.fb.group({
      nomeCliente: ['', Validators.minLength(3)],
      codTipoPessoa: [ 0 ],
      numDocumento: ['']
    });
    this.paginator._intl.itemsPerPageLabel = 'Itens por página ';
  }

  ngAfterViewInit(): void {

  }

  pesquisar(): void {
    if (this.filtroListagem.valid && this.validarPesquisa()) {
      const filtro = new FiltroClienteDto();
      filtro.Nome = this.filtroListagem.controls['nomeCliente'].value;
      filtro.CodTipoPessoa = this.filtroListagem.controls['codTipoPessoa'].value;
      filtro.NumDocumento = this.filtroListagem.controls['numDocumento'].value;
      this.clienteFiltrados = [];
      this.dataSource = undefined;
      this.clienteService.pesquisar(filtro)
        .then((lista) => {
        if (!Util.isNullOrEmpty(lista[0].foneFormatado)) {
            this.clienteFiltrados = lista;
            this.dataSource = new MatTableDataSource(this.clienteFiltrados);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
            this.dataSource.paginator.firstPage();
        } else {
          this.dataSource.paginator._intl = undefined;
        }
      });
    }
  }

  receberCliente(itemRecebido): void {

  }

  editar(id: number): void {
    this.clienteService.id = id;
    this.clienteService.cargaDados = false;
    this.modalDetalheCliente.title = 'Alteração';
    this.modalDetalheCliente.showModal();
  }

  locar(id: number): void {
    this.clienteService.id = id;
    this.clienteService.cargaDados = false;
    this.modalLocacaoCliente.title = 'Locar';
    this.modalLocacaoCliente.showModal();
  }

  excluir(id: number): void {
    const config = {
      data: {
        titulo: 'Confirmar',
        descricao: 'Confirma a exclusão?',
        btnSucesso: 'Ok',
        corBtnSucesso: 'accent',
        possuirBtnFechar: true
      } as Alerta
    };
    const dialogRef = this.dialog.open(AlertaComponent, config);
    dialogRef.afterClosed().subscribe((opcao: boolean) => {
      if (opcao) {
         if (! Util.isNullOrEmpty(id)) {
            this.clienteService.excluir(id).then(() => {
              this.pesquisar();
            });
         }
      }
    });
  }

  incluir(): void {
    this.clienteService.clienteDto = null;
    this.clienteService.id = null;
    this.clienteService.cargaDados = false;
    this.modalDetalheCliente.title = 'Inclusão';
    this.modalDetalheCliente.showModal();
  }

  alterarMascara(): void {
    let retorno = '000.000.000-00';
    const codTipoPessoa = this.filtroListagem.controls['codTipoPessoa'].value;
    if (codTipoPessoa === 1 && this.numDoc.maskTexto !== '000.000.000-00') {
      retorno = '000.000.000-00';
      this.filtroListagem.controls['numDocumento'].setValidators(Validators.compose([
      Validators.minLength(11), CNPJCPFValidator.validaCNPJCPF.bind(this)]));
      this.filtroListagem.controls['numDocumento'].setValue('');
      this.numDoc.isDisabled = false;
      this.numDoc.maskTexto = retorno;
      this.numDoc.titulo = 'CPF';
      this.numDoc.update();
    } else if (codTipoPessoa === 2 && this.numDoc.maskTexto !== '00.000.000/0000-00') {
      this.filtroListagem.controls['numDocumento'].setValidators(Validators.compose([
        Validators.minLength(14), CNPJCPFValidator.validaCNPJCPF.bind(this)]));
      retorno = '00.000.000/0000-00';
      this.filtroListagem.controls['numDocumento'].setValue('');
      this.numDoc.isDisabled = false;
      this.numDoc.maskTexto = retorno;
      this.numDoc.titulo = 'CNPJ';
      this.numDoc.update();
    } else if (codTipoPessoa === 0) {
      this.numDoc.maskTexto = '';
      this.numDoc.isDisabled = true;
      this.filtroListagem.controls['numDocumento'].setValue('');
      this.numDoc.titulo = '';
      this.numDoc.update();
    }
  }

  validarPesquisa(): boolean {
    let retorno = true;

    if (Util.isNullOrEmpty(this.filtroListagem.controls['nomeCliente'].value) &&
        Util.isNullOrEmpty(this.filtroListagem.controls['numDocumento'].value)) {
          retorno = false;
          this.messagingService.message.emit(new ModalMessage('Aviso',
          'Informe pelo menos um filtro de pesquisa.', 0, 'warning'));
    }
    return retorno;
  }
}
