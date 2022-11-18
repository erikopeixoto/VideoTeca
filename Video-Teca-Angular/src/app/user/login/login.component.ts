import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
//import { ToastrService } from 'ngx-toastr';
import { AutorizacaoService } from '../../servicos/autorizacao.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  titulo = 'Login';
  model: any = {};

  constructor(private autorizacaoService: AutorizacaoService
    , public router: Router
    //, private toastr: ToastrService
    ) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigate(['/dashboard']);
    }
  }

  login() {
    this.autorizacaoService.login(this.model)
      .then(
        () => {
          this.router.navigate(['/dashboard']);
          //this.toastr.success('Logado com Sucesso');
        },
        error => {
          //this.toastr.error('Falha ao tentar Logar');
        }
      );
  }
}
