# Validaciones de Usuario

Cuando queremos que la información de un form se pase al backend tenemos que estar seguros de que todos los campos de captura así como el submit estén dentro del mismo elemento `form`.

Los `form` envían datos en GET y POST, pero que son diferentes a los de un API. En GET se envían los datos a través del URL como parámetros, mientras con post se envían como parte de, por ejemplo `asp-for` y un `[BindProperty]`. Esto se pone como `<form method="post"></form>

Recordar que `[BindProperty]` significa ligar eternamente un elemento del backend con un elemento visual del frontend, así que cualquiera que cambie, siempre estarán sincronizados entre ambos. Esto se hace para cada variable que creemos. También se podría hacer el binding con un objeto de una clase. Esto generalmente solo se hace para las variables que queremos que el usuario también pueda editar directamente desde el frontend, como una captura del usuario.

Recordar nuevamente que se debe colocar en el frontend al elemento que queremos ligar la propiedad `asp-for="[nombre de propiedad dentro de backend]"` dentro de su tag.

Un objeto de los que no son básicos, de una clase, siempre pueden ser nulos, pero los tipos de dato básicos en teoría nada más debería de tener los valores que permite, como en int nada más -, +, ó 0. Pero al agregar en la definición `int?` podemos asignar nulo al int.

Una validación se puede agregar como:

```c#
[BindProperty]
// If the value is null, throw error
[Required(ErrorMessage = "Edad es requrido")]
// If the value is between 18 and 110 then the input is correct, else throw error
[Range(18,110,ErrorMessage = "Eres menor de edad, no puedes votar")]
public int? Edad { get; set; }
```

Cualquier cosa que truene en las validaciones se muestra en el frontend a través de:

```c#
<span asp-validation-for="Edad" class="text-danger"></span>
```

Siempre debemos hacer validaciones en el backend porque todo en el frontend es editable por el usuario y se podrían saltar las validaciones a propósito o algo.
