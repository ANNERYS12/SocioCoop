import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SocioService, Socio } from './services/socio.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrls: []
})
export class AppComponent implements OnInit {
  socios: Socio[] = [];
  pestanaActiva: string = 'padron';
  filtroBusqueda: string = '';
  socioSeleccionado: Socio | null = null;
  montoAporte: number = 0;
  conceptoAporte: string = 'Aporte Ordinario';

  nuevoSocio: { nombre: string; cedula: string; aporteInicial: number } = {
    nombre: '',
    cedula: '',
    aporteInicial: 0
  };

  constructor(private socioService: SocioService) { }

  ngOnInit(): void {
    this.cargarSocios();
  }

  cargarSocios(): void {
    this.socioService.getSocios().subscribe({
      next: (data) => (this.socios = data),
      error: (err) => console.error('Error al cargar socios:', err)
    });
  }

  cambiarPestana(pestana: string): void {
    this.pestanaActiva = pestana;
  }

  get sociosFiltrados(): Socio[] {
    if (!this.filtroBusqueda.trim()) return this.socios;
    return this.socios.filter(s =>
      s.nombre.toLowerCase().includes(this.filtroBusqueda.toLowerCase()) ||
      s.cedula.includes(this.filtroBusqueda)
    );
  }

  get totalAportes(): number {
    return this.socios.reduce((acc, s) => acc + Number(s.balanceAportes), 0);
  }

  guardarSocio(): void {
    if (!this.nuevoSocio.nombre || !this.nuevoSocio.cedula) {
      alert('Por favor complete los campos requeridos.');
      return;
    }

    this.socioService.crearSocio(this.nuevoSocio).subscribe({
      next: (socioCreado) => {
        this.socios.push(socioCreado);
        alert('¡Socio registrado exitosamente!');
        this.nuevoSocio = { nombre: '', cedula: '', aporteInicial: 0 };
        this.pestanaActiva = 'padron';
        this.cargarSocios();
      },
      error: (err) => {
        console.error('Error al guardar socio:', err);
        alert('Error al registrar el socio.');
      }
    });
  }

  eliminarSocio(id: number): void {
    if (!confirm('¿Estás seguro de eliminar este socio?')) return;

    this.socioService.eliminarSocio(id).subscribe({
      next: () => {
        alert('Socio eliminado exitosamente.');
        this.cargarSocios();
      },
      error: (err) => {
        console.error('Error al eliminar socio:', err);
        alert('Error al eliminar el socio.');
      }
    });
  }

  abrirFormularioAporte(socio: Socio): void {
    this.socioSeleccionado = socio;
    this.montoAporte = 0;
    this.conceptoAporte = 'Aporte Ordinario';
  }

  cancelarAporte(): void {
    this.socioSeleccionado = null;
  }

  guardarAporte(): void {
    if (!this.socioSeleccionado || this.montoAporte <= 0) {
      alert('Ingrese un monto válido.');
      return;
    }

    this.socioService.agregarAporte(this.socioSeleccionado.id!, this.montoAporte, this.conceptoAporte).subscribe({
      next: () => {
        alert('Aporte registrado exitosamente.');
        this.socioSeleccionado = null;
        this.cargarSocios();
      },
      error: (err) => {
        console.error('Error al registrar aporte:', err);
        alert('Error al registrar el aporte.');
      }
    });
  } 
}
