using Wallet.Helpers;
using Wallet.Shared;

namespace Data
{
    public class MockDatabaseContext : IDatabaseContext
    {
        private readonly string filePath;

        public MockDatabaseContext(string filePath)
        {
            this.filePath = filePath;

            if (!File.Exists(this.filePath))
            {
                try
                {
                    File.WriteAllText(this.filePath, "0");
                }
                catch
                {
                    throw new IOException(FormatHelper.FormatMessage(Constants.FailedToInitializeDBFileError, this.filePath));
                }
            }
        }

        public async Task<decimal> GetBalanceAsync()
        {
            string text;
            try
            {
                text = await File.ReadAllTextAsync(this.filePath);

            }
            catch
            {
                throw new IOException(FormatHelper.FormatMessage(Constants.FailedToGetBalanceFromFileError, this.filePath));
            }

            if (!decimal.TryParse(text, out var balance))
            {
                throw new InvalidDataException(FormatHelper.FormatMessage(Constants.InvalidBalanceValueError, text));
            }

            return balance;
        }

        public async Task SetBalanceAsync(decimal newBalance)
        {
            try
            {
                await File.WriteAllTextAsync(this.filePath, newBalance.ToString());
            }
            catch
            {
                throw new IOException(FormatHelper.FormatMessage(Constants.FailedToWriteBalanceToFileError, this.filePath));
            }
        }
    }
}
