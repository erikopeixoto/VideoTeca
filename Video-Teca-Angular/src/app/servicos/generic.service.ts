import { Injectable } from '@angular/core';
import { HttpParams, HttpErrorResponse, HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError , take } from 'rxjs/operators';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/toPromise';
import { Router } from '@angular/router';
import { MessagingService } from './messaging.service';
import { ModalMessage } from '../shared/modelos/modal-message';
import { ProgressSpinnerService } from '../shared/progress-spinner/progress-spinner.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class GenericHttpService<T, K> {
  private url: string;
  public contador = 0;
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(
    private readonly httpClient: HttpClient,
    private readonly rotaService: Router,
    private readonly messagingService: MessagingService,
    private readonly progressSpinnerService: ProgressSpinnerService
  ) { }

  carregando(): void {
    if (this.contador === 0 ){
       this.progressSpinnerService.show();
    }
    this.contador++;
  }

  carregado(): void {
    this.contador--;
    if (this.contador === 0 ){
       this.progressSpinnerService.hide();
    }
  }

  getAll(httpParams?: HttpParams, url?: string): Promise<Array<K>> {
    this.url = url;
    const header = this.getHeaders();
    this.carregando();

    return this.httpClient.get<Array<K>>(this.url, {
      headers: header,
      params: httpParams,
      responseType: 'json'
    })
    .pipe(map((response) => this.handleResponseK(response)),
          take(1),
          catchError((err) => this.handleError(err)))
    .toPromise();
  }

  public post(t: T, url?: string): Promise<any> {
    this.url = url;
    const payLoad = JSON.stringify(t);
    this.carregando();

    return this.httpClient.post(this.url, payLoad,
      {
        headers: this.getRequestOptionsContentTypeJson(),
        responseType: 'json'
      })
      .pipe(map((response) => this.handleResponseAny(response)),
            catchError((err) => this.handleError(err)))
      .toPromise();
  }

  public postLogin(t: T, url?: string): Promise<any> {
    this.url = url;
    const payLoad = JSON.stringify(t);
    this.carregando();

    return this.httpClient
    .post(this.url, payLoad,
      {
        headers: this.getRequestOptionsContentTypeJson(),
        responseType: 'json'
      })
    .pipe(map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          sessionStorage.setItem('username', this.decodedToken.unique_name);
        }
      })
    )
  .toPromise();
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  public getByIdCli(urlpath: string): Promise<T> {
    this.url = urlpath;
    const header = this.getHeaders();
    this.carregando();
    return this.httpClient.get<T>(this.url, {
      headers: header,
      responseType: 'json'
    })
    .pipe(map((response) => this.handleResponseT(response)),
          take(1),
          catchError((err) => this.handleError(err)))
    .toPromise();
  }

  public put(t: T, url?: string): Promise<any> {
    this.url = url;
    const payLoad = JSON.stringify(t);
    const header = this.getRequestOptionsContentTypeJson();
    this.carregando();
    return this.httpClient.put(this.url, payLoad, { headers: header })
    .pipe(map((response) => this.handleResponseAny(response)),
          take(1),
          catchError((err) => this.handleError(err)))
    .toPromise();
  }

  public delete(url: string): Promise<any> {
    this.url = url;
    this.carregando();
    return this.httpClient.delete(this.url,
      { headers: this.getHeaders() })
      .pipe(map((response) => this.handleResponseAny(response)),
            take(1),
            catchError((err) => this.handleError(err)))
      .toPromise();
  }

  public setUrl(url: string): void {
    this.url = url;
  }

  setError(error: any): any {
    return Promise.reject(error.json() || 'Error!');
  }

  public getParam(): any {
    return new HttpParams();
  }

  public handleMessage(response): any {
    if (!response) {
      return;
    }
    if (!(response['title'] && response['message'] && response['type'])) {
      return;
    }
    this.messagingService.message.emit(new ModalMessage(response['title'], response['message'], response['typeInt'], response['type']));
  }

  public handleResponse = (response: T[]) => {
    this.handleMessage(response);
    this.carregado();
    return response;
  }

  public handleResponseK = (response: K[]) => {
    this.handleMessage(response);
    this.carregado();
    return response;
  }

  public handleResponseT = (responseT: T) => {
    this.handleMessage(responseT);
    this.carregado();
    return responseT;
  }

  public handleResponseAny = (responseAny: any) => {
    this.handleMessage(responseAny);
    this.carregado();
    return responseAny;
  }

  public handleResponseTBlob = (response: any) => {
    this.carregado();
    return new Blob([response.body]);
  }

  public handleError = (error: HttpErrorResponse) => {
    this.carregado();
    if (error) {
      const message = error.error;
      if (message && message['title'] && message['message'] && message['type']) {
        this.messagingService.message.emit(new ModalMessage(message['title'], message['message'], message['typeInt'], message['type']));
      } else {
        if (error.statusText === 'Token expirado') {
          this.messagingService.message.emit(new ModalMessage('Alerta', 'Tempo de sessão expirado.', 1, 'warning'));
        } else if (error.message.indexOf('Http failure') !== -1) {
          this.messagingService.message.emit(new ModalMessage('Erro', 'Não foi possível se comunicar com o servidor.', 1, 'danger'));
        }
        this.logouf();
      }
    }
    if (error.status === 521) {
      return Observable.throwError({ status: error.status, Mensagem: error.statusText });
    }
  }

  private logouf(): void {
    this.rotaService.navigate(['/']);
  }

  public download(url: string, obj: T): Observable<Blob> {
    const header = this.getRequestOptionsContentTypeJsonBlob();
    const payLoad = JSON.stringify(obj);

    return this.httpClient
      .post(url, payLoad, {
        headers: header,
        observe: 'response',
        responseType: 'blob'
      })
      .pipe(map((response) => this.handleResponseTBlob(response)),
            catchError((err) => this.handleError(err)));
  }

  protected getHeaders(): HttpHeaders {
    return new HttpHeaders().set('Access-Control-Allow-Origin', '*');
  }

  protected getRequestOptionsContentTypeJson(): HttpHeaders {
    return this.getHeaders().append('Content-type', 'application/json');
  }

  protected getRequestOptionsContentTypeJsonBlob(): HttpHeaders {
    return this.getRequestOptionsContentTypeJson().set('ResponseContentType', 'Blob');
  }

  public downloadRelatorio(url: string, obj: T): Observable<Blob> {
    const header = this.getRequestOptionsContentTypeJsonBlob();
    const payLoad = JSON.stringify(obj);

    return this.httpClient
      .post(url, payLoad, {
        headers: header,
        observe: 'response',
        responseType: 'blob'
      })
      .pipe(
        map(response => this.handleResponseTBlob(response)),
        catchError(err => this.handleErrorRelatorio(err))
      );
  }

  public handleErrorRelatorio = (error: HttpErrorResponse) => {
    const message = error.error;
    if (message && message['title'] && message['message'] && message['type']) {
      this.messagingService.message.emit(new ModalMessage(message['title'], message['message'], message['TypeInt'], message['Type']));
    }
    if (error.status === 521) {
      return Observable.throwError({ status: error.status, Mensagem: error.statusText });
    } else if (error.status === 401) {
      this.logouf();
      return Observable.throwError('');
    } else if (error.status === 500) {
      return Observable.throwError(error.error);
    } else {
      return Observable.throwError(error);
    }
  }
}
