import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from '@angular/cdk/layout';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { ProgressSpinnerComponent } from './shared/progress-spinner/progress-spinner.component';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { MaterialModule } from './modulos/material/material.module';
import { TopoComponent } from './shared/components/topo/topo.component';
import { RodapeComponent } from './shared/components/rodape/rodape.component';
import { AlertaComponent } from './shared/components/alerta/alerta.component';
import { ClienteComponent } from './video-teca/cliente/cliente.component';
import { ClienteDetalheComponent } from './video-teca/cliente/cliente-detalhe/cliente-detalhe.component';
import { ClienteLocacaoComponent } from './video-teca/cliente/cliente-locacao/cliente-locacao.component';
import { GeneroComponent } from './video-teca/genero/genero.component';
import { TipoMidiaComponent } from './video-teca/tipo-midia/tipo-midia.component';
import { CamposModule } from './shared/components/campos/campos.module';
import { ModalComponent } from './shared/modal/modal.component';
import { NgxbootstrapModule } from './modulos/ngxbootstrap/ngxbootstrap.module';
import { GeneroDetalheComponent } from './video-teca/genero/genero-detalhe/genero-detalhe.component';
import { TipoMidiaDetalheComponent } from './video-teca/tipo-midia/tipo-midia-detalhe/tipo-midia-detalhe.component';
import { CatalogoComponent } from './video-teca/catalogo/catalogo.component';
import { CatalogoDetalheComponent } from './video-teca/catalogo/catalogo-detalhe/catalogo-detalhe.component';
import { CatalogoTipoMidiaComponent } from './video-teca/catalogo/catalogo-detalhe/catalogo-tipo-midia/catalogo-tipo-midia.component';

@NgModule({
  declarations: [
    AppComponent,
    TopoComponent,
    RodapeComponent,
    AlertaComponent,
    ClienteComponent,
    ClienteDetalheComponent,
    ClienteLocacaoComponent,
    GeneroComponent,
    ModalComponent,
    GeneroDetalheComponent,
    ProgressSpinnerComponent,
    TipoMidiaComponent,
    TipoMidiaDetalheComponent,
    CatalogoComponent,
    CatalogoDetalheComponent,
    CatalogoTipoMidiaComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    LayoutModule,
    AppRoutingModule,
    ReactiveFormsModule,
    NgxbootstrapModule,
    CamposModule
  ],
  providers: [{ provide: MAT_DATE_LOCALE, useValue: 'pt' }],
  bootstrap: [AppComponent],
  exports: [ProgressSpinnerComponent]
})
export class AppModule { }
