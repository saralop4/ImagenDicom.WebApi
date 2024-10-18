using Atower.ImagenDicom.WebApi.Aplicacion.Interfaces;
using Atower.ImagenDicom.WebApi.Dominio.Modelos;

namespace Atower.ImagenDicom.WebApi.Aplicacion.Servicios;

public class DicomServicio : IDicomServicio
{
   
    public Task<DicomImage> GetDicomImageAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task SaveDicomImageAsync(byte[] image)
    {
        throw new NotImplementedException();
    }
}
