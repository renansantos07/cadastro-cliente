import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Cliente } from '../../models/cliente.model';
import { environment } from '../../../environment';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  constructor(private http: HttpClient) { }

  async ListarTodos(): Promise<Cliente[]> {
    const headers = { 'Authorization': `Bearer ${window.localStorage.getItem("token")}` }

    return await firstValueFrom(this.http.get<any>(`${environment.endPoint}/Cliente/ObterTodos`, { headers }))
      .then((result) => {
        console.log(result);
        return result.data;
      }).catch((error) => {
        console.log(error);
        return error;
      });
  }

  async Obter(Id: String): Promise<Cliente> {
    const headers = { 'Authorization': `Bearer ${window.localStorage.getItem("token")}` }

    return await firstValueFrom(this.http.get<any>(`${environment.endPoint}/Cliente/Obter?Id=${Id}`, { headers }))
      .then((result) => {
        return result.data;
      }).catch((error) => {
        return error;
      });
  }

  async ListarQuery(cliente: Cliente): Promise<Cliente[]> {
    const headers = { 'Authorization': `Bearer ${window.localStorage.getItem("token")}` }

    return await firstValueFrom(this.http.post<any>(`${environment.endPoint}/Cliente/Query`, cliente, { headers }))
      .then((result) => {
        return result.data;
      }).catch((error) => {
        return error;
      });
  }

  async atualizar(Id: String, cliente: Cliente) {
    const headers = { 'Authorization': `Bearer ${window.localStorage.getItem("token")}` }

    return await firstValueFrom(this.http.put<any>(`${environment.endPoint}/Cliente/AtualizarCliente?Id=${Id}`, cliente, { headers }))
      .then((result) => {
        return {
          sucesso: true,
          data: result.data,
          mensagem: ""
        }
      }).catch((error) => {
        return {
          sucesso: false,
          mensagem: error.error.errors
        }
      });
  }

  async registrar(cliente: Cliente) {
    const headers = { 'Authorization': `Bearer ${window.localStorage.getItem("token")}` }

    return await firstValueFrom(this.http.post<any>(`${environment.endPoint}/Cliente/CriarCliente`, cliente, { headers }))
      .then((result) => {
        return {
          sucesso: true,
          data: result.data,
          mensagem: ""
        }
      }).catch((error) => {
        return {
          sucesso: false,
          mensagem: error.error.errors
        }
      });
  }

  async remover(Id: String){
    const headers = { 'Authorization': `Bearer ${window.localStorage.getItem("token")}` }

    return await firstValueFrom(this.http.delete<any>(`${environment.endPoint}/Cliente/DeletarCliente?Id=${Id}`, { headers }))
      .then((result) => {
        return result.success;
      }).catch((error) => {
        return error;
      });
  }
}
