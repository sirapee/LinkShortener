import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AlltouseService {

  constructor(
    private http: HttpClient
  ) { }

  // tslint:disable-next-line:typedef
  encodeUrl(fullLink){
    return this.http.get(`${environment.baseUrl}api/encode?fullLink=${fullLink}`);
  }

  // tslint:disable-next-line:typedef
  decodeUrl(shortLink){
    return this.http.get(`${environment.baseUrl}api/decode?shortLink=${shortLink}`);
  }

  // tslint:disable-next-line:typedef
  getAllUrl(){
    return this.http.get(`${environment.baseUrl}api/list`);
  }

  // tslint:disable-next-line:typedef
  redirectAlias(aliasLink){
    return this.http.get(`${environment.baseUrl}${aliasLink}`);
  }

}
