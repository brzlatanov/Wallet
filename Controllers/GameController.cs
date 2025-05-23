using Microsoft.VisualBasic;
using Wallet;
using Wallet.Interfaces;
using Wallet.Models;

namespace Wallet.Controllers
{
    internal class GameController : IController
    {
        IView view;
        IValidator validator;
        public GameController(IView view, IValidator validator)
        {
            this.view = view;
            this.validator = validator;
        }

        public void ProcessInput(string input)
        {
            var errors = validator.ValidateInput(input);

            if (errors.Any())
            {
                view.RenderView(errors.Any() ? errors : null);
                return;
            }

            string command = input.Split(" ")[0];
            decimal amount = decimal.Parse(input.Split(" ")[1]);

            var actionRequest = new ActionRequest
            {
                Command = command,
                Amount = amount
            };

            // need validation and splitting into handlers - command pattern? 
        }
    }
}
