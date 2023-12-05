import { Injectable } from '@angular/core'
import { Observable, of, throwError } from 'rxjs';
import { catchError, map, tap, retry } from 'rxjs/operators';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

import { LandingPageComponent } from '../landing-page/landing-page.component';
import { User } from '../Interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class RegistroService {

  //private url = '/HotSpot';
  //private url = 'localhost:5066/HotSpot';

  constructor(private http: HttpClient) { }


    httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };






}
