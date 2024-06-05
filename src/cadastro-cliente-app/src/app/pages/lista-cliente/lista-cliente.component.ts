import { Component, OnInit } from '@angular/core';
import { Cliente } from '../../models/cliente.model';
import { ClienteService } from '../../service/cliente/cliente.service';
import swal from 'sweetalert2'

@Component({
  selector: 'app-lista-cliente',
  templateUrl: './lista-cliente.component.html',
  styleUrl: './lista-cliente.component.css'
})
export class ListaClienteComponent implements OnInit {

  clientes: Cliente[] = [];

  clienteQuery: Cliente = {
    razaoSocial: '',
    nomeFantasia: '',
    documento: ''
  }

  constructor(private clienteService: ClienteService) { }

  async ngOnInit(): Promise<void> {
    this.clientes = await this.clienteService.ListarTodos();
  }

  async filtrar() {
    if (!this.clienteQuery.nomeFantasia && !this.clienteQuery.razaoSocial && !this.clienteQuery.documento) {
      this.clientes = await this.clienteService.ListarTodos();
    } else {
      this.clientes = await this.clienteService.ListarQuery(this.clienteQuery);
    }
  }

  async showDialogDelete(id: any, nomeFantasia: any) {
    swal.fire({
      title: "Você tem certeza?",
      text: `Vamos remover o cliente ${nomeFantasia}, podemos continuar?`,
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Sim!",
      cancelButtonText: "Não"
    }).then(async (result) => {
      if (result.isConfirmed) {
        let result = await this.clienteService.remover(id)
        if (result) {
          swal.fire({
            title: "Removido!",
            text: "O cliente foi removido com sucesso.",
            icon: "success"
          }).then((result) => {
            if (result.isConfirmed) {
              this.filtrar()
            }
          });
        }
      }
    });
  }
}
