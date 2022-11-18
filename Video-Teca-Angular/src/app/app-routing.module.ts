import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClienteComponent } from './video-teca/cliente/cliente.component';
import { GeneroComponent } from './video-teca/genero/genero.component';
import { TipoMidiaComponent } from './video-teca/tipo-midia/tipo-midia.component';
import { CatalogoComponent } from './video-teca/catalogo/catalogo.component';

const routes: Routes = [
  { path: 'cliente', component: ClienteComponent },
  { path: 'genero', component: GeneroComponent },
  { path: 'tipoMidia', component: TipoMidiaComponent },
  { path: 'catalogo', component: CatalogoComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
