# Casos de uso

En base a la bibliografía leída, decidimos redactar una serie de casos de uso que definen claramente la lógica de negocio y las operaciones a implementar. Al final, nos quedaron los siguientes:

## Caso de uso: Agendar una cita

- **Código:** CU-01
- **Descripción:** El usuario selecciona un horario y día para agendar una cita.

### Flujo principal

1. El usuario selecciona una fecha y hora.
2. El usuario ingresa sus datos
3. El sistema valida los datos ingresados por el usuario
4. El sistema valida la fecha y hora seleccionada para la ciita
5. El sistema registra la cita
6. El sistema informa al usuario que se realizó con éxito la reserva de su cita

### Poscondición

El turno queda registrado con el estado "Pendiente", y el horario de esa fecha seleccionada deja de estar dsponible para otros usuarios.

### Excepciones

4. i. Si la fecha u hora seleccionadas están ocupadas, el sistema no registra la cita y le informa al usuario

---

## Caso de uso: Reagendar una cita

- **Código:** CU-02
- **Descripción:** El usuario puede modificar la fecha y/u hora de la cita seleccionada

### Precondición

i. Deben quedar más de 8 horas para la realización la cita
ii. La cita debe continuar en estado de "Pendiente"

### Flujo principal

1. El usuario selecciona la cita que desea reagendar
2. El usuario modifica la fecha u hora de la cita
3. El usuario confirma la nueva fecha y hora
4. El sistema valida la nueva fecha y hora seleccionada
5. El sistema informa al usuario que se reagendó la cita

### Poscondición

La cita es reagendada y permanece con estado de "Pendiente"

### Excepciones

4. i. Si la nueva fecha y/u hora seleccionada están ocupadas, el sistema informa el error al usuario y solicita reingresar la fecha.
4. ii. Si el usuario intenta reagendar una cita para la que quedan menos de 8 horas, el sistema debe informarle que ya no puede reagendar

---

## Caso de uso: Cancelar una cita

- **Código:** CU-03
- **Descripción:** Se cancela la cita de un usuario

### Precondición

i. La cita no debe haber sido confirmada
ii. Deben quedar más de 2 horas para la realización de la cita

### Flujo principal

1. El usuario selecciona una cita
2. El usuario confirma que desea cancelar la cita
3. El sistema cancela la cita
4. El sistema informa al usuario que su cita ha sido cancelada

### Poscondición

i. El estado de la cita se actualiza a "Cancelada"
ii. El horario de la cita se libera para poder agendarse nuevamente

### Excepciones

1. i. Si la cita ya está confirmada, el sistema debe informar al usuario que ya no puede cancelarla
2. i. Si quedan menos de dos horas para la cita, el sistema debe informar al usuario que ya no puede cancelarla

---

## Caso de uso: Consultar citas agendadas

- **Código:** CU-04
- **Descripción:** El usuario visualiza todas sus citas agendadas que se encuentren pendientes, confirmadas o canceladas, pudiendo visualizar la fecha y hora de cada una

### Precondición

El usuario debe haber iniciado sesión en la plataforma

### Flujo principal

1. Un usuario solicita ver sus citas
2. El sistema realiza la búsqueda de sus citas mediante su DNI
3. El sistema muestra al usuario un listado con sus turnos

### Poscondición

El usuario visualiza la información de sus citas, pudiendo realizar operaciones sobre ellas como cancelarlas o reagendarlas

### Excepciones

2. i. Si el sistema no encuentra citas anteriores del usuario, se lo informa con un mensaje

---

## Caso de uso: Confirmar una cita

- **Código:** CU-05
- **Descripción:** El usuario confirma su asistencia a una cita

### Precondición

i. El usuario debe haber iniciado sesión en la plataforma
ii. La cita debe tener el estado de "Pendiente"

### Flujo principal

1. El usuario solicita confirmar una cita
2. El sistema valida que la cita tenga el estado de "Pendiente"
3. El sistema confirma la cita

### Poscondición

La cita debe modificar su estado a "Confirmada" en la agenda

### Excepciones

2. i. Si la cita está confirmada o cancelada, el sistema informa al usuario por pantalla

---

⬅️ [Volver a la introducción](01-introduccion.md) | [Siguiente](03-iteraciones-tdd.md) ➡️
