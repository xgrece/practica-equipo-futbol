#Conexión a una base de datos a partir de un archivo.txt

Para establecer una conexión a una base de datos utilizando los parámetros almacenados en un archivo .txt, debes seguir estos pasos:

1. Crear el archivo .txt con los parámetros de conexión.
2. Leer el archivo .txt desde tu aplicación.
3. Parsear los datos del archivo para obtener los parámetros de conexión.
4. Utilizar los parámetros de conexión para establecer la conexión a la base de datos.

------------
### nota:
- Asegúrate de tener instalado el paquete MySql.Data en tu proyecto. Puedes instalarlo a través de NuGet con el siguiente comando:

> 
Install-Package MySql.Data
>

- El archivo dbconfig.txt debe estar en el directorio de ejecución de tu proyecto o debes proporcionar la ruta completa al archivo.
------------
 ### Ejemplo utilizando c# y una conexion a una base de datos MySQL. Supondremos que el archivo .txt contiene los parámetros de conexión en un formato clave-valor.

### **Paso 1: Crear el archivo .txt**
Crea un archivo **dbconfig.txt** con el siguiente contenido:
> 
Server=127.0.0.1
Database=equipos
User ID=root
Password=123
>

### **Paso 2: Leer el archivo .txt desde tu aplicación**
Utiliza C# para leer el archivo .txt.

### **Paso 3: Parsear los datos del archivo para obtener los parámetros de conexión**
Parsea el contenido del archivo y construye la cadena de conexión.

### **Paso 4: Utilizar los parámetros de conexión para establecer la conexión a la base de datos**
Establece la conexión a la base de datos utilizando los parámetros leídos.

## **Codigo:**
```
using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;

namespace DbConnectionFromTxt
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "dbconfig.txt"; // Ruta del archivo de configuración
            Dictionary<string, string> dbParams = ReadDbConfig(filePath); // Leer el archivo de configuración
            
            if (dbParams != null)
            {
                string connectionString = BuildConnectionString(dbParams); // Construir la cadena de conexión
                TestDbConnection(connectionString); // Probar la conexión a la base de datos
            }
            else
            {
                Console.WriteLine("Error al leer el archivo de configuración.");
            }
        }

        static Dictionary<string, string> ReadDbConfig(string filePath)
        {
            try
            {
                Dictionary<string, string> dbParams = new Dictionary<string, string>();
                foreach (var line in File.ReadLines(filePath))
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        dbParams[parts[0].Trim()] = parts[1].Trim();
                    }
                }
                return dbParams;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo: {ex.Message}");
                return null;
            }
        }

        static string BuildConnectionString(Dictionary<string, string> dbParams)
        {
            return $"Server={dbParams["Server"]};Database={dbParams["Database"]};User ID={dbParams["User ID"]};Password={dbParams["Password"]};";
        }

        static void TestDbConnection(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Conexión a la base de datos establecida correctamente.");
                    // Puedes ejecutar consultas aquí si es necesario
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
            }
        }
    }
}
```
## **Explicación del Código**
**1. Lectura del Archivo:**
- La función ReadDbConfig lee cada línea del archivo y parsea las claves y valores, almacenándolos en un diccionario.

**2. Construcción de la Cadena de Conexión:**
 - La función BuildConnectionString construye la cadena de conexión utilizando los valores del diccionario.

**3. Prueba de Conexión:**
- La función TestDbConnection establece una conexión a la base de datos utilizando la cadena de conexión generada y prueba si la conexión es exitosa.
