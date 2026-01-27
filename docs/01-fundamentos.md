# Fundamentos de TDD

El desarrollo guiado por pruebas (Test-Driven Development, por sus siglas en inglés TDD) es una práctica iterativa e incremental de diseño de software basada en ciclos cortos de: (1) escribir un test que falla, (2) implementar lo mínimo para que apruebe dicho caso de prueba, y (3) refactorización continua del código implementado.

Esta metodología propone tres sub-prácticas fundamentales: **desarrollo de casos de prueba** donde se producen bloques de testing ejecutables que indican fail/pass, **Test-First** que implica escribir los casos de prueba antes que el código mismo, y la **refactorización continua**, la cual se aplica posterior a pasar los casos de prueba para mejorar el diseño sin alterar el comportamiento esperado del sistema.
El funcionamiento de TDD se estructura en el conocido ciclo red-green-refactor:

* **RED - Escribir la especificación del requisito (el ejemplo, el test)**. Una vez que tenemos claro cuál es el requisito, lo expresamos en forma de una prueba que falle. Un test no es inicialmente un test, sino un ejemplo o especificación que indica que se espera que haga el sistema.
* **GREEN - Implementar el código según dicho ejemplo**. A partir del caso de prueba del punto anterior, codificamos lo mínimo y necesario para que el test pase. No importa que el código parezca feo ya que eso lo vamos a enmendar en el siguiente paso. Hay que estar concentrados para ser eficientes y evitar deliberadamente el sobrediseño. 
* **REFACTOR - Refactorizar para eliminar duplicidad y hacer mejoras**. Refactorizar es modificar el diseño sin alterar su comportamiento. En este paso rastreamos el código (también el del test) en busca de líneas duplicadas, acoplamiento, baja cohesión, estructuras inapropiadas, y refactorizamos, haciendo el código más claro y fácil de mantener. De este modo, nos aseguramos que el código cumpla con ciertos principios de diseño del software mientras que verificamos que su comportamiento siga siendo el mismo gracias a nuestra suite de pruebas. 

La clave es hacerlo en pasos pequeños, se hace un cambio, se ejecutan todos los tests y, si todo sigue funcionando, se hace otro pequeño cambio, por esto muchos autores mencionan que TDD es una forma de diseño incremental extrema.

Miguel Carlos Fontela, en su trabajo de especialización sobre Test-Driven Development, nos adjunta un diagrama de actividades que muestra el ciclo de TDD:

![Diagrama de actividades del ciclo red-green-factor de TDD](/docs/images/diagrama-ciclo-tdd.png)

Habrán notado que al inicio de esta sección, definimos TDD como una **metodología de diseño de software** y no de pruebas, pues el enfoque principal de TDD no es proveer una suite de casos de prueba, esto es más un efecto colateral positivo de su aplicación. Desde esta perspectiva, las pruebas actúan como un mecanismo de diseño al forzar a los desarrolladores a escribirlas antes del código, e incluso, el código deriva de las mismas pruebas. De este modo, TDD impulsa la creación de unidades pequeñas altamente cohesivas y desacopladas, ya que el código difícil de probar suele indicar problemas de diseño. 

Esta característica es la que posiciona a TDD como una técnica de diseño, donde la estructura del sistema evoluciona de manera incremental a partir de ejemplos de uso expresados en las pruebas.

---

⬅️ [Volver al índice](../README.md) | [Siguiente](02-objetivos.md) ➡️
