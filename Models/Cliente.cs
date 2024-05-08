using System;
using System.Collections.Generic;

namespace registromercy_developers.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Correo { get; set; }

    public int? Numero { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    public virtual ICollection<Servicio> ServicioIdServicios { get; set; } = new List<Servicio>();
}
