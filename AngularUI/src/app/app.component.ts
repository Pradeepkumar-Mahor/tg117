import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import {CommonModule} from '@angular/common';
import * as studentDataOrg from './assets/students.json';

@Component({
  selector: 'app-root',
  standalone:true,
  imports: [CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'json-read-example';
  data: any = studentDataOrg;
  studentData: any= this.data;

   ngOnInit() {
    console.log('Data', this.data);
  }
}
