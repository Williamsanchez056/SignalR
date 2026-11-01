using Microsoft.AspNetCore.SignalR;
using SignalR.Shared.Contracts;

namespace SignalR.Hub.Hubs;

public sealed class ChatHub : Hub
{
    private static readonly HashSet<string> SalasPermitidas =
        new(StringComparer.OrdinalIgnoreCase) { "Desarrollo", "Diseno" };

    public async Task UnirseASala(string sala)
    {
        sala = ValidarSala(sala);

        if (Context.Items.TryGetValue("SalaActual", out var actual) &&
            actual is string salaAnterior)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, salaAnterior);
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, sala);
        Context.Items["SalaActual"] = sala;
    }

    public async Task SendMessage(string sala, string user, string message)
    {
        sala = ValidarSala(sala);

        if (!Context.Items.TryGetValue("SalaActual", out var actual) ||
            actual is not string salaActual ||
            !string.Equals(salaActual, sala, StringComparison.OrdinalIgnoreCase))
        {
            throw new HubException("Primero debes unirte a esa sala.");
        }

        user = user?.Trim() ?? string.Empty;
        message = message?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(message))
            throw new HubException("El usuario y el mensaje son obligatorios.");

        if (user.Length > 40 || message.Length > 500)
            throw new HubException("El usuario o el mensaje supera el limite permitido.");

        var nuevoMensaje = new ChatMessage(user, message, DateTimeOffset.UtcNow);
        await Clients.Group(sala).SendAsync("ReceiveMessage", nuevoMensaje);
    }

    private static string ValidarSala(string sala)
    {
        if (string.IsNullOrWhiteSpace(sala))
            throw new HubException("Debes seleccionar una sala.");

        sala = sala.Trim();

        if (!SalasPermitidas.Contains(sala))
            throw new HubException("La sala seleccionada no existe.");

        return sala;
    }
}