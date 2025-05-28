namespace Wallet.Shared
{
    public static class Constants
    {
        public const string SubmitActionPrompt = "Please, submit action:";
        public const string InvalidActionError = "Invalid command format. Use: <command> <amount>.";
        public const string InvalidCommandError = "Invalid command. Available commands: {0}.";
        public const string CommandMustBeAStringError = "Command must be a string.";
        public const string AmountMustBeANumberError = "Amount must be a number.";
        public const string AmountMustBePositiveError = "Amount must be positive.";
        public const string AmountMustBeWithinRangeError = "Amount must be between {0} and {1}.";
        public const string BetLostMessage = "No luck this time! Your current balance is: {0}.";
        public const string BetWonMessage = "Congrats - you won {0}! Your current balance is: {1}.";
        public const string BetFailedDepositError = "Your bet failed, and your account couldn't be credited. Please contact support.";
        public const string DepositError = "Your account couldn't be credited. Please contact support.";
        public const string ExitCommand = "Exit";
        public const string GoodbyeMessage = "Thank you for playing! Hope to see you again soon!";
        public const string SuccessfulDepositMessage = "Your deposit of {0} was successful. Your new balance is: {1}.";
        public const string UnsuccessfulDepositMessage = "Your deposit of {0} was not successful. Please try again.";
        public const string DepositFailedErrorMessage = "An error occurred depositing the balance.";
        public const string UnsuccessfulBetMessage = "Your bet of {0} could not be placed. Please try again.";
        public const string ErrorRetrievingBalanceMessage = "An error occurred while retrieving the wallet balance.";
        public const string ErrorWithdrawingBalanceMessage = "An error occurred while withdrawing from the wallet.";

        public const string SuccessfulWithdrawalMessage = "Your withdrawal of {0} was successful. Your new balance is: {1}.";
        public const string InsufficientFundsMessage = "Insufficient funds for withdrawal. Current balance: {0}.";
        public const string WithdrawalSucceededFailedToRetrieveNewBalanceMessage = "Withdrawal succeeded, but failed to retrieve the new balance.";
        public const string FailedToWriteBalanceToFileError = "Failed to write balance to file '{0}'.";
        public const string FailedToInitializeDBFileError = "Failed to initialize DB file '{0}'.";
        public const string FailedToGetBalanceFromFileError = "Failed to get balance from file '{0}'.";
        public const string InvalidBalanceValueError = "Invalid balance value in file: '{0}'.";
        public const string ErrorPlacingBetMessage = "An error occurred while placing bet.";


        public const decimal MinBetAmount = 1;
        public const decimal MaxBetAmount = 10;

        public static class Actions
        {
            public const string Deposit = "deposit";
            public const string Bet = "bet";
            public const string Withdraw = "withdraw";
            public const string Exit = "exit";

            public static readonly List<string> All = new() { Deposit, Bet, Withdraw, Exit };
        }
    }
}


