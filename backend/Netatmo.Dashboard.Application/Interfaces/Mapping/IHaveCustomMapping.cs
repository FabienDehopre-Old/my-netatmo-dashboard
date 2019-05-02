using AutoMapper;

namespace Netatmo.Dashboard.Application.Interfaces.Mapping
{
    public interface IHaveCustomMapping
    {
        void CreateMapping(Profile configuration);
    }
}
