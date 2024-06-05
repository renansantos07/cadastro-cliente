import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { HomeComponent } from './pages/home/home.component';
import { authGuard } from './guards/auth.guard';
import { ListaClienteComponent } from './pages/lista-cliente/lista-cliente.component';
import { CadastrarClienteComponent } from './pages/cadastrar-cliente/cadastrar-cliente.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [authGuard],
    children: [
      { path: '', component: ListaClienteComponent },
      { path: 'cadastro', component: CadastrarClienteComponent },
      { path: 'atualizar/:id', component: CadastrarClienteComponent }
    ]
  },
  { path: '', pathMatch: 'full', redirectTo: "login" },
  { path: 'login', component: LoginComponent },
  { path: '', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
