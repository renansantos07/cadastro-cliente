import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClienteService } from '../../service/cliente/cliente.service';
import { Cliente } from '../../models/cliente.model';
import swal from 'sweetalert2'

@Component({
  selector: 'app-cadastrar-cliente',
  templateUrl: './cadastrar-cliente.component.html',
  styleUrl: './cadastrar-cliente.component.css'
})
export class CadastrarClienteComponent implements OnInit, OnDestroy {

  constructor(public formBuilder: FormBuilder, private router: Router, private clienteService: ClienteService, private route: ActivatedRoute) { }

  cadastroCLiente: FormGroup = this.formBuilder.group(
    {
      razaoSocial: ['', [Validators.required]],
      nomeFantasia: ['', [Validators.required]],
      documento: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(14)]],
    }
  );

  cliente: Cliente = {
    razaoSocial: '',
    nomeFantasia: '',
    documento: ''
  }
  mensagem: String = "";
  id: String = "";
  atualizar: boolean = false;
  private sub: any;

  async ngOnInit(): Promise<void> {
    this.atualizar = false;
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id'];
      this.atualizar = params['id'];
    });

    if (this.atualizar) {
      this.cliente = await this.clienteService.Obter(this.id)
    }
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  get dadosCadastro() {
    return this, this.cadastroCLiente.controls;
  }

  async Cadastrar() {
    this.cliente.razaoSocial = this.dadosCadastro["razaoSocial"].value;
    this.cliente.nomeFantasia = this.dadosCadastro["nomeFantasia"].value;
    this.cliente.documento = this.dadosCadastro["documento"].value;

    let result = await this.clienteService.registrar(this.cliente);

    if (result.sucesso) {
      this.showDialogConfirmacao(`Cliente ${this.cliente.nomeFantasia} Cadastrado com Sucesso`)
    } else {
      this.mensagem = result.mensagem.toString();
    }
  }

  async Atualizar() {
    this.cliente.razaoSocial = this.dadosCadastro["razaoSocial"].value;
    this.cliente.nomeFantasia = this.dadosCadastro["nomeFantasia"].value;
    this.cliente.documento = this.dadosCadastro["documento"].value;

    let result = await this.clienteService.atualizar(this.id, this.cliente);

    if (result.sucesso) {
      this.showDialogConfirmacao(`Cliente ${this.cliente.nomeFantasia} Atualizado com Sucesso`)
    } else {
      this.mensagem = result.mensagem.toString();
    }
  }

  showDialogConfirmacao(mensagem: any) {
    swal.fire({
      title: "Tudo certo!",
      text: mensagem,
      icon: "success",
      confirmButtonText: "Ok",
    }).then((result) => {
      if (result.isConfirmed) {
        this.router.navigate(['']);
      }
    });
  }
}
