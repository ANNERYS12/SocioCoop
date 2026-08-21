import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Socio {
  id?: number;
  nombre: string;
  cedula: string;
  balanceAportes: number;
}

@Injectable({
  providedIn: 'root'
})
export class SocioService {
  private apiUrl = 'https://localhost:7195/api/socios';

  constructor(private http: HttpClient) { }

  getSocios(): Observable<Socio[]> {
    return this.http.get<Socio[]>(this.apiUrl);
  }

  crearSocio(socio: { nombre: string; cedula: string; aporteInicial: number }): Observable<Socio> {
    return this.http.post<Socio>(this.apiUrl, socio);
  }

  eliminarSocio(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  agregarAporte(socioId: number, monto: number, concepto: string): Observable<any> {
    return this.http.post('https://localhost:7195/api/aportes', { socioId, monto, concepto });
  }
} 
