using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace RealtimeDocsClone.Api.Hubs;

public class DocumentHub : Hub
{
    private readonly ILogger<DocumentHub> _logger;
    private const int MaxDocumentIdLength = 256;
    private const int MaxContentLength = 200_000; // protect against huge payloads

    public DocumentHub(ILogger<DocumentHub> logger)
    {
        _logger = logger;
    }

    // When a user sends updated text, broadcast to everyone else
    public async Task SendUpdate(string documentId, string content)
    {
        if (string.IsNullOrWhiteSpace(documentId) || documentId.Length > MaxDocumentIdLength)
        {
            _logger.LogWarning("SendUpdate called with invalid documentId from connection {ConnectionId}", Context.ConnectionId);
            throw new HubException("Invalid documentId");
        }

        if (content is null)
        {
            _logger.LogWarning("SendUpdate called with null content for document {DocumentId} by {ConnectionId}", documentId, Context.ConnectionId);
            throw new HubException("Content cannot be null");
        }

        if (content.Length > MaxContentLength)
        {
            _logger.LogWarning("SendUpdate rejected oversized content for document {DocumentId} (size={Size}) by {ConnectionId}", documentId, content.Length, Context.ConnectionId);
            throw new HubException($"Content too large (max: {MaxContentLength} characters)");
        }

        await Clients.OthersInGroup(documentId)
            .SendAsync("ReceiveUpdate", documentId, content);
    }

    // When a user opens a document, join a group
    public async Task JoinDocument(string documentId)
    {
        if (string.IsNullOrWhiteSpace(documentId) || documentId.Length > MaxDocumentIdLength)
        {
            _logger.LogWarning("JoinDocument called with invalid documentId from connection {ConnectionId}", Context.ConnectionId);
            throw new HubException("Invalid documentId");
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, documentId);
    }

    // Leave group when done
    public async Task LeaveDocument(string documentId)
    {
        if (string.IsNullOrWhiteSpace(documentId) || documentId.Length > MaxDocumentIdLength)
        {
            _logger.LogWarning("LeaveDocument called with invalid documentId from connection {ConnectionId}", Context.ConnectionId);
            return;
        }

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, documentId);
    }
}
