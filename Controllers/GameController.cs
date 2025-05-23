using Microsoft.VisualBasic;
using Wallet.Interfaces;
using Wallet;

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
        public void Run()
        {
            this.view.RenderView(null);
            string input = this.view.ReceiveInput();

            while(input.ToLower() != Data.Constants.ExitCommand.ToLower())
            {
                IEnumerable<string> inputValidationErrors = this.validator.ValidateInput(input);

                if (inputValidationErrors.Any())
                {
                    this.view.RenderView(inputValidationErrors);
                    input = this.view.ReceiveInput();
                }
            }
        }
    }
}
