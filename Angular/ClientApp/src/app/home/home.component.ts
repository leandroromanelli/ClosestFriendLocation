import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public friends: Array<any> = new Array<any>();
  public latitude: number = 0.0;
  public longitude: number = 0.0;
  public error: string = '';
  public token: string = '';

  constructor(private http: HttpClient) {
    this.http.get('http://localhost:49885/').subscribe(data => { this.token = data as string });
  }

  getClosestFriends() {
    var headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('authorization', 'bearer ' + this.token);

    this.http.post('http://localhost:49885/',
      { latitude: this.latitude, longitude: this.longitude },
    )
      .subscribe(data => {
      this.friends = data as Array<any>
      },
      error => {
        this.error = error
      });
  }
}
