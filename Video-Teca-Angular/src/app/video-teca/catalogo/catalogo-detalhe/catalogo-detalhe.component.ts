import { Event, Router } from '@angular/router';
import { Component, AfterViewInit,
         OnInit, Input, Output, EventEmitter , ChangeDetectionStrategy, ViewChild, resolveForwardRef,
} from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ModalComponent } from '../../../shared/modal/modal.component';

import { CatalogoService } from '../../../servicos/catalogo.service';
import { Alerta } from '../../../shared/modelos/alerta';
import { MatDialog } from '@angular/material/dialog';
import { AlertaComponent } from '../../../shared/components/alerta/alerta.component';
import { CatalogoTipoMidia } from '../../../modelos/catalogo-tipo-midia';
import { CatalogoTipoMidiaDto } from '../../../dtos/catalogo-tipo-midia-dto';
import { Catalogo } from '../../../modelos/catalogo';
import { Util } from '../../../utils/util';
import { MaskManager } from '../../../utils/mask-manager';
import { InputTextComponent } from '../../../shared/components/campos/input-text/input-text.component';
@Component({
  selector: 'app-catalogo-detalhe',
  templateUrl: './catalogo-detalhe.component.html',
  styleUrls: ['./catalogo-detalhe.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CatalogoDetalheComponent implements OnInit, AfterViewInit {
  @Input() modalParent: any;
  @Input() item: any;
  @Output() itemDestino = new EventEmitter();

  displayedColumns: string[] = [ 'id' , 'tipomidia', 'quantidade', 'acoes'];
  dataSource: MatTableDataSource<CatalogoTipoMidiaDto>;

  @ViewChild('modalCatalogoTipoMidia', {static: true} ) modalCatalogoTipoMidia: ModalComponent;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  get getVisibleCatalogoDetalhe(): boolean {
    return this.modalCatalogoTipoMidia.getVisible();
  }

  public catalogo: Catalogo;
  public id: number;
  public formCatalogo: FormGroup;
  public operacao: string;
  public maskManager: MaskManager;
  public pai: CatalogoDetalheComponent;
  public cargaDados: boolean;
  public catalogoTipoMidia: CatalogoTipoMidia;
  public catalogoTipoMidiaDto: CatalogoTipoMidiaDto;

  constructor(
    public catalogoService: CatalogoService,
    private router: Router,
    public fb: FormBuilder,
    public dialog: MatDialog,
  ) {
      this.pai = this;
  }

  ngOnInit(): void {
    this.cargaDados = false;
    this.formCatalogo = this.fb.group({
      codigo: ['' ,[Validators.required, Validators.maxLength(5)]],
      idGenero: ['', Validators.required],
      nomAutor: ['', [Validators.required, Validators.minLength(10)]],
      desTitulo: ['', [Validators.required, Validators.minLength(5)]],
      id: [],
      dtcAtualizacao: [],
      desGenero: [''],
      catalogoTipoMidiasDto: [''],
      anoLancamento: ['', Validators.required]
    });

    if (this.catalogoService.id) {
      this.operacao = 'Confirma a alteração?';
      this.id = this.catalogoService.id;
      this.cargaDados = true;
    } else {
      this.operacao = 'Confirma a inclusão?';
    }
  }

  ngAfterViewInit(): void{
    this.dataSource = new MatTableDataSource();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
   if (this.cargaDados) {
      this.carregar();
    }
  }

  carregar(): Promise<boolean> {
   return new Promise((resolve) => {this.catalogoService.buscar(this.id ).then((catalogo: Catalogo) => {
      if (! Util.isNullOrEmpty(catalogo)) {
        this.formCatalogo.setValue(catalogo);
        this.dataSource = new MatTableDataSource(catalogo.catalogoTipoMidiasDto);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.dataSource.paginator.firstPage();
        this.formCatalogo.updateValueAndValidity();
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
    this.atribuirTipoMidia(this.dataSource.data);
    this.catalogoService.incluir(this.catalogo).then(() => {
      this.modalParent.pesquisar();
      this.modalParent.modalDetalheCatalogo.closeModal();
    });
  }

  alterar(): void {
    this.catalogo = this.formCatalogo.getRawValue() as Catalogo;
    this.atribuirTipoMidia(this.dataSource.data);
    this.catalogoService.alterar(this.catalogo).then(() => {
      this.modalParent.pesquisar();
      this.modalParent.modalDetalheCatalogo.closeModal();
    });
  }

  atribuirTipoMidia(tipoMidia): void {
    this.catalogo.catalogoTipoMidias = [];
    this.catalogo.catalogoTipoMidiasDto = [];
    tipoMidia.forEach(element => {
      this.catalogoTipoMidia = new CatalogoTipoMidia();
      this.catalogoTipoMidia.id = element.id;
      this.catalogoTipoMidia.idCatalogo = element.idCatalogo ?? 0;
      this.catalogoTipoMidia.idTipoMidia = element.idTipoMidia;
      this.catalogoTipoMidia.qtdTitulo = element.qtdTitulo;
      this.catalogoTipoMidia.qtdDisponivel = element.qtdTitulo;
      this.catalogo.catalogoTipoMidias.push(this.catalogoTipoMidia);
      this.catalogoTipoMidiaDto = new CatalogoTipoMidiaDto();
      this.catalogoTipoMidiaDto.id = element.id;
      this.catalogoTipoMidiaDto.idCatalogo = element.idCatalogo ?? 0;
      this.catalogoTipoMidiaDto.idTipoMidia = element.idTipoMidia;
      this.catalogoTipoMidiaDto.qtdTitulo = element.qtdTitulo;
      this.catalogoTipoMidiaDto.qtdDisponivel = element.qtdTitulo;
      this.catalogo.catalogoTipoMidiasDto.push(this.catalogoTipoMidiaDto);
    });

  }

  incluirMidia(): void {
    this.catalogoService.catalogoTipoMidiaDto = null;
    this.modalCatalogoTipoMidia.title = 'Inclusão';
    this.modalCatalogoTipoMidia.showModal();
  }

  editarMidia(item: CatalogoTipoMidiaDto): void {
    this.catalogoService.catalogoTipoMidiaDto = item;
    this.modalCatalogoTipoMidia.title = 'Alteração';
    this.modalCatalogoTipoMidia.showModal();
  }

  excluirMidia(id: number): void {
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
            const data = this.dataSource.data;
            let itemIndex = data.findIndex(item => item.id == id);
            data.splice(itemIndex, 1);
            this.dataSource.data = data;
         }
      }
    });
  }

  fechar(): void {
    this.modalParent.modalDetalheCatalogo.closeModal();
  }
}
