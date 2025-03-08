CONEXION SQL

Esta aplicación es una herramienta de verificación de salud para instancias de SQL Server. La interfaz de usuario permite al usuario seleccionar diferentes opciones de verificación a través de una lista de verificación . Al hacer clic en el botón, la aplicación se conecta a la instancia de SQL Server especificada y ejecuta diferentes consultas SQL basadas en las opciones seleccionadas. Los resultados de estas consultas se guardan en archivos CSV.

Características principales:
Conexión a SQL Server: Utiliza SqlConnection para conectarse a la base de datos.
Ejecución de consultas SQL: Ejecuta diferentes consultas SQL para obtener información sobre la instancia de SQL Server.
Exportación a CSV: Guarda los resultados de las consultas en archivos CSV.
Manejo de excepciones: Muestra mensajes de error en caso de fallos en la conexión o ejecución de las consultas.

Archivos generados:
propiedadesInstancia.csv
propiedadesServidor.csv
servicesInfo.csv
contadores.csv
infoHardware.csv
propiedadesBD.csv
autoCrecimiento.csv
UsoCPUBD.csv
UsoDiscoBD.csv
estadisticasBD.csv
waits.csv
sysadmin.csv
Tecnologías utilizadas:
C#
Windows Forms
SQL Server
ADO.NET para la conexión y ejecución de consultas SQL
StreamWriter para la exportación de datos a CSV
