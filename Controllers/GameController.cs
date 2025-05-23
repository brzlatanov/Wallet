using Wallet.Interfaces;
using Wallet.Models;

namespace Wallet.Controllers
{
    internal class GameController : IController
    {
        IView view;
        IValidator validator;
        IEnumerable<ICommandHandler> handlers;
        public GameController(IView view,
            IValidator validator,
            IEnumerable<ICommandHandler> handlers)
        {
            this.view = view;
            this.validator = validator;
            this.handlers = handlers;
        }

        public void ProcessInput(string input)
        {
            var errors = validator.ValidateInput(input);

            if (errors.Any())
            {
                view.RenderView(errors);
                return;
            }

            string command = input.Split(" ")[0];
            decimal amount = decimal.Parse(input.Split(" ")[1]);

            var handler = handlers.FirstOrDefault(h => h.CanHandle(command));
            if (handler != null)
            {
                try
                {
                    var message = handler.Handle(amount);
                    view.RenderView(new List<string> { message });
                }
                catch (Exception ex)
                {
                    view.RenderView(new List<string> { ex.Message });
                }
            }
        }
    }
}
