
## Tablas

### Companies:
Propósito: Registrar y almacenar las empresas que operan los barcos.

Columnas:
- `id (primary key)`: Identificador de la empresa.
- `name`: Nombre de la empresa.

Query:
```sql
CREATE TABLE companies (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);
```

### Vessels:
Propósito: Registrar los buques y asociarlos a una empresa

Columnas:
- `id (primary key)`: Identificador del barco.
- `name`: Nombre del buque.
-  `company_id (foreign key)`: relacion con la tabla `companies` para identificar el barco.

Query:
```sql
CREATE TABLE vessels (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    company_id INT NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (company_id) REFERENCES companies(id) ON DELETE CASCADE
);
```

### Operations:
Propósito: Registrar todas las operaciones asociadas a un barco en especifico, con tiempos y detalles de carga.

Columnas:
- `id (primary key)`: Identificador de la operacion.
- `vessel_id (foreign key)`:  Relacion con la tabla `vessels` para identificar el barco.
- `loa`: Longitud del barco.
- `operation_time`: Duracion estimada de la operacion.
- `eta, pob, etb, etc, etd`: Tiempos de llegada, operacion y salida.
- `cargo`: Detalle de carga.

Query:
```sql
CREATE TABLE operations (
    id INT PRIMARY KEY IDENTITY(1,1),
    vessel_id INT NOT NULL,
    loa DECIMAL(10, 2) NOT NULL,
    operation_time TIME NOT NULL,
    eta DATETIME NOT NULL,
    pob DATETIME NULL,
    etb DATETIME NULL,
    etc DATETIME NULL,
    etd DATETIME NULL,
    cargo NVARCHAR(255),
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (vessel_id) REFERENCES vessels(id) ON DELETE CASCADE
);
```

## Relaciones entre tablas

**Companies (1) --- (N) vessels**: Una empresa puede tener varios barcos, pero cada operación pertenece a una única empresa.

**Vessels (1) --- (N) operations**: Un barco puede tener multiples operaciones, pero cada operación esta asociada a un barco.

## Triggers

**Automatizar las fechas de `POB`, `ETB`, `ETC` y `ETD`**:
```sql
CREATE TRIGGER trg_update_operation_times
ON operations
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE operations
    SET 
        pob = DATEADD(MINUTE, DATEDIFF(MINUTE, '00:00', operation_time), eta),  
        -- POB = ETA + operation_time
        etb = DATEADD(HOUR, 1, pob),                                           
        -- ETB = POB + 1 hour
        etc = DATEADD(MINUTE, DATEDIFF(MINUTE, '00:00', operation_time), etb),  
        -- ETC = ETB + operation_time
        etd = DATEADD(HOUR, 1, etc)                                           
        -- ETD = ETC + 1 hour
    FROM inserted
    WHERE operations.id = inserted.id;
END;
GO
```

Este trigger realiza lo siguiente:
- Calcula `pob` sumando `operation_time` a `eta`.
- Calcula `etb` sumando 1 hora a `pob`.
- Calcula `etc` sumando `operation_time` a `etb`.
- Calcula `etd` sumando 1 hora a `etc`.


## Funciones

**Auditoria para las fechas `created_at` y `updadet_at`**:
Para actualizar automáticamente la columna `updated_at` cada vez que se realiza un cambio en una fila, ejemplo en tabla `operations`.
```sql
CREATE TRIGGER trg_update_timestamp_operations
ON operations
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE operations
	SET update_at = GETDATE()
	FROM inserted
	WHERE operations.id = inserted.id;
END;
GO
```


## Indices

Para mejorar el rendimiento de las consultas en las columnas usadas frecuentemente como filtros (`vessels_id`, `eta` y `company_id`).
```sql
CREATE INDEX idx_operations_vessel_id ON operations (vessel_id);
CREATE INDEX idx_operations_eta ON operations (eta);
CREATE INDEX idx_vessels_company_id ON vessels (company_id);
```


## Insertar datos

**En `companies`**:
```sql
INSERT INTO companies (name) VALUES ('Shipping Corp'), ('Global Maritime');
```

**En `vessels`**:
```sql
INSERT INTO vessels (name, company_id) VALUES 
('Sea Hawk', 1),
('Ocean Amitie', 1),
('Inlaco Harmony', 2);
```

**En `operations`**:
```sql
INSERT INTO operations (vessel_id, loa, operation_time, eta, cargo) VALUES 
(1, 181.64, '17:00', '2024-05-15 06:00', 'DISCH. 557 STEEL COILS | 2,714.838 MTS'),
(2, 180.00, '48:00', '2024-05-15 19:00', 'DISCH. 5.885 MT ALUMINIUM INGOTS'),
(3, 176.83, '14:00', '2024-05-16 08:00', 'DISCH. 302 COILS | 2,132 MTS');
```


## Consultas útiles

**Obtener todas las operaciones de un barco en especifico**:
```sql
SELECT o.*, v.name AS vessel_name, c.name AS company_name
FROM operations o
JOIN vessels v ON o.vessel_id = v.id
JOIN companies c ON v.company_id = c.id
WHERE v.name = 'Sea Hawk';
```

**Obtener todas las operaciones programadas en un rango de fechas**:
```sql
SELECT o.*, v.name AS vessel_name, c.name AS company_name
FROM operations o
JOIN vessels v ON o.vessel_id = v.id
JOIN companies c ON v.company_id = c.id
WHERE o.eta BETWEEN '2024-05-15' AND '2024-05-20';
```


## Pendientes

**Historial de operaciones**: Crear una tabla `historical_operations` o agregar un campo `is_archived` en `operations` para marcar las operaciones que ya finalizaron y necesitan archivarse.

**Automatizar fechas y tiempos**: Implementar triggers o lógica en el backend para actualizar `pob`, `etb`, `etc`, y `etd` automáticamente cuando se agreguen o cambien `operation_time` y `eta`, similar al frontend.

**Hostear base de datos en PC local**: La base de datos sera hosteada en una PC local para que esta se pueda usar desde cualquier equipo con una dirección IP única, sin la necesidad utilizar backups.