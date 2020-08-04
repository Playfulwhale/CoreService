namespace ApiTemplate.Commands.CurrencyCommands
{
    using System.Threading;
    using System.Threading.Tasks;
    using Repositories;
    using Microsoft.AspNetCore.Mvc;

    public class DeleteCurrencyCommand : IDeleteCurrencyCommand
    {
        private readonly ICurrencyRepository _currencyRepository;

        public DeleteCurrencyCommand(ICurrencyRepository currencyRepository) =>
            _currencyRepository = currencyRepository;

        public async Task<IActionResult> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var currency = await _currencyRepository.Get(id, cancellationToken);
            if (currency == null)
            {
                return new NotFoundResult();
            }

            await _currencyRepository.Delete(currency, cancellationToken);

            return new NoContentResult();
        }
    }
}
