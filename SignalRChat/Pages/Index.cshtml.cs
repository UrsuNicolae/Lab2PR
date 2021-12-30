using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Data;
using SignalRChat.Messaging;
using SignalRChat.Model;

namespace SignalRChat.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IMessageService _messageService;

        public IndexModel(AppDbContext context, IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }

        public List<Message> Messages { get; set; } = new List<Message>();

        [BindProperty]
        [DataType(DataType.EmailAddress)] 
        public string Email { get; set; } = "colea.ursu.99@gmail.com";//stop page from refreshing

        public async void OnGetAsync()
        {
            Messages = await _context.Messages.OrderBy(m => m.CreatedAt).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var messageOptions = new MessageOptions();
            messageOptions.toEamilAddress = Email;
            messageOptions.subjcet = "Last message";
            var lastMessage = await _context.Messages.OrderBy(m => m.CreatedAt).LastAsync();
            messageOptions.message = lastMessage.User + " says: " + lastMessage.Content;
            await _messageService.SendEmailAsync(messageOptions);
            return RedirectToPage("./Index");
        }
    }
}
