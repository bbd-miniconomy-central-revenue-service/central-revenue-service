import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(){}

  ngOnInit() {   
  }

  onLoginClick(){
    const URL="https://bbdrs.auth.us-east-1.amazoncognito.com/login?response_type=code&client_id=165320a8ntn332eks21c3q7r8l&redirect_uri=https://mers.projects.bbdgrad.com";
    window.location.assign(URL);
  }
}
