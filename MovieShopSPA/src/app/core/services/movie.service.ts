import { Injectable } from '@angular/core';
import { MovieCard } from 'src/app/shared/models/moviecard';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private http: HttpClient) { }

  getTopRevenueMovies(): Observable<MovieCard[]> {
    return this.http.get(`${environment.apiUrl}`+'movies/toprevenue')
    .pipe( map (resp => resp as MovieCard[]));

  }
}
