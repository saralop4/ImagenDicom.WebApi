using Atower.ImagenDicom.WebApi.Dominio.Modelos;

namespace Atower.ImagenDicom.WebApi.Aplicacion.Interfaces;

public interface IDicomServicio
{
    Task<DicomImage> GetDicomImageAsync(string studyInstanceUid);
    Task SaveDicomImageAsync(byte[] image);

}
