import { Component, OnInit, OnDestroy } from '@angular/core';

import { ToastrService } from "ngx-toastr";
import { User } from '../Interfaces/user';
import { RegistroService } from '../services/registro.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';

import {Subject} from 'rxjs';
import {takeUntil} from 'rxjs/operators';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent implements OnDestroy {

  destroyed = new Subject<void>();
  currentScreenSize: string = '';

  displayNameMap = new Map([
    [Breakpoints.XSmall, 'XSmall'],
    [Breakpoints.Small, 'Small'],
    [Breakpoints.Medium, 'Medium'],
    [Breakpoints.Large, 'Large'],
    [Breakpoints.XLarge, 'XLarge'],
  ]);

  // user = {
  //   nome: null,
  //   email: null,
  //   telefone: null
  // };

  // constructor(private registroService: RegistroService, private toastr: ToastrService)
  // {
  // }

  constructor(private http: HttpClient, private toastr: ToastrService, public breakpointObserver: BreakpointObserver) {

    breakpointObserver
      .observe([
        Breakpoints.XSmall,
        Breakpoints.Small,
        Breakpoints.Medium,
        Breakpoints.Large,
        Breakpoints.XLarge,
      ])
      .pipe(takeUntil(this.destroyed))
      .subscribe(result => {
        for (const query of Object.keys(result.breakpoints)) {
          if (result.breakpoints[query]) {
            this.currentScreenSize = this.displayNameMap.get(query) ?? 'Unknown';
          }
        }
      });

  }

  ngOnDestroy() {
    this.destroyed.next();
    this.destroyed.complete();
  }




  // onSubmit(user: any): void {

  //   user = user.trim();
  //   if(!user) {return;}
  //   //this.registroService.addRegistro(user as any).subscribe(user => this.user = user)

  //   this.toastr.show("Obrigado pelo cadastro. Bem vindo.");
  //   console.log(user);
  // }

  onSubmit(user: {nome: string, email: string, telefone:string}) {

    const headers = new HttpHeaders({'myHeader': 'hotspot'}) //optional

    this.http.post('http://localhost:5066/HotSpot', user, {headers: headers}).subscribe((response) => {})

    // user = user.trim();
    // if(!user) {return;}
    //this.registroService.addRegistro(user as any).subscribe(user => this.user = user)

    this.toastr.show("Obrigado pelo cadastro. Bem vindo.")
    console.log(user);
  }

}
