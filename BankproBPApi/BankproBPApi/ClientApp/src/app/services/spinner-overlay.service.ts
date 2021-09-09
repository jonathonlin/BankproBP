import { SpinnerOverlayComponent } from './../shared/components/spinner-overlay/spinner-overlay.component';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { ComponentPortal } from '@angular/cdk/portal';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SpinnerOverlayService {
  private overlayRef: OverlayRef;

  constructor(private overlay: Overlay) { }

  show(){
    if(!this.overlayRef){
      this.overlayRef = this.overlay.create({
        hasBackdrop: true,
        positionStrategy: this.overlay
          .position()
          .global()
          .centerHorizontally()
          .centerVertically()
      });
    }

    const spinnerOverlayPortal = new ComponentPortal(SpinnerOverlayComponent);
    if(!this.overlayRef.hasAttached())
      this.overlayRef.attach(spinnerOverlayPortal);
  }

  hide(){
    if(!!this.overlayRef){
      this.overlayRef.detach();
    }
  }
}
