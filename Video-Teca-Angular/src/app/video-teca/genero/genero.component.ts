import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Genero } from '../../modelos/genero';
import { ModalComponent } from '../../shared/modal/modal.component';
import { GeneroService } from '../../servicos/genero.service';
import { Util } from '../../utils/util';
import { Alerta } from '../../shared/modelos/alerta';
import { MatDialog } from '@angular/material/dialog';
import { AlertaComponent } from '../../shared/components/alerta/alerta.component';
import { InputTextComponent } from '../../shared/components/campos/input-text/input-text.component';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'app-genero',
  templateUrl: './genero.component.html',
  styleUrls: ['./genero.component.css']
})
export class GeneroComponent implements OnInit, AfterViewInit {

  @ViewChild('modalDetalheGenero', {static: true} ) modalDetalheGenero: ModalComponent;
  displayedColumns: string[] = [ 'id', 'descricao', 'acoes'];
  dataSource: MatTableDataSource<Genero>;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  get getVisibleGeneroDetalhe(): boolean {
    return this.modalDetalheGenero.getVisible();
  }

  public genero: Genero;
  public parente: GeneroComponent;
  public stsGenero = false;

  constructor(
    public generoService: GeneroService,
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
    this.generoService.listar().then((lista) => {
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

  receberGenero(itemRecebido): void {

  }

  editar(id: number): void {
    this.generoService.id = id;
    this.generoService.cargaDados = false;
    this.modalDetalheGenero.title = 'Alteração';
    this.modalDetalheGenero.showModal();
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
            this.generoService.excluir(id).then(() => {
              this.pesquisar();
            });
         }
      }
    });
  }

  incluir(): void {
    this.generoService.genero = null;
    this.generoService.id = null;
    this.generoService.cargaDados = false;
    this.modalDetalheGenero.title = 'Inclusão';
    this.modalDetalheGenero.showModal();
  }
}
