using Wallet.Interfaces;
using Wallet.Shared;
using Wallet.Helpers;
using System.Runtime.CompilerServices;

namespace Wallet.Controllers
{
    internal class GameController : IController
    {
        private readonly IView view;
        private readonly IValidator validator;
        private readonly IEnumerable<ICommandHandler> handlers;
        

        public GameController(
            IView view,
            IValidator validator,
            IEnumerable<ICommandHandler> handlers  
        )

        {
            this.view = view;
            this.validator = validator;
            this.handlers = handlers;
        }

        public void ProcessInput(string input)
        {
            input = input.Trim();
            var error = this.validator.ValidateInput(input);

            if (!String.IsNullOrEmpty(error))
            {
                this.view.RenderView(error);
                return;
            }

            string command = input.Split(" ")[0];
            decimal amount = decimal.Parse(input.Split(" ")[1]);

            List<string> messages = new();
            var handler = this.handlers.FirstOrDefault(h => h.CanHandle(command));
            if (handler != null)
            {
                try
                {
                    var message = handler.Handle(amount).Result;
                    this.view.RenderView(message);
                }
                catch (Exception ex)
                {
                    this.view.RenderView(ex.Message);
                }
            }
            else
            {
                this.view.RenderView(FormatHelper.FormatMessage(Constants.InvalidCommandError, String.Join(", ", Constants.Actions.All)));
            }
        }
    }
}
