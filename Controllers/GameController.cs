using Wallet.Interfaces;

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
            var errors = this.validator.ValidateInput(input);

            if (errors.Any())
            {
                this.view.RenderView(errors);
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
                    var message = handler.Handle(amount);
                    this.view.RenderView(new List<string> { message });
                }
                catch (Exception ex)
                {
                    this.view.RenderView(new List<string> { ex.Message });
                }
            }
            else
            {
                this.view.RenderView(new List<string> { message })
            }
        }
    }
}
