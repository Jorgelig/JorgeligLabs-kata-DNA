
# JorgeligLabs-kata-DNA

## Desafio: 
Proyecto que detecta si una persona tiene diferencias genéticas basándose en su secuencia de ADN.


## Ejemplo de Cadena sin mutación
| A | T | G | C | G | A |

| C | A | G | T | G | C |

| T | T | A | T | T | T |

| A | G | A | C | G | G |

| G | C | G | T | C | A |

| T | C | A | C | T | G |


## Ejemplo de cadena con mutación
| A | T | C | G | G | A |

| C | A | G | T | G | C |

| T | T | A | T | G | T |

| A | G | A | A | G | G |

| C | C | C | C | T | A |

| T | C | A | C | T | G |



## Requerimientos
- Visual Studio 2022 o superior
- .Net 6.0.x
- Docker Desktop


## Herramientas usadas
- Base de datos: MongoDB (local) y Azure Cosmos DB API for MongoDB (en la nube)
- Hosting: Azure App Services
- Proveedor de identidad: Auth0
- CI/CD: Azure DevOps
- Pruebas Unitarias: xUnit

## End points disponibles
**URL**     /mutation
**Method**  POST
**Request**
    ```json
    {"dna": ["string"]}
    ```

**Success Response** 200
**Forbiden Response** 403

**URL**     /stats
**Success Response** 200
    ```json
    {
        "count_mutations": "int32",
        "count_no_mutation": "int32",
        "ratio": "double"
    }
    ```



## Pasos para hacer una prueba en ambiente local
1. Clonar el repositorio
2. Abrir la solucion con Visual Studio 2022
3. Seleccionar docker-compose como ""startup proyect""
4. Abrir swagger: https://localhost:56274/swagger/index.html
5. Generar un token valido
    ```bash
        curl -X 'GET' \
            'https://localhost:56274/api/AuthHelper/get-access-token' \
             -H 'accept: */*'
    ```
6. Usar access_token generado en los Headers para los end point /mutation y /stats
7. ![image](https://user-images.githubusercontent.com/581672/139580127-5764cdbe-bd45-4701-b253-0e6e03d08814.png)


## Pasos para hacer una prueba en linea
1. Abrir swagger: https://jorgeliglabs-kata-dna.azurewebsites.net/swagger/index.html
2. Generar un token valido
    ```bash
        curl -X 'GET' \
            'https://jorgeliglabs-kata-dna.azurewebsites.net/api/AuthHelper/get-access-token' \
             -H 'accept: */*'
    ```
6. Usar access_token generado en los Headers para los end point /mutation y /stats


## Coverage
![image](https://user-images.githubusercontent.com/581672/139580084-4b3ebf80-913d-4c28-801e-8461d9796b21.png)
![image](https://user-images.githubusercontent.com/581672/139580091-9df30c58-341e-40f3-9682-cefe70f5e1de.png)


