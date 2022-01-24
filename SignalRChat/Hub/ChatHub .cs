using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Data;
using SignalRChat.Messaging;
using SignalRChat.Model;

namespace SignalRChat.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly AppDbContext _context;
        private readonly IMessageService _messageService;
        private readonly string Email  = "terstters86@gmail.com";
        public ChatHub(AppDbContext context,
            IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }

        public async Task SendMessage(string user, string message)
        {
            await _context.Messages.AddAsync(new Message
            {
                User = user,
                Content = message,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", user, message);
            var messageOptions = new MessageOptions();
            messageOptions.toEamilAddress = Email;
            messageOptions.subjcet =$"User {user} sent message";
            messageOptions.message = user + " says: " + message;
            await _messageService.SendEmailAsync(messageOptions);
        }
    }
}
