import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  constructor(public formBuilder: FormBuilder, private router: Router) {

  }

  mensagem: String = "";
  isCadastro: Boolean = false;
  formLogin: FormGroup = this.formBuilder.group(
    {
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required]],
    }
  );

  get dadosLogin() {
    return this, this.formLogin.controls;
  }

  Logar(){
    alert("Logar");
  }

  Cadastrar(){
    alert("Cadastrar")
  }

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

}
