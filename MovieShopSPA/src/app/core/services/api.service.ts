import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable, observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private headers: HttpHeaders | undefined;

  //HttpClient is for communicating with API
  constructor(protected http: HttpClient) { }

  //get array of json objects
  getList(path: string): Observable<any[]>{
    //call only urls that get json array from API
    return this.http
    .get(`${environment.apiUrl}${path}`)
    .pipe(map((resp) => resp as any[]));
  }

  //getting single json object with movie/id
  getOne(path: string, id?: number){
    return this.http
    .get(`${environment.apiUrl}${path}` + id)
    .pipe(map((resp) => resp as any));
  }

  //posting an item
  create(path: string, resource: any, options?: any): Observable<any> {
    return this.http
    .post(`${environment.apiUrl}${path}`, resource, {headers: this.headers})
    .pipe(map((resp) => resp));
  }

  // update(){

  // }

  // delete() {

  // }


}
