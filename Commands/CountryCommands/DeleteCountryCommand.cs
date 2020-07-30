namespace ApiTemplate.Commands.CountryCommands
{
    using System.Threading;
    using System.Threading.Tasks;
    using Repositories;
    using Microsoft.AspNetCore.Mvc;

    public class DeleteCountryCommand : IDeleteCountryCommand
    {
        private readonly ICountryRepository _countryRepository;

        public DeleteCountryCommand(ICountryRepository countryRepository) =>
            _countryRepository = countryRepository;

        public async Task<IActionResult> ExecuteAsync(int countryId, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.Get(countryId, cancellationToken);
            if (country == null)
            {
                return new NotFoundResult();
            }

            await _countryRepository.Delete(country, cancellationToken);

            return new NoContentResult();
        }
    }
}
