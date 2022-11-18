import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Catalogo } from '../../modelos/catalogo';
import { Genero } from '../../modelos/genero';
import { CatalogoDto } from '../../dtos/catalogo-dto';
import { FiltroCatalogoDto } from '../../dtos/filtro-catalogo-dto';
import { ModalComponent } from '../../shared/modal/modal.component';
import { CatalogoService } from '../../servicos/catalogo.service';
import { GeneroService } from '../../servicos/genero.service';
import { Util } from '../../utils/util';
import { Alerta } from '../../shared/modelos/alerta';
import { MatDialog } from '@angular/material/dialog';
import { AlertaComponent } from '../../shared/components/alerta/alerta.component';
import { InputTextComponent } from '../../shared/components/campos/input-text/input-text.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MessagingService } from '../../servicos/messaging.service';
import { ModalMessage } from '../../shared/modelos/modal-message';

@Component({
  selector: 'app-catalogo',
  templateUrl: './catalogo.component.html',
  styleUrls: ['./catalogo.component.css']
})
export class CatalogoComponent implements OnInit, AfterViewInit {

  @ViewChild('modalDetalheCatalogo', {static: true} ) modalDetalheCatalogo: ModalComponent;
  displayedColumns: string[] = [ 'titulo', 'autor', 'genero', 'codigo', 'acoes'];
  dataSource: MatTableDataSource<CatalogoDto>;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  get getVisibleCatalogoDetalhe(): boolean {
    return this.modalDetalheCatalogo.getVisible();
  }

  public catalogoDto: CatalogoDto;
  public parente: CatalogoComponent;
  public stsCatalogo = false;
  public catalogo: Catalogo;
  public generos: Genero[] = [];
  public catalogoFiltrados: CatalogoDto [];
  public filtroLista: Catalogo;
  public filtroListagem: FormGroup;

  constructor(
    public catalogoService: CatalogoService,
    public generoService: GeneroService,
    private readonly messagingService: MessagingService,
    public fb: FormBuilder,
    public dialog: MatDialog
  ) {
    this.parente = this;
  }

  ngOnInit(): void {
    this.catalogo = new Catalogo();
    this.filtroListagem = this.fb.group({
      nomeAutor: ['', Validators.minLength(3)],
      titulo: ['', Validators.minLength(3)],
      idGenero: [0]
    });
    this.paginator._intl.itemsPerPageLabel = 'Itens por página ';
  }

  ngAfterViewInit(): void {
    this.generos.push({ id: 0, descricao: '' });
    this.generoService.listar().then((lista) => {
      if (lista.length > 0) {
        lista.sort((a, b) => Util.ordenacao(a , b, 'descricao'));
        lista.forEach(element => {
          this.generos.push(element);
        });
      }
    });
  }

  pesquisar(): void {
    if (this.filtroListagem.valid && this.validarPesquisa()) {
      const filtro = new FiltroCatalogoDto();
      filtro.NomeAutor = this.filtroListagem.controls['nomeAutor'].value;
      filtro.Titulo = this.filtroListagem.controls['titulo'].value;
      filtro.IdGenero = this.filtroListagem.controls['idGenero'].value;
      this.catalogoFiltrados = [];
      this.dataSource = undefined;
      this.catalogoService.pesquisar(filtro).then((lista) => {
        if (lista.length > 0 ) {
            this.catalogoFiltrados = lista;
            this.dataSource = new MatTableDataSource(this.catalogoFiltrados);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
            this.dataSource.paginator.firstPage();
        } else {
          this.dataSource.paginator._intl = undefined;
        }
      });
    }
  }

  receberCatalogo(itemRecebido): void {

  }

  editar(id: number): void {
    this.catalogoService.id = id;
    this.catalogoService.cargaDados = false;
    this.modalDetalheCatalogo.title = 'Alteração';
    this.modalDetalheCatalogo.showModal();
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
            this.catalogoService.excluir(id).then(() => {
              this.pesquisar();
            });
         }
      }
    });
  }

  incluir(): void {
    this.catalogoService.catalogoDto = null;
    this.catalogoService.id = null;
    this.catalogoService.cargaDados = false;
    this.modalDetalheCatalogo.title = 'Inclusão';
    this.modalDetalheCatalogo.showModal();
  }
  validarPesquisa(): boolean {
    let retorno = true;

    if (Util.isNullOrEmpty(this.filtroListagem.controls['titulo'].value) &&
        Util.isNullOrEmpty(this.filtroListagem.controls['nomeAutor'].value) &&
        Number(this.filtroListagem.controls['idGenero'].value) === 0) {
          retorno = false;
          this.messagingService.message.emit(new ModalMessage('Aviso',
          'Informe pelo menos um filtro de pesquisa.', 0, 'warning'));
    }
    return retorno;
  }
}
