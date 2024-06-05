import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environment';
import { tokenResult } from '../../models/tokenResult.model';
import { Usuario } from '../../models/usuario.model';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ContaService {

  constructor(private http: HttpClient) { }

  async login(user: Usuario) {
    return await firstValueFrom(this.http.post<tokenResult>(`${environment.endPoint}/login`, user))
      .then((result) => {
        if (result && result.accessToken) {
          window.localStorage.setItem('token', result.accessToken)
          window.localStorage.setItem('email', user.email)
          return {
            sucesso: true,
            token: result.accessToken
          }
        } else {
          return {
            sucesso: false,
            mensagem: "E-mail e senha inválidos"
          }
        }
      }).catch((error) => {
        return {
          sucesso: false,
          mensagem: "E-mail e senha inválidos"
        }
      });
  }

  async registrar(user: Usuario) {
    return await firstValueFrom(this.http.post<any>(`${environment.endPoint}/register`, user))
      .then((result) => {
        return "";
      }).catch((error) => {
        const errorList = error.error.errors;
        let passwordRequiresDigit = ""
        let passwordRequiresLower = ""
        let passwordRequiresUpper = ""
        let passwordTooShort = ""
        let passwordRequiresNonAlphanumeric = ""
        let duplicateUserName = ""

        if (errorList["PasswordRequiresDigit"]) {
          passwordRequiresDigit = errorList["PasswordRequiresDigit"][0] + "\n"
        }

        if (errorList["PasswordRequiresLower"]) {
          passwordRequiresLower = errorList["PasswordRequiresLower"][0] + "\n"
        }

        if (errorList["PasswordRequiresUpper"]) {
          passwordRequiresUpper = errorList["PasswordRequiresUpper"][0] + "\n"
        }

        if (errorList["PasswordTooShort"]) {
          passwordTooShort = errorList["PasswordTooShort"][0] + "\n"
        }

        if (errorList["PasswordRequiresNonAlphanumeric"]) {
          passwordRequiresNonAlphanumeric = errorList["PasswordRequiresNonAlphanumeric"][0] + "\n"
        }
        
        if(errorList["DuplicateUserName"]){
          duplicateUserName = errorList["DuplicateUserName"][0]
        }

        const mensagensErro = `${passwordRequiresDigit} ${passwordRequiresLower} ${passwordRequiresUpper} ${passwordTooShort} ${passwordRequiresNonAlphanumeric} ${duplicateUserName}`
        return mensagensErro;
      });
  }
}
