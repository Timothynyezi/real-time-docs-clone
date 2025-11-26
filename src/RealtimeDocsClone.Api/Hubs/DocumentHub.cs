using Microsoft.AspNetCore.SignalR;

namespace RealtimeDocsClone.Api.Hubs;

public class DocumentHub : Hub
{
    // When a user sends updated text, broadcast to everyone else
    public async Task SendUpdate(string documentId, string content)
    {
        await Clients.OthersInGroup(documentId)
            .SendAsync("ReceiveUpdate", documentId, content);
    }

    // When a user opens a document, join a group
    public async Task JoinDocument(string documentId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, documentId);
    }

    // Leave group when done
    public async Task LeaveDocument(string documentId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, documentId);
    }
}
