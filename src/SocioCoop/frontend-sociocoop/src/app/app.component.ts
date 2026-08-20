import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { SocioService, Socio } from './services/socio.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  providers: [SocioService],
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  socios: Socio[] = [];

  constructor(private socioService: SocioService) { }

  ngOnInit(): void {
    this.cargarSocios();
  }

  cargarSocios(): void {
    this.socioService.getSocios().subscribe({
      next: (data) => this.socios = data,
      error: (err) => console.error('Error al conectar con la API', err)
    });
  }
}
