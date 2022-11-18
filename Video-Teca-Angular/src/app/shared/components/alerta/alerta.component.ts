import { Injectable, Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Alerta } from '../../modelos/alerta';
import { MessagingService } from '../../../servicos/messaging.service';
import { ModalMessage } from '../../../shared/modelos/modal-message';

@Component({
  selector: 'app-alerta',
  templateUrl: './alerta.component.html',
  styleUrls: ['./alerta.component.css']
})

@Injectable()

export class AlertaComponent implements OnInit {

  alerta = {
    titulo: 'Sucesso!',
    descricao: 'Seu registro foi cadastrado com sucesso!',
    btnSucesso: 'OK',
    btnCancelar: 'Cancelar',
    corBtnSucesso: 'accent',
    corBtnCancelar: 'warn',
    possuirBtnFechar: false,
    style: 'color: warn',
    styleTitulo: 'color: warn; height: 5%'
  } as Alerta;
  
  constructor(public dialogRef: MatDialogRef<AlertaComponent>,
              private readonly messagingService: MessagingService,
              @Inject(MAT_DIALOG_DATA) public data: Alerta) {  }

  ngOnInit(): void {
    if (this.data) {
      this.alerta.titulo = this.data.titulo || this.alerta.titulo;
      this.alerta.descricao = this.data.descricao || this.alerta.descricao;
      this.alerta.btnSucesso = this.data.btnSucesso || this.alerta.btnSucesso;
      this.alerta.btnCancelar = this.data.btnCancelar || this.alerta.btnCancelar;
      this.alerta.corBtnSucesso = this.data.corBtnSucesso || this.alerta.corBtnSucesso;
      this.alerta.corBtnCancelar = this.data.corBtnCancelar || this.alerta.corBtnCancelar;
      this.alerta.possuirBtnFechar = this.data.possuirBtnFechar || this.alerta.possuirBtnFechar;
      this.alerta.style = this.data.style || this.alerta.style;
      this.alerta.styleTitulo = this.data.styleTitulo || this.alerta.styleTitulo;
    }
  }

  public mensagemAviso(titulo: string, mensagem: string, codtipo: number, tipo: string): void {
    this.messagingService.message.emit(new ModalMessage(titulo, mensagem, codtipo, tipo));

  }
}
