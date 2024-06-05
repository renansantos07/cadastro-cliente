import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';

export const authGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot) => {

  const token = window.localStorage.getItem('token');
  if(token){
    return true;
  }else{
    inject(Router).navigate(["login"])
    return false;
  }
};
