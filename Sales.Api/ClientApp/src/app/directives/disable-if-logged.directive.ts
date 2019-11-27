import { Directive, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { LoginService } from '@/services/login.service';

@Directive({
  selector: '[appDisableIfLogged]'
})
export class DisableIfLoggedDirective implements OnInit, OnDestroy {
  private subscription: Subscription;

  constructor(private loginService: LoginService, private el: ElementRef) {}

  ngOnInit(): void {
    const elDisplay = this.el.nativeElement.style.display;

    this.subscription = this.loginService.asObservable().subscribe(_ => {
      if (!this.loginService.isUserLogged()) {
        this.el.nativeElement.style.display = elDisplay;
      } else {
        this.el.nativeElement.style.display = 'none';
      }
    });
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
