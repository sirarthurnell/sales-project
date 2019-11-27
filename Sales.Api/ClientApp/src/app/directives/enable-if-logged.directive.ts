import { Directive, ElementRef, OnInit, OnDestroy } from '@angular/core';
import { LoginService } from '@/services/login.service';
import { Subscription } from 'rxjs';

@Directive({
  selector: '[appEnableIfLogged]'
})
export class EnableIfLoggedDirective implements OnInit, OnDestroy {
  private subscription: Subscription;

  constructor(private loginService: LoginService, private el: ElementRef) {}

  ngOnInit(): void {
    const elDisplay = this.el.nativeElement.style.display;

    this.subscription = this.loginService.asObservable().subscribe(_ => {
      if (!this.loginService.isUserLogged()) {
        this.el.nativeElement.style.display = 'none';
      } else {
        this.el.nativeElement.style.display = elDisplay;
      }
    });
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
