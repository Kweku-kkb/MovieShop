import { Component, OnInit } from '@angular/core';
import { MovieService } from '../core/services/movie.service';
import { MovieCard } from '../shared/models/moviecard';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  movies!: MovieCard[];
    
  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    console.log('inside the ngOninit method of Home Component')
    this.movieService.getTopRevenueMovies()
    .subscribe(m => { 
      this.movies = m;
      console.table(this.movies);
    })
  }

}
