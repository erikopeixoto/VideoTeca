import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { TipoMidia } from '../../modelos/tipo-midia';
import { ModalComponent } from '../../shared/modal/modal.component';
import { TipoMidiaService } from '../../servicos/tipo-midia.service';
import { Util } from '../../utils/util';
import { Alerta } from '../../shared/modelos/alerta';
import { MatDialog } from '@angular/material/dialog';
import { AlertaComponent } from '../../shared/components/alerta/alerta.component';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'app-tipo-midia',
  templateUrl: './tipo-midia.component.html',
  styleUrls: ['./tipo-midia.component.css']
})
export class TipoMidiaComponent implements OnInit, AfterViewInit {

  @ViewChild('modalDetalheTipoMidia', {static: true} ) modalDetalheTipoMidia: ModalComponent;
  displayedColumns: string[] = [ 'id', 'descricao', 'acoes'];
  dataSource: MatTableDataSource<TipoMidia>;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  get getVisibleTipoMidiaDetalhe(): boolean {
    return this.modalDetalheTipoMidia.getVisible();
  }

  public tipoMidia: TipoMidia;
  public parente: TipoMidiaComponent;
  public stsTipoMidia = false;

  constructor(
    public tipoMidiaService: TipoMidiaService,
    public dialog: MatDialog
  ) {
    this.parente = this;
  }

  ngOnInit(): void {
    this.paginator._intl.itemsPerPageLabel = 'Itens por página ';
    this.pesquisar();
  }

  ngAfterViewInit() {

  }

  pesquisar(): void {
    this.dataSource = undefined;
    this.tipoMidiaService.listar().then((lista) => {
      if (!Util.isNullOrEmpty(lista[0].descricao)) {
          this.dataSource = new MatTableDataSource(lista);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
          this.dataSource.paginator.firstPage();
      } else {
        this.dataSource.paginator._intl = undefined;
      }
    });
  }

  receberTipoMidia(itemRecebido): void {

  }

  editar(id: number): void {
    this.tipoMidiaService.id = id;
    this.tipoMidiaService.cargaDados = false;
    this.modalDetalheTipoMidia.title = 'Alteração';
    this.modalDetalheTipoMidia.showModal();
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
            this.tipoMidiaService.excluir(id).then(() => {
              this.pesquisar();
            });
         }
      }
    });
  }

  incluir(): void {
    this.tipoMidiaService.tipomidia = null;
    this.tipoMidiaService.id = null;
    this.tipoMidiaService.cargaDados = false;
    this.modalDetalheTipoMidia.title = 'Inclusão';
    this.modalDetalheTipoMidia.showModal();
  }
}
