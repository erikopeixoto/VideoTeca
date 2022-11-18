import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Usuario } from '../modelos/usuario';
import { map } from 'rxjs/operators';
import { BaseService } from './base.service';
import { GenericHttpService } from './generic.service';

@Injectable({
  providedIn: 'root'
})
export class AutorizacaoService  extends BaseService{

  constructor(
    private readonly servicoHttpGenerico: GenericHttpService<Usuario, Usuario>
  ) {
    super();
  }

  public login(usuario: Usuario): Promise<Usuario> {
    return this.servicoHttpGenerico.postLogin(usuario, `${BaseService.getBaseAPI()}login`);
  }

  public registro(usuario: Usuario): Promise<Usuario> {
    return this.servicoHttpGenerico.post(usuario, `${BaseService.getBaseAPI()}registro`);
  }

  loggedIn() {
    return this.servicoHttpGenerico.loggedIn();
  }

}
