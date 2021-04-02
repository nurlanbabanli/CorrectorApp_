using Entities.Concrete;
using FieldEntities.Concrete;

namespace Business.Helper.ParameterConverters
{
    public class EventArchiveParameterConverters
    {
        public static EventArchiveParameter ConvertToDatabaseFormat(FieldEventArchiveParameter fieldEventArchiveParameter)
        {
            var eventArchiveParameter = new EventArchiveParameter();

            //codes will be added...

            return eventArchiveParameter;
        }
    }
}
