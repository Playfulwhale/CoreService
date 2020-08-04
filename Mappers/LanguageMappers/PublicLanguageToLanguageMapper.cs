namespace ApiTemplate.Mappers.LanguageMappers
{
    using ViewModels.LanguageViewModels;
    using Boxed.Mapping;
    using System;

    public class PublicLanguageToLanguageMapper : IMapper<Models.Language, PublicLanguage>
    {

        public void Map(Models.Language source, PublicLanguage destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Title = source.Title;
            destination.Code = source.Code;
            destination.Flag = source.Flag;
            destination.IsDefault = source.IsDefault;
        }

    }
}
