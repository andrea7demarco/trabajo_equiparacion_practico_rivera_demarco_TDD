# Limitaciones y críticas

A pesar de los beneficios atribuidos a TDD, su aplicación fue objeto de muchas críticas que cuestionan tanto su efectividad como su aplicabilidad a distintos contextos. La literatura incluso, muestra resultados variados, y en algunos casos, contradictorios.

Diversos autores señalan que sacarle provecho a las aparentes ventajas de TDD depende de factores conceptuales y contextuales que escapan del alcance de la metodología, tales como: la experiencia del equipo, la calidad de las pruebas, el nivel de disciplina en la aplicación de TDD, e incluso el tipo de sistema desarrollado. Ahondaremos más en dichos factores con el fin de comprender bajo qué condiciones estas ventajas se potencian o se ven limitadas.

Una de las críticas más recurrentes, se refiere al **incremento en el tiempo de desarrollo**, especialmente para las etapas iniciales. Diversos estudios empíricos reportan que equipos que adoptan TDD experimentan un aumento en el esfuerzo requerido para implementar funcionalidades, atribuible principalmente a la curva de aprendizaje y al tiempo invertido en la escritura de pruebas. No obstante, otros autores señalan que esta medición suele ser incompleta, ya que no se considera el tiempo ahorrado en etapas posteriores mediante la reducción de defectos y el retrabajo. En estudios donde se aclaró que se consideró el costo total del ciclo de vida, se tiende a mostrar efectos neutros o positivos tras la aplicación de la metodología.

Otra crítica muy resaltada se refiere a la **aplicabilidad de TDD en proyectos de código legado**. En muchas de las fuentes de información sobre TDD, se aplica en proyectos iniciados desde cero, y muchos señalan que resulta difícil de aplicar en sistemas preexistentes, especialmente si estos carecen de pruebas o tienen un mal diseño. No obstante, otras posturas señalan que TDD puede aplicarse de manera incremental en este tipo de proyectos, utilizándolo principalmente para desarrollar nuevas funcionalidades o modificar partes del sistema.

Por otro lado, se reporta que **las pruebas de sistemas que tienen fuerte dependencia a interfaces gráficas, bases de datos o servicios externos, son más difíciles de realizar**, las pruebas en estos casos suelen ser más lentas y frágiles. Frente a estas críticas, distintos autores proponen el uso sistemático de mocks o test doubles, separación explícita entre lógica de dominio e infraestructura, y combinar pruebas unitarias con algunas pruebas de integración, la evidencia empírica sostiene que, de este modo, se pueden mitigar significativamente estos problemas asociados.

Otras limitación que se comenta es sobre una  **pérdida de la visión global de la arquitectura por centrarse exclusivamente en unidades (UTDD)**, y para reforzar esta crítica, algunos estudios empíricos no encuentran mejoras significativas en métricas de diseño a nivel sistema. Otros autores dicen que puede reducirse este riesgo combinando TDD con prácticas de diseño arquitectónico explícito y pruebas de mayor nivel, sin abandonar el enfoque incremental propio de TDD.

La influencia de la **calidad de las pruebas** también es fuente de debate. Se observó que suites de pruebas mal diseñadas se vuelven difíciles de mantener, y más cuando están fuertemente acopladas a detalles de implementación. Pero este problema no es inherente a TDD, sino que es consecuencia de una aplicación deficiente de la práctica.

---

⬅️ [Anterior](04-ventajas.md) | [Siguiente](06-conclusiones-teoria.md) ➡️