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

        public void ProcessInput(string input)
        {
            var errors = validator.ValidateInput(input);
            view.RenderView(errors.Any() ? errors : null);
        }
    }
}
