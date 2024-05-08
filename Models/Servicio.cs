using System;
using System.Collections.Generic;

namespace registromercy_developers.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string? TipoServicio { get; set; }

    public int? CostoServicio { get; set; }

    public string? Descripcion { get; set; }

    public DateOnly? FechaServicio { get; set; }

    public virtual ICollection<Cliente> ClienteIdClientes { get; set; } = new List<Cliente>();
}
