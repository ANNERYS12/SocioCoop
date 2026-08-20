# Documento de Requerimientos Base - SocioCoop

## 1. Idea del Proyecto
SocioCoop es un Sistema de Gestión de Aportes para Cooperativas diseñado para administrar el registro de socios y el control del flujo de sus aportaciones financieras.

## 2. Funcionalidades Principales
* Registro y consulta de socios con balance actualizado.
* Registro de aportes financieros ordinarios y con concepto personalizado.
* Arquitectura separada en capas con consumo distribuido desde Angular.

## 3. Estructura y POO
* **EntidadBase (Clase Abstracta):** Modelo base con atributos Id, FechaCreacion, Activo y método abstracto ObtenerResumen().
* **Socio y Aporte (Clases Normales):** Entidades dominantes con relaciones EF Core.
* **Sobrecargas y Constructores:** Implementados en las entidades de dominio y métodos de registro.