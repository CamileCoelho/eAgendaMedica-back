﻿namespace eAgendaMedica.Dominio.Compartilhado
{
    public interface ITenantProvider
    {
        Guid UsuarioId { get; }
    }
}
