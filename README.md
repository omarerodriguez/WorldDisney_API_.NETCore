# MundoDeDisney
<img src="https://upload.wikimedia.org/wikipedia/commons/3/3e/Disney%2B_logo.svg" alt="disney logo" width="550">
Aqui puedes conocer el mundo de disney, realizar crud en sus personajes, series o peliculas y el genero al que pertenece!!


 # JWT <img src="https://miro.medium.com/max/720/1*8_H6bhApuPNI7QQEbDByzg.png" alt="jwt logo" width="200">
 Implementacion de JWT para la autorizacion del usuario al registrarse y hacer login
 
  # Framework(.NetCore, Swagger)<img src="https://img.stackshare.io/service/11331/asp.net-core.png" alt="netcore logo" width="34"><img src="https://upload.wikimedia.org/wikipedia/commons/a/ab/Swagger-logo.png" alt="swagger logo" width="32">
 
 se uso este Framework Netcore para obtener mayor funcionabilidad, ayudandonos con la creacion de controladores, modelos y servicios.
 implementamos Swagger para exponer la documentacion de nuestra API e implemetar la autorizacion por JWT
 
 # Idioma(C#) <img src="https://user-images.githubusercontent.com/109057897/180828183-a0f1cd76-a690-4f14-9247-bd78df1b73e0.png" alt="c# logo" width="40">
 Se uso lenguaje C# implementando conocimiento basicos y avanzados.
 
 # Tester (Postman) <img src="https://cdn.worldvectorlogo.com/logos/postman.svg" alt="netcore logo" width="34">
 se utilizo postman para verificar el funcionanmiento optimo de la web api, Agregandole el Register y Login de usuario, tambien aplicando el metodo CRUD = create,Read,Update,Delete.
 
 # Base de datos(MySqlServer) <img src="https://blog.artegrafico.net/wp-content/uploads/2019/10/mysql-logo.png" alt="netcore logo" width="46">
 Se uso SqlServer para guardar y/o almacenar los datos de esta Web Api.
 
 
  # Para realizar el test
 - las siguientes rutas solo son para usuarios con role administrador (admin)
 
 admin=
 
 Characters[
 -post
 -put
 -delete]
 
 Movie[
 -post
 -put
 -delete]
 
 **1)** Para realizar el register puede crear cualquier usuario, escogiendo entre los Roles: user o admin (sin mayusculas)
 
 **NOTA**
 los usuarios creados se guardaran en memoria, por tal motivo si cierra el proyecto tendria que crear otro usuario, pero si no lo requiere puede usar el token ya generado para volverse autorizar sin problemas (el token tiene vigencia de 1 dia).
 
 **Query & Endpoints**
 -Characters:
 
 **LISTAR TODOS**:
 https://localhost:7014/api/Character
 
 **LISTAR POR ID**:
 https://localhost:7014/api/Character/id?id=1
 
 **LISTAR POR EDAD**:
 https://localhost:7014/api/Character/age?age=16
 
 **LISTAR POR NOMBRE**:
 https://localhost:7014/api/Character/name?name=goofy
 
 **LISTAR POR PESO**:
 https://localhost:7014/api/Character/weight?weight=45
 
 
 -Movie:
 
 **LISTAR TODAS**:
 https://localhost:7014/api/Movie/movies
 
 **LISTAR POR ID**:
 https://localhost:7014/api/Movie/id?id=1
 
 **LISTAR POR NOMBRE**:
 https://localhost:7014/api/Movie/Tittle?title=mulan
 
 **LISTAR POR ID GENERO**:
 https://localhost:7014/api/Movie/idGender?idGender=1
 
 **LISTAR ORDEN ASC**:
 https://localhost:7014/api/Movie/OrderA?ASC=asc
 
 **LISTAR ORDEN DESC**:
 https://localhost:7014/api/Movie/OrderD?DESC=desc
 
 
 
