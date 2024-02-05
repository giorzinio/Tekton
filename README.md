# Tekton
## Descripción
Este proyecto se realizo con fines profesionales, para rendir una prueba de conocimiento. A continuación se va detallar la Arquitectura, patrones y los pasos para levantar el proyecto localmente.
## Arquitectura
Para realizar este proyecto, se implemento en base a Clean Architecture, con una base de datos en SQL SERVER, utilizando a Entity Framework como conector de la capa de persistencia de datos.
![Clean Architecture](https://matthewrenze.com/wp-content/uploads/2019/04/Clean-Architecture.png)

## Patrones de diseño
### Pátron Health Check
Nos permitira en el proyecto determinar el estado general y disponibilidad de nuestra aplicación. Health Check proporciona tres niveles:
- Healthy(Saludable)
- Degraded(Degradado)
- Unhealthy(En mal estado)

Para poder revisar el estado lo realizaremos ingresando a la siguiente url [/healthchecks-ui](https://localhost:7008/healthchecks-ui#/healthchecks).

### Pátron Repository
Lo usaremos en este proyecto para crear una capa de abstracción entre la capa de acceso a datos y la capa logica. Esto nos va permitir acceder a diferentes fuentes de datos.
Tambien ya que hemos aislado las fuentes de datos, nos facilitará realizar pruebas unitarias o en pruebas (TDD).

Esta implementación lo visualizaran en la capa de Infraestructura.

### Pátron Unit of Work
Para el proyecto su objetivo será tratar como una Unidad todos aquellos objetos nuevos, modificados o eliminados con el propósito de centralizar las operaciones contra las fuentes de datos.
Con este patrón será muy ultil a la hora de persistir un conjuntos de acciones en la base de datos, evitando el exceso de conexiones.

![Representación del pátron repositorio y unit of work.](https://miro.medium.com/v2/resize:fit:1400/1*XnEPuZmUZRVJxKhFr7Avig.png)

### Pátron Rate Limiting
El diseño de este patrón nos ayudara a resolver el uso excesivo del API, mejorando el performance, la fiabilidad y confiabilidad, ataques de servicios y continuidad. Los algoritmos usados por Rate Limiting son los siguientes:

- Fixed Window: Nos permitira aplicaar limites como "60 solicitudes"
- Sliding Window: Cuantas ejecuciones permitidas por segundo
- Token Bucket: En 60 solicitudes cada minuto si lo realiza en 10 segundos, debera esperar 1 minuto antes de que se le permitan mas solicitudes
- Concurrency: Permitira cuantas solicitudes simultaneas se pueden realizar

Para nuestro proyecto implementaremos la politica Fixed Window, la cual como limite de peticiones seran de 20, la espera despues del limite sera de 30 segundos y como ultimo paso estableceremos el proceso en cola en 2 en nuestra capa de servicios.

### Pátron CQRS
El propósito de utilizar este pátron sera de separar las operaciones de lectura y escritura del repositorio de datos. Para esto vamos a dividir 2 diferentes modelos:

- Commands: Donde hacen algo, deben modificar algo y no debería devolver un valor idealmente
- Queries: Por lo general lo haremos para responder una pregunta, no modificaran  el estado del modelo y siempre devolveran un valor.

![Pátron CQRS](https://learn.microsoft.com/es-es/dotnet/architecture/cloud-native/media/cqrs-implementation.png)

### Pátron MediatR
Servira en el proyecto para la comunicación entre componentes, este pátrton nos proporciona un canal de comunicación centralizado.

# Middleware 
Lo usaremos para la canalización de solicitudes HTTP. Lo ejecutaremos en cada solicitud para el manejo de las excepciones que se pueden dar dentro de la aplicación.

# Swagger RecDoc
Este framework se usara para la documentación de los endpoint del API, ingresando a la sigiente ruta [/healthchecks-ui](https://localhost:7008/api-docs).


