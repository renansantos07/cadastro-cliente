import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ContaService } from '../../service/conta/conta.service';
import { Usuario } from '../../models/usuario.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  constructor(public formBuilder: FormBuilder, private router: Router, private contaService: ContaService) {

  }

  mensagem: String = "";
  isCadastro: Boolean = false;
  formLogin: FormGroup = this.formBuilder.group(
    {
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(6)]],
    }
  );

  usuario: Usuario = {
    email: '',
    password: ''
  };

  get dadosLogin() {
    return this, this.formLogin.controls;
  }

  async Logar() {
    this.usuario.email = this.dadosLogin["email"].value;
    this.usuario.password = this.dadosLogin["senha"].value;

    let result = await this.contaService.login(this.usuario);

    if(result.sucesso){
      this.router.navigate(['']);
    }else{
      this.mensagem = result.mensagem ?? "";
    }
  }

  async Cadastrar() {
    this.usuario.email = this.dadosLogin["email"].value;
    this.usuario.password = this.dadosLogin["senha"].value;

    let result = await this.contaService.registrar(this.usuario);

    if(result){
      this.mensagem = result;
    }else{
      this.Logar();
    }
  }
}
