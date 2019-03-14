import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  result?: any;

  constructor(private readonly http: HttpClient) {}

  callApi(): void {
    this.http.get('https://localhost:44388/api/values')
      .pipe(first())
      .subscribe(values => this.result = values);
  }
}
