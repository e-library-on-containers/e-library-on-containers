import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'front';
  loggedIn: boolean;

  constructor() {
    this.loggedIn = localStorage.getItem('access_token') != null
  }

  logOut() {
    localStorage.removeItem('access_token')
    localStorage.removeItem('user-id')
    window.location.replace("/")
  }
}
